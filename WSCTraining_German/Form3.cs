using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCTraining_German
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            
        }

        WSCTraining_German2Entities ent = new WSCTraining_German2Entities();

        public void loadData()
        {
            dataGridView1.Rows.Clear();

            var allsub = ent.Student_Subject.ToList().Where(x => x.SubjectID == int.Parse(comboBox1.SelectedValue.ToString())).ToList();
            

            int count = 0;
            foreach (var item in allsub)
            {
                var whichstu = ent.Students.ToList().FirstOrDefault(x => x.ID == item.StudentID);


                dataGridView1.Rows.Add(whichstu.FirstName + " " + whichstu.LastName);

                    double count2 = 0;
                    double total = 0;
                    string[] allval = item.Grade.Split('|');

                    for (int j = 0; j < allval.Count(); j++)
                    {
                        try
                        {
                        total += double.Parse(allval[j]);
                        count2++;
                        dataGridView1.Rows[count].Cells[j + 1].Value = allval[j];

                        }
                        catch (Exception)
                        {

                        }

                    if (count2 > 0)
                    {
                        dataGridView1.Rows[count].Cells[9].Value = (total / count2).ToString();
                        dataGridView1.Rows[count].Cells[10].Value = Math.Round(total / count2).ToString();
                    }
                    else
                    {
                        dataGridView1.Rows[count].Cells[9].Value = "0";
                        dataGridView1.Rows[count].Cells[10].Value = "0";

                    }
                }

                count++;
            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wSCTraining_German2DataSet.Subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.wSCTraining_German2DataSet.Subject);

            dataGridView1.Columns.Add("Name", "Name");
            for (int i = 0; i < 8; i++)
            {
                dataGridView1.Columns.Add("Mark " + i.ToString(), "Mark " + i.ToString());
            }

            dataGridView1.Columns.Add("Average", "Average");
            dataGridView1.Columns.Add("Result", "Result");

            loadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
