using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cs2Open
{
    public partial class Form1 : Form
    {
        private string processCSGO = "csgo";
        private string pathCS2 = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\bin\win64\cs2.exe";

        public Form1()
        {
            InitializeComponent();
            RegisterProgramOnStartup();
            this.Load += new EventHandler(Function);
        }

        private void Function(object sender, EventArgs e)
        {
            bool cs2Opened = false;

            while (true)
            {
                if (Process.GetProcessesByName(processCSGO).Length > 0)
                {
                    if (!cs2Opened && File.Exists(pathCS2))
                    {
                        try
                        {
                            Process.Start(pathCS2);
                            cs2Opened = true; 
                            Thread.Sleep(40000); 
                            foreach (var process in Process.GetProcessesByName("cs2"))
                            {
                                process.Kill();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error opening the file: {ex.Message}");
                        }
                    }
                    else if (!File.Exists(pathCS2))
                    {
                        MessageBox.Show("Your cs2 path is different");
                    }
                }
                else
                {
                    cs2Opened = false; 
                }
            }
        }

        private void RegisterProgramOnStartup()
        {
            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            startupKey.SetValue("Cs2Open", Process.GetCurrentProcess().MainModule.FileName);
            startupKey.Close();
        }
    }
}
