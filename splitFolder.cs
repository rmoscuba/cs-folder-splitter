using System;
using System.IO;

using System.Text;
using System.Threading;

using System.Windows.Forms;

using System.Globalization;

namespace rmortega77.splitFolder
{
	/// <summary>
	/// Summary description for folderSplitter.
	/// </summary>
	public class folderSplitter
	{
		Thread copyThread;
		Thread countThread;

		const int oneMega = 1048576;

		public folderSplitter(long bLength, Button strBtn, ProgressBar pBar, Control pText)
		{
			// Constructor
			this.baseSize = bLength * oneMega; // in Mb
			//
			this.strControl = strBtn;
			//
			this.prgBar = pBar;
			//
			this.prgControl = pText;

		}

		int piecesNumber=1;
		long copiedSize=0;
		long baseSize=0;

		string baseDestiny = "";
		string pieceDestiny = "";

		string folderOrigin;
		string folderDestiny;

		Control prgControl;

		Button strControl;

		ProgressBar prgBar;

		const int bufferSize = oneMega;
		byte[] streamBuffer = new byte[bufferSize]; 
		int numBytesRead;

		int numFiles = 0;

		bool deleteTruncatedProp = false;
		//
		public bool deleteTruncated
		{
			get 
			{
				return this.deleteTruncatedProp;
			}
			set
			{
				this.deleteTruncatedProp = value;
			}
		}

		bool moveSameVolumeProp = false;
		//
		public bool moveSameVolume
		{
			get 
			{
				return this.moveSameVolumeProp;
			}
			set
			{
				this.moveSameVolumeProp = value;
			}
		}

		private void Copy()
		{
			Directory.CreateDirectory(pieceDestiny);
			CopyMoveDir(new DirectoryInfo(folderOrigin), Path.GetFileName(folderOrigin), false, false);
			if (this.prgControl != null)
				this.prgControl.Text = "Terminado...";
			if (this.strControl != null)
				this.strControl.Enabled = true;
		}


		private void Count()
		{
			this.countFolderFiles(new DirectoryInfo(folderOrigin));
		}

		public static void AddLinesToFile(string[] LinesToAdd, string path)
		{
			// Create an instance of StreamWriter to write to the text file.
			// The using statement also closes the StreamWriter.
			using (StreamWriter sr = new StreamWriter(path,true,Encoding.Default)) 
			{
				// Write new lines to the file
				for (int i = 0; i < LinesToAdd.Length; i++)
					sr.WriteLine(LinesToAdd[i]);
			}
		}

