namespace SCUMServerListener
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btn_save = new System.Windows.Forms.Button();
            this.cb_background = new System.Windows.Forms.CheckBox();
            this.cb_allwindows = new System.Windows.Forms.CheckBox();
            this.cb_serveronline = new System.Windows.Forms.ComboBox();
            this.lbl_color1 = new System.Windows.Forms.Label();
            this.lbl_color2 = new System.Windows.Forms.Label();
            this.cb_serveroffline = new System.Windows.Forms.ComboBox();
            this.lbl_coords = new System.Windows.Forms.Label();
            this.tb_po1 = new System.Windows.Forms.TextBox();
            this.tb_po2 = new System.Windows.Forms.TextBox();
            this.allwindows_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.cb_bgColor = new System.Windows.Forms.ComboBox();
            this.lbl_bgcolor = new System.Windows.Forms.Label();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_right = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.cb_name = new System.Windows.Forms.CheckBox();
            this.cb_players = new System.Windows.Forms.CheckBox();
            this.cb_time = new System.Windows.Forms.CheckBox();
            this.cb_ping = new System.Windows.Forms.CheckBox();
            this.lbl_displayopt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(57, 400);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cb_background
            // 
            this.cb_background.AutoSize = true;
            this.cb_background.Location = new System.Drawing.Point(12, 26);
            this.cb_background.Name = "cb_background";
            this.cb_background.Size = new System.Drawing.Size(146, 17);
            this.cb_background.TabIndex = 2;
            this.cb_background.Text = "Disable Text Background";
            this.cb_background.UseVisualStyleBackColor = true;
            // 
            // cb_allwindows
            // 
            this.cb_allwindows.AutoSize = true;
            this.cb_allwindows.Location = new System.Drawing.Point(12, 50);
            this.cb_allwindows.Name = "cb_allwindows";
            this.cb_allwindows.Size = new System.Drawing.Size(123, 17);
            this.cb_allwindows.TabIndex = 3;
            this.cb_allwindows.Text = "Overlay All Windows";
            this.cb_allwindows.UseVisualStyleBackColor = true;
            // 
            // cb_serveronline
            // 
            this.cb_serveronline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_serveronline.FormattingEnabled = true;
            this.cb_serveronline.Items.AddRange(new object[] {
            "Green",
            "Red",
            "Blue",
            "Yellow",
            "White",
            "Cyan",
            "Black"});
            this.cb_serveronline.Location = new System.Drawing.Point(12, 130);
            this.cb_serveronline.Name = "cb_serveronline";
            this.cb_serveronline.Size = new System.Drawing.Size(121, 21);
            this.cb_serveronline.TabIndex = 4;
            // 
            // lbl_color1
            // 
            this.lbl_color1.AutoSize = true;
            this.lbl_color1.Location = new System.Drawing.Point(12, 114);
            this.lbl_color1.Name = "lbl_color1";
            this.lbl_color1.Size = new System.Drawing.Size(98, 13);
            this.lbl_color1.TabIndex = 5;
            this.lbl_color1.Text = "Server Online Color";
            // 
            // lbl_color2
            // 
            this.lbl_color2.AutoSize = true;
            this.lbl_color2.Location = new System.Drawing.Point(12, 159);
            this.lbl_color2.Name = "lbl_color2";
            this.lbl_color2.Size = new System.Drawing.Size(98, 13);
            this.lbl_color2.TabIndex = 6;
            this.lbl_color2.Text = "Server Offline Color";
            // 
            // cb_serveroffline
            // 
            this.cb_serveroffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_serveroffline.FormattingEnabled = true;
            this.cb_serveroffline.Items.AddRange(new object[] {
            "Green",
            "Red",
            "Blue",
            "Yellow",
            "White",
            "Cyan",
            "Black"});
            this.cb_serveroffline.Location = new System.Drawing.Point(12, 175);
            this.cb_serveroffline.Name = "cb_serveroffline";
            this.cb_serveroffline.Size = new System.Drawing.Size(121, 21);
            this.cb_serveroffline.TabIndex = 7;
            // 
            // lbl_coords
            // 
            this.lbl_coords.AutoSize = true;
            this.lbl_coords.Location = new System.Drawing.Point(9, 275);
            this.lbl_coords.Name = "lbl_coords";
            this.lbl_coords.Size = new System.Drawing.Size(83, 13);
            this.lbl_coords.TabIndex = 8;
            this.lbl_coords.Text = "Overlay Position";
            // 
            // tb_po1
            // 
            this.tb_po1.Location = new System.Drawing.Point(12, 292);
            this.tb_po1.Name = "tb_po1";
            this.tb_po1.Size = new System.Drawing.Size(39, 20);
            this.tb_po1.TabIndex = 9;
            // 
            // tb_po2
            // 
            this.tb_po2.Location = new System.Drawing.Point(57, 292);
            this.tb_po2.Name = "tb_po2";
            this.tb_po2.Size = new System.Drawing.Size(39, 20);
            this.tb_po2.TabIndex = 10;
            // 
            // cb_bgColor
            // 
            this.cb_bgColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_bgColor.FormattingEnabled = true;
            this.cb_bgColor.Items.AddRange(new object[] {
            "Dark",
            "Grid",
            "Green",
            "Red",
            "Blue",
            "Yellow",
            "White",
            "Cyan",
            "Black"});
            this.cb_bgColor.Location = new System.Drawing.Point(12, 90);
            this.cb_bgColor.Name = "cb_bgColor";
            this.cb_bgColor.Size = new System.Drawing.Size(121, 21);
            this.cb_bgColor.TabIndex = 15;
            // 
            // lbl_bgcolor
            // 
            this.lbl_bgcolor.AutoSize = true;
            this.lbl_bgcolor.Location = new System.Drawing.Point(15, 71);
            this.lbl_bgcolor.Name = "lbl_bgcolor";
            this.lbl_bgcolor.Size = new System.Drawing.Size(92, 13);
            this.lbl_bgcolor.TabIndex = 16;
            this.lbl_bgcolor.Text = "Background Color";
            // 
            // btn_left
            // 
            this.btn_left.Image = global::SCUMServerListener.Properties.Resources.left_arrow_small;
            this.btn_left.Location = new System.Drawing.Point(15, 335);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(27, 23);
            this.btn_left.TabIndex = 14;
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_right
            // 
            this.btn_right.Image = global::SCUMServerListener.Properties.Resources.arrow_point_to_right_small;
            this.btn_right.Location = new System.Drawing.Point(77, 335);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(27, 23);
            this.btn_right.TabIndex = 13;
            this.btn_right.UseVisualStyleBackColor = true;
            this.btn_right.Click += new System.EventHandler(this.btn_right_Click);
            // 
            // btn_down
            // 
            this.btn_down.Image = global::SCUMServerListener.Properties.Resources.arrow_down_sign_to_navigate_small;
            this.btn_down.Location = new System.Drawing.Point(46, 348);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(27, 23);
            this.btn_down.TabIndex = 12;
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Image = global::SCUMServerListener.Properties.Resources.navigate_up_arrow_small2;
            this.btn_up.Location = new System.Drawing.Point(46, 318);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(27, 23);
            this.btn_up.TabIndex = 11;
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // cb_name
            // 
            this.cb_name.AutoSize = true;
            this.cb_name.Location = new System.Drawing.Point(12, 219);
            this.cb_name.Name = "cb_name";
            this.cb_name.Size = new System.Drawing.Size(88, 17);
            this.cb_name.TabIndex = 17;
            this.cb_name.Text = "Server Name";
            this.cb_name.UseVisualStyleBackColor = true;
            // 
            // cb_players
            // 
            this.cb_players.AutoSize = true;
            this.cb_players.Location = new System.Drawing.Point(12, 243);
            this.cb_players.Name = "cb_players";
            this.cb_players.Size = new System.Drawing.Size(86, 17);
            this.cb_players.TabIndex = 18;
            this.cb_players.Text = "Player Count";
            this.cb_players.UseVisualStyleBackColor = true;
            // 
            // cb_time
            // 
            this.cb_time.AutoSize = true;
            this.cb_time.Location = new System.Drawing.Point(99, 219);
            this.cb_time.Name = "cb_time";
            this.cb_time.Size = new System.Drawing.Size(49, 17);
            this.cb_time.TabIndex = 19;
            this.cb_time.Text = "Time";
            this.cb_time.UseVisualStyleBackColor = true;
            // 
            // cb_ping
            // 
            this.cb_ping.AutoSize = true;
            this.cb_ping.Location = new System.Drawing.Point(99, 243);
            this.cb_ping.Name = "cb_ping";
            this.cb_ping.Size = new System.Drawing.Size(47, 17);
            this.cb_ping.TabIndex = 20;
            this.cb_ping.Text = "Ping";
            this.cb_ping.UseVisualStyleBackColor = true;
            // 
            // lbl_displayopt
            // 
            this.lbl_displayopt.AutoSize = true;
            this.lbl_displayopt.Location = new System.Drawing.Point(9, 203);
            this.lbl_displayopt.Name = "lbl_displayopt";
            this.lbl_displayopt.Size = new System.Drawing.Size(80, 13);
            this.lbl_displayopt.TabIndex = 21;
            this.lbl_displayopt.Text = "Display Options";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 430);
            this.Controls.Add(this.lbl_displayopt);
            this.Controls.Add(this.cb_ping);
            this.Controls.Add(this.cb_time);
            this.Controls.Add(this.cb_players);
            this.Controls.Add(this.cb_name);
            this.Controls.Add(this.lbl_bgcolor);
            this.Controls.Add(this.cb_bgColor);
            this.Controls.Add(this.btn_left);
            this.Controls.Add(this.btn_right);
            this.Controls.Add(this.btn_down);
            this.Controls.Add(this.btn_up);
            this.Controls.Add(this.tb_po2);
            this.Controls.Add(this.tb_po1);
            this.Controls.Add(this.lbl_coords);
            this.Controls.Add(this.cb_serveroffline);
            this.Controls.Add(this.lbl_color2);
            this.Controls.Add(this.lbl_color1);
            this.Controls.Add(this.cb_serveronline);
            this.Controls.Add(this.cb_allwindows);
            this.Controls.Add(this.cb_background);
            this.Controls.Add(this.btn_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Overlay Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckBox cb_background;
        private System.Windows.Forms.CheckBox cb_allwindows;
        private System.Windows.Forms.ComboBox cb_serveronline;
        private System.Windows.Forms.Label lbl_color1;
        private System.Windows.Forms.Label lbl_color2;
        private System.Windows.Forms.ComboBox cb_serveroffline;
        private System.Windows.Forms.Label lbl_coords;
        private System.Windows.Forms.TextBox tb_po1;
        private System.Windows.Forms.TextBox tb_po2;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.ToolTip allwindows_tooltip;
        private System.Windows.Forms.ComboBox cb_bgColor;
        private System.Windows.Forms.Label lbl_bgcolor;
        private System.Windows.Forms.CheckBox cb_name;
        private System.Windows.Forms.CheckBox cb_players;
        private System.Windows.Forms.CheckBox cb_time;
        private System.Windows.Forms.CheckBox cb_ping;
        private System.Windows.Forms.Label lbl_displayopt;
    }
}