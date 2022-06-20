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
    public partial class TakeReturnBook : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        SqlConnection con1;
        SqlCommand cmd1;
        SqlDataReader reader1;
        public TakeReturnBook()
        {
            InitializeComponent();
        }

        private void TakeReturnBook_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM issue_return_books", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label5.Text = (counter + 1).ToString();
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM student", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetInt32(0));
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int status;
                if (comboBox2.Text == "Issue")
                    status = 1;         //status 0 = it was returned, status 1 = it is issued, status 2 = it was issued before
                else
                    status = 0;
                cmd = new SqlCommand("SELECT * FROM library WHERE title = '"+ comboBox3.Text +"'", con);
                reader = cmd.ExecuteReader();
                int bookId=0;
                int bookQuantity = 0;
                while (reader.Read())
                {
                    bookId = reader.GetInt32(0);
                    bookQuantity = reader.GetInt32(3);
                }
                con.Close();
                con.Open();
                cmd = new SqlCommand("INSERT INTO issue_return_books VALUES(" + label5.Text + ", " + comboBox1.Text + ", " + status + ", " + bookId + ")", con);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                if (status == 1)
                {
                    bookQuantity = bookQuantity - 1;
                    cmd = new SqlCommand("UPDATE library SET book_quantity = "+ bookQuantity +" WHERE title = '"+ comboBox3.Text +"'", con);
                    cmd.ExecuteNonQuery();
                } else if (status == 0)
                {
                    bookQuantity = bookQuantity + 1;
                    cmd = new SqlCommand("UPDATE library SET book_quantity = " + bookQuantity + " WHERE title = '" + comboBox3.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("UPDATE issue_return_books SET status = 2 WHERE student_id = "+ comboBox1.Text +" AND status = 1", con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Data has been saved successfully");

                GetData();
            }
            catch
            {
                MessageBox.Show("Please select every possible option on the page");
            }
        }
        public void GetData()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM issue_return_books", con);
            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            label5.Text = (counter + 1).ToString();
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM student", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetInt32(0));
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Visible = true;
            int count = 999;
            try
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox2.Text = "";
                comboBox3.Text = "";
                con1 = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
                con.Open();
                cmd = new SqlCommand("SELECT * FROM issue_return_books WHERE status = 1 AND student_id = " + comboBox1.Text, con);
                reader = cmd.ExecuteReader();

                if (reader.HasRows == false)
                {
                    count = 99;
                }

                while (reader.Read())
                {
                    if (reader.GetInt32(1) == Convert.ToInt32(comboBox1.Text))
                    {
                        comboBox2.Items.Add("Return");
                        con1.Open();
                        cmd1 = new SqlCommand("SELECT * FROM issue_return_books INNER JOIN library ON issue_return_books.book_id = library.id WHERE status = 1 AND student_id = " + comboBox1.Text, con1);
                        reader1 = cmd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            comboBox3.Items.Add(reader1.GetString(5));
                        }
                        con1.Close();
                        count  = comboBox2.Items.Count;
                    }
                    
                }
                con.Close();
                if (count == 99)
                {
                    comboBox2.Items.Add("Issue");
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM library WHERE book_quantity > 0", con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader.GetString(1));
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Please don't try to crash our app!");
            }
        }
    }
}
