using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KFlearning.Control
{
    public class FlatListBox
    {
        public static void DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            e.DrawFocusRectangle();

            var listBox = (ListBox) sender;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle bgRect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

            SolidBrush stateBrush = e.State.HasFlag(DrawItemState.Selected)
                ? new SolidBrush(Color.FromArgb(35, 168, 109))
                : new SolidBrush(Color.FromArgb(51, 53, 55));

            e.Graphics.FillRectangle(stateBrush, bgRect);

            e.Graphics.DrawString(" " + listBox.Items[e.Index], new Font("Segoe UI", 8), Brushes.White, e.Bounds.X,
                e.Bounds.Y + 2);
            e.Graphics.Dispose();
        }
    }
}