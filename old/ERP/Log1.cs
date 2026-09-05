using GenuineHR;
using Maintanence_Printing_Tool;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CRM_App
{
    public partial class Log1 : Form
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

        // Interactive Lamp & Animation State
        private bool isLampOn = true;
        private float currentLight = 1.0f;
        private float targetLight = 1.0f;
        private float ambientPulse = 0f;

        // Pull cord physics animation
        private float cordPullOffset = 0f;
        private float cordVelocity = 0f;
        private bool isPulling = false;
        private Rectangle cordBeadRect;
        private Rectangle lampShadeRect;

        // Form Shake State
        private int originalCardLeft;
        private int shakeStep = 0;

        // Timers
        private Timer animTimer;
        private Timer shakeTimer;

        // Show/hide password state
        private bool isPasswordHidden = true;

        public Log1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            lampShadeRect = new Rectangle(85, 135, 120, 55);
            cordBeadRect = new Rectangle(152, 235, 14, 18);

            // Animation Loop (50 FPS)
            animTimer = new Timer();
            animTimer.Interval = 20;
            animTimer.Tick += AnimTimer_Tick;

            // Shake Timer
            shakeTimer = new Timer();
            shakeTimer.Interval = 25;
            shakeTimer.Tick += ShakeTimer_Tick;
        }

        private void Log1_Load(object sender, EventArgs e)
        {
            ApplyFormRoundedCorners(14);
            originalCardLeft = pnlLoginCard.Left;
            animTimer.Start();
            txtusername.Focus();
        }

        private void ApplyFormRoundedCorners(int radius)
        {
            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(new Rectangle(0, 0, Width, Height), radius))
            {
                this.Region = new Region(path);
            }
        }

        #region Animation Engine

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            bool needRepaint = false;

            // 1. Smooth Light Transition
            if (Math.Abs(currentLight - targetLight) > 0.01f)
            {
                currentLight += (targetLight - currentLight) * 0.15f;
                needRepaint = true;
            }
            else
            {
                currentLight = targetLight;
            }

            // 2. Ambient subtle breathing light oscillation
            ambientPulse += 0.04f;
            if (ambientPulse > (float)(Math.PI * 2)) ambientPulse = 0f;

            // 3. Pull cord spring physics rebound
            if (cordPullOffset != 0f || cordVelocity != 0f)
            {
                float springForce = -0.22f * cordPullOffset;
                float damping = 0.82f;
                cordVelocity = (cordVelocity + springForce) * damping;
                cordPullOffset += cordVelocity;

                if (Math.Abs(cordPullOffset) < 0.2f && Math.Abs(cordVelocity) < 0.2f)
                {
                    cordPullOffset = 0f;
                    cordVelocity = 0f;
                }
                needRepaint = true;
            }

            if (needRepaint || isLampOn)
            {
                this.Invalidate();
                pnlLoginCard.Invalidate();
            }
        }

        public void ToggleLamp()
        {
            isLampOn = !isLampOn;
            targetLight = isLampOn ? 1.0f : 0.0f;

            // Trigger spring cord pull animation
            cordPullOffset = 16f;
            cordVelocity = -2f;
        }

        #endregion

        #region Custom Anti-Aliased Painting (Lamp, Light Glow & Scene)

        private void Log1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int w = Width;
            int h = Height;

            // 1. Deep Matte Dark Background
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(20, 21, 25)))
            {
                g.FillRectangle(bgBrush, 0, 0, w, h);
            }

            // 2. Volumetric Glowing Light Bloom (Radiating from Lamp Shade)
            if (currentLight > 0.01f)
            {
                float pulse = (float)(Math.Sin(ambientPulse) * 0.04f);
                float effectiveLight = Math.Max(0f, Math.Min(1f, currentLight + pulse));

                int lampCenterX = 145;
                int lampCenterY = 165;

                // Multi-layered radial glowing halo
                int[] glowRadii = new int[] { 220, 160, 110, 70, 40 };
                int[] glowAlphas = new int[] { (int)(22 * effectiveLight), (int)(45 * effectiveLight), (int)(75 * effectiveLight), (int)(110 * effectiveLight), (int)(160 * effectiveLight) };

                for (int i = 0; i < glowRadii.Length; i++)
                {
                    int r = glowRadii[i];
                    Rectangle glowRect = new Rectangle(lampCenterX - r, lampCenterY - r, r * 2, r * 2);
                    using (GraphicsPath glowPath = new GraphicsPath())
                    {
                        glowPath.AddEllipse(glowRect);
                        using (PathGradientBrush pgb = new PathGradientBrush(glowPath))
                        {
                            pgb.CenterPoint = new PointF(lampCenterX, lampCenterY);
                            pgb.CenterColor = Color.FromArgb(glowAlphas[i], 255, 240, 165);
                            pgb.SurroundColors = new Color[] { Color.FromArgb(0, 240, 220, 140) };
                            g.FillPath(pgb, glowPath);
                        }
                    }
                }

                // Soft warm cone beam cast toward the login card
                using (GraphicsPath conePath = new GraphicsPath())
                {
                    conePath.AddPolygon(new PointF[] {
                        new PointF(lampCenterX - 30, lampCenterY + 15),
                        new PointF(w, 40),
                        new PointF(w, h - 20),
                        new PointF(lampCenterX + 40, lampCenterY + 25)
                    });

                    using (PathGradientBrush coneBrush = new PathGradientBrush(conePath))
                    {
                        coneBrush.CenterPoint = new PointF(lampCenterX + 20, lampCenterY + 20);
                        coneBrush.CenterColor = Color.FromArgb((int)(38 * effectiveLight), 255, 235, 160);
                        coneBrush.SurroundColors = new Color[] { Color.FromArgb(0, 20, 21, 25) };
                        g.FillPath(coneBrush, conePath);
                    }
                }
            }

            // 3. Desk Floor Shadow under Lamp Base
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(12, 12, 15)))
            {
                g.FillEllipse(shadowBrush, 90, 316, 110, 16);
            }

            // 4. Lamp Stand Pole (Warm Brushed Metal / Off-White)
            int poleX = 141;
            int poleY = 175;
            int poleW = 8;
            int poleH = 135;

            Color poleColor = isLampOn ? Color.FromArgb(235, 232, 224) : Color.FromArgb(110, 112, 120);
            using (SolidBrush poleBrush = new SolidBrush(poleColor))
            {
                g.FillRectangle(poleBrush, poleX, poleY, poleW, poleH);
            }

            // 5. Lamp Base (Pill Shape)
            Rectangle baseRect = new Rectangle(105, 304, 80, 14);
            using (GraphicsPath basePath = VectorIcons.CreateRoundedRectPath(baseRect, 7))
            {
                using (SolidBrush baseBrush = new SolidBrush(poleColor))
                {
                    g.FillPath(baseBrush, basePath);
                }
            }

            // 6. Interactive Pull-Cord & Bead Switch
            int cordStartX = 158;
            int cordStartY = 180;
            int cordEndY = (int)(235 + cordPullOffset);

            using (Pen cordPen = new Pen(Color.FromArgb(120, 122, 130), 1.5f))
            {
                g.DrawLine(cordPen, cordStartX, cordStartY, cordStartX, cordEndY);
            }

            // Pull Bead / Knob
            Rectangle beadRect = new Rectangle(cordStartX - 5, cordEndY, 10, 14);
            cordBeadRect = new Rectangle(cordStartX - 10, cordEndY - 5, 20, 24);

            using (GraphicsPath beadPath = VectorIcons.CreateRoundedRectPath(beadRect, 4))
            {
                Color beadColor = isLampOn ? Color.FromArgb(210, 185, 120) : Color.FromArgb(90, 92, 100);
                using (SolidBrush beadBrush = new SolidBrush(beadColor))
                {
                    g.FillPath(beadBrush, beadPath);
                }
            }

            // 7. Lamp Shade (Mushroom / Half-Oval Dome)
            Rectangle shadeRect = new Rectangle(90, 135, 110, 50);
            using (GraphicsPath shadePath = new GraphicsPath())
            {
                shadePath.AddArc(shadeRect.X, shadeRect.Y, shadeRect.Width, shadeRect.Height * 2, 180, 180);
                shadePath.CloseFigure();

                if (currentLight > 0.01f)
                {
                    // Radiant Luminous Glowing Shade
                    using (LinearGradientBrush shadeLgb = new LinearGradientBrush(
                        new Point(shadeRect.X, shadeRect.Y),
                        new Point(shadeRect.X, shadeRect.Bottom),
                        Color.FromArgb(255, 255, 255),
                        Color.FromArgb(255, 248, 220)))
                    {
                        g.FillPath(shadeLgb, shadePath);
                    }

                    // Bright bottom rim glow
                    using (Pen rimPen = new Pen(Color.FromArgb((int)(255 * currentLight), 255, 255, 255), 2.5f))
                    {
                        g.DrawLine(rimPen, shadeRect.X + 2, shadeRect.Bottom - 2, shadeRect.Right - 2, shadeRect.Bottom - 2);
                    }
                }
                else
                {
                    // Dark matte shade when light is off
                    using (LinearGradientBrush darkLgb = new LinearGradientBrush(
                        new Point(shadeRect.X, shadeRect.Y),
                        new Point(shadeRect.X, shadeRect.Bottom),
                        Color.FromArgb(130, 132, 140),
                        Color.FromArgb(85, 87, 95)))
                    {
                        g.FillPath(darkLgb, shadePath);
                    }
                }
            }

            // 8. Subtle Outer Form Border
            using (Pen borderPen = new Pen(Color.FromArgb(40, 45, 55), 1.2f))
            {
                using (GraphicsPath outerPath = VectorIcons.CreateRoundedRectPath(new Rectangle(0, 0, w - 1, h - 1), 14))
                {
                    g.DrawPath(borderPen, outerPath);
                }
            }
        }

        #endregion

        #region Login Card Glassmorphism Painting

        private void PnlLoginCard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cw = pnlLoginCard.Width;
            int ch = pnlLoginCard.Height;
            Rectangle cardRect = new Rectangle(0, 0, cw - 1, ch - 1);

            // 1. Dark Glassmorphism Card Background
            int bgAlpha = isLampOn ? 220 : 240;
            Color cardBg = Color.FromArgb(bgAlpha, 34, 36, 42);

            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(cardRect, 18))
            {
                using (SolidBrush cardBrush = new SolidBrush(cardBg))
                {
                    g.FillPath(cardBrush, path);
                }

                // 1px Subtle Glass Border Highlight
                Color borderColor = isLampOn ? Color.FromArgb(55, 255, 255, 255) : Color.FromArgb(30, 255, 255, 255);
                using (Pen borderPen = new Pen(borderColor, 1.2f))
                {
                    g.DrawPath(borderPen, path);
                }
            }

            // 2. Rounded Pill Container for Username TextBox
            Rectangle userPillRect = new Rectangle(20, 84, 210, 32);
            using (GraphicsPath userPath = VectorIcons.CreateRoundedRectPath(userPillRect, 10))
            {
                using (SolidBrush pillBrush = new SolidBrush(Color.FromArgb(46, 49, 56)))
                {
                    g.FillPath(pillBrush, userPath);
                }
                using (Pen p = new Pen(Color.FromArgb(65, 68, 78), 1f))
                {
                    g.DrawPath(p, userPath);
                }
            }

            // 3. Rounded Pill Container for Password TextBox
            Rectangle passPillRect = new Rectangle(20, 142, 210, 32);
            using (GraphicsPath passPath = VectorIcons.CreateRoundedRectPath(passPillRect, 10))
            {
                using (SolidBrush pillBrush = new SolidBrush(Color.FromArgb(46, 49, 56)))
                {
                    g.FillPath(pillBrush, passPath);
                }
                using (Pen p = new Pen(Color.FromArgb(65, 68, 78), 1f))
                {
                    g.DrawPath(p, passPath);
                }
            }
        }

        private void BtnLogin_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, btnSignIn.Width - 1, btnSignIn.Height - 1);

            // Luxury Golden Gradient Pill Button
            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(rect, 17))
            {
                Color topGold = Color.FromArgb(242, 222, 158);
                Color btmGold = Color.FromArgb(198, 160, 80);

                using (LinearGradientBrush lgb = new LinearGradientBrush(
                    new Point(0, 0), new Point(0, btnSignIn.Height), topGold, btmGold))
                {
                    g.FillPath(lgb, path);
                }

                // Ambient Glow Border
                using (Pen glowPen = new Pen(Color.FromArgb(140, 255, 240, 180), 1.2f))
                {
                    g.DrawPath(glowPen, path);
                }
            }

            // Text "Sign in"
            SizeF sz = g.MeasureString("Sign in", btnSignIn.Font);
            int tx = (btnSignIn.Width - (int)sz.Width) / 2;
            int ty = (btnSignIn.Height - (int)sz.Height) / 2;

            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(22, 24, 28)))
            {
                g.DrawString("Sign in", btnSignIn.Font, textBrush, tx, ty);
            }
        }

        #endregion

        #region User Actions, Authentication & Interactivity

        private void Log1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Check if user clicked the pull-cord bead or the lamp shade
                if (cordBeadRect.Contains(e.Location) || lampShadeRect.Contains(e.Location))
                {
                    ToggleLamp();
                    return;
                }

                // Borderless window dragging
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void Log1_MouseMove(object sender, MouseEventArgs e)
        {
            if (cordBeadRect.Contains(e.Location) || lampShadeRect.Contains(e.Location))
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void Log1_MouseUp(object sender, MouseEventArgs e)
        {
            isPulling = false;
        }

        private void BtnEye_Click(object sender, EventArgs e)
        {
            isPasswordHidden = !isPasswordHidden;
            txtpassword.PasswordChar = isPasswordHidden ? '•' : '\0';
            btnEye.ForeColor = isPasswordHidden ? Color.FromArgb(140, 145, 155) : Color.FromArgb(225, 198, 130);
            txtpassword.Focus();
        }

        private bool BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                string connectionString = "YOUR_CONNECTION_STRING";

                using (System.Data.SqlClient.SqlConnection con =
                       new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    con.Open();

                    using (System.Data.SqlClient.SqlCommand cmd =
                           new System.Data.SqlClient.SqlCommand("pr_getLogin", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (System.Data.SqlClient.SqlDataAdapter da =
                               new System.Data.SqlClient.SqlDataAdapter(cmd))
                        {
                            System.Data.DataTable dt =
                                new System.Data.DataTable();

                            da.Fill(dt);

                            return dt.Rows.Count > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            // Default demo credentials or any non-empty credentials for testing
            if ((username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "1234") ||
                (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin") ||
                (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)))
            {
                return true;
            }

            return false;
        }

        private void ShakeCard()
        {
            if (shakeTimer != null && shakeTimer.Enabled) return;

            originalCardLeft = pnlLoginCard.Left;
            shakeStep = 0;
            shakeTimer.Start();
        }

        private void ShakeTimer_Tick(object sender, EventArgs e)
        {
            int[] offsets = new int[] { 8, -8, 6, -6, 4, -4, 2, -2, 0 };
            if (shakeStep < offsets.Length)
            {
                pnlLoginCard.Left = originalCardLeft + offsets[shakeStep];
                shakeStep++;
            }
            else
            {
                pnlLoginCard.Left = originalCardLeft;
                shakeTimer.Stop();
            }
        }

        private void TxtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtpassword.Focus();
            }
        }

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSignIn.PerformClick();
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

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
    }
}
