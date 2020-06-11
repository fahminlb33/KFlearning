using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace KFmaintenance.Services
{
    public static class WindowsHelpers
    {
        public static void Shutdown()
        {
            Process.Start("shutdown -s -t 0");
        }

        public static void ApplyStyle(this DataGridView dgv)
        {
            var font = new Font("Segoe UI", 9.87F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // globals
            dgv.Font = font;
            dgv.BackColor = Color.FromArgb(45, 47, 49);
            dgv.ForeColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // column header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 50;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // rows
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 25;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // cell style
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = font,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 47, 49),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
                
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(60, 70, 73),
                SelectionBackColor = Color.FromArgb(35, 168, 109),
                SelectionForeColor = Color.White
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.SlateGray
            };
        }
    }
}
