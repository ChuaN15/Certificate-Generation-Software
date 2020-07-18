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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(226, 35, 26);

            var buttonlist = this.Controls.OfType<Button>().ToList();

            foreach (var item in buttonlist)
            {
                item.BackColor = Color.FromArgb(0, 92, 185);
            }


            //var allss = ent.Student_Subject.Where(X => X.StudentID == 26).ToList();

            //for (int i = 0; i < allss.Count; i++)
            //{
            //    if (ent.Class_Subject.ToList().FirstOrDefault(x => x.SubjectID == allss[i].ID) == null)
            //    {
            //        Class_Subject cs = new Class_Subject();
            //        cs.ClassID = 2;
            //        cs.SubjectID = allss[i].SubjectID;
            //        ent.Class_Subject.Add(cs);
            //        ent.SaveChanges();
            //    }

            //}
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wSCTraining_German2DataSet.Class' table. You can move, or remove it, as needed.
            this.classTableAdapter.Fill(this.wSCTraining_German2DataSet.Class);

            loadData();
        }

        WSCTraining_German2Entities ent = new WSCTraining_German2Entities();

        public void loadData()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Name", "Name");

            var allsub = ent.Class_Subject.ToList().Where(x => x.ClassID == int.Parse(comboBox1.SelectedValue.ToString())).ToList();

            for (int i = 0; i < allsub.Count; i++)
            {
                var whichsub = ent.Subjects.ToList().FirstOrDefault(x => x.ID == allsub[i].SubjectID);
                dataGridView1.Columns.Add(whichsub.Name,whichsub.Name);
            }

            var allstu = ent.Students.ToList().Where(X => X.ClassID == int.Parse(comboBox1.SelectedValue.ToString())).ToList();
            allstu = allstu.OrderBy(X => X.FirstName).ToList();

            int count = 0;
            foreach (var item in allstu)
            {
                var allss = ent.Student_Subject.ToList().Where(x => x.StudentID == item.ID).ToList();


                dataGridView1.Rows.Add(item.FirstName + " " + item.LastName);

                for (int i = 0; i < allsub.Count; i++)
                {
                    double count2 = 0;
                    double total = 0;
                    string[] allval = allss.FirstOrDefault(x => x.SubjectID == allsub[i].SubjectID).Grade.Split('|');

                    for (int j = 0; j < allval.Count(); j++)
                    {
                        try
                        {
                            total += double.Parse(allval[j]);
                            count2++;
                        }
                        catch (Exception)
                        {
                            
                        }
                        
                    }

                    if(count2 > 0)
                    {
                        dataGridView1.Rows[count].Cells[i + 1].Value = Math.Round(total / count2).ToString();
                    }
                    else
                    {
                        dataGridView1.Rows[count].Cells[i + 1].Value = "0";

                    }
                }

                count++;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void subjectViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
}
