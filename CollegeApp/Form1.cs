using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollegeApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent addstudent = new AddStudent();
            addstudent.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployee addemployee = new AddEmployee();
            addemployee.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddClasses addclasses = new AddClasses();
            addclasses.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Library library = new Library();
            library.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AssignSubjects assignSubjects = new AssignSubjects();
            assignSubjects.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddSubject addSubject = new AddSubject();
            addSubject.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TakeClass takeClass = new TakeClass();
            takeClass.Show();
        }
    }
}
