using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCTraining_German3
{
    public partial class CertForm : Form
    {
        int stuid;
        WSCTraining_German2Entities ent = new WSCTraining_German2Entities();
        public CertForm(int stuid)
        {
            InitializeComponent();
            this.stuid = stuid;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        Bitmap bm;

        private void CertForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(226, 35, 26);

            var buttonlist = this.Controls.OfType<Button>().ToList();
            var labellist = this.Controls.OfType<Label>().ToList();

            foreach (var item in buttonlist)
            {
                item.BackColor = Color.FromArgb(0, 92, 185);
            }

            foreach (var item in labellist)
            {
                item.BackColor = Color.White;

                if (item.Name == "label2")
                {
                    item.BackColor = Color.White;
                }
                else
                {
                    item.BackColor = Color.FromArgb(255, 139, 0);

                }
            }


            var whichstu = ent.Students.FirstOrDefault(x => x.ID == stuid);

            DateTime graddate;
            if (whichstu.Class.Name.Contains("09"))
            {
                graddate = whichstu.StartDate.Value.AddYears(2);
            }
            else
            {
                graddate = whichstu.StartDate.Value.AddYears(2).AddMonths(6);

            }

            label3.Text = whichstu.Class.Name;

            label4.Text = whichstu.FirstName + " " + whichstu.LastName;

            label2.Text = "The student " + whichstu.FirstName + " " + whichstu.LastName + " started the education in the " + whichstu.Class.Name + " in the WorldSkills Germany School of IT Skills Neubrandenburg \n\n\n" +
                "on " + whichstu.StartDate.Value.ToShortDateString() + "\n\n\n" +
                "and will finish his apprenticeship here probably\n\n\n" +
                "on " + graddate.ToShortDateString() + "\n\n\n" +
                "\n\n\n\n" +
                "_____________   ______________________\n" +
                "Class Teacher   Director of the School\n\n" +
                "Neubrandenburg, " + DateTime.Now.ToShortDateString();

            pictureBox1.BringToFront();

            bm = new Bitmap(this.Width, this.Height);

            this.FormBorderStyle = FormBorderStyle.None;
            this.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

            bm.Save("temp.jpg");

            printDocument1.PrintPage += PrintDocument1_PrintPage;
            printDocument1.Print();

            pictureBox1.SendToBack();

            Hide();
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bm = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

            e.Graphics.DrawImage((Image)bm, 0, 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            pictureBox1.BringToFront();

            bm = new Bitmap(this.Width, this.Height);

            this.FormBorderStyle = FormBorderStyle.None;
            this.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

            bm.Save("temp.jpg");

            pictureBox1.SendToBack();
        }
    }
}
