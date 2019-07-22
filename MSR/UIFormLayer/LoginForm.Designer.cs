namespace MSR
{
    partial class LoginForm
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
            this.username_Login_label = new System.Windows.Forms.Label();
            this.username_Login_textBox = new System.Windows.Forms.TextBox();
            this.password_Login_textBox = new System.Windows.Forms.TextBox();
            this.password_Login_label = new System.Windows.Forms.Label();
            this.ok_Login_button = new System.Windows.Forms.Button();
            this.cancel_Login_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username_Login_label
            // 
            this.username_Login_label.AutoSize = true;
            this.username_Login_label.Location = new System.Drawing.Point(168, 22);
            this.username_Login_label.Name = "username_Login_label";
            this.username_Login_label.Size = new System.Drawing.Size(58, 13);
            this.username_Login_label.TabIndex = 0;
            this.username_Login_label.Text = "&Username:";
            // 
            // username_Login_textBox
            // 
            this.username_Login_textBox.Location = new System.Drawing.Point(171, 38);
            this.username_Login_textBox.Name = "username_Login_textBox";
            this.username_Login_textBox.Size = new System.Drawing.Size(221, 20);
            this.username_Login_textBox.TabIndex = 1;
            // 
            // password_Login_textBox
            // 
            this.password_Login_textBox.Location = new System.Drawing.Point(171, 101);
            this.password_Login_textBox.Name = "password_Login_textBox";
            this.password_Login_textBox.PasswordChar = '*';
            this.password_Login_textBox.Size = new System.Drawing.Size(221, 20);
            this.password_Login_textBox.TabIndex = 3;
            this.password_Login_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Password_Login_textBox_KeyDown);
            // 
            // password_Login_label
            // 
            this.password_Login_label.AutoSize = true;
            this.password_Login_label.Location = new System.Drawing.Point(168, 85);
            this.password_Login_label.Name = "password_Login_label";
            this.password_Login_label.Size = new System.Drawing.Size(56, 13);
            this.password_Login_label.TabIndex = 2;
            this.password_Login_label.Text = "&Password:";
            // 
            // ok_Login_button
            // 
            this.ok_Login_button.Location = new System.Drawing.Point(196, 146);
            this.ok_Login_button.Name = "ok_Login_button";
            this.ok_Login_button.Size = new System.Drawing.Size(90, 23);
            this.ok_Login_button.TabIndex = 4;
            this.ok_Login_button.Text = "&OK";
            this.ok_Login_button.UseVisualStyleBackColor = true;
            this.ok_Login_button.Click += new System.EventHandler(this.Ok_Login_button_Click);
            // 
            // cancel_Login_button
            // 
            this.cancel_Login_button.Location = new System.Drawing.Point(302, 146);
            this.cancel_Login_button.Name = "cancel_Login_button";
            this.cancel_Login_button.Size = new System.Drawing.Size(90, 23);
            this.cancel_Login_button.TabIndex = 5;
            this.cancel_Login_button.Text = "&Cancel";
            this.cancel_Login_button.UseVisualStyleBackColor = true;
            this.cancel_Login_button.Click += new System.EventHandler(this.Cancel_Login_button_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 181);
            this.Controls.Add(this.cancel_Login_button);
            this.Controls.Add(this.ok_Login_button);
            this.Controls.Add(this.password_Login_textBox);
            this.Controls.Add(this.password_Login_label);
            this.Controls.Add(this.username_Login_textBox);
            this.Controls.Add(this.username_Login_label);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label username_Login_label;
        private System.Windows.Forms.TextBox username_Login_textBox;
        private System.Windows.Forms.TextBox password_Login_textBox;
        private System.Windows.Forms.Label password_Login_label;
        private System.Windows.Forms.Button ok_Login_button;
        private System.Windows.Forms.Button cancel_Login_button;
    }
}