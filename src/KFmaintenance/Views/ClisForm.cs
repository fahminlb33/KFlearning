using ExcelDataReader;
using KFlearning.Core.CLIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KFmaintenance.Views
{
    public partial class ClisForm : Form
    {
        private readonly BindingList<StudentRecord> _records = new BindingList<StudentRecord>();
        public ClisForm()
        {
            InitializeComponent();

            dataGridView1.DataSource = _records;
        }
        
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open("D:\\test.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    _records.Clear();

                    reader.Read(); // read header
                    while (reader.Read())
                    {
                        _records.Add(new StudentRecord
                        {
                            Npm = reader.GetString(0),
                            Name = reader.GetString(1),
                            Score = reader.GetDouble(2)
                        });
                    }
                }
            }
        }
    }
}
