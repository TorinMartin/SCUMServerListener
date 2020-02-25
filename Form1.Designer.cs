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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.setdft_btn = new System.Windows.Forms.Button();
            this.label_ping = new System.Windows.Forms.Label();
            this.Ping = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchbox
            // 
            this.searchbox.Location = new System.Drawing.Point(12, 12);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(222, 20);
            this.searchbox.TabIndex = 0;
            // 
            // searchbutton
            // 
            this.searchbutton.Location = new System.Drawing.Point(240, 9);
            this.searchbutton.Name = "searchbutton";
            this.searchbutton.Size = new System.Drawing.Size(75, 23);
            this.searchbutton.TabIndex = 1;
            this.searchbutton.Text = "Search";
            this.searchbutton.UseVisualStyleBackColor = true;
            this.searchbutton.Click += new System.EventHandler(this.searchbutton_Click);
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
            this.status.Location = new System.Drawing.Point(86, 222);
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
            this.players.Location = new System.Drawing.Point(86, 265);
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
            this.label_status.Location = new System.Drawing.Point(33, 225);
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
            this.label_players.Location = new System.Drawing.Point(33, 268);
            this.label_players.Name = "label_players";
            this.label_players.Size = new System.Drawing.Size(47, 13);
            this.label_players.TabIndex = 8;
            this.label_players.Text = "Players: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SCUMServerListener.Properties.Resources.grad;
            this.pictureBox1.Location = new System.Drawing.Point(28, 170);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(656, 128);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Black;
            this.progressBar1.ForeColor = System.Drawing.Color.Orange;
            this.progressBar1.Location = new System.Drawing.Point(457, 271);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(216, 10);
            this.progressBar1.TabIndex = 11;
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
            // GUI
            // 
            this.AcceptButton = this.searchbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SCUMServerListener.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(696, 310);
            this.Controls.Add(this.Ping);
            this.Controls.Add(this.label_ping);
            this.Controls.Add(this.setdft_btn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label_players);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.players);
            this.Controls.Add(this.status);
            this.Controls.Add(this.title);
            this.Controls.Add(this.searchbutton);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.Text = "SCUM Server Listener";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button setdft_btn;
        private System.Windows.Forms.Label label_ping;
        private System.Windows.Forms.Label Ping;
    }
}

