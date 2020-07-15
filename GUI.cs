using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerListener;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SCUMServerListener
{
    public partial class GUI : Form
    {
        string ServerID = "2608083";
        private int counter = 0;
        private System.Windows.Forms.Timer updateTimer;

        ServerData ServerData = new ServerData();

        SettingsForm settingsForm;

        Thread t_overlay;
        Overlay ol = null;
        bool overlayEnabled = false;

        public GUI()
        {
            InitializeComponent();
            this.MaximizeBox = false; 
            this.ServerID = loadDefault();
            CreateTimer();
            Update();
            update_tooltip.ShowAlways = true;
            update_tooltip.SetToolTip(this.update_progbar, "Server Status will update every 30 seconds...");
        }

        private void StartOverlay()
        {
            ol = new Overlay();
            t_overlay = new Thread(new ThreadStart(ol.Run));
            t_overlay.IsBackground = true;
            t_overlay.Start();
            if (ol.isCreated)
            {
                overlayEnabled = true;
                btn_overlay.Text = "Disable Overlay";
                Update();
            }
            else
            {
                StopOverlay();
            }
        }

        private void StopOverlay()
        {
            ol.Dispose();
            t_overlay.Abort();
            btn_overlay.Text = "Enable Overlay";
            overlayEnabled = false;
            ol = null;
        }

        private void CreateTimer()
        {
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Interval = 1000;
            updateTimer.Start();
            update_progbar.Maximum = 30;
            update_progbar.Value = 0;
        }

        private void Update()
        {
            String[] Results = ServerData.RetrieveData(this.ServerID);
            if (Results != null)
            {
                name.Text = Results[0];
                var ip = Results[4];
                var port = Results[5];
                string serverTime = "";
                string ping = "";

                if (Results[2] == "online")
                {
                    serverTime = ServerData.GetServerTime(ip, port);
                    ping = ServerData.Ping(ip, 4).ToString();

                    name.ForeColor = System.Drawing.Color.Green;
                    status.ForeColor = System.Drawing.Color.Green;
                    players.ForeColor = System.Drawing.Color.Green;
                    status.Text = "Online";
                    players.Text = Results[1] + " / " + Results[3];
                    time.Text = serverTime;
                    Ping.Text = ping;
                }
                else
                {
                    status.ForeColor = System.Drawing.Color.Red;
                    players.ForeColor = System.Drawing.Color.Red;
                    name.ForeColor = System.Drawing.Color.Red;
                    status.Text = "Offline";
                    players.Text = "0";
                    time.Text = "";
                }
                if (overlayEnabled)
                {
                    ol.Name = Results[0];
                    ol.Status = Results[2];
                    ol.Players = Results[1] + " / " + Results[3];
                    ol.Time = serverTime;
                    ol.Ping = ping;
                }
            }
            else
            {
                MessageBox.Show("No Connection! Retrying...", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            searchbutton.Enabled = true;
        }

        private string IterateResults(List<Server> servers)
        {
            for(int i = 0; i < servers.Count(); i++)
            {
                DialogResult dialogResult = MessageBox.Show(servers[i].Name, "Search Results", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    return servers[i].ID;
                }
                else if(dialogResult == DialogResult.Cancel)
                {
                    break;
                }
            }
            MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return this.ServerID;
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            String SearchInput = searchbox.Text;
            String LookUpString = ServerData.GetLookupString(SearchInput);
            var servers = ServerData.GetServerID(LookUpString);
            this.ServerID = IterateResults(servers);

            updateTimer_Reset();

            if (ServerID != "scum")
            {
                searchbutton.Enabled = false;
                Update();
            }
        }

        private void updateTimer_Reset()
        {
            update_progbar.Value = 0;
            counter = 0;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (ol != null)
                if (!ol.overlayAllWindows)
                {
                    ol.SetWindowVisibility();
                    if (ol.HasProcessExited())
                        StopOverlay();
                }

            if (counter == 30)
            {
                update_progbar.Value = 0;
                counter = 0;
                Update();
            }
            counter++;
            update_progbar.Value = counter;
        }

        private string loadDefault()
        {
            return SettingsManager.LoadDefault();
        }

        private void setdft_btn_Click(object sender, EventArgs e)
        {
            SettingsManager.SetDefault(this.ServerID);
        }

        private void btn_overlay_Click(object sender, EventArgs e)
        {
            if (!overlayEnabled)
            {
                StartOverlay();
            }
            else
            {
                StopOverlay();
            }
        }

        private void overlaySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm = new SettingsForm(ol);
            settingsForm.Show();
        }
    }
}
