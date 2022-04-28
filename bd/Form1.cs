using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace bd
{
    public partial class Form1 : Form
    {
        DataBase database = new DataBase(); 
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Авторизация form1 = new Авторизация();
            this.Hide();
            form1.ShowDialog();
            this.Show();
        }
        private void sign_up_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            pictureBox3.Visible = false;
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataBase dataBase = new DataBase();
            var login = textBox1.Text;
            var password = textBox2.Text;



            dataBase.OpenConnection();
            string querystring = $"insert into register(login_user, password_user) values ('{login}', '{password}')";
            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());
            
            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                Авторизация form2 = new Авторизация();
                this.Hide();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            database.CloseConnection();

        }
        private Boolean checkuser()
        {
            var loginuser = textBox1.Text;
            var passuser = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id, login_user, password_user from register where login_user = '{loginuser}' and password_user = '{passuser}'";

            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
        }
    }

    
}