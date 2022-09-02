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
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public static string BD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path.Combine(Application.StartupPath, "test.mdb;");
        private OleDbConnection con;
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 ss = new Form8();
            ss.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0: panelType1.BringToFront(); break;
                case 1: panelType2.BringToFront(); break;
                case 2: panelType3.BringToFront(); break;
                case 3: panelType4.BringToFront(); break;
                default: break;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Form10_Load(object sender, EventArgs e)

        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string put1 = "SELECT * FROM [База знаний]";
            OleDbDataAdapter dataAdapter1 = new OleDbDataAdapter(put1, BD);
            // создаем объект DataSet
            DataSet ds1 = new DataSet();
            // заполняем таблицу Order  
            // данными из базы данных
            dataAdapter1.Fill(ds1, "[База знаний]");
            dataGridView1.DataSource = ds1.Tables[0].DefaultView;

            string put = "SELECT * FROM Тема";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
            // создаем объект DataSet
            DataSet ds = new DataSet();
            // заполняем таблицу Order  
            // данными из базы данных
            dataAdapter.Fill(ds, "Тема");
            dataGridView2.DataSource = ds.Tables[0].DefaultView;

            con = new OleDbConnection(BD);
            con.Open();

            string tabl2 = "SELECT КодТемы FROM Тема";
            OleDbCommand command2 = new OleDbCommand(tabl2, con);
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                comboBox1.Items.Add(reader2["КодТемы"]);
            }
            reader2.Close();

            string tabl1 = "SELECT КодВопроса FROM [База знаний]";
            OleDbCommand command1 = new OleDbCommand(tabl1, con);
            OleDbDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox3.Items.Add(reader1["КодВопроса"]);
            }
            reader1.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
                con.Open();
                if (comboBox2.SelectedIndex == 0)
                {
                    string queryString = "INSERT INTO [База знаний] ([КодТемы], [ТипТеста], [Вопрос], [Ответ]) values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox1.Text + "','" + textBox2.Text + "')";
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    string queryString = "INSERT INTO [База знаний] ([КодТемы], [ТипТеста], [Вопрос], [Ответ]) values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                }
                else if (comboBox2.SelectedIndex == 2)
                {
                    string queryString = "INSERT INTO [База знаний] ([КодТемы], [ТипТеста], [Вопрос], [Ответ]) values('" + comboBox1.Text + "','" + comboBox2.Text + "', 'Упорядочить' ,'" + textBox6.Text + "')";
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                }
                else if (comboBox2.SelectedIndex == 3)
                {
                    string queryString = "INSERT INTO [База знаний] ([КодТемы], [ТипТеста], [Вопрос], [Ответ]) values('" + comboBox1.Text + "','" + comboBox2.Text + "', 'Установить соответсвие' ,'" + textBox5.Text + "')";
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                }
                string put = "SELECT * FROM [База знаний]";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
                // создаем объект DataSet
                DataSet ds = new DataSet();
                // заполняем таблицу Order  
                // данными из базы данных
                dataAdapter.Fill(ds, "[База знаний]");
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                this.Refresh();
                string tabl2 = "SELECT КодВопроса FROM [База знаний]";
                OleDbCommand command2 = new OleDbCommand(tabl2, con);
                OleDbDataReader reader2 = command2.ExecuteReader();
                comboBox3.Items.Clear();
                while (reader2.Read())
                {
                    comboBox3.Items.Add(reader2["КодВопроса"]);
                }
                reader2.Close();
                con.Close();
                comboBox3.SelectedIndex = -1;
            }
            catch
            {
                MessageBox.Show("Введите данные!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
            con.Open();
            try
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    string queryString = "UPDATE [База знаний] SET [КодТемы]  = '" + comboBox1.Text + "',[ТипТеста]  = '" + comboBox2.Text + "',[Вопрос] =  = '" + textBox1.Text + "',[Ответ] =  = '" + textBox2.Text + "' WHERE [КодВопроса]=" + comboBox3.Text;
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно изменен!");
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    string queryString = "UPDATE [База знаний] SET [КодТемы]  = '" + comboBox1.Text + "',[ТипТеста]  = '" + comboBox2.Text + "',[Вопрос] =  = '" + textBox3.Text + "',[Ответ] =  = '" + textBox4.Text + "' WHERE [КодВопроса]=" + comboBox3.Text;
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно изменен!");
                }
                else if (comboBox2.SelectedIndex == 2)
                {
                    string queryString = "UPDATE [База знаний] SET [КодТемы]  = '" + comboBox1.Text + "',[ТипТеста]  = '" + comboBox2.Text + "',[Вопрос] =  = 'Упорядочить:',[Ответ] =  = '" + textBox6.Text + "' WHERE [КодВопроса]=" + comboBox3.Text;
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно изменен!");
                }
                else if (comboBox2.SelectedIndex == 3)
                {
                    string queryString = "UPDATE [База знаний] SET [КодТемы]  = '" + comboBox1.Text + "',[ТипТеста]  = '" + comboBox2.Text + "',[Вопрос] =  = 'Установить соответствие:',[Ответ] =  = '" + textBox5.Text + "' WHERE [КодВопроса]=" + comboBox3.Text;
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно изменен!");
                }
            }
            catch
            {
                MessageBox.Show("Выберите КодВопроса и введите данные!");
            }

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM [База знаний]", con);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            this.Refresh();
            con.Close();

            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection(BD);
                con.Open();

                string queryString = "DELETE FROM [База знаний] WHERE КодВопроса=" + comboBox3.Text;
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Успешно удалён!");


                string put = "SELECT * FROM [База знаний]";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
                // создаем объект DataSet
                DataSet ds = new DataSet();
                // заполняем таблицу Order  
                // данными из базы данных
                dataAdapter.Fill(ds, "[База знаний]");
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                this.Refresh();
                string tabl2 = "SELECT КодВопроса FROM [База знаний]";
                OleDbCommand command2 = new OleDbCommand(tabl2, con);
                OleDbDataReader reader2 = command2.ExecuteReader();
                comboBox3.Items.Clear();
                while (reader2.Read())
                {
                    comboBox3.Items.Add(reader2["КодВопроса"]);
                }
                reader2.Close();
                con.Close();
                comboBox3.SelectedIndex = -1;
            }

            catch
            {
                MessageBox.Show("Выберите номер вопроса!");
            }
        }
    }
}