		/// <summary>
		/// Procedimiento recursivo para Copy o mover un directorio hacia otro volumen.
		/// </summary>
		/// <param name="d">Estructura DirectoryInfo Directorio</param>
		/// <param name="folderDestiny">Camino folderDestiny, incluido nombre del directorio copia</param>
		private void CopyMoveDir(DirectoryInfo d, string folderDestiny, bool deleteOriginFolder, bool createEmptyDirs) 
		{       
			//
			string sError;
			int errCount = 0;
			bool Error = false;

			//
			// Copy directories first
			//
			DirectoryInfo[] dis = null;
			//

			do 
			{
				try 
				{		
					dis = d.GetDirectories();
					Error = false;
				}
				catch (Exception e)
				{
					Error = true;
					errCount ++;
					//
					sError = "¡Error. Reintentando... leer <" + d.FullName + ">" +
						e.Message + "! ";
					//
					prgControl.Text = sError;
					Console.WriteLine(sError);
					//
					Thread.Sleep(TimeSpan.FromSeconds(2));
					//
					if (errCount > 20)
					{
						sError = "¡Error leyendo <" + d.FullName + "> \n\n" +
							e.Message;
						if (
							MessageBox.Show( 
							sError + "!\n\n¿Intentarlo de nuevo?","Error...",MessageBoxButtons.RetryCancel 
							) != DialogResult.Retry)
						{
							MessageBox.Show(sError + "!\n\n Vuelva a intentarlo más tarde. Note que si especifica el mismo " +
								"folderOrigin y folderDestiny. Se continuará desde donde se quedó...","Error...");
							this.Stop();
						}
					}
				}
			} while (Error);

			foreach (DirectoryInfo di in dis) 
			{
				CopyMoveDir(di,folderDestiny + "\\" + di.Name, deleteOriginFolder, createEmptyDirs);			
			}

			//
			// Copy files next
			//

			FileInfo[] fis = null;

			// Try catch, retry loop

			errCount = 0;
			Error = false;

			do 
			{
				try
				{
					//
					fis = d.GetFiles();
					Error = false;
				}
				catch (Exception e)
				{
					Error = true;
					errCount ++;
					//
					sError = "¡Error. Reintentando... leer <" + d.FullName + ">" +
						e.Message + "! ";
					//
					if (prgControl != null)
						prgControl.Text = sError;
					Console.WriteLine(sError);
					//
					Thread.Sleep(TimeSpan.FromSeconds(2));
					//
					if (errCount > 20)
					{
						sError = "¡Error leyendo <" + d.FullName + "> \n\n" +
							e.Message;
						if (
							MessageBox.Show( 
							sError + "!\n\n¿Intentarlo de nuevo?","Error...",MessageBoxButtons.RetryCancel 
							) != DialogResult.Retry)
						{
							MessageBox.Show(sError + "!\n\n Vuelva a intentarlo más tarde. Note que si especifica el mismo " +
								"folderOrigin y folderDestiny. Se continuará desde donde se quedó...","Error...");
							this.Stop();
							return;
						}
					}
				}
			} while (Error);

			// Creo el directorio folderDestiny si crear directorios vacios
			// o existen ficheros en folderOrigin y el directorio no existe
			if ( ((fis.Length > 0) || (createEmptyDirs)) 
				&& (!Directory.Exists(pieceDestiny + "\\" + folderDestiny)) )
			{
				Directory.CreateDirectory(pieceDestiny + "\\" + folderDestiny);
				string[] lines = new string[1];
				lines[0] = folderDestiny;
				AddLinesToFile(lines, pieceDestiny + "\\list.txt" );
			}
			
			foreach (FileInfo fi in fis) 
			{      
				copiedSize += fi.Length;
				//
				//Console.WriteLine(copiedSize.ToString() + "/" + baseSize.ToString());
				if (copiedSize > baseSize)
				{
					//
					Console.WriteLine( Math.Round((1.0*copiedSize / oneMega),2).ToString() + "/" + copiedSize.ToString());
					//
					piecesNumber += 1;
					//
					copiedSize = fi.Length;
					//
					pieceDestiny = baseDestiny + "_"+ ((1000 + piecesNumber)).ToString().Substring(1,3);
					
					if (!Directory.Exists(pieceDestiny + "\\" + folderDestiny))
					{
						Directory.CreateDirectory(pieceDestiny + "\\" + folderDestiny);
						string[] lines = new string[1];
						lines[0] = folderDestiny;
						AddLinesToFile(lines, pieceDestiny + "\\list.txt" );
					}
				}
				//
				
				string sfolderDestinyPath = pieceDestiny + "\\" + folderDestiny + "\\" + fi.Name;
				
				if (prgControl != null)
					this.prgControl.Text = Math.Round((1.0*copiedSize / oneMega),2).ToString() + "/" + Math.Round((1.0*fi.Length  / oneMega),2).ToString() + ": " + sfolderDestinyPath;
				
				//
				FileStream output = null;
				FileStream input = null;
				errCount = 0;
				Error = false;

				//
				bool Continuar = false;
				//
				bool existeFichero = File.Exists(sfolderDestinyPath);
				//
				if (existeFichero)
					Continuar = File.GetCreationTime(sfolderDestinyPath) != fi.CreationTime;
				//
				if (Continuar || !existeFichero)
				{
					if (moveSameVolumeProp) 
					{
						//
						try 
						{
							fi.MoveTo(sfolderDestinyPath);
							//
							if (prgBar != null)
								prgBar.PerformStep();
						}
						catch (Exception e)
						{
							Console.WriteLine(e.Message);
						}
					}
					else do 
					{
						try
						{
							// Si no primera vez,  sobreescribo
							//
							// Get the input file stream
							//
							input = fi.OpenRead();
							//
							// Get the output file Stream 
							//
							if (deleteTruncatedProp)
							{ 
								output = new FileStream(sfolderDestinyPath, FileMode.Create);
							}
							else
							{
								output = new FileStream(sfolderDestinyPath, FileMode.Append);            
								input.Seek(output.Position,SeekOrigin.Begin);
							}	

							while((numBytesRead = input.Read(streamBuffer, 0, bufferSize)) > 0)         
								output.Write(streamBuffer, 0, numBytesRead);   
   
							// Close the file Streams.
							//
							input.Close();
							output.Close();
							//
							File.SetAttributes(sfolderDestinyPath,fi.Attributes);
							File.SetCreationTime(sfolderDestinyPath,fi.CreationTime);
							File.SetLastAccessTime(sfolderDestinyPath,fi.LastAccessTime);
							File.SetLastWriteTime(sfolderDestinyPath,fi.LastWriteTime);

							// fi.CopyTo(sfolderDestinyPath,Error);
							//
							if (prgBar != null)
								prgBar.PerformStep();
							//
							Error = false;
							//
						}
						catch (ThreadAbortException)
						{
							// Detiene el proceso de terminar copyThread
							// 
							Thread.ResetAbort();
							// Intento borrar el fichero muy probablemente erróneo
							//
							try 
							{
								// Close handles
								//
								output.Close();
								input.Close();
								//
								Console.WriteLine("Borrando fichero muy probablemente erróneo <" + sfolderDestinyPath + ">");
								// delete error proning file
								//
								if (deleteTruncatedProp)
									File.Delete(sfolderDestinyPath);
							}
							catch (Exception e)
							{
								Console.WriteLine(e.Message);
							}
							// Ahora si, termina
							//
							this.Stop();
						}
						catch (Exception e)
						{
							Error = true;
							errCount ++;
							//
							if (prgControl != null)
								prgControl.Text = "¡Error. Reintentando... copiando <" + sfolderDestinyPath +">" +
								e.Message + "! ";
							Console.WriteLine("¡Error. Reintentando... copiando <" + sfolderDestinyPath +">" +
								e.Message + "! ");
							//
							Thread.Sleep(TimeSpan.FromSeconds(2));
							//
							if (errCount > 20)
							{
								if (
									MessageBox.Show( 
									"¡Error copiando <" + sfolderDestinyPath +"> \n\n" +
									e.Message + "!\n\n¿Intentarlo de nuevo?","Error...",MessageBoxButtons.RetryCancel 
									) != DialogResult.Retry)
								{
										
									try 
									{
										if (deleteTruncatedProp)
											File.Delete(sfolderDestinyPath);
									}
									catch
									{
									}
							
									MessageBox.Show("¡Error copiando <" + sfolderDestinyPath +"> \n\n" +
										e.Message + "!\n\n Vuelva a intentarlo más tarde. Note que si especifica el mismo " +
										"folderOrigin y folderDestiny. Se continuará desde donde se quedó...","Error...");
									this.Stop();
								}
							}
						}
					} while (Error);
				}
				else
					if (prgBar != null)
						prgBar.PerformStep();
			    
				// Borro el fichero si borrar folderOrigin
				//
				if (deleteOriginFolder)
					fi.Delete(); 
			}

			// Borro el directorio si borrar folderOrigin
			if ( (deleteOriginFolder) || (moveSameVolumeProp) )
				d.Delete();
			
		}


