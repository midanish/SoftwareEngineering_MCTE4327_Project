namespace LoginPage
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userbox = new System.Windows.Forms.TextBox();
            this.passbox = new System.Windows.Forms.TextBox();
            this.labeluser = new System.Windows.Forms.Label();
            this.labelpass = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(281, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(236, 176);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // userbox
            // 
            this.userbox.Location = new System.Drawing.Point(397, 218);
            this.userbox.Name = "userbox";
            this.userbox.Size = new System.Drawing.Size(100, 22);
            this.userbox.TabIndex = 1;
            // 
            // passbox
            // 
            this.passbox.Location = new System.Drawing.Point(397, 277);
            this.passbox.Name = "passbox";
            this.passbox.PasswordChar = '*';
            this.passbox.Size = new System.Drawing.Size(100, 22);
            this.passbox.TabIndex = 2;
            // 
            // labeluser
            // 
            this.labeluser.AutoSize = true;
            this.labeluser.Location = new System.Drawing.Point(303, 221);
            this.labeluser.Name = "labeluser";
            this.labeluser.Size = new System.Drawing.Size(70, 16);
            this.labeluser.TabIndex = 3;
            this.labeluser.Text = "Username";
            // 
            // labelpass
            // 
            this.labelpass.AutoSize = true;
            this.labelpass.Location = new System.Drawing.Point(303, 280);
            this.labelpass.Name = "labelpass";
            this.labelpass.Size = new System.Drawing.Size(67, 16);
            this.labelpass.TabIndex = 4;
            this.labelpass.Text = "Password";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(365, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelpass);
            this.Controls.Add(this.labeluser);
            this.Controls.Add(this.passbox);
            this.Controls.Add(this.userbox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox userbox;
        private System.Windows.Forms.TextBox passbox;
        private System.Windows.Forms.Label labeluser;
        private System.Windows.Forms.Label labelpass;
        private System.Windows.Forms.Button button1;
    }
}

