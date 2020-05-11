// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : FlatTabControl.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KFlearning.Core.Forms
{
    public class FlatTabControl : TabControl
    {
        private StringFormat CenterSF = new StringFormat
            {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

        private Color BGColor = Color.FromArgb(60, 70, 73);
        private Color BaseColor = Color.FromArgb(45, 47, 49);
        private Color ActiveColor = Color.FromArgb(35, 168, 109);

        public FlatTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = BGColor;

            Font = new Font("Segoe UI", 10);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(120, 40);
        }

        #region Overrides

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Alignment = TabAlignment.Top;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var bitmap = new Bitmap(Width, Height))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.Clear(BaseColor);

                try
                {
                    SelectedTab.BackColor = BGColor;
                }
                catch
                {
                    // ignore
                }

                for (int i = 0; i < TabCount; i++)
                {
                    var tabRect = GetTabRect(i);
                    var baseRect = new Rectangle(new Point(tabRect.Location.X + 2, tabRect.Location.Y), tabRect.Size);
                    var baseSize = new Rectangle(baseRect.Location, baseRect.Size);

                    g.FillRectangle(new SolidBrush(BaseColor), baseSize);
                    if (i == SelectedIndex)
                    {
                        g.FillRectangle(new SolidBrush(ActiveColor), baseSize);
                    }

                    g.DrawString(TabPages[i].Text, Font, Brushes.White, baseSize, CenterSF);
                }

                base.OnPaint(e);
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            }
        }

        #endregion
    }
}