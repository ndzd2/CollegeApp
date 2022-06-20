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
    public partial class AssignSubjects : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public AssignSubjects()
        {
            InitializeComponent();
        }

        private void AssignSubjects_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM employees WHERE designition = 'Teacher'", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(1));
            }
            con.Close();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM subject", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(2));
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM assign", con);
                reader = cmd.ExecuteReader();
                int id = 1;
                int idSubject = 0, idTeacher = 0;
                while (reader.Read())
                {
                    id++;
                }
                con.Close();
                con.Open();
                cmd = new SqlCommand("SELECT * FROM employees WHERE name = '" + comboBox1.Text + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idTeacher = reader.GetInt32(0);
                }
                con.Close();
                con.Open();
                cmd = new SqlCommand("SELECT * FROM subject WHERE name = '" + comboBox2.Text + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idSubject = reader.GetInt32(0);
                }
                con.Close();
                con.Open();
                cmd = new SqlCommand("INSERT INTO assign VALUES("+ id +", "+ idSubject +", " + idTeacher + ")", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Success!");
    
                GetData();
            }
            catch
            {
                MessageBox.Show("Please choose every possible box in the page");
            }
        }
        public void GetData()
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
    }
}
