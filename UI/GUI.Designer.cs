namespace SCUMServerListener
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.searchbox = new System.Windows.Forms.TextBox();
            this.searchbutton = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.players = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label_players = new System.Windows.Forms.Label();
            this.gradbg_picbox = new System.Windows.Forms.PictureBox();
            this.update_progbar = new System.Windows.Forms.ProgressBar();
            this.update_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.setdft_btn = new System.Windows.Forms.Button();
            this.label_ping = new System.Windows.Forms.Label();
            this.Ping = new System.Windows.Forms.Label();
            this.btn_overlay = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overlaySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_time = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.btn_drag_overlay = new System.Windows.Forms.Button();
            this.drag_tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gradbg_picbox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchbox
            // 
            this.searchbox.Location = new System.Drawing.Point(12, 38);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(222, 20);
            this.searchbox.TabIndex = 0;
            // 
            // searchbutton
            // 
            this.searchbutton.Location = new System.Drawing.Point(240, 35);
            this.searchbutton.Name = "searchbutton";
            this.searchbutton.Size = new System.Drawing.Size(75, 23);
            this.searchbutton.TabIndex = 1;
            this.searchbutton.Text = "Search";
            this.searchbutton.UseVisualStyleBackColor = true;
            this.searchbutton.Click += new System.EventHandler(searchbutton_Click);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.Black;
            this.title.Location = new System.Drawing.Point(293, 141);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(133, 26);
            this.title.TabIndex = 2;
            this.title.Text = "Server Status:";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.BackColor = System.Drawing.Color.Black;
            this.name.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.ForeColor = System.Drawing.Color.White;
            this.name.Location = new System.Drawing.Point(86, 180);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(0, 16);
            this.name.TabIndex = 3;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.BackColor = System.Drawing.Color.Black;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(86, 209);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 16);
            this.status.TabIndex = 4;
            // 
            // players
            // 
            this.players.AutoSize = true;
            this.players.BackColor = System.Drawing.Color.Black;
            this.players.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.players.ForeColor = System.Drawing.Color.Black;
            this.players.Location = new System.Drawing.Point(86, 266);
            this.players.Name = "players";
            this.players.Size = new System.Drawing.Size(0, 16);
            this.players.TabIndex = 5;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.BackColor = System.Drawing.Color.Black;
            this.label_name.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_name.Location = new System.Drawing.Point(35, 180);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(38, 13);
            this.label_name.TabIndex = 6;
            this.label_name.Text = "Name:";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.BackColor = System.Drawing.Color.Black;
            this.label_status.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_status.Location = new System.Drawing.Point(35, 211);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(40, 13);
            this.label_status.TabIndex = 7;
            this.label_status.Text = "Status:";
            // 
            // label_players
            // 
            this.label_players.AutoSize = true;
            this.label_players.BackColor = System.Drawing.Color.Black;
            this.label_players.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_players.Location = new System.Drawing.Point(35, 268);
            this.label_players.Name = "label_players";
            this.label_players.Size = new System.Drawing.Size(47, 13);
            this.label_players.TabIndex = 8;
            this.label_players.Text = "Players: ";
            // 
            // gradbg_picbox
            // 
            this.gradbg_picbox.BackgroundImage = global::SCUMServerListener.Properties.Resources.grad;
            this.gradbg_picbox.Location = new System.Drawing.Point(28, 170);
            this.gradbg_picbox.Name = "gradbg_picbox";
            this.gradbg_picbox.Size = new System.Drawing.Size(656, 125);
            this.gradbg_picbox.TabIndex = 9;
            this.gradbg_picbox.TabStop = false;
            // 
            // update_progbar
            // 
            this.update_progbar.BackColor = System.Drawing.Color.Black;
            this.update_progbar.ForeColor = System.Drawing.Color.Orange;
            this.update_progbar.Location = new System.Drawing.Point(459, 272);
            this.update_progbar.Name = "update_progbar";
            this.update_progbar.Size = new System.Drawing.Size(216, 10);
            this.update_progbar.TabIndex = 11;
            this.update_tooltip.SetToolTip(this.update_progbar, "Server Status will update every 30 seconds...");
            // 
            // setdft_btn
            // 
            this.setdft_btn.BackColor = System.Drawing.Color.Transparent;
            this.setdft_btn.ForeColor = System.Drawing.Color.Black;
            this.setdft_btn.Location = new System.Drawing.Point(609, 144);
            this.setdft_btn.Name = "setdft_btn";
            this.setdft_btn.Size = new System.Drawing.Size(75, 23);
            this.setdft_btn.TabIndex = 12;
            this.setdft_btn.Text = "Set Default";
            this.setdft_btn.UseVisualStyleBackColor = false;
            this.setdft_btn.Click += new System.EventHandler(this.setdft_btn_Click);
            // 
            // label_ping
            // 
            this.label_ping.AutoSize = true;
            this.label_ping.ForeColor = System.Drawing.Color.LawnGreen;
            this.label_ping.Location = new System.Drawing.Point(624, 182);
            this.label_ping.Name = "label_ping";
            this.label_ping.Size = new System.Drawing.Size(34, 13);
            this.label_ping.TabIndex = 13;
            this.label_ping.Text = "Ping: ";
            // 
            // Ping
            // 
            this.Ping.AutoSize = true;
            this.Ping.ForeColor = System.Drawing.Color.LawnGreen;
            this.Ping.Location = new System.Drawing.Point(654, 182);
            this.Ping.Name = "Ping";
            this.Ping.Size = new System.Drawing.Size(0, 13);
            this.Ping.TabIndex = 14;
            // 
            // btn_overlay
            // 
            this.btn_overlay.Location = new System.Drawing.Point(28, 144);
            this.btn_overlay.Name = "btn_overlay";
            this.btn_overlay.Size = new System.Drawing.Size(95, 23);
            this.btn_overlay.TabIndex = 15;
            this.btn_overlay.Text = "Enable Overlay";
            this.btn_overlay.UseVisualStyleBackColor = true;
            this.btn_overlay.Click += new System.EventHandler(this.btn_overlay_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(696, 24);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "menuStrip";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overlaySettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // overlaySettingsToolStripMenuItem
            // 
            this.overlaySettingsToolStripMenuItem.Name = "overlaySettingsToolStripMenuItem";
            this.overlaySettingsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.overlaySettingsToolStripMenuItem.Text = "Overlay Settings";
            this.overlaySettingsToolStripMenuItem.Click += new System.EventHandler(this.overlaySettingsToolStripMenuItem_Click);
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_time.Location = new System.Drawing.Point(35, 241);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(36, 13);
            this.label_time.TabIndex = 17;
            this.label_time.Text = "Time: ";
            this.label_time.UseMnemonic = false;
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.ForeColor = System.Drawing.Color.Green;
            this.time.Location = new System.Drawing.Point(86, 239);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(0, 15);
            this.time.TabIndex = 18;
            // 
            // btn_drag_overlay
            // 
            this.btn_drag_overlay.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_drag_overlay.Location = new System.Drawing.Point(129, 144);
            this.btn_drag_overlay.Name = "btn_drag_overlay";
            this.btn_drag_overlay.Size = new System.Drawing.Size(95, 23);
            this.btn_drag_overlay.TabIndex = 19;
            this.btn_drag_overlay.Text = "Drag Overlay";
            this.drag_tooltip.SetToolTip(this.btn_drag_overlay, "Position overlay with your mouse! Left click to save!");
            this.btn_drag_overlay.UseVisualStyleBackColor = true;
            this.btn_drag_overlay.Visible = false;
            this.btn_drag_overlay.Click += new System.EventHandler(this.btn_drag_overlay_Click);
            // 
            // drag_tooltip
            // 
            this.drag_tooltip.AutoPopDelay = 5000;
            this.drag_tooltip.InitialDelay = 50;
            this.drag_tooltip.IsBalloon = true;
            this.drag_tooltip.ReshowDelay = 10;
            this.drag_tooltip.ShowAlways = true;
            this.drag_tooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.drag_tooltip.ToolTipTitle = "Drag Overlay";
            // 
            // GUI
            // 
            this.AcceptButton = this.searchbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SCUMServerListener.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(696, 305);
            this.Controls.Add(this.btn_drag_overlay);
            this.Controls.Add(this.time);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.btn_overlay);
            this.Controls.Add(this.Ping);
            this.Controls.Add(this.label_ping);
            this.Controls.Add(this.setdft_btn);
            this.Controls.Add(this.update_progbar);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label_players);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.players);
            this.Controls.Add(this.status);
            this.Controls.Add(this.title);
            this.Controls.Add(this.searchbutton);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.gradbg_picbox);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.Text = "SCUM Server Listener";
            ((System.ComponentModel.ISupportInitialize)(this.gradbg_picbox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchbox;
        private System.Windows.Forms.Button searchbutton;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label players;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_players;
        private System.Windows.Forms.PictureBox gradbg_picbox;
        private System.Windows.Forms.ProgressBar update_progbar;
        private System.Windows.Forms.ToolTip update_tooltip;
        private System.Windows.Forms.Button setdft_btn;
        private System.Windows.Forms.Label label_ping;
        private System.Windows.Forms.Label Ping;
        private System.Windows.Forms.Button btn_overlay;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overlaySettingsToolStripMenuItem;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Button btn_drag_overlay;
        private System.Windows.Forms.ToolTip drag_tooltip;
    }
}

