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
    public partial class TakeClass : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public List<Students> StudentsList { get; set; }

        public TakeClass()
        {
            
            InitializeComponent();
        }

        private void TakeClass_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT employees.name FROM subject INNER JOIN assign ON subject.id = assign.subject_id INNER JOIN employees ON assign.teacher_id = employees.id ", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            con.Close();
            DateTime date = DateTime.Today;
            label5.Text = date.Day + "/" + date.Month + "/" + date.Year;
            dataGridView2.Visible = false;
            button2.Visible = false;
        }

        public void GetData()
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            
            DateTime date = DateTime.Today;
            label5.Text = date.Day + "/" + date.Month + "/" + date.Year;

            comboBox3.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT student.semester_no FROM subject INNER JOIN student ON subject.class_id = student.class_id WHERE subject.name = '" + comboBox2.Text + "'", con);
            reader = cmd.ExecuteReader();
            comboBox3.Text = "";
            if (comboBox3.Items.Count > 0)
            {
                comboBox3.Items.Clear();
            }
            while (reader.Read())
            {
                comboBox3.Items.Add(reader.GetInt32(0));
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT subject.name FROM subject INNER JOIN assign ON subject.id = assign.subject_id INNER JOIN employees ON assign.teacher_id = employees.id WHERE employees.name = '" + comboBox1.Text+"'", con);
            reader = cmd.ExecuteReader();
            comboBox2.Text = "";
            comboBox3.Text = "";
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.Items.Clear();
            }
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }
            con.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(@"data source = DESKTOP-OP572JJ; initial catalog = College; integrated security = true");
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT student.id, student.name FROM student INNER JOIN subject ON student.class_id = subject.class_id INNER JOIN assign ON subject.id = assign.subject_id INNER JOIN employees ON assign.teacher_id = employees.id WHERE student.semester_no = " + comboBox3.Text + " AND subject.name = '" + comboBox2.Text + "'", con);
            reader = cmd.ExecuteReader();
            var list = new List<Students>();

            while (reader.Read())
            {
                list.Add(new Students()
                {
                    studentId = reader.GetInt32(0),
                    studentName = reader.GetString(1)
                });
            }
            con.Close();

            StudentsList = list;

            var studensList = this.StudentsList;

            dataGridView2.DataSource = studensList;

            dataGridView2.Visible = true;
            button2.Visible = true;
            button1.Location = new Point(629, 361);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int attendenceNo = new Random().Next(1, 99999);
            
                con.Open();
                cmd = new SqlCommand("SELECT * FROM attendence", con);
                reader = cmd.ExecuteReader();
                int idAttendence = 1;
                while (reader.Read())
                {
                    idAttendence++;

                    while (attendenceNo == reader.GetInt32(5))
                    {
                        attendenceNo = new Random().Next(1, 99999);
                    }
                }
                con.Close();

                con.Open();
                cmd = new SqlCommand("SELECT * FROM subject INNER JOIN assign ON subject.id = assign.subject_id INNER JOIN employees ON assign.teacher_id = employees.id WHERE subject.name = '" + comboBox2.Text + "'", con);
                reader = cmd.ExecuteReader();
                int idSubject = 0;
                while (reader.Read())
                {
                    idSubject = reader.GetInt32(4);
                }
                con.Close();

                con.Open();
                cmd = new SqlCommand("SELECT * FROM subject INNER JOIN assign ON subject.id = assign.subject_id INNER JOIN employees ON assign.teacher_id = employees.id WHERE employees.name = '" + comboBox1.Text + "'", con);
                reader = cmd.ExecuteReader();
                int idTeacher = 0;
                while (reader.Read())
                {
                    idTeacher = reader.GetInt32(5);
                }
                con.Close();

                int selectedCellCount = dataGridView2.GetCellCount(DataGridViewElementStates.Selected);

                for (int i = 0; i < selectedCellCount; i++)
                {
                    if (selectedCellCount > 0)
                    {
                        try
                        {
                            int rowIndex = Convert.ToInt32(dataGridView2.SelectedCells[i].RowIndex);
                            int columnIndex = Convert.ToInt32(dataGridView2.SelectedCells[i].ColumnIndex);
                            int idStudent = 0;
                            if (dataGridView2.Rows[rowIndex].Cells[columnIndex].Value != null)
                            {
                                idStudent = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells[columnIndex].Value);
                            }

                            con.Open();
                            cmd = new SqlCommand("INSERT INTO attendence VALUES(" + idAttendence + "," + idStudent + ", '" + label5.Text + "', " + idSubject + ", " + idTeacher + ", " + attendenceNo + ")", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            idAttendence++;

                            MessageBox.Show("Student nr " + idStudent + " assigned!");
                        }
                        catch
                        {
                            MessageBox.Show("Select only IDs!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select students!");
                    }
                }

                GetData();
            }
            catch
            {
                MessageBox.Show("Please select every avaliable option on the page");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button1.Location = new Point(310, 359);
            button2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListStudents listStudents = new ListStudents();
            listStudents.Show();
        }
    }
}
