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
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this); // Передаем ссылку на Form2
            form2.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();

        }

        public Button Getsavebutton()
        {
            return button1;
        }
        public Button Getsearchbutton()
        {
            return button2;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }
    }
}


public class UserManagerForm3
{
    private string connectionString = "Data Source=localhost;Persist Security Info=True;User ID=sa;Password=S3cur3P@ssW0rd!;Encrypt=True;TrustServerCertificate=True";

    public bool RegisterUser(string username, string password)
    {
        // Проверяем, существует ли пользователь с таким именем
        if (IsUserExists(username))
        {
            // Пользователь с таким именем уже существует
            return false;
        }

        // Если пользователя с таким именем нет, регистрируем нового пользователя
        string sqlQuery = "INSERT INTO Users (username, password) VALUES (@username, @password)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Параметризация запроса
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                // Выполняем запрос
                int rowsAffected = command.ExecuteNonQuery();

                // Если добавление прошло успешно (была добавлена хотя бы одна строка), возвращаем true
                return rowsAffected > 0;
            }
        }
    }

    private bool IsUserExists(string username)
    {
        // Проверяем, существует ли пользователь с таким именем
        string sqlQuery = "SELECT COUNT(*) FROM Users WHERE username = @username";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Параметризация запроса
                command.Parameters.AddWithValue("@username", username);

                // Получаем количество пользователей с заданным именем
                int userCount = (int)command.ExecuteScalar();

                // Если userCount больше нуля, значит, пользователь существует
                return userCount > 0;
            }
        }
    }
}