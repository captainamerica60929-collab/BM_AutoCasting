using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CRM_App
{
    /// <summary>
    /// High-precision anti-aliased GDI+ vector graphics renderer.
    /// Provides pixel-perfect, resolution-independent vector icons matching the reference image.
    /// </summary>
    public static class VectorIcons
    {
        /// <summary>
        /// Draws the large green circular user avatar icon for the login header.
        /// </summary>
        public static void DrawUserCircle(Graphics g, Rectangle bounds, Color circleColor, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Green circle background
            using (SolidBrush brush = new SolidBrush(circleColor))
            {
                g.FillEllipse(brush, bounds);
            }

            int cx = bounds.X + bounds.Width / 2;
            int cy = bounds.Y + bounds.Height / 2;
            int r = bounds.Width / 2;

            using (SolidBrush iconBrush = new SolidBrush(iconColor))
            {
                // Head circle
                int headR = (int)(r * 0.36f);
                int headY = cy - (int)(r * 0.44f);
                g.FillEllipse(iconBrush, cx - headR, headY, headR * 2, headR * 2);

                // Shoulders (clipped to circle)
                GraphicsState state = g.Save();
                using (GraphicsPath clipPath = new GraphicsPath())
                {
                    clipPath.AddEllipse(bounds);
                    g.SetClip(clipPath);

                    int bodyW = (int)(r * 1.55f);
                    int bodyH = (int)(r * 1.25f);
                    int bodyY = cy + (int)(r * 0.12f);

                    using (GraphicsPath bodyPath = new GraphicsPath())
                    {
                        bodyPath.AddEllipse(cx - bodyW / 2, bodyY, bodyW, bodyH);
                        g.FillPath(iconBrush, bodyPath);
                    }
                }
                g.Restore(state);
            }
        }

        /// <summary>
        /// Draws the user silhouette outline icon for username labels and footer.
        /// </summary>
        public static void DrawUserOutline(Graphics g, Rectangle bounds, Color color, float penWidth = 1.8f)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(color, penWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int cx = bounds.X + bounds.Width / 2;
                int headRadius = bounds.Width / 4;
                int headY = bounds.Y + 2;

                // Head
                g.DrawEllipse(pen, cx - headRadius, headY, headRadius * 2, headRadius * 2);

                // Body arc
                int bodyY = headY + headRadius * 2 + 3;
                int bodyW = bounds.Width - 4;
                int bodyH = bounds.Height - (bodyY - bounds.Y) + 2;

                g.DrawArc(pen, bounds.X + 2, bodyY, bodyW, bodyH * 2, 195, 150);
            }
        }

        /// <summary>
        /// Draws the lock outline icon for password labels.
        /// </summary>
        public static void DrawLockOutline(Graphics g, Rectangle bounds, Color color, float penWidth = 1.8f)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(color, penWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int cx = bounds.X + bounds.Width / 2;
                int bodyW = (int)(bounds.Width * 0.78f);
                int bodyH = (int)(bounds.Height * 0.54f);
                int bodyX = cx - bodyW / 2;
                int bodyY = bounds.Bottom - bodyH - 1;

                // Body
                using (GraphicsPath path = CreateRoundedRectPath(new Rectangle(bodyX, bodyY, bodyW, bodyH), 3))
                {
                    g.DrawPath(pen, path);
                }

                // Shackle
                int shackleW = (int)(bodyW * 0.60f);
                int shackleX = cx - shackleW / 2;
                int shackleY = bounds.Y + 2;
                int shackleH = (bodyY - shackleY) * 2;

                g.DrawArc(pen, shackleX, shackleY, shackleW, shackleH, 180, 180);

                // Keyhole
                using (SolidBrush b = new SolidBrush(color))
                {
                    int keyholeR = 2;
                    g.FillEllipse(b, cx - keyholeR, bodyY + (int)(bodyH * 0.32f), keyholeR * 2, keyholeR * 2);
                    g.DrawLine(pen, cx, bodyY + (int)(bodyH * 0.32f) + keyholeR, cx, bodyY + (int)(bodyH * 0.68f));
                }
            }
        }

        /// <summary>
        /// Draws the password eye visibility toggle icon.
        /// </summary>
        public static void DrawEyeIcon(Graphics g, Rectangle bounds, Color color, bool strikeThrough, float penWidth = 1.6f)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(color, penWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int cx = bounds.X + bounds.Width / 2;
                int cy = bounds.Y + bounds.Height / 2;
                int w = bounds.Width - 4;
                int h = 10;

                // Upper & lower eye curves
                using (GraphicsPath eyePath = new GraphicsPath())
                {
                    eyePath.AddArc(bounds.X + 2, cy - h, w, h * 2, 28, 124);
                    g.DrawPath(pen, eyePath);
                }
                using (GraphicsPath eyePath2 = new GraphicsPath())
                {
                    eyePath2.AddArc(bounds.X + 2, cy - h, w, h * 2, 208, 124);
                    g.DrawPath(pen, eyePath2);
                }

                // Center pupil
                int pupilR = 2;
                using (SolidBrush b = new SolidBrush(color))
                {
                    g.FillEllipse(b, cx - pupilR, cy - pupilR, pupilR * 2, pupilR * 2);
                }

                // Diagonal slash when password is masked
                if (strikeThrough)
                {
                    g.DrawLine(pen, bounds.X + 3, bounds.Y + 2, bounds.Right - 3, bounds.Bottom - 2);
                }
            }
        }

        /// <summary>
        /// Draws the login door and entrance arrow icon for the Sign In button.
        /// </summary>
        public static void DrawLoginArrow(Graphics g, Rectangle bounds, Color color, float penWidth = 2.0f)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(color, penWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int w = bounds.Width;
                int h = bounds.Height;
                int x = bounds.X;
                int y = bounds.Y;

                // Door frame [
                int frameW = (int)(w * 0.44f);
                int frameH = (int)(h * 0.85f);
                int frameY = y + (h - frameH) / 2;
                int frameX = x + (int)(w * 0.12f);

                Point[] bracket = new Point[]
                {
                    new Point(frameX + frameW, frameY),
                    new Point(frameX, frameY),
                    new Point(frameX, frameY + frameH),
                    new Point(frameX + frameW, frameY + frameH)
                };
                g.DrawLines(pen, bracket);

                // Right arrow entering door
                int arrowY = y + h / 2;
                int arrowStartX = frameX + (int)(frameW * 0.15f);
                int arrowEndX = x + w - 2;

                g.DrawLine(pen, arrowStartX, arrowY, arrowEndX, arrowY);

                // Arrow head
                int headSize = (int)(h * 0.26f);
                g.DrawLine(pen, arrowEndX - headSize, arrowY - headSize, arrowEndX, arrowY);
                g.DrawLine(pen, arrowEndX - headSize, arrowY + headSize, arrowEndX, arrowY);
            }
        }

        /// <summary>
        /// Draws the database / server stacked disks icon for the footer.
        /// </summary>
        public static void DrawDatabaseIcon(Graphics g, Rectangle bounds, Color color, float penWidth = 1.7f)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(color, penWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int x = bounds.X + 2;
                int w = bounds.Width - 4;
                int diskH = 7;
                int y1 = bounds.Y + 2;
                int y2 = y1 + 5;
                int y3 = y2 + 5;

                // Top disk ellipse
                g.DrawEllipse(pen, x, y1, w, diskH);

                // Vertical sides
                g.DrawLine(pen, x, y1 + diskH / 2, x, y3 + diskH / 2);
                g.DrawLine(pen, x + w, y1 + diskH / 2, x + w, y3 + diskH / 2);

                // Lower arcs
                g.DrawArc(pen, x, y2, w, diskH, 0, 180);
                g.DrawArc(pen, x, y3, w, diskH, 0, 180);
            }
        }

        /// <summary>
        /// Draws the calendar icon for AMC in the footer.
        /// </summary>
        public static void DrawCalendarIcon(Graphics g, Rectangle bounds, Color color, float penWidth = 1.7f)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(color, penWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int x = bounds.X + 2;
                int y = bounds.Y + 3;
                int w = bounds.Width - 4;
                int h = bounds.Height - 5;

                // Body
                using (GraphicsPath path = CreateRoundedRectPath(new Rectangle(x, y, w, h), 3))
                {
                    g.DrawPath(pen, path);
                }

                // Header line
                int headerY = y + (int)(h * 0.32f);
                g.DrawLine(pen, x, headerY, x + w, headerY);

                // Binder tabs
                int ring1X = x + (int)(w * 0.28f);
                int ring2X = x + (int)(w * 0.72f);
                g.DrawLine(pen, ring1X, y - 2, ring1X, y + 2);
                g.DrawLine(pen, ring2X, y - 2, ring2X, y + 2);

                // Grid dots
                using (SolidBrush brush = new SolidBrush(color))
                {
                    int dotSize = 2;
                    int row1Y = headerY + 4;
                    int row2Y = row1Y + 4;

                    g.FillRectangle(brush, x + (int)(w * 0.25f), row1Y, dotSize, dotSize);
                    g.FillRectangle(brush, x + (int)(w * 0.50f), row1Y, dotSize, dotSize);
                    g.FillRectangle(brush, x + (int)(w * 0.75f), row1Y, dotSize, dotSize);

                    g.FillRectangle(brush, x + (int)(w * 0.25f), row2Y, dotSize, dotSize);
                    g.FillRectangle(brush, x + (int)(w * 0.50f), row2Y, dotSize, dotSize);
                    g.FillRectangle(brush, x + (int)(w * 0.75f), row2Y, dotSize, dotSize);
                }
            }
        }

        public static GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            Rectangle arc = new Rectangle(rect.X, rect.Y, diameter, diameter);

            // Top-left
            path.AddArc(arc, 180, 90);

            // Top-right
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom-right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom-left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}

