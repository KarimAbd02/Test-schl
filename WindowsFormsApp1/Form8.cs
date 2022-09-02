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
    public partial class Form8 : Form
    {
        List<Questions> list = new List<Questions>();
        

        public Form8()
        {
            InitializeComponent();
            

            
        }
        public static string BD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path.Combine(Application.StartupPath, "test.mdb;");
        private OleDbConnection con;
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 ss = new Form6();
            ss.Show();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
                con.Open();
                string queryString = "INSERT INTO Создание ([КодТеста], [КодВопроса]) values('" + comboBox1.Text + "','" + comboBox2.Text + "')";
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM Создание", con);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0].DefaultView;
                this.Refresh();

                string tabl2 = "SELECT КодТеста FROM Тест";
                OleDbCommand command2 = new OleDbCommand(tabl2, con);
                OleDbDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    comboBox1.Items.Add(reader2["КодТеста"]);
                }
                reader2.Close();

                string tabl3 = "SELECT КодТеста FROM Тест";
                OleDbCommand command3 = new OleDbCommand(tabl3, con);
                OleDbDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    comboBox3.Items.Add(reader3["КодТеста"]);
                }
                reader3.Close();

                con.Close();
            }
            catch
           { 
               MessageBox.Show("При создании возникла ошибка!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
            con.Open();
            string queryString = "INSERT INTO Тест ([Название]) values('" + textBox1.Text + "')";
            OleDbCommand command = new OleDbCommand(queryString, con);
            command.ExecuteNonQuery();
            this.Refresh();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM Тест", con);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;



            textBox1.Clear();
            

            this.Refresh();
            string tabl2 = "SELECT КодТеста FROM Тест";
            OleDbCommand command2 = new OleDbCommand(tabl2, con);
            OleDbDataReader reader2 = command2.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader2.Read())
            {
                comboBox1.Items.Add(reader2["КодТеста"]);
            }
            reader2.Close();
            string tabl3 = "SELECT КодТеста FROM Тест";
            OleDbCommand command3 = new OleDbCommand(tabl3, con);
            OleDbDataReader reader3 = command3.ExecuteReader();
            comboBox3.Items.Clear();
            while (reader3.Read())
            {
                comboBox3.Items.Add(reader3["КодТеста"]);
            }
            reader3.Close();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 ss = new Form10();
            ss.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection(BD);
                con.Open();
                string queryString = "UPDATE Тест SET [Название]  = '" + textBox1.Text + "' WHERE [КодТеста]=" + comboBox3.Text;
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Успешно изменен!");

                string put = "SELECT * FROM Тест";
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
                // создаем объект DataSet
                DataSet ds = new DataSet();
                // заполняем таблицу Order  
                // данными из базы данных
                dataAdapter.Fill(ds, "Тест");
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                this.Refresh();

                textBox1.Clear();
                comboBox3.SelectedIndex = -1;
            }
            catch
            {
                MessageBox.Show("Выберите Код теста и введите новое название теста");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Form8_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            string pt = "SELECT * FROM Создание";
            OleDbDataAdapter dataAdapte = new OleDbDataAdapter(pt, BD);
            // создаем объект DataSet
            DataSet d = new DataSet();
            // заполняем таблицу Order  
            // данными из базы данных
            dataAdapte.Fill(d, "Создание");
            dataGridView3.DataSource = d.Tables[0].DefaultView;

            string put = "SELECT * FROM Тест";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(put, BD);
            // создаем объект DataSet
            DataSet ds = new DataSet();
            // заполняем таблицу Order  
            // данными из базы данных
            dataAdapter.Fill(ds, "Тест");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2),
                (ScreenHeight / 2) - (this.Height / 2));
            this.ControlBox = false;

            string put1 = "SELECT * FROM [База знаний]";
            OleDbDataAdapter dataAdapter1 = new OleDbDataAdapter(put1, BD);
            // создаем объект DataSet
            DataSet ds1 = new DataSet();
            // заполняем таблицу Order  
            // данными из базы данных
            dataAdapter1.Fill(ds1, "[База знаний]");
            dataGridView2.DataSource = ds1.Tables[0].DefaultView;

            con = new OleDbConnection(BD);
            con.Open();

            string tabl2 = "SELECT КодТеста FROM Тест";
            OleDbCommand command2 = new OleDbCommand(tabl2, con);
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                comboBox1.Items.Add(reader2["КодТеста"]);
            }
            reader2.Close();

            string tabl3 = "SELECT КодТеста FROM Тест";
            OleDbCommand command3 = new OleDbCommand(tabl3, con);
            OleDbDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                comboBox3.Items.Add(reader3["КодТеста"]);
            }
            reader3.Close();

            string tabl1 = "SELECT КодВопроса FROM [База знаний]";
            OleDbCommand command1 = new OleDbCommand(tabl1, con);
            OleDbDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox2.Items.Add(reader1["КодВопроса"]);
            }
            reader1.Close();
            con.Close();

        }
    }
}
