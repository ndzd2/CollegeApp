using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollegeApp
{
    public partial class ListStudents : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public ListStudents()
        {
            InitializeComponent();
        }

        private void ListStudents_Load(object sender, EventArgs e)
        {
            listView1.Visible = false;
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT attendence.subject_id, subject.name FROM attendence INNER JOIN subject ON attendence.subject_id = subject.id", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(1));
            }
            
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT attendence.date FROM attendence INNER JOIN subject ON attendence.subject_id = subject.id WHERE subject.name = '"+ comboBox1.Text +"'", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }

            con.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT attendence.attendence_no FROM attendence INNER JOIN subject ON attendence.subject_id = subject.id WHERE subject.name = '"+ comboBox1.Text +"' AND attendence.date = '"+ comboBox2.Text +"'", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox3.Items.Add(reader.GetInt32(0));
            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Visible = true;
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT attendence.student_id, student.name FROM attendence INNER JOIN student ON attendence.student_id = student.id WHERE attendence.attendence_no = "+ comboBox3.Text +" ORDER BY student_id", con);
                reader = cmd.ExecuteReader();

                listView1.Items.Add("Attendence number: " + comboBox3.Text + "\t\t");

                while (reader.Read())
                {
                    listView1.Items.Add("ID = " + reader.GetInt32(0) + ", name = " + reader.GetString(1));
                }

                con.Close();
                comboBox3.Text = "";
            } catch
            {
                MessageBox.Show("Please don't try to ruin our app!");
            }
        }
    }
}
