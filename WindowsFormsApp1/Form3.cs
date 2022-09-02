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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        List<Test> testsList = new List<Test>();
        User user;
        public Form3()
        {
            
            InitializeComponent();
            
            GetTests();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss = new Form1();
            ss.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 ss = new Form5();
            ss.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void GetTests()
        {
           
            OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT КодТеста, Название FROM Тест", connect);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                testsList.Add(new Test
                {
                    About = table.Rows[i]["Название"].ToString(),
                    Index = int.Parse(table.Rows[i]["КодТеста"].ToString())
                });
            }
            AddTestsInListView();
        }

        private void AddTestsInListView()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < testsList.Count; i++)
            {
                listBox1.Items.Add(string.Format("{0}", testsList[i].About));
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 ss = new Form4(testsList[listBox1.SelectedIndex], user);
            ss.Show();
        }

        
    }
}
