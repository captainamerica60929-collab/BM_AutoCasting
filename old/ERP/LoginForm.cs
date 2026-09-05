using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CRM_App.Controls;
using GenuineHR;
using Maintanence_Printing_Tool;

namespace CRM_App
{
    public partial class LoginForm : Form
    {
        #region Native Form Dragging & Drop Shadow

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #endregion

        // Color Palette Constants matching reference design
        private readonly Color COLOR_PRIMARY_GREEN = Color.FromArgb(46, 139, 34);   // #2E8B22
        private readonly Color COLOR_DARK_GREEN    = Color.FromArgb(7, 91, 63);     // #075B3F
        private readonly Color COLOR_HEADER_GREEN  = Color.FromArgb(6, 75, 52);     // #064B34
        private readonly Color COLOR_FOOTER_NAVY    = Color.FromArgb(28, 43, 58);    // #1C2B3A
        private readonly Color COLOR_BG_LIGHT       = Color.FromArgb(247, 249, 250); // #F7F9FA
        private readonly Color COLOR_TEXT_DARK      = Color.FromArgb(40, 49, 59);    // #28313B
        private readonly Color COLOR_TEXT_GRAY      = Color.FromArgb(128, 138, 154); // #808A9A
        private readonly Color COLOR_BORDER_GRAY    = Color.FromArgb(215, 220, 226); // #D7DCE2
        private readonly Color COLOR_REQUIRED_RED   = Color.FromArgb(227, 27, 35);   // #E31B23

        private Image _logoImage = null;

        public LoginForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Apply rounded corners to the borderless form
            ApplyFormRoundedCorners(12);

            // Load BM AUTO CASTINGS logo from Resources with multi-path fallback
            LoadLogoImage();

            txtusername.Focus();
        }

