namespace DMFDSteamSaveTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.steamID64_enc = new System.Windows.Forms.TextBox();
            this.steamID64_dec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.encFileNameLabel = new System.Windows.Forms.Label();
            this.decFileNameLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.browseEnc_button = new System.Windows.Forms.Button();
            this.browseDec_button = new System.Windows.Forms.Button();
            this.decrypt_button = new System.Windows.Forms.Button();
            this.encrypt_button = new System.Windows.Forms.Button();


            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // steamID64_enc
            // 
            this.steamID64_enc.Location = new System.Drawing.Point(76, 19);
            this.steamID64_enc.MaxLength = 17;
            this.steamID64_enc.Name = "steamID64_enc";
            this.steamID64_enc.Size = new System.Drawing.Size(122, 20);
            this.steamID64_enc.TabIndex = 0;
            // 
            // steamID64_dec
            // 
            this.steamID64_dec.Location = new System.Drawing.Point(76, 19);
            this.steamID64_dec.MaxLength = 17;
            this.steamID64_dec.Name = "steamID64_dec";
            this.steamID64_dec.Size = new System.Drawing.Size(122, 20);
            this.steamID64_dec.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 99;
            this.label1.Text = "steamID64:";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 99;
            this.label2.Text = "steamID64:";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // encFileNameLabel
            // 
            this.encFileNameLabel.AutoSize = true;
            this.encFileNameLabel.Location = new System.Drawing.Point(76, 49);
            this.encFileNameLabel.Name = "encFileNameLabel";
            this.encFileNameLabel.Size = new System.Drawing.Size(87, 17);
            this.encFileNameLabel.TabIndex = 99;
            this.encFileNameLabel.Text = "(no file selected)";
            this.encFileNameLabel.UseCompatibleTextRendering = true;
            // 
            // decFileNameLabel
            // 
            this.decFileNameLabel.AutoSize = true;
            this.decFileNameLabel.Location = new System.Drawing.Point(76, 49);
            this.decFileNameLabel.Name = "decFileNameLabel";
            this.decFileNameLabel.Size = new System.Drawing.Size(87, 17);
            this.decFileNameLabel.TabIndex = 99;
            this.decFileNameLabel.Text = "(no file selected)";
            this.decFileNameLabel.UseCompatibleTextRendering = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.decrypt_button);
            this.groupBox1.Controls.Add(this.encFileNameLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.steamID64_enc);
            this.groupBox1.Controls.Add(this.browseEnc_button);
            this.groupBox1.Location = new System.Drawing.Point(10, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 102);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encrypted save file";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.encrypt_button);
            this.groupBox2.Controls.Add(this.steamID64_dec);
            this.groupBox2.Controls.Add(this.decFileNameLabel);
            this.groupBox2.Controls.Add(this.browseDec_button);
            this.groupBox2.Location = new System.Drawing.Point(10, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 102);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Decrypted save file";
            this.groupBox2.UseCompatibleTextRendering = true;
            // 
            // browseEnc_button
            // 
            this.browseEnc_button.Location = new System.Drawing.Point(5, 45);
            this.browseEnc_button.Name = "browseEnc_button";
            this.browseEnc_button.Size = new System.Drawing.Size(64, 20);
            this.browseEnc_button.TabIndex = 1;
            this.browseEnc_button.Text = "Browse...";
            this.browseEnc_button.UseCompatibleTextRendering = true;
            this.browseEnc_button.UseVisualStyleBackColor = true;
            this.browseEnc_button.Click += new System.EventHandler(this.browseEnc_button_Click);
            // 
            // browseDec_button
            // 
            this.browseDec_button.Location = new System.Drawing.Point(5, 45);
            this.browseDec_button.Name = "browseDec_button";
            this.browseDec_button.Size = new System.Drawing.Size(64, 20);
            this.browseDec_button.TabIndex = 4;
            this.browseDec_button.Text = "Browse...";
            this.browseDec_button.UseCompatibleTextRendering = true;
            this.browseDec_button.UseVisualStyleBackColor = true;
            this.browseDec_button.Click += new System.EventHandler(this.browseDec_button_Click);
            // 
            // decrypt_button
            // 
            this.decrypt_button.Enabled = false;
            this.decrypt_button.Location = new System.Drawing.Point(5, 70);
            this.decrypt_button.Name = "decrypt_button";
            this.decrypt_button.Size = new System.Drawing.Size(213, 20);
            this.decrypt_button.TabIndex = 2;
            this.decrypt_button.Text = "Decrypt with Steam ID";
            this.decrypt_button.UseCompatibleTextRendering = true;
            this.decrypt_button.UseVisualStyleBackColor = true;
            this.decrypt_button.Click += new System.EventHandler(this.decrypt_button_Click);
            // 
            // encrypt_button
            // 
            this.encrypt_button.Enabled = false;
            this.encrypt_button.Location = new System.Drawing.Point(5, 70);
            this.encrypt_button.Name = "encrypt_button";
            this.encrypt_button.Size = new System.Drawing.Size(213, 20);
            this.encrypt_button.TabIndex = 5;
            this.encrypt_button.Text = "Encrypt with Steam ID";
            this.encrypt_button.UseCompatibleTextRendering = true;
            this.encrypt_button.UseVisualStyleBackColor = true;
            this.encrypt_button.Click += new System.EventHandler(this.encrypt_button_Click);


            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(244, 239);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "DMFDSteamSaveTool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox steamID64_enc;
        private System.Windows.Forms.TextBox steamID64_dec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label encFileNameLabel;
        private System.Windows.Forms.Label decFileNameLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button browseEnc_button;
        private System.Windows.Forms.Button browseDec_button;
        private System.Windows.Forms.Button decrypt_button;
        private System.Windows.Forms.Button encrypt_button;

        
    }
}

