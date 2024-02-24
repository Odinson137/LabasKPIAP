using Laba29.DataSet1TableAdapters;
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

namespace Laba29
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Drinks". При необходимости она может быть перемещена или удалена.
            this.drinksTableAdapter.Fill(this.dataSet1.Drinks);

        }        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.drinksTableAdapter.Update(this.dataSet1.Drinks);
                dataSet1.Drinks.Clear();
                this.drinksTableAdapter.Fill(this.dataSet1.Drinks);
                MessageBox.Show("Изменения сохранены успешно");
            } catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = textBox1.Text.Trim();
                dataSet1.Clear();
                string sqlQuery = "SELECT * FROM Drinks WHERE name LIKE @search"; 
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Persist Security Info=True;User ID=sa;Password=S3cur3P@ssW0rd!;Encrypt=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet1, "Drinks");
                        }
                    }
                }

                dataGridView1.DataSource = dataSet1.Tables["Drinks"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при выполнении запроса: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = textBox1.Text.Trim();
                dataSet1.Clear();
                string sqlQuery = "SELECT * FROM Drinks"; 
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Persist Security Info=True;User ID=sa;Password=S3cur3P@ssW0rd!;Encrypt=True;TrustServerCertificate=True"))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@search", "%" + searchQuery + "%"); 
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet1, "Drinks");
                        }
                    }
                }

                dataGridView1.DataSource = dataSet1.Tables["Drinks"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}