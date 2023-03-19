﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SCUMServerListener
{
    public partial class GUI : Form
    {
        private const int UpdateAtSeconds = 30;

        private string ServerID;
        private int counter;
        private bool overlayEnabled;
        private System.Windows.Forms.Timer updateTimer;
        private SettingsForm settingsForm;
        private Thread overlayThread;
        private Overlay overlay;

        public GUI()
        { 
            InitializeComponent();
            ServerID = AppSettings.Instance.DefaultServerId;
            update_progbar.Maximum = UpdateAtSeconds;
            update_progbar.Value = 0;
            CreateTimer();
            counter = 30;
            overlayEnabled = false;
        }

        private void StartOverlay()
        {
            if (overlay is not null)
            {
                if (overlayThread.IsAlive)
                    StopOverlay();
                else
                    overlay.Dispose();
            }

            overlay = new Overlay(this);

            overlayThread = new Thread(overlay.Run) { IsBackground = true };
            overlayThread.Start();

            if (!overlay.IsCreated)
            {
                StopOverlay();
                return;
            }

            overlayEnabled = true;
            btn_overlay.Text = "Disable Overlay";
            btn_drag_overlay.Show();
            Task.Run(() => Update());
        }

        private void StopOverlay()
        {
            overlay.Dispose();
            overlayThread.Join();
            overlayEnabled = false;
            overlay = null;
            btn_overlay.Text = "Enable Overlay";
            btn_drag_overlay.Hide();
        }

        private void CreateTimer()
        {
            updateTimer = new System.Windows.Forms.Timer() { Interval = 1000 };
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Start();
        }

        private void Update()
        {
            if(!ServerData.RetrieveData(this.ServerID, out var server) || server is null)
            {
                MessageBox.Show("Unable to fetch server data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Action UpdateUIElements;

            try
            {
                var isOnline = server.Status == "online";
                var color = isOnline ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                var serverPing = isOnline ? ServerData.Ping(server.Ip, 4).ToString() : string.Empty;

                UpdateUIElements = new Action(() => {
                    name.Text = server.Name;
                    status.Text = isOnline ? "Online" : "Offline";
                    players.Text = isOnline ? $"{server.Players} / {server.MaxPlayers}" : "0";
                    time.Text = isOnline ? server.Time : "00:00";
                    Ping.Text = serverPing;
                    name.ForeColor = color;
                    status.ForeColor = color;
                    players.ForeColor = color;
                    time.ForeColor = color;
                });

                if (!searchbutton.Enabled) searchbutton.Enabled = true;

                if (overlayEnabled)
                {
                    overlay.Name = server.Name;
                    overlay.Status = server.Status;
                    overlay.Players = $"{server.Players} / {server.MaxPlayers}";
                    overlay.Time = server.Time;
                    overlay.Ping = serverPing;
                }
            } catch (NullReferenceException)
            {
                MessageBox.Show("There was a problem while retrieving server data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Run on UI thread
            this.BeginInvoke(UpdateUIElements);
        }

        private string IterateResults(IEnumerable<dynamic> servers)
        {
            if (servers is null) return this.ServerID;

            foreach (var server in servers)
            {
                DialogResult dialogResult = MessageBox.Show(server.Name, "Search Results", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    return server.ID;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    break;
                }
            }
            MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return this.ServerID;
        }

        private async void searchbutton_Click(object sender, EventArgs e)
        {
            var searchInput = searchbox.Text;
            var lookUpString = ServerData.GetLookupString(searchInput);

            if(!ServerData.GetServers(lookUpString, out var servers))
            {
                MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            this.ServerID = IterateResults(servers);

            updateTimer_Reset();

            await Task.Run(() => Update());
        }

        private void updateTimer_Reset()
        {
            update_progbar.Value = 0;
            counter = 0;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (counter >= 30)
            {
                update_progbar.Value = 0;
                counter = 0;
                Task.Run(() => Update());
            }
            counter++;
            update_progbar.Value = counter;

            if (overlayEnabled && overlay is not null)
            {
                if (AppSettings.Instance.OverlayAllWindows) return;
                try
                {
                    overlay.SetWindowVisibility();
                    if (overlay.HasProcessExited())
                        StopOverlay();
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    StopOverlay();
                    if (ex.Message.Contains("Access is denied"))
                        MessageBox.Show("Please run as administrator in order for overlay to stick to SCUM game window!", "Access Denied!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void setdft_btn_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.DefaultServerId = this.ServerID;
            if (!Configuration.Save(AppSettings.Instance))
            {
                MessageBox.Show("Unable to save default server!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Default Server Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            settingsForm = new SettingsForm(overlay);
            settingsForm.Show();
        }

        private void btn_drag_overlay_Click(object sender, EventArgs e)
        {
            if (overlay is null || !overlayEnabled) return;

            toggle_overlay_btn(false);
            overlay.DragOverlay();
        }

        public void toggle_overlay_btn(bool isDoneDragging) => btn_drag_overlay.Enabled = isDoneDragging;

        public void InvokeOnUIThread(Action del) => this.BeginInvoke(del);

    }
}
