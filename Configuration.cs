// Divide Carpeta
//
// rmortega77@yahoo.es
// 

using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Text;

using System.Windows.Forms;

namespace rmortega77.splitFolder
{

	/// <summary>
	/// Application configuration
	/// </summary>
	public class Configuration
	{
		// configuration file name
		private string settingsFile;

		// main window size and position
		public Point	mainWindowLocation = new Point(100, 50);
		public FormWindowState mainWindowState;

		// Main window start options
		public bool		startMinimized = false;
		public bool		startCopying = false;
		public bool		startOnWindows = false;

		// Split Folder settings 
		public string	dFolder = "G:\\";
		public string	oFolder = "S:\\";
		public string	sSize = "700";
		public bool		Continuar = true;
		
		// Constructor
		public Configuration(string path)
		{
			settingsFile = Path.Combine(path, "app.config");
		}

		// Save application settings
		public void SaveSettings()
		{
			// open file
			FileStream		fs = new FileStream(settingsFile, FileMode.Create);
			// create XML writer
			XmlTextWriter	xmlOut = new XmlTextWriter(fs, Encoding.UTF8); 

			// use indenting for readability
			xmlOut.Formatting = Formatting.Indented;

			// start document
			xmlOut.WriteStartDocument();
			xmlOut.WriteComment("Fichero de Configuración de Divide Carpeta");

			// main node
			xmlOut.WriteStartElement("DivideCarpeta");

			// main window node
			xmlOut.WriteStartElement("MainWindow");
			xmlOut.WriteAttributeString("x", mainWindowLocation.X.ToString());
			xmlOut.WriteAttributeString("y", mainWindowLocation.Y.ToString());
			xmlOut.WriteAttributeString("mainWindowState", ((Int32)mainWindowState).ToString());
			xmlOut.WriteEndElement();

			// Ventana node
			xmlOut.WriteStartElement("Ventana");
			xmlOut.WriteAttributeString("startMinimized", this.startMinimized.ToString());
			xmlOut.WriteAttributeString("startCopying", this.startCopying.ToString());
			xmlOut.WriteAttributeString("startOnWindows", this.startOnWindows.ToString());
			//
			xmlOut.WriteAttributeString("dFolder", this.dFolder);
			xmlOut.WriteAttributeString("oFolder", this.oFolder.ToString());
			xmlOut.WriteAttributeString("sSize",this.sSize);
			xmlOut.WriteAttributeString("Continuar",this.Continuar.ToString());
			//
			xmlOut.WriteEndElement();


			// end document
			xmlOut.WriteEndElement();

			// close file
			xmlOut.Close();
		}

		// Load application settings
		public bool LoadSettings()
		{
			bool	ret = false;

			// check file existance
			if (File.Exists(settingsFile))
			{
				FileStream		fs = null;
				XmlTextReader	xmlIn = null;

				try
				{
					// open file
					fs = new FileStream(settingsFile, FileMode.Open);
					// create XML reader
					xmlIn = new XmlTextReader(fs);

					xmlIn.WhitespaceHandling = WhitespaceHandling.None;
					xmlIn.MoveToContent();

					// check for main node
					if (xmlIn.Name != "DivideCarpeta")
						throw new ApplicationException("");

					// move to next node
					xmlIn.Read();
					if (xmlIn.NodeType == XmlNodeType.EndElement)
						xmlIn.Read();

					// check for main window node
					if (xmlIn.Name != "MainWindow")
						throw new ApplicationException("");

					// read main window position
					int	x = Convert.ToInt32(xmlIn.GetAttribute("x"));
					int	y = Convert.ToInt32(xmlIn.GetAttribute("y"));
					
					//
					this.mainWindowState = (FormWindowState)Convert.ToInt32(xmlIn.GetAttribute("mainWindowState"));

					// move to next node
					xmlIn.Read();
					if (xmlIn.NodeType == XmlNodeType.EndElement)
						xmlIn.Read();

					// check for Ventana node
					if (xmlIn.Name != "Ventana")
						throw new ApplicationException("");

					startMinimized = Convert.ToBoolean(xmlIn.GetAttribute("startMinimized"));
					startCopying = Convert.ToBoolean(xmlIn.GetAttribute("startCopying"));
					startOnWindows = Convert.ToBoolean(xmlIn.GetAttribute("startOnWindows"));

					dFolder = xmlIn.GetAttribute("dFolder");
					oFolder = xmlIn.GetAttribute("oFolder");
					sSize = xmlIn.GetAttribute("sSize");
					Continuar = Convert.ToBoolean(xmlIn.GetAttribute("Continuar"));

					mainWindowLocation = new Point(x, y);

					ret = true;
				}
					// catch any exceptions
				catch (Exception)
				{
				}
				finally
				{
					if (xmlIn != null)
						xmlIn.Close();
				}
			}
			return ret;
		}

	
	}
}