		/// <summary>
		/// Procedimiento recursivo para Count número de folderSplitter en un directorio
		/// </summary>
		/// <param name="d">Estructura DirectoryInfo Directorio</param>
		/// <param name="folderDestiny">Camino folderDestiny, incluido nombre del directorio copia</param>
		private void countFolderFiles(DirectoryInfo d) 
		{     
			string sError;
			//
			FileInfo[] fis = null;
			//
			//int errCount = 0;
			bool Error = false;
			do 
			{
				try
				{
					fis = d.GetFiles();
					Error = false;
				}
				catch (Exception e)
				{
					Error = true;
					//errCount ++;
					//
					sError = "¡Error. Reintentando... leer <" + d.FullName + ">" +
						e.Message + "! ";
					//
					//prgControl.Text = sError;
					//
					Console.WriteLine(sError);
					//
					Thread.Sleep(TimeSpan.FromSeconds(2));
					//
				}
			} while (Error);

			//
			numFiles += fis.Length;
			//
			if(prgBar != null)
				prgBar.Maximum = numFiles;
			//
			DirectoryInfo[] dis = null;
			//
			Error = false;
			do 
			{
				try
				{
					dis = d.GetDirectories();
					Error = false;
				}
				catch (Exception e)
				{
					Error = true;
					//
					sError = "¡Error. Reintentando... leer <" + d.FullName + ">" +
						e.Message + "! ";
					//
					prgControl.Text = sError;
					Console.WriteLine(sError);
					//
					Thread.Sleep(TimeSpan.FromSeconds(2));
					//
				}
			} while (Error);

			foreach (DirectoryInfo di in dis) 
			{
				countFolderFiles(di);			
			}
			
		}

