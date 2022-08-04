using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Launcher
{
    public partial class Installer : Form
    {
        public Installer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Title = "GHOST Bootstrapper v1.6";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to GHOST bootstrapper");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Info] Checking for updates");
            try
            {
                WebClient wb = new WebClient();
                string version = wb.DownloadString("https://ghost-storage.7m.pl/ver.txt");

                if (int.Parse(version) > 16) //If the server replies with a int higher than 16 it will stop the launching
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] Your bootstrapper is outdated, please redownload it.");
                    DialogResult dialogResult = MessageBox.Show("Looks like your bootstrapper is outdated, do you want to join the discord?", "Outdated bootstrapper", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Process.Start("https://discord.gg/FnnHGWpgk8");
                    }
                    return;
                }
            }
            catch (Exception ex) //Catches any error that may occur checking for updates
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Error] Couldn't check for updates, this may be cause you dont have internet connection or you are using an old version of the bootstrapper");
                DialogResult dialogResult = MessageBox.Show("Couldn't check for updates! Do you want to join the discord?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start("https://discord.gg/FnnHGWpgk8");
                }
                return;
            }

            //Im to lazy to comment down here ;_;, learn C# if you want to know how it works 

            if (!Directory.Exists("bin"))
            {
                Console.WriteLine("Installing GHOST");
                Console.WriteLine("GHOST will automatically open when this finishes");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[Info] bin folder not found, creating folder");
                Directory.CreateDirectory("bin");
                Console.WriteLine("[Info] Done!");

            }
            else
            {
                Console.WriteLine("[Info] Updating/Fixing your GHOST installation, this may take a few seconds.");
                Console.WriteLine("[Info] GHOST will automatically open when this finishes");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!Directory.Exists("bin/Scripts"))
            {
                Console.WriteLine("[Info] scripts folder not found, creating folder");
                Directory.CreateDirectory("bin/Scripts");
                Console.WriteLine("Done!");
            }

            if (!File.Exists("bin/Ghost.exe.config"))
            {
                Console.WriteLine("[Info] config file not found");
                Console.WriteLine("[Info] Downloading config file");
                using (WebClient config = new WebClient())
                {
                    config.DownloadFile(new Uri("https://ghost-storage.7m.pl/Ghost.exe.config"), Application.StartupPath + "/bin/Ghost.exe.config");
                }
                Console.WriteLine("Done!");
            }

            if (!Directory.Exists("bin/lite"))
            {
                Console.WriteLine("[Info] bin/lite folder not found, creating folder");
                Directory.CreateDirectory("bin/lite");
                Console.WriteLine("[Info] Done!");

            }

            if (!File.Exists("bin/lite/FastColoredTextBox.dll"))
            {
                Console.WriteLine("[Info] FastColoredTextBox.dll not found");
                Console.WriteLine("[Info] Downloading FastColoredTextBox.dll");
                using (WebClient fastcolored = new WebClient())
                {
                    fastcolored.DownloadFile(new Uri("https://ghost-storage.7m.pl/FastColoredTextBox"), Application.StartupPath + "/bin/lite/FastColoredTextBox.dll");
                }
                Console.WriteLine("Done!");
            }

            if (!File.Exists("bin/lite/FluxAPI.dll"))
            {
                Console.WriteLine("[Info] FluxAPI.dll not found");
                Console.WriteLine("[Info] Downloading FluxAPI.dll");
                using (WebClient fluxapi = new WebClient())
                {
                    fluxapi.DownloadFile(new Uri("https://ghost-storage.7m.pl/FluxAPI.dll"), Application.StartupPath + "/bin/lite/FluxAPI.dll");
                }
                Console.WriteLine("Done!");
            }
            if (!File.Exists("bin/lite/ghost_lite.exe"))
            {
                Console.WriteLine("[Info] Ghost_lite.exe not found");
                Console.WriteLine("[Info] Downloading Ghost_lite.exe");
                using (WebClient ghlite = new WebClient())
                {
                    ghlite.DownloadFile(new Uri("https://ghost-storage.7m.pl/Ghost_lite"), Application.StartupPath + "/bin/lite/Ghost_lite.exe");
                }
                Console.WriteLine("Done!");
            }

            try
            {
                using (WebClient wc = new WebClient())
                {
                    Console.WriteLine("[Info] Ready! Starting...");
                    wc.DownloadFile(new Uri("https://ghost-storage.7m.pl/Ghost2.1"), Application.StartupPath + "/bin/ghost.exe");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Completed();
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Some errors occurred, make sure ghost is not running and your antivirus is disabled");
                Console.WriteLine("Exception info is below");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine(exception);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("End of exception");
            }
            
        }
        void Completed()
        {
            if (File.Exists("bin/ghost.exe"))
            {
                var startInfo = new ProcessStartInfo();

                startInfo.WorkingDirectory = "bin";
                startInfo.FileName = "ghost.exe";

                Process proc = Process.Start(startInfo);
                Application.Exit();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Error occurred!  ghost.exe was deleted, Make sure your antivirus is disabled");
            }

        }
    }
}
