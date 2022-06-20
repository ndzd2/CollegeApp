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
    public partial class AddSubject : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public AddSubject()
        {
            InitializeComponent();
        }

        private void AddSubject_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM subject", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label4.Text = (counter + 1).ToString();
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
                cmd = new SqlCommand("SELECT * FROM class WHERE name = '"+ comboBox1.Text +"'", con);
                reader = cmd.ExecuteReader();
                int idClass = 0;
                while (reader.Read())
                {
                    idClass = reader.GetInt32(0);
                }
                con.Close();

                con.Open();
                cmd = new SqlCommand("INSERT INTO subject VALUES("+ label4.Text + ", "+ idClass +", '"+ textBox1.Text +"')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data successfully saved");

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
            cmd = new SqlCommand("SELECT * FROM subject", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label4.Text = (counter + 1).ToString();
            con.Close();

            comboBox1.Text = "";
            textBox1.Clear();
        }
    }
}
