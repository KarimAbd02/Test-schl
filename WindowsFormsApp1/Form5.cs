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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public static string BD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path.Combine(Application.StartupPath, "test.mdb;");
        private OleDbConnection con;
        public string tema;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 ss = new Form3();
            ss.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss = new Form1();
            ss.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection(BD);
            con.Open();

            string tabl2 = "SELECT Тема FROM Тема";
            OleDbCommand command2 = new OleDbCommand(tabl2, con);
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                comboBox1.Items.Add(reader2["Тема"]);
            }
            reader2.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tema = "";
                if (comboBox1.Text != "Название темы" || comboBox1.Text != "")
                {
                    string sql1 = "SELECT * FROM Тема  WHERE  [Тема] =" + "'" + comboBox1.Text + "'";
                    string connectionString1;
                    connectionString1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb;";

                    OleDbConnection connection1 = new OleDbConnection(connectionString1);
                    connection1.Open();
                    OleDbCommand command1 = new OleDbCommand(sql1, connection1);
                    OleDbDataReader dataReader1 = command1.ExecuteReader();

                    tema = "";
                    while (dataReader1.Read())
                    {
                        tema += dataReader1["Файл темы"];
                    }
                    dataReader1.Close();
                    connection1.Close();
                }
                richTextBox1.LoadFile(@"" + tema);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("Проверте правильность введённых данных");
            }
        }
    }
}
