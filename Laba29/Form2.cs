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

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private Form1 _mainForm;

        public Form2(Form1 mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text; // поле для ввода логина
            string password = textBox2.Text; // поле для ввода пароля
            UserManager userManager = new UserManager(); // создаем объект класса для проведения дальнейшей авторизации
            bool res = userManager.AuthenticateUser(login, password);
            if (res == true)  // если пользователь есть в БД
            {
                MessageBox.Show($"Добрый день, {login} ");
                Button savebtn = _mainForm.Getsavebutton();
                Button searchbutton = _mainForm.Getsearchbutton();
                //делаем кнопки Найти и Сохранить с основной формы активными
                savebtn.Enabled = true;
                searchbutton.Enabled = true;
                this.Close();

            }
            else // если пользователя нет в БД
            {
                MessageBox.Show("Введены неверные данные. Попробуйте еще раз");
                //button2_Click(sender, e);

            }

        }
    }
}

public class UserManager
{
    private string connectionString = "Data Source=localhost;Persist Security Info=True;User ID=sa;Password=S3cur3P@ssW0rd!;Encrypt=True;TrustServerCertificate=True";

    public bool AuthenticateUser(string username, string password)
    {
        //SQL-запрос для проверки учетных данных
        string sqlQuery = "SELECT COUNT(*) FROM Users WHERE username = @username AND password = @password";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Параметризация запроса
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                // Проверка существования пользователя с заданными учетными данными
                int userCount = (int)command.ExecuteScalar();

                // Если userCount больше нуля, значит, пользователь существует и пароль верен
                return userCount > 0;
            }
        }
    }
}