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
    public partial class AddStudent : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;

        public AddStudent()
        {
            InitializeComponent();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM student", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label7.Text = (counter + 1).ToString();
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM class", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(1));
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int classId=0;
                cmd = new SqlCommand("SELECT * FROM class WHERE name = '"+comboBox1.Text+"'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classId = reader.GetInt32(0);
                }
                con.Close();
                double percentage;
                try
                {
                    percentage = Convert.ToDouble(textBox2.Text);
                }
                catch (Exception ex)
                {
                    percentage = 0;
                }
                if (percentage > 40 && percentage <= 100)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO student VALUES(" + label7.Text + ", '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', " + classId + ", "+ comboBox2.Text +")", con);
                    cmd.ExecuteNonQuery();
                   con.Close();
                    MessageBox.Show("Data has been saved successfully");
                }
                else
                    MessageBox.Show("You are not allowed to study in our college or you typed something else than percentage!");

                GetData();
            }
            catch
            {
                MessageBox.Show("Please fill every textbox!");
            }
        }
        public void GetData()
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM student", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label7.Text = (counter + 1).ToString();
            con.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
    }
}
