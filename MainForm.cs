using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.IO;

using System.Diagnostics;

using Microsoft.Win32;

namespace rmortega77.splitFolder
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button Ok;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox TOrigen;
		private System.Windows.Forms.TextBox TDestino;
		private System.Windows.Forms.TextBox TSize;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.TextBox Progreso;
		private System.Windows.Forms.ProgressBar progressBar;		
		//
		folderSplitter frs;
		//
		private Configuration config = new Configuration(Path.GetDirectoryName(Application.ExecutablePath));
		private System.Windows.Forms.RadioButton Continuar;
		private System.Windows.Forms.RadioButton Sobrescribir;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton Mover;
		private System.Windows.Forms.RadioButton Copiar;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.Ok = new System.Windows.Forms.Button();
			this.TOrigen = new System.Windows.Forms.TextBox();
			this.TDestino = new System.Windows.Forms.TextBox();
			this.Cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.TSize = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.Progreso = new System.Windows.Forms.TextBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.Continuar = new System.Windows.Forms.RadioButton();
			this.Sobrescribir = new System.Windows.Forms.RadioButton();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.Mover = new System.Windows.Forms.RadioButton();
			this.Copiar = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// Ok
			// 
			this.Ok.BackColor = System.Drawing.Color.Transparent;
			this.Ok.Enabled = false;
			this.Ok.Location = new System.Drawing.Point(78, 129);
			this.Ok.Name = "Ok";
			this.Ok.TabIndex = 0;
			this.Ok.Text = "OK";
			this.Ok.Click += new System.EventHandler(this.Ok_Click);
			// 
			// TOrigen
			// 
			this.TOrigen.Location = new System.Drawing.Point(55, 4);
			this.TOrigen.Name = "TOrigen";
			this.TOrigen.Size = new System.Drawing.Size(469, 20);
			this.TOrigen.TabIndex = 1;
			this.TOrigen.Text = "";
			this.TOrigen.TextChanged += new System.EventHandler(this.TOrigen_TextChanged);
			// 
			// TDestino
			// 
			this.TDestino.Location = new System.Drawing.Point(55, 28);
			this.TDestino.Name = "TDestino";
			this.TDestino.Size = new System.Drawing.Size(469, 20);
			this.TDestino.TabIndex = 2;
			this.TDestino.Text = "";
			this.TDestino.TextChanged += new System.EventHandler(this.TOrigen_TextChanged);
			// 
			// Cancel
			// 
			this.Cancel.BackColor = System.Drawing.Color.Transparent;
			this.Cancel.Location = new System.Drawing.Point(401, 130);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 3;
			this.Cancel.Text = "Cancel";
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Origen";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(5, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Destino";
			// 
			// TSize
			// 
			this.TSize.Location = new System.Drawing.Point(55, 52);
			this.TSize.Name = "TSize";
			this.TSize.Size = new System.Drawing.Size(48, 20);
			this.TSize.TabIndex = 6;
			this.TSize.Text = "700";
			this.TSize.Validating += new System.ComponentModel.CancelEventHandler(this.TSize_Validating);
			this.TSize.Validated += new System.EventHandler(this.TSize_Validated);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(5, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "Tamaño";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(103, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Mb";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.Location = new System.Drawing.Point(527, 2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(23, 23);
			this.button1.TabIndex = 9;
			this.button1.Text = "...";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Transparent;
			this.button2.Location = new System.Drawing.Point(527, 27);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(23, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = "...";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// Progreso
			// 
			this.Progreso.BackColor = System.Drawing.SystemColors.Control;
			this.Progreso.Location = new System.Drawing.Point(4, 80);
			this.Progreso.Name = "Progreso";
			this.Progreso.ReadOnly = true;
			this.Progreso.Size = new System.Drawing.Size(544, 20);
			this.Progreso.TabIndex = 14;
			this.Progreso.Text = "";
			this.Progreso.TextChanged += new System.EventHandler(this.Progreso_TextChanged);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(4, 103);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(544, 10);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 15;
			// 
			// Continuar
			// 
			this.Continuar.BackColor = System.Drawing.Color.Transparent;
			this.Continuar.Checked = true;
			this.Continuar.Location = new System.Drawing.Point(3, 1);
			this.Continuar.Name = "Continuar";
			this.Continuar.Size = new System.Drawing.Size(132, 18);
			this.Continuar.TabIndex = 16;
			this.Continuar.TabStop = true;
			this.Continuar.Text = "Continuar truncados.";
			// 
			// Sobrescribir
			// 
			this.Sobrescribir.BackColor = System.Drawing.Color.Transparent;
			this.Sobrescribir.Location = new System.Drawing.Point(125, 1);
			this.Sobrescribir.Name = "Sobrescribir";
			this.Sobrescribir.Size = new System.Drawing.Size(95, 18);
			this.Sobrescribir.TabIndex = 17;
			this.Sobrescribir.Text = "Sobrescribir";
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenu = this.contextMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Divide Carpeta - rmortega77@yahoo.es";
			this.trayIcon.Visible = true;
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuItem1,
																						this.menuItem2,
																						this.menuItem5,
																						this.menuItem3,
																						this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "M&ostrar";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Oc&ultar";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "&Cancelar";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "&Salir";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// label5
			// 
			this.label5.AccessibleDescription = "";
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.Location = new System.Drawing.Point(279, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 96);
			this.label5.TabIndex = 18;
			this.label5.Click += new System.EventHandler(this.label5_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.Sobrescribir);
			this.panel1.Controls.Add(this.Continuar);
			this.panel1.Location = new System.Drawing.Point(337, 53);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(210, 21);
			this.panel1.TabIndex = 19;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.Mover);
			this.panel2.Controls.Add(this.Copiar);
			this.panel2.Location = new System.Drawing.Point(134, 53);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(199, 21);
			this.panel2.TabIndex = 20;
			// 
			// Mover
			// 
			this.Mover.BackColor = System.Drawing.Color.Transparent;
			this.Mover.Location = new System.Drawing.Point(61, 1);
			this.Mover.Name = "Mover";
			this.Mover.Size = new System.Drawing.Size(150, 18);
			this.Mover.TabIndex = 17;
			this.Mover.Text = "Mover (*mismo volumen)";
			// 
			// Copiar
			// 
			this.Copiar.BackColor = System.Drawing.Color.Transparent;
			this.Copiar.Checked = true;
			this.Copiar.Location = new System.Drawing.Point(3, 1);
			this.Copiar.Name = "Copiar";
			this.Copiar.Size = new System.Drawing.Size(66, 18);
			this.Copiar.TabIndex = 16;
			this.Copiar.TabStop = true;
			this.Copiar.Text = "Copiar";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(552, 166);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.Progreso);
			this.Controls.Add(this.TOrigen);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.TDestino);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.TSize);
			this.Controls.Add(this.Ok);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.label5);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(1024, 191);
			this.MinimumSize = new System.Drawing.Size(558, 191);
			this.Name = "Form1";
			this.Text = "Divide Carpeta";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//
			if (Directory.Exists(TOrigen.Text))
				folderBrowserDialog1.SelectedPath = TOrigen.Text;
			else if (File.Exists(TOrigen.Text))
				folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(TOrigen.Text);
			//
			folderBrowserDialog1.ShowNewFolderButton = false;
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				TOrigen.Text = folderBrowserDialog1.SelectedPath;
			//
			Ok.Enabled = Directory.Exists(TOrigen.Text) & Directory.Exists(TDestino.Text);
		}

		private void Ok_Click(object sender, System.EventArgs e)
		{
			if (Directory.Exists(TOrigen.Text) & Directory.Exists(TDestino.Text))
			{
				//
				oldPercent = 0;
				//
				frs = new folderSplitter(Convert.ToInt32(TSize.Text), Ok, progressBar, Progreso);
				//
				char [] arr = {'\\', ' '};
				//
				frs.CopyDirectory(TOrigen.Text.TrimEnd( arr ), TDestino.Text.TrimEnd( arr ));
				//
				frs.deleteTruncated = Sobrescribir.Checked;
				//
				frs.moveSameVolume = Mover.Checked;
				//
				Ok.Enabled = false;
				//
			}
			else
				MessageBox.Show ("No existe la carpeta Origen o Destino.");
		}

		private void Cancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				frs.Stop();
			}
			catch 
			{}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//
			if (Directory.Exists(TDestino.Text))
				folderBrowserDialog1.SelectedPath = TDestino.Text;
			else if (File.Exists(TDestino.Text))
				folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(TDestino.Text);
			//
			folderBrowserDialog1.ShowNewFolderButton = true;
			//
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				TDestino.Text = folderBrowserDialog1.SelectedPath;
			//
			Ok.Enabled = Directory.Exists(TOrigen.Text) & Directory.Exists(TDestino.Text);
		}

		private void TSize_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string errorMsg="";
			//
			try
			{
				int val = Int32.Parse(TSize.Text);
				if (val <= 0)
					errorMsg = "¡Debe especificar un tamaño válido!";
			}
			catch 
			{
				errorMsg = "¡Debe especificar un tamaño válido!";
			}
			//
			if(errorMsg != "")
			{
				// Cancel the event and select the text to be corrected by the user.
				e.Cancel = true;
				TSize.Select(0, TSize.Text.Length);

				// Set the ErrorProvider error with the text to display. 
				this.errorProvider1.SetError(TSize, errorMsg);
			}
		}

		private void TSize_Validated(object sender, System.EventArgs e)
		{
			// If all conditions have been met, clear the ErrorProvider of errors.
			errorProvider1.SetError(TSize, "");
		}

		private void TOrigen_TextChanged(object sender, System.EventArgs e)
		{
			Ok.Enabled = Directory.Exists(TOrigen.Text) & Directory.Exists(TDestino.Text);
		}

		private void Form1_SizeChanged(object sender, System.EventArgs e)
		{
			//
			Progreso.Width = this.Width - 14;
			progressBar.Width = Progreso.Width;
			//
			button1.Left = this.Width - 30;
			button2.Left = button1.Left;
			//
			TOrigen.Width = this.Width - 88;
			TDestino.Width = TOrigen.Width;
			//
			Cancel.Left = this.Width - 157;
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try 
			{
				frs.Stop();
			}
			catch 
			{}

			// save configuration
			//
			config.mainWindowLocation = this.Location;
			//
			config.mainWindowState = this.WindowState;
			//
			config.dFolder = TDestino.Text;
			//
			config.oFolder = TOrigen.Text;
			//
			config.sSize = TSize.Text;
			//
			config.Continuar = Continuar.Checked;
			//
			config.SaveSettings();
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// load configuration
			//
			config.LoadSettings();
			// set window location and size
			this.Location = config.mainWindowLocation;
			//
			this.WindowState = config.mainWindowState;
			//
			this.TDestino.Text = config.dFolder;
			this.TOrigen.Text = config.oFolder;
			//
			this.TSize.Text = config.sSize;
			//
			this.Continuar.Checked = config.Continuar;
			this.Sobrescribir.Checked = !config.Continuar;
			//
			if (config.startCopying)
				Ok_Click(this,e);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.Show();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		private void Progreso_TextChanged(object sender, System.EventArgs e)
		{
			int prgPercent = 0;
			if (progressBar.Maximum > 0)
				prgPercent = Convert.ToInt32( Math.Round(((1.0*progressBar.Value / progressBar.Maximum)*100),0));
			if (prgPercent != oldPercent) 
			{

				oldPercent =  prgPercent;

				trayIcon.Text = "Divide Carpeta - rmortega77@yahoo.es " + prgPercent.ToString()+" %";

				//
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
				Bitmap bmp =( (System.Drawing.Icon)(resources.GetObject("trayIcon.Icon"))).ToBitmap();

				// Create an ImageGraphics Graphics object from bitmap Image
				System.Drawing.Graphics ImageGraphics = System.Drawing.Graphics.FromImage(bmp);

				// Draw random code within Image
				System.Drawing.Font drawFont = new System.Drawing.Font("Arial Narrow", 16, FontStyle.Regular);
				System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
				//
				System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
				ImageGraphics.DrawString(prgPercent.ToString()+"%", drawFont, drawBrush, -2, 2, drawFormat);
				//
				// Dispose used Objects
				//
				drawFont.Dispose();
				drawBrush.Dispose();
				ImageGraphics.Dispose();
				//
				trayIcon.Icon = Icon.FromHandle(bmp.GetHicon());
				//
				bmp.Dispose();
			}

		}

		private void OpenInBrowser(string URL)
		{
			//
			RegistryKey rk = Registry.ClassesRoot.OpenSubKey(@"htmlfile\shell\open\command", true);
			//
			// Get the data from a specified item in the key.
			//
			String s = (String)rk.GetValue("");
			//
			int next = s.IndexOf("\"");
			if (next != -1)
				s = s.Substring(next+1,s.IndexOf("\"",next+1) - next-1);
			//
			try
			{
				Process.Start(s,URL);
			}
			catch (Win32Exception er)
			{
				if(er.NativeErrorCode == 2)
				{
					MessageBox.Show(er.Message + ". Check the path.");
				} 

				else if (er.NativeErrorCode == 5)
				{
					MessageBox.Show(er.Message + 
						". You do not have permission to run the default browser.");
				}
			}
		}

		private void label5_Click(object sender, System.EventArgs e)
		{
			OpenInBrowser("http://www.codeproject.com/script/articles/list_articles.asp?userid=970931");
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// // Old percent track
		/// </summary>
		private int oldPercent;

	}
}
