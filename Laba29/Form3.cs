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



    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //private void Form4_Load(object sender, EventArgs e)
        //{
        //    textBox1.Focus();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            UserManagerForm3 userManager = new UserManagerForm3();
            bool res = userManager.RegisterUser(login, password);
            if (res == true)
            {
                MessageBox.Show("Вы успешно зарегистрированы. Пожалуйста, пройдите авторизацию");
                this.Close();

            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже зарегистрирован. Введите другое имя пользователя или пройдите авторизацию");
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
