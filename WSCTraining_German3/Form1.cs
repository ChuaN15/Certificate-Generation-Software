using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCTraining_German3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(226, 35, 26);

            var buttonlist = this.Controls.OfType<System.Windows.Forms.Button>().ToList();

            foreach (var item in buttonlist)
            {
                //item.BackColor = Color.FromArgb(0, 92, 185);
            }

            loadData();

            var alldata = File.ReadAllLines(@"C:\Users\chuan\source\repos\WSCTraining_German\WSCTraining_German3\bin\Debug\strings.xml");

            string data = "";
            for (int i = 0; i < alldata.Count(); i++)
            {
                data += alldata;
            }

            MessageBox.Show(data);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        WSCTraining_German2Entities ent = new WSCTraining_German2Entities();

        public void loadData()
        {
            dataGridView1.Rows.Clear();

            var allstudent = ent.Students.ToList();

            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                allstudent = allstudent.ToList().Where(x => x.FirstName.ToUpper().Contains(textBox1.Text.ToUpper()) || x.LastName.ToUpper().Contains(textBox1.Text.ToUpper())).ToList();

            }

            foreach (var item in allstudent)
            {
                dataGridView1.Rows.Add(item.ID.ToString(),item.FirstName, item.LastName, item.Class.Name, item.StartDate.Value.ToShortDateString(), DateTime.Now.ToShortDateString());
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wSCTraining_German2DataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.wSCTraining_German2DataSet.Student);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = "";

            if(dataGridView1.SelectedRows.Count > 5 || dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Please select 1-5students");
                return;
            }

            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                string skill = dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[3].Value.ToString();
                DateTime dt = DateTime.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[4].Value.ToString());
                DateTime now = DateTime.Now;

                TimeSpan ts = now.Subtract(dt);

                double year = 365 * 1;
                double year2 = 365 * 1.5;

                if (skill.Contains("09"))
                    {
                    if(ts.TotalDays <= year)
                    {
                        MessageBox.Show("IT Software Solutions for Business required 2years to graduate.");
                        return;
                    }
                }
                else
                {
                    if (ts.TotalDays <= year2)
                    {
                        MessageBox.Show("IT Network Systems Administration required 2.5years to graduate.");
                        return;
                    }
                }
                
                data += dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[0].Value.ToString() + ",";
            }

            string[] strings = data.Split(',');
            foreach (var item in strings)
            {
                try
                {
                    new CertForm(int.Parse(item.ToString())).Show();

                }
                catch (Exception)
                {
                    
                }
            }
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.SelectedCells[dataGridView1.SelectedCells.Count-1].RowIndex].Selected = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data = "";

            if (dataGridView1.SelectedRows.Count > 5 || dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Please select 1-5students");
                return;
            }

            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                string skill = dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[3].Value.ToString();
                DateTime dt = DateTime.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[4].Value.ToString());
                DateTime now = DateTime.Now;

                TimeSpan ts = now.Subtract(dt);

                double year = 365 * 1;
                double year2 = 365 * 1.5;

                if (skill.Contains("09"))
                {
                    if (ts.TotalDays <= year)
                    {
                        MessageBox.Show("IT Software Solutions for Business required 2years to graduate.");
                        return;
                    }
                }
                else
                {
                    if (ts.TotalDays <= year2)
                    {
                        MessageBox.Show("IT Network Systems Administration required 2.5years to graduate.");
                        return;
                    }
                }

                data += dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[0].Value.ToString() + ",";
            }

            FolderBrowserDialog ofd = new FolderBrowserDialog();

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc = new Document();
            Microsoft.Office.Interop.Word.Range docRange = doc.Range();

            string names = "";
            string[] strings = data.Split(',');
            foreach (var item in strings)
            {
                if(!string.IsNullOrEmpty(item))
                {
                    var whichuser = ent.Students.ToList().FirstOrDefault(x => x.ID.ToString() == item);
                    names += whichuser.FirstName + "_";

                    CertForm form = new CertForm(int.Parse(item.ToString()));
                    form.Show();
                    form.Hide();

                    //doc.Content.Text = "123";
                    
                    InlineShape shape = docRange.InlineShapes.AddPicture(@"C:\Users\chuan\source\repos\WSCTraining_German\WSCTraining_German3\bin\Debug\temp.jpg");
                    //shape.Range.InsertBefore(item);
                }
                
            }
            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string date = DateTime.Now.ToShortDateString();

                string name = ofd.SelectedPath + "\\" + names + ".docx";
                string name2 = ofd.SelectedPath + "\\" + names + ".pdf";
                doc.SaveAs2(name.Replace(" ","").Replace("|",""));
                doc.SaveAs2(name2.Replace(" ", "").Replace("|", ""), WdExportFormat.wdExportFormatPDF);
                
                doc.Close();
                app.Quit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = app.Workbooks.Add(Type.Missing);
            Worksheet worksheet  = workbook.Worksheets[1];
            var missValue = Type.Missing;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "EXCEL | *.xlsx;*.xls";

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    worksheet.Cells[i + 1, 1] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                }

                workbook.SaveAs(sfd.FileName);
                workbook.Close(true, missValue, missValue);
                app.Quit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "EXCEL | *.xlsx;*.xls";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = app.Workbooks.Open(ofd.FileName);
                Worksheet worksheet = workbook.Worksheets[1];
                Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

                string data = "";
                for (int i = 0; i < range.Rows.Count; i++)
                {
                    data += range.Cells[i + 1, 1].Value2.ToString() + ",";
                }

                MessageBox.Show(data);
            }
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var misValue = Type.Missing;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = app.Workbooks.Add(misValue);
            Worksheet worksheet = workbook.Worksheets[1];

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                worksheet.UsedRange.Cells[i + 1, 1] = dataGridView1.Rows[i].Cells[1].Value.ToString();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "EXCEL | *.xlsx";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName);
                workbook.Close();
                app.Quit();

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXCEL | *.xlsx";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                var misValue = Type.Missing;
                Workbook workbook = app.Workbooks.Open(openFileDialog.FileName);
                 Worksheet worksheet = workbook.Worksheets[1];
                Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

                string data = "";
                for (int i = 0; i < range.Rows.Count; i++)
                {
                    data += range.Cells[i+1, 1].Value2.ToString();
                }

                MessageBox.Show(data);
            }
            


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(this.Width, this.Height);

            this.FormBorderStyle = FormBorderStyle.None;

            this.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            bm.Save("abc.png");
        }
    }
}