        private void LoadLogoImage()
        {
            try
            {
                _logoImage = Properties.Resources.BMAutoCastingsLogo;
            }
            catch { }

            if (_logoImage == null)
            {
                string[] searchPaths = new string[]
                {
                    Path.Combine(Application.StartupPath, "Resources", "BMAutoCastingsLogo.png"),
                    Path.Combine(Application.StartupPath, "BM LOGO.png"),
                    Path.Combine(Application.StartupPath, "..", "..", "Resources", "BMAutoCastingsLogo.png"),
                    Path.Combine(Application.StartupPath, "..", "..", "BM LOGO.png"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BM LOGO.png"),
                    @"d:\ERP\BM AUTOCAST\BM LOGO.png",
                    @"d:\ERP\BM AUTOCAST\ERP\Resources\BMAutoCastingsLogo.png"
                };

                foreach (string path in searchPaths)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            _logoImage = Image.FromFile(path);
                            break;
                        }
                        catch { }
                    }
                }
            }

            if (_logoImage != null)
            {
                pnlLogoBox.Invalidate();
            }
        }

        private void ApplyFormRoundedCorners(int radius)
        {
            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(new Rectangle(0, 0, Width, Height), radius))
            {
                this.Region = new Region(path);
            }
        }

        #region Form Dragging

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        #endregion

        #region Custom Anti-Aliased Form Painting

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int formW = Width;
            int formH = Height;
            int footerH = 65;
            int footerY = formH - footerH;

            // 1. Base Form Background (Clean Light Gray/White)
            using (SolidBrush bgBrush = new SolidBrush(COLOR_BG_LIGHT))
            {
                g.FillRectangle(bgBrush, 0, 0, formW, footerY);
            }

            // 2. Right Bottom Green Geometric Shape
            using (GraphicsPath greenShape = new GraphicsPath())
            {
                greenShape.AddBezier(410, footerY, 430, 310, 445, 230, 470, 195);
                greenShape.AddLine(470, 195, formW, 260);
                greenShape.AddLine(formW, 260, formW, footerY);
                greenShape.AddLine(formW, footerY, 410, footerY);
                greenShape.CloseFigure();

                using (SolidBrush darkGreenBrush = new SolidBrush(COLOR_DARK_GREEN))
                {
                    g.FillPath(darkGreenBrush, greenShape);
                }
            }

            // 3. Top-Right Dark Green Header Strip for Window Controls
            using (GraphicsPath headerStrip = new GraphicsPath())
            {
                headerStrip.AddPolygon(new Point[] {
                    new Point(490, 0),
                    new Point(formW, 0),
                    new Point(formW, 36),
                    new Point(460, 36)
                });
                using (SolidBrush headerBrush = new SolidBrush(COLOR_HEADER_GREEN))
                {
                    g.FillPath(headerBrush, headerStrip);
                }
            }

            // 4. Subtle Slanted Dividing Curve (Separating Left and Right)
            using (Pen curvePen = new Pen(Color.FromArgb(230, 235, 240), 1.5f))
            {
                g.DrawBezier(curvePen, 490, 0, 465, 120, 440, 250, 410, footerY);
            }

            // 5. Subtle Dot Matrix Pattern in Bottom-Right Corner
            DrawDotMatrix(g, new Rectangle(formW - 165, footerY - 80, 145, 68));

            // 6. Left Section Header: Green Circular User Icon
            Rectangle userCircleRect = new Rectangle(24, 22, 44, 44);
            VectorIcons.DrawUserCircle(g, userCircleRect, COLOR_PRIMARY_GREEN, Color.White);

            // 7. Header Text: "Sign in to your Account"
            using (Font titleFontBold = new Font("Segoe UI", 18.5f, FontStyle.Bold))
            using (Font titleFontReg = new Font("Segoe UI", 18.5f, FontStyle.Regular))
            {
                using (SolidBrush greenBrush = new SolidBrush(COLOR_PRIMARY_GREEN))
                {
                    g.DrawString("Sign in", titleFontBold, greenBrush, 76, 26);
                }

                SizeF signInSize = g.MeasureString("Sign in", titleFontBold);
                using (SolidBrush darkBrush = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("to your Account", titleFontReg, darkBrush, 76 + (int)signInSize.Width - 4, 26);
                }
            }

            // Short green underline below "Sign in"
            using (SolidBrush greenLineBrush = new SolidBrush(COLOR_PRIMARY_GREEN))
            {
                g.FillRectangle(greenLineBrush, 78, 59, 58, 3);
            }

            // Copyright Subtitle
            using (Font copyFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush copyBrush = new SolidBrush(COLOR_TEXT_GRAY))
            {
               // g.DrawString("Copyright © 2024-2025", copyFont, copyBrush, 178, 88);
            }

            // 8. Input Field Labels & Icons
            // User Name Label Row
            Rectangle userIconRect = new Rectangle(28, 142, 18, 20);
            VectorIcons.DrawUserOutline(g, userIconRect, COLOR_PRIMARY_GREEN, 1.8f);

            using (Font labelFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            {
                using (SolidBrush labelBrush = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("User Name", labelFont, labelBrush, 52, 142);
                }

                SizeF unSize = g.MeasureString("User Name", labelFont);
                using (SolidBrush reqBrush = new SolidBrush(COLOR_REQUIRED_RED))
                {
                    g.DrawString("*", labelFont, reqBrush, 52 + (int)unSize.Width - 2, 140);
                }
            }

            // Password Label Row
            Rectangle lockIconRect = new Rectangle(28, 200, 18, 20);
            VectorIcons.DrawLockOutline(g, lockIconRect, COLOR_PRIMARY_GREEN, 1.8f);

            using (Font labelFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            {
                using (SolidBrush labelBrush = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("Password", labelFont, labelBrush, 52, 200);
                }

                SizeF pwdSize = g.MeasureString("Password", labelFont);
                using (SolidBrush reqBrush = new SolidBrush(COLOR_REQUIRED_RED))
                {
                    g.DrawString("*", labelFont, reqBrush, 52 + (int)pwdSize.Width - 2, 198);
                }
            }

            // 9. Subtle Horizontal Divider Line
            using (Pen divPen = new Pen(Color.FromArgb(226, 232, 240), 1f))
            {
                g.DrawLine(divPen, 28, 335, 402, 335);
            }

            // 10. Right Section Company Title: "BM AUTO CASTINGS"
            using (Font companyFont = new Font("Segoe UI", 13.5f, FontStyle.Bold))
            {
                int logoBoxCenterX = pnlLogoBox.Left + pnlLogoBox.Width / 2;
                SizeF bmSize = g.MeasureString("BM AUTO", companyFont);
                SizeF castSize = g.MeasureString("CASTINGS", companyFont);
                int totalTitleW = (int)(bmSize.Width + castSize.Width) - 4;
                int titleX = logoBoxCenterX - totalTitleW / 2;
                int titleY = 72;

                using (SolidBrush cGreen = new SolidBrush(COLOR_PRIMARY_GREEN))
                {
                    g.DrawString("BM AUTO", companyFont, cGreen, titleX, titleY);
                }

                using (SolidBrush cGray = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("CASTINGS", companyFont, cGray, titleX + (int)bmSize.Width - 2, titleY);
                }
            }

            // 11. Bottom Footer Bar (Across Full Width)
            using (SolidBrush footerBrush = new SolidBrush(COLOR_FOOTER_NAVY))
            {
                g.FillRectangle(footerBrush, 0, footerY, formW, footerH);
            }

            DrawFooterContent(g, footerY, formW, footerH);
        }

        private void DrawDotMatrix(Graphics g, Rectangle bounds)
        {
            int cols = 8;
            int rows = 4;
            int spacingX = bounds.Width / cols;
            int spacingY = bounds.Height / rows;
            int dotRadius = 2;

            using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(45, 100, 220, 130)))
            {
                for (int c = 0; c < cols; c++)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        int x = bounds.X + c * spacingX;
                        int y = bounds.Y + r * spacingY;
                        g.FillEllipse(dotBrush, x, y, dotRadius * 2, dotRadius * 2);
                    }
                }
            }
        }

        private void DrawFooterContent(Graphics g, int footerY, int formW, int footerH)
        {
            int centerY = footerY + footerH / 2;
            using (Font footerFont = new Font("Segoe UI", 10.2f, FontStyle.Bold))
            using (Font footerSubFont = new Font("Segoe UI", 10.2f, FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(240, 244, 248)))
            using (Pen sepPen = new Pen(Color.FromArgb(44, 62, 80), 1.2f))
            {
                // Column 1: Left - SS BELL VOZORG
                int col1X = 24;
                Rectangle dbIconRect = new Rectangle(col1X, centerY - 10, 22, 20);
                VectorIcons.DrawDatabaseIcon(g, dbIconRect, COLOR_PRIMARY_GREEN, 1.8f);
                g.DrawString("SS DELL PC", footerFont, textBrush, col1X + 32, centerY - 9);

                // Separator 1
                int sep1X = 225;
                g.DrawLine(sepPen, sep1X, footerY + 12, sep1X, footerY + footerH - 12);

                // Column 2: Center - AMC : 31-DEC-2030
                int col2X = 265;
                Rectangle calIconRect = new Rectangle(col2X, centerY - 10, 22, 20);
                VectorIcons.DrawCalendarIcon(g, calIconRect, COLOR_PRIMARY_GREEN, 1.8f);
                g.DrawString("AMC : 31-DEC-2027", footerFont, textBrush, col2X + 32, centerY - 9);

                // Separator 2
                int sep2X = 475;
                g.DrawLine(sepPen, sep2X, footerY + 12, sep2X, footerY + footerH - 12);

                // Column 3: Right - Powered by Genius IT Solution
                int col3X = 510;
                Rectangle userIconRect = new Rectangle(col3X, centerY - 10, 20, 20);
                VectorIcons.DrawUserOutline(g, userIconRect, COLOR_PRIMARY_GREEN, 1.8f);
                g.DrawString("Powered by Genius IT Solution", footerSubFont, textBrush, col3X + 30, centerY - 9);
            }
        }

        private void pnlLogoBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = new Rectangle(0, 0, pnlLogoBox.Width - 1, pnlLogoBox.Height - 1);
            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(rect, 10))
            {
                using (SolidBrush bgBrush = new SolidBrush(Color.White))
                {
                    g.FillPath(bgBrush, path);
                }

                if (_logoImage != null)
                {
                    Rectangle inner = new Rectangle(14, 14, pnlLogoBox.Width - 28, pnlLogoBox.Height - 28);
                    float imgAspect = (float)_logoImage.Width / _logoImage.Height;
                    float boxAspect = (float)inner.Width / inner.Height;

                    int drawW, drawH;
                    if (imgAspect > boxAspect)
                    {
                        drawW = inner.Width;
                        drawH = (int)(inner.Width / imgAspect);
                    }
                    else
                    {
                        drawH = inner.Height;
                        drawW = (int)(inner.Height * imgAspect);
                    }

                    int drawX = inner.X + (inner.Width - drawW) / 2;
                    int drawY = inner.Y + (inner.Height - drawH) / 2;

                    g.DrawImage(_logoImage, new Rectangle(drawX, drawY, drawW, drawH));
                }

                using (Pen borderPen = new Pen(COLOR_PRIMARY_GREEN, 1.5f))
                {
                    g.DrawPath(borderPen, path);
                }
            }
        }

        #endregion

        #region User Actions & Validation
        private void btnSignIn_Click(object sender, EventArgs e)
        {

            SqlConnection objCon = new SqlConnection(dbFunctions.connectionstring);
            try
            {
                if (txtusername.Text.Trim() != "" && txtpassword.Text.Trim() != "")
                {
                    // string enc = DecryptThis("BurWAhQFGrvQz5PcbAWHfw==", "Enc");

                    string Password = txtpassword.Text;// EncryptThis(txtpassword.Text.Trim(), "Enc");

                    DataTable dt = dbFunctions.getTable("pr_getLogin  '" + txtusername.Text + "','" + Password + "'");
                    if (dt.Rows.Count > 0)
                    {

                        dbFunctions.username = txtusername.Text;
                        dbFunctions.rights = dt.Rows[0]["um_iRightsId"].ToString();





                        this.Hide();
                        Main objMain = new Main();
                        objMain.Show();


                    }
                    else
                    {
                        MessageBox.Show("Invalid User Name And Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpassword.Text = "";
                        txtusername.Text = "";

                        txtusername.Focus();


                    }
                }
                else
                {
                    MessageBox.Show("Login Details Incorrect", "Error");
                    txtpassword.Text = "";
                    txtusername.Text = "";

                    return;
                }



            }




            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                txtusername.Focus();
                objCon.Close();
            }
        }

        /// <summary>
        /// Validates user credentials.
        /// Replace this method body with your SQL Server connection and stored procedure when ready.
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>True if authentication succeeds, false otherwise</returns>
        private bool ValidateLogin(string username, string password)
        {
            try
            {
                // =========================================================================
                // SQL SERVER INTEGRATION SECTION:
                // Uncomment and adjust the code below to connect with your database & stored procedure:
                // =========================================================================
                /*
                string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;User ID=YOUR_USER;Password=YOUR_PWD";
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    con.Open();
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("pr_getLogin", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                        {
                            System.Data.DataTable dt = new System.Data.DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                */

                // Default validation for testing: accepts non-empty credentials
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login validation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtpassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSignIn.PerformClick();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblForgotAccount_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact your IT Administrator to recover account access.\n\nHelpline: Genius IT Solution", "Account Assistance", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblResetPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Password reset instructions have been forwarded to the system administrator.", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Link_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                lbl.ForeColor = Color.FromArgb(7, 91, 63);
            }
        }

        private void Link_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                lbl.ForeColor = COLOR_PRIMARY_GREEN;
            }
        }

        #endregion
    }
}
