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
    
    public partial class AddBook : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public AddBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("INSERT INTO library VALUES(" + label4.Text + ", '" + textBox1.Text + "', '"+textBox2.Text+ "', '" + textBox3.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data has been saved successfully");

                GetData();
            }
            catch
            {
                MessageBox.Show("Please fill every textbox!");
            }
        }

        private void AddBook_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM library", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label4.Text = (counter + 1).ToString();
            con.Close();
        }

        public void GetData()
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM library", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label4.Text = (counter + 1).ToString();
            con.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}
