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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ADO.NET_LAB2
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Warehouse;Integrated Security=True";
        private DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                dt = new DataTable();
                string queryStr = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(queryStr, connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int line = 0;
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.GetName(i));
                            }
                        }
                        line++;
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dt.Rows.Add(row);
                    }
                    dataGridViewDB.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection?.Close();
                    reader?.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryStr = $"Insert into Products VALUES(" +
                $"{numericUpDown1.Value}, " +
                    $"{numericUpDown2.Value}, " +
                    $"N'{textBox1.Text}', " +
                    $"{numericUpDown3.Value}, " +
                    $"{numericUpDown4.Value}, " +
                    $"'{dateTimePicker1.Value.ToString("MM-dd-yyyy")}')";
                SqlCommand command = new SqlCommand(queryStr, connection);
                try
                {
                    connection.Open();
                    int count = command.ExecuteNonQuery();
                    MessageBox.Show($"{count} rows affected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