namespace CRM_App.Controls
{
    // Alias to support CRM_App.Controls.VectorIcons seamlessly
    public static class VectorIcons
    {
        public static void DrawUserCircle(Graphics g, Rectangle bounds, Color circleColor, Color iconColor)
        {
            CRM_App.VectorIcons.DrawUserCircle(g, bounds, circleColor, iconColor);
        }

        public static void DrawUserOutline(Graphics g, Rectangle bounds, Color color, float penWidth = 1.8f)
        {
            CRM_App.VectorIcons.DrawUserOutline(g, bounds, color, penWidth);
        }

        public static void DrawLockOutline(Graphics g, Rectangle bounds, Color color, float penWidth = 1.8f)
        {
            CRM_App.VectorIcons.DrawLockOutline(g, bounds, color, penWidth);
        }

        public static void DrawEyeIcon(Graphics g, Rectangle bounds, Color color, bool strikeThrough, float penWidth = 1.6f)
        {
            CRM_App.VectorIcons.DrawEyeIcon(g, bounds, color, strikeThrough, penWidth);
        }

        public static void DrawLoginArrow(Graphics g, Rectangle bounds, Color color, float penWidth = 2.0f)
        {
            CRM_App.VectorIcons.DrawLoginArrow(g, bounds, color, penWidth);
        }

        public static void DrawDatabaseIcon(Graphics g, Rectangle bounds, Color color, float penWidth = 1.7f)
        {
            CRM_App.VectorIcons.DrawDatabaseIcon(g, bounds, color, penWidth);
        }

        public static void DrawCalendarIcon(Graphics g, Rectangle bounds, Color color, float penWidth = 1.7f)
        {
            CRM_App.VectorIcons.DrawCalendarIcon(g, bounds, color, penWidth);
        }

        public static GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            return CRM_App.VectorIcons.CreateRoundedRectPath(rect, radius);
        }
    }
}
