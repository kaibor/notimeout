using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace NoTimeout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Value = Properties.Settings.Default.timeout;
            numericUpDown2.Value = Properties.Settings.Default.refresh;
            checkBox1.Checked = Properties.Settings.Default.autostart;
            checkBox2.Checked = Properties.Settings.Default.notify;

            timer1.Interval = Convert.ToInt32(Properties.Settings.Default.refresh);
            timer1.Start();

            using (RegistryKey autostartreg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (Properties.Settings.Default.autostart == true)
                {
                    autostartreg.SetValue("NoTimeout", Application.ExecutablePath.ToString());
                }
                else
                {
                    autostartreg.DeleteValue("NoTimeout", false);
                }
            }
                


            if (Properties.Settings.Default.notify == true)
            {
                notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "NoTimeout v1.0 by Kai Borchert";
                notifyIcon1.BalloonTipText = "Process started successfully!";
                notifyIcon1.ShowBalloonTip(3000);
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            this.Hide();  
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.timeout = Convert.ToInt16(numericUpDown1.Value);
            Properties.Settings.Default.refresh = Convert.ToInt32(numericUpDown2.Value);
            Properties.Settings.Default.autostart = checkBox1.Checked;
            Properties.Settings.Default.notify = checkBox2.Checked;
            Properties.Settings.Default.Save();
            MessageBox.Show("Config saved. Restart the application to apply the changes.");
            Application.Restart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            using (RegistryKey registrykey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes\381b4222-f694-41f0-9685-ff5bb260df2e\7516b95f-f776-4464-8c53-06167f40cc99\3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e"))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "powercfg.exe";
                startInfo.Arguments = "-x -monitor-timeout-ac " + Properties.Settings.Default.timeout;
                process.StartInfo = startInfo;

                if (Convert.ToInt16(registrykey.GetValue("ACSettingIndex")) != (Properties.Settings.Default.timeout * 60))
                {
                    process.Start();

                    if (Properties.Settings.Default.notify == true)
                    {
                        notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
                        notifyIcon1.BalloonTipTitle = "NoTimeout v1.0";
                        notifyIcon1.BalloonTipText = "Something tried to modify the value.";
                        notifyIcon1.ShowBalloonTip(3000);
                    }
                }
            }
        }
    }
}
