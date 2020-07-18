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

namespace WSCTraining_German
{
    public partial class Form1 : Form
    {
        WSCTraining_German2Entities ent = new WSCTraining_German2Entities();
        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(226, 35, 26);

            var buttonlist = this.Controls.OfType<Button>().ToList();

            foreach (var item in buttonlist)
            {
                item.BackColor = Color.FromArgb(0, 92, 185);
            }

            string[] strings = "Software_Development,Mathematics_for_Programmers,Practical_Programming,Operating_Systems,Electronics,PC-Technics,Networking,IT_Security,English,Economy".Split(',');

            //foreach (var item in strings)
            //{
            //    Subject sub = new Subject();
            //    sub.Name = item;
            //    ent.Subjects.Add(sub);
            //    ent.SaveChanges();

            //    Class_Subject cs = new Class_Subject();
            //    cs.ClassID = 1;
            //    cs.SubjectID = sub.ID;
            //    ent.Class_Subject.Add(cs);
            //    ent.SaveChanges();
            //}

            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "CSV | *.csv";

            //if(ofd.ShowDialog() == DialogResult.OK)
            //{
            //    var allcsv = File.ReadAllLines(ofd.FileName);
            //    var csvwithcols = allcsv.Select(x => x.Split(','));

            //    int count = 0;
            //    foreach (var item in csvwithcols)
            //    {
            //        if (count >= 6)
            //        {
            //            Student newstu = new Student();
            //            newstu.ClassID = 2;
            //            newstu.FirstName = item[0].ToString();
            //            newstu.LastName = item[1].ToString();
            //            newstu.StartDate = DateTime.Parse(item[2].ToString());
            //            newstu.Password = item[3].ToString();
            //            ent.Students.Add(newstu);
            //            ent.SaveChanges();


            //                for (int i = 4; i < item.Length; i++)
            //                {
            //                    try
            //                    {
            //                        Student_Subject ss = new Student_Subject();
            //                        ss.Grade = item[i].ToString();
            //                        ss.StudentID = newstu.ID;
            //                        ss.SubjectID = ent.Subjects.ToList().FirstOrDefault(x => x.Name == strings[i - 4]).ID;
            //                        ent.Student_Subject.Add(ss);
            //                        ent.SaveChanges();
            //                    }
            //                    catch (Exception)
            //                    {
            //                    Subject sub = new Subject();
            //                    sub.Name = strings[i - 4];
            //                    ent.Subjects.Add(sub);
            //                    ent.SaveChanges();

            //                    Class_Subject cs = new Class_Subject();
            //                    cs.ClassID = 2;
            //                    cs.SubjectID = sub.ID;
            //                    ent.Class_Subject.Add(cs);
            //                    ent.SaveChanges();

            //                    Student_Subject ss = new Student_Subject();
            //                    ss.Grade = item[i].ToString();
            //                    ss.StudentID = newstu.ID;
            //                    ss.SubjectID = sub.ID;
            //                    ent.Student_Subject.Add(ss);
            //                    ent.SaveChanges();
            //                }

            //            }
                       
            //        }

            //        count++;
            //    }
            //}
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var whichstu = ent.Students.FirstOrDefault(X => X.FirstName == textBox1.Text && X.Password == textBox2.Text);

            if (whichstu == null)
            {
                MessageBox.Show("Please enter valid name and password to login");
            }
            else
            {
                new Form2().Show();
                Hide();
            }
        }
    }
}