		private void CountStart()
		{
			//
			this.countThread = new Thread(new ThreadStart(this.Count));
			//
			this.countThread.Name = "Count";
			//
			this.countThread.Priority = ThreadPriority.Highest;
			//
			this.countThread.Start();
		}

		/// <summary>
		/// Copy directorio. Envoltura para llamada al procedimiento Recursivo
		/// </summary>
		/// <param name="folderOrigin">Camino folderOrigin, incluido directorio a Copy</param>
		/// <param name="folderDestiny">Camino folderDestiny, incluido nombre del directorio copia</param>
		public void CopyDirectory(string Orig, string Dest)
		{
			//
			this.baseDestiny = Dest + "\\CD";
			//
			this.pieceDestiny = baseDestiny + "_001";
			//
			this.folderOrigin = Orig;
			//
			this.CountStart();
			//
			this.folderDestiny = Dest;
			//
			this.Start();
		}

//		/// <summary>
//		/// Copy directorio. Envoltura para llamada al procedimiento Recursivo
//		/// </summary>
//		/// <param name="folderOrigin">Camino folderOrigin, incluido directorio a Copy</param>
//		/// <param name="folderDestiny">Camino folderDestiny, incluido nombre del directorio copia</param>
//		public void MoverDirectorio(string folderOrigin, string folderDestiny)
//		{
//			try
//			{
//				Directory.Move( folderOrigin, folderDestiny);
//			}
//			catch (IOException)
//			{
//				CopyMoveDir(new DirectoryInfo(folderOrigin), folderDestiny, true, false);
//			}
//		}

		private void Start()
		{
			//
			this.copyThread = new Thread(new ThreadStart(this.Copy));
			//
			this.copyThread.Name = "Copy";
			//
			this.copyThread.Priority = ThreadPriority.Highest;
			//
			this.copyThread.Start();
		}

		public void Stop()
		{
			try 
			{
				if (strControl != null)
					strControl.Enabled = true;
				this.copyThread.Abort();
				this.countThread.Abort();
			}
			catch {}
		}

		public void Suspend()
		{
			try
			{
				this.copyThread.Suspend();
				this.countThread.Suspend();
			}
			catch {}
		}

		public void Resume()
		{
			try 
			{
				this.copyThread.Resume();
				this.countThread.Resume();
			}
			catch {}
		}

		public ThreadState Running()
		{
			return this.copyThread.ThreadState;
		}

	}
}
