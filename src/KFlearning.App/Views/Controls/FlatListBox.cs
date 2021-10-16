using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KFlearning.App.Views.Controls
{
    public class FlatListBox
    {
        public static void DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();
            e.DrawFocusRectangle();

            using var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var bgRect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

            SolidBrush stateBrush = e.State.HasFlag(DrawItemState.Selected)
                ? new SolidBrush(Color.FromArgb(35, 168, 109))
                : new SolidBrush(Color.FromArgb(51, 53, 55));

            g.FillRectangle(stateBrush, bgRect);
            
            var listBox = (ListBox) sender;
            g.DrawString(" " + listBox.Items[e.Index], 
                new Font("Segoe UI", 8), Brushes.White, e.Bounds.X, e.Bounds.Y + 2);
        }
    }
}