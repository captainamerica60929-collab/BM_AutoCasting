using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CRM_App.Controls;

namespace CRM_App
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Form controls
        private RoundedTextBox txtusername;
        private RoundedTextBox txtpassword;
        private RoundedButton btnSignIn;
        private Label lblForgotAccount;
        private Label lblResetPassword;
        private Button btnMinimize;
        private Button btnClose;
        private Panel pnlLogoBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblForgotAccount = new System.Windows.Forms.Label();
            this.lblResetPassword = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlLogoBox = new System.Windows.Forms.Panel();
            this.btnSignIn = new CRM_App.RoundedButton();
            this.txtpassword = new CRM_App.RoundedTextBox();
            this.txtusername = new CRM_App.RoundedTextBox();
            this.SuspendLayout();
            // 
            // lblForgotAccount
            // 
            this.lblForgotAccount.AutoSize = true;
            this.lblForgotAccount.BackColor = System.Drawing.Color.Transparent;
            this.lblForgotAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblForgotAccount.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForgotAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblForgotAccount.Location = new System.Drawing.Point(37, 322);
            this.lblForgotAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblForgotAccount.Name = "lblForgotAccount";
            this.lblForgotAccount.Size = new System.Drawing.Size(213, 21);
            this.lblForgotAccount.TabIndex = 3;
            this.lblForgotAccount.Text = "Cannot access your account ?";
            this.lblForgotAccount.Click += new System.EventHandler(this.lblForgotAccount_Click);
            this.lblForgotAccount.MouseEnter += new System.EventHandler(this.Link_MouseEnter);
            this.lblForgotAccount.MouseLeave += new System.EventHandler(this.Link_MouseLeave);
            // 
            // lblResetPassword
            // 
            this.lblResetPassword.AutoSize = true;
            this.lblResetPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblResetPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblResetPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResetPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblResetPassword.Location = new System.Drawing.Point(37, 359);
            this.lblResetPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResetPassword.Name = "lblResetPassword";
            this.lblResetPassword.Size = new System.Drawing.Size(154, 21);
            this.lblResetPassword.TabIndex = 4;
            this.lblResetPassword.Text = "Reset your Password";
            this.lblResetPassword.Click += new System.EventHandler(this.lblResetPassword_Click);
            this.lblResetPassword.MouseEnter += new System.EventHandler(this.Link_MouseEnter);
            this.lblResetPassword.MouseLeave += new System.EventHandler(this.Link_MouseLeave);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(65)))), ((int)(((byte)(45)))));
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(100)))), ((int)(((byte)(70)))));
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(907, 5);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(4);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(48, 34);
            this.btnMinimize.TabIndex = 5;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(27)))), ((int)(((byte)(35)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(960, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(48, 34);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlLogoBox
            // 
            this.pnlLogoBox.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogoBox.Location = new System.Drawing.Point(633, 128);
            this.pnlLogoBox.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLogoBox.Name = "pnlLogoBox";
            this.pnlLogoBox.Size = new System.Drawing.Size(353, 228);
            this.pnlLogoBox.TabIndex = 7;
            this.pnlLogoBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLogoBox_Paint);
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.Transparent;
            this.btnSignIn.BorderColor = System.Drawing.Color.Transparent;
            this.btnSignIn.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.btnSignIn.ButtonColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(115)))), ((int)(((byte)(25)))));
            this.btnSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignIn.FlatAppearance.BorderSize = 0;
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignIn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.ForeColor = System.Drawing.Color.White;
            this.btnSignIn.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(160)))), ((int)(((byte)(42)))));
            this.btnSignIn.HoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(130)))), ((int)(((byte)(30)))));
            this.btnSignIn.Location = new System.Drawing.Point(363, 320);
            this.btnSignIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(95)))), ((int)(((byte)(20)))));
            this.btnSignIn.ShowLoginIcon = true;
            this.btnSignIn.Size = new System.Drawing.Size(173, 54);
            this.btnSignIn.TabIndex = 2;
            this.btnSignIn.Text = "Sign in";
            this.btnSignIn.UseVisualStyleBackColor = false;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // txtpassword
            // 
            this.txtpassword.BackColor = System.Drawing.Color.White;
            this.txtpassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(226)))));
            this.txtpassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.txtpassword.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(49)))), ((int)(((byte)(59)))));
            this.txtpassword.LeadingIcon = CRM_App.LeadingIconType.Lock;
            this.txtpassword.LeadingIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(155)))), ((int)(((byte)(168)))));
            this.txtpassword.Location = new System.Drawing.Point(207, 236);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.txtpassword.ShowPasswordToggle = true;
            this.txtpassword.Size = new System.Drawing.Size(331, 47);
            this.txtpassword.TabIndex = 1;
            this.txtpassword.UseSystemPasswordChar = true;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtusername
            // 
            this.txtusername.BackColor = System.Drawing.Color.White;
            this.txtusername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(226)))));
            this.txtusername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.txtusername.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(49)))), ((int)(((byte)(59)))));
            this.txtusername.LeadingIcon = CRM_App.LeadingIconType.User;
            this.txtusername.LeadingIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(155)))), ((int)(((byte)(168)))));
            this.txtusername.Location = new System.Drawing.Point(207, 165);
            this.txtusername.Margin = new System.Windows.Forms.Padding(4);
            this.txtusername.Name = "txtusername";
            this.txtusername.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.txtusername.Size = new System.Drawing.Size(331, 47);
            this.txtusername.TabIndex = 0;
            this.txtusername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1016, 572);
            this.Controls.Add(this.pnlLogoBox);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.lblResetPassword);
            this.Controls.Add(this.lblForgotAccount);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtusername);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BM AUTO CASTINGS - Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginForm_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
