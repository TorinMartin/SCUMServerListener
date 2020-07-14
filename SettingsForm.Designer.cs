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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbl_color1 = new System.Windows.Forms.Label();
            this.lbl_color2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbl_coords = new System.Windows.Forms.Label();
            this.tb_po1 = new System.Windows.Forms.TextBox();
            this.tb_po2 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cb_bgColor = new System.Windows.Forms.ComboBox();
            this.lbl_bgcolor = new System.Windows.Forms.Label();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_right = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(60, 328);
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
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Green",
            "Red",
            "Blue",
            "Yellow",
            "White",
            "Pink",
            "Black"});
            this.comboBox1.Location = new System.Drawing.Point(12, 130);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
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
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Green",
            "Red",
            "Blue",
            "Yellow",
            "White",
            "Pink",
            "Black"});
            this.comboBox2.Location = new System.Drawing.Point(12, 175);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 7;
            // 
            // lbl_coords
            // 
            this.lbl_coords.AutoSize = true;
            this.lbl_coords.Location = new System.Drawing.Point(12, 203);
            this.lbl_coords.Name = "lbl_coords";
            this.lbl_coords.Size = new System.Drawing.Size(83, 13);
            this.lbl_coords.TabIndex = 8;
            this.lbl_coords.Text = "Overlay Position";
            // 
            // tb_po1
            // 
            this.tb_po1.Location = new System.Drawing.Point(15, 220);
            this.tb_po1.Name = "tb_po1";
            this.tb_po1.Size = new System.Drawing.Size(39, 20);
            this.tb_po1.TabIndex = 9;
            // 
            // tb_po2
            // 
            this.tb_po2.Location = new System.Drawing.Point(60, 220);
            this.tb_po2.Name = "tb_po2";
            this.tb_po2.Size = new System.Drawing.Size(39, 20);
            this.tb_po2.TabIndex = 10;
            // 
            // cb_bgColor
            // 
            this.cb_bgColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_bgColor.Enabled = false;
            this.cb_bgColor.FormattingEnabled = true;
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
            this.btn_left.Location = new System.Drawing.Point(18, 263);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(27, 23);
            this.btn_left.TabIndex = 14;
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_right
            // 
            this.btn_right.Image = global::SCUMServerListener.Properties.Resources.arrow_point_to_right_small;
            this.btn_right.Location = new System.Drawing.Point(80, 263);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(27, 23);
            this.btn_right.TabIndex = 13;
            this.btn_right.UseVisualStyleBackColor = true;
            this.btn_right.Click += new System.EventHandler(this.btn_right_Click);
            // 
            // btn_down
            // 
            this.btn_down.Image = global::SCUMServerListener.Properties.Resources.arrow_down_sign_to_navigate_small;
            this.btn_down.Location = new System.Drawing.Point(49, 276);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(27, 23);
            this.btn_down.TabIndex = 12;
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Image = global::SCUMServerListener.Properties.Resources.navigate_up_arrow_small2;
            this.btn_up.Location = new System.Drawing.Point(49, 246);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(27, 23);
            this.btn_up.TabIndex = 11;
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 363);
            this.Controls.Add(this.lbl_bgcolor);
            this.Controls.Add(this.cb_bgColor);
            this.Controls.Add(this.btn_left);
            this.Controls.Add(this.btn_right);
            this.Controls.Add(this.btn_down);
            this.Controls.Add(this.btn_up);
            this.Controls.Add(this.tb_po2);
            this.Controls.Add(this.tb_po1);
            this.Controls.Add(this.lbl_coords);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.lbl_color2);
            this.Controls.Add(this.lbl_color1);
            this.Controls.Add(this.comboBox1);
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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbl_color1;
        private System.Windows.Forms.Label lbl_color2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lbl_coords;
        private System.Windows.Forms.TextBox tb_po1;
        private System.Windows.Forms.TextBox tb_po2;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cb_bgColor;
        private System.Windows.Forms.Label lbl_bgcolor;
    }
}