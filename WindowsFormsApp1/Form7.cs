using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            comboBox1.KeyPress += (sender, e) => e.Handled = true;
        }
        public static string BD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path.Combine(Application.StartupPath, "test.mdb;");
        private OleDbConnection con;

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 ss = new Form6();
            ss.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
            con.Open();
            string queryString = "INSERT INTO Пользователь ([Пользователь], [Логин], [Пароль]) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
            OleDbCommand command = new OleDbCommand(queryString, con);
            command.ExecuteNonQuery();
            this.Refresh();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM Пользователь", con);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

           

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;

            this.Refresh();
            string tabl2 = "SELECT КодПользователя FROM Пользователь";
            OleDbCommand command2 = new OleDbCommand(tabl2, con);
            OleDbDataReader reader2 = command2.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader2.Read())
            {
                comboBox1.Items.Add(reader2["КодПользователя"]);
            }
            reader2.Close();
            con.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string put = "SELECT * FROM Пользователь";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
            // создаем объект DataSet
            DataSet ds = new DataSet();
            // заполняем таблицу Order  
            // данными из базы данных
            dataAdapter.Fill(ds, "Пользователь");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
           

            con = new OleDbConnection(BD);
            con.Open();

            string tabl2 = "SELECT КодПользователя FROM Пользователь";
            OleDbCommand command2 = new OleDbCommand(tabl2, con);
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                comboBox1.Items.Add(reader2["КодПользователя"]);
            }
            reader2.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection(BD);
                con.Open();
                string queryString = "UPDATE Пользователь SET [Пользователь]  = '" + textBox1.Text + "',[Логин]  = '" + textBox2.Text + "',[Пароль] =  = '" + textBox3.Text + "' WHERE [КодПользователя]=" + comboBox1.Text;
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Успешно изменен!");

                string put = "SELECT * FROM Пользователь";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
                // создаем объект DataSet
                DataSet ds = new DataSet();
                // заполняем таблицу Order  
                // данными из базы данных
                dataAdapter.Fill(ds, "Пользователь");
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                this.Refresh();

                textBox1.Clear();
                textBox3.Clear();
                textBox2.Clear();
                comboBox1.SelectedIndex = -1;
            }
            catch
            {
                MessageBox.Show("Выберите код пользователя и заполните поля!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection(BD);
                con.Open();

                string queryString = "DELETE FROM Пользователь WHERE КодПользователя=" + comboBox1.Text;
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Успешно удалён!");


                string put = "SELECT * FROM Пользователь";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
                // создаем объект DataSet
                DataSet ds = new DataSet();
                // заполняем таблицу Order  
                // данными из базы данных
                dataAdapter.Fill(ds, "Пользователь");
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                this.Refresh();
                string tabl2 = "SELECT КодПользователя FROM Пользователь";
                OleDbCommand command2 = new OleDbCommand(tabl2, con);
                OleDbDataReader reader2 = command2.ExecuteReader();
                comboBox1.Items.Clear();
                while (reader2.Read())
                {
                    comboBox1.Items.Add(reader2["КодПользователя"]);
                }
                reader2.Close();
                con.Close();
                comboBox1.SelectedIndex = -1;
            }

            catch
            {
                MessageBox.Show("Выберите КодПользователя!");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
