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
        private System.Windows.Forms.Timer timer1;

        ServerData ServerData = new ServerData();
        public GUI()
        {
            InitializeComponent();
            this.MaximizeBox = false; 
            this.ServerID = loadDefault();
            CreateTimer();
            Update();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.progressBar1, "Server Status will update every 30 seconds...");
        }

        private void CreateTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; 
            timer1.Start();
            progressBar1.Maximum = 30;
            progressBar1.Value = 0;
        }

        private void Update()
        {
            String[] Results = ServerData.RetrieveData(this.ServerID);
            if (Results != null)
            {
                name.Text = Results[0];
                var ip = Results[4];
                if (Results[2] == "online")
                {
                    name.ForeColor = System.Drawing.Color.Green;
                    status.ForeColor = System.Drawing.Color.Green;
                    players.ForeColor = System.Drawing.Color.Green;
                    status.Text = "Online";
                    players.Text = Results[1] + " / " + Results[3];
                    Ping.Text = ServerData.Ping(ip, 4).ToString();
                }
                else
                {
                    status.ForeColor = System.Drawing.Color.Red;
                    players.ForeColor = System.Drawing.Color.Red;
                    name.ForeColor = System.Drawing.Color.Red;
                    status.Text = "Offline";
                    players.Text = "0";
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
                DialogResult dialogResult = MessageBox.Show(servers[i].GetName(), "Search Results", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    return servers[i].getID();
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

            timer1_Reset();

            if (ServerID != "scum")
            {
                searchbutton.Enabled = false;
                Update();
            }
            else
            {
                this.ServerID = "2608083";
                MessageBox.Show("Search query returned no result!", "Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Reset()
        {
            progressBar1.Value = 0;
            counter = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (counter == 30)
            {
                progressBar1.Value = 0;
                counter = 0;
                Update();
            }
            counter++;
            progressBar1.Value = counter;
        }

        private string loadDefault()
        {
            string id = null;

            if (!File.Exists("settings.ini"))
            {
                MessageBox.Show("Settings file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var settings_json = File.ReadAllText("settings.ini");
                dynamic result = JsonConvert.DeserializeObject(settings_json);

                id = result["settings"]["default_id"].ToString();
            }

            return id;
        }

        private void setdft_btn_Click(object sender, EventArgs e)
        {
            JObject settings =
            new JObject(
                new JProperty("settings",
                    new JObject(
                        new JProperty("default_id", this.ServerID))));

            string path = @"settings.ini";
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                TextWriter tw = new StreamWriter(path);
                tw.Write(settings.ToString());
                tw.Close();
                MessageBox.Show("Default Server Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("Path does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Unable to write to file!. Permission is denied!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
