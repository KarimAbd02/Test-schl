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
    public partial class Form4 : Form
    {
        List<Questions> listQuest = new List<Questions>();
        Test test;
        User user;
        double point = 0;

        public Form4(Test _test, User _user)
        {
            InitializeComponent();
            user = _user;
            test = _test;
            GetFullTest();
            GetQuest();
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 ss = new Form3();
            ss.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void WriteResult()
        {
            OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
            OleDbCommand command = new OleDbCommand(string.Format("insert into Результат(КодРезультата,КодПользователя,КодТеста,Результат) values({0},{1},{2})", user.ID, test.Index, (100.0 * point / dictionary.Count).ToString("#.##")), connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
        }
        private void GetFullTest()
        {
            string command = "SELECT * FROM (Создание inner join Тест ON Создание.КодТеста=Тест.КодТеста) inner join [База знаний] on Создание.КодВопроса = [База знаний].КодВопроса where Тест.КодТеста = " + test.Index.ToString();
            OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\test.mdb");
            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("{0}", command), connect);
            DataTable table = new DataTable();
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                listQuest.Add(new Questions
                {
                    Index = int.Parse(table.Rows[i]["База знаний.КодВопроса"].ToString()),
                    CategoryObj = (CategoryQuest)int.Parse(table.Rows[i]["ТипТеста"].ToString()),
                    numbertheme = int.Parse(table.Rows[i]["КодТемы"].ToString()),
                    Value1 = table.Rows[i]["Вопрос"].ToString(),
                    Value2 = table.Rows[i]["Ответ"].ToString()
                });
            }
        }
        int index = -1;
        Dictionary<int, ObjQuest> dictionary = new Dictionary<int, ObjQuest>();
        class ObjQuest
        {
            public CategoryQuest _Category { get; set; }
            public object MyClass { get; set; }
        }
        private void GetQuest()
        {

            for (int i = 0; i < listQuest.Count; i++)
            {
                switch ((int)listQuest[i].CategoryObj)
                {
                    case 0: dictionary.Add(i, new ObjQuest { _Category = CategoryQuest.Category_1, MyClass = TaskWithOpenAnswer(listQuest[i]) }); break;
                    case 1: dictionary.Add(i, new ObjQuest { _Category = CategoryQuest.Category_2, MyClass = TaskWithSelectedValue(listQuest[i]) }); break;
                    case 2: dictionary.Add(i, new ObjQuest { _Category = CategoryQuest.Category_3, MyClass = OrderingSequence(listQuest[i]) }); break;
                    case 3: dictionary.Add(i, new ObjQuest { _Category = CategoryQuest.Category_4, MyClass = RelevantIdentified(listQuest[i]) }); break;
                }
            }

            SetQuest();
        }

        private void SetQuest()
        {
            index++;
            if (index == dictionary.Count)
            {
                WriteResult();
                MessageBox.Show(string.Format("Тестируемый: {0} {1} \nРезультат: {2}", user.Name1, (100.0 * point / dictionary.Count).ToString("#.##")), "Результат!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            else
            {
                label5.Text = string.Format("Вопрос: {0}\\{1}", index + 1, dictionary.Count);
                Random r = new Random();
                switch ((int)dictionary[index]._Category)
                {
                    case 0:
                        WithOpenAnswer quest_WithOpenAnswer = (WithOpenAnswer)dictionary[index].MyClass;
                        textBox2.Text = quest_WithOpenAnswer.Question;
                        panel3.BringToFront();
                        break;
                    case 1:
                        WithSelectedValue quest_WithSelectedValue = (WithSelectedValue)dictionary[index].MyClass;
                        checkedListBox1.Items.Clear();
                        textBox1.Text = quest_WithSelectedValue.Questions;
                        for (int i = 0; i < quest_WithSelectedValue._Quest.Count; i++)
                        {
                            checkedListBox1.Items.Add(quest_WithSelectedValue._Quest[i].Answer);
                        }
                        panel2.BringToFront();
                        break;
                    case 2:
                        Sequence quest_Sequence = (Sequence)dictionary[index].MyClass;
                        Column2.Items.Clear();
                        dataGridView1.Rows.Clear();
                        var resultMix = quest_Sequence.OrdSeq.OrderBy(x => r.Next()).ToArray();
                        Column2.Items.AddRange(resultMix);

                        for (int i = 0; i < quest_Sequence.OrdSeq.Length; i++)
                        {
                            dataGridView1.Rows.Add((i + 1).ToString());
                        }

                        panel4.BringToFront();
                        break;

                    case 3:
                        dataGridView2.Rows.Clear();
                        RelevantId quest_RelevantId = (RelevantId)dictionary[index].MyClass;
                        Column3.Items.Clear();
                        Column4.Items.Clear();
                        var resultMix2 = quest_RelevantId.Listfirst.OrderBy(x => r.Next()).ToArray();
                        var resultMix3 = quest_RelevantId.ListSecond.OrderBy(x => r.Next()).ToArray();
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows.AddCopies(0, resultMix2.Length - 1);
                        Column3.Items.AddRange(resultMix2);
                        Column4.Items.AddRange(resultMix3);
                        panel5.BringToFront();
                        break;
                }
            }
        }

        /// <summary>
        /// Задание с открытым ответом
        /// </summary>
        private WithOpenAnswer TaskWithOpenAnswer(Questions quest)
        {
            return new WithOpenAnswer
            {
                ID = quest.Index,
                Answer = quest.Value2,
                Question = quest.Value1
            };
        }
        /// <summary>
        /// Задание с выбором ответа
        /// </summary>
        private WithSelectedValue TaskWithSelectedValue(Questions quest)
        {

            var array = quest.Value2.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var listt = new List<_Value>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i][0] == '+')
                    listt.Add(new _Value { Answer = array[i].Remove(0, 1), Valid = true });
                else
                    listt.Add(new _Value { Answer = array[i].Remove(0, 1), Valid = false });
            }
            return new WithSelectedValue
            {
                ID = quest.Index,
                Questions = quest.Value1,
                _Quest = listt
            };
        }

        /// <summary>
        /// Задание на упорядочивание последовательности
        /// </summary>
        private Sequence OrderingSequence(Questions quest)
        {
            var array = quest.Value1.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            return new Sequence
            {
                OrdSeq = array
            };
        }


        /// <summary>
        /// Задание на установление соответствия
        /// </summary>
        /// <param name="quest"></param>
        private RelevantId RelevantIdentified(Questions quest)
        {
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            List<string> list4 = new List<string>();

            var array1 = quest.Value1.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < array1.Length; i++)
            {
                var a = array1[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                list1.Add(a[0]);
                
            }

            return new RelevantId
            {
                ID = quest.Index,
                Listfirst = list1,
                ListSecond = list2
               
            };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Задание на установление соответствия
            var _cl = ((RelevantId)dictionary[index].MyClass);

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null && dataGridView2.Rows[i].Cells[1].Value != null)
                {
                    var first = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    var second = dataGridView2.Rows[i].Cells[1].Value.ToString();

                    var res = _cl.Listfirst.IndexOf(first);
                    if (!(_cl.ListSecond[res] == second))
                        break;
                    if (i == dataGridView2.Rows.Count - 1)
                        point++;
                }
                else
                    break;
            }

            CheckPoint();
            SetQuest();
        }

        private void button3_Click(object sender, EventArgs e)
        {//Задание на упорядочивание последовательности

            var _cl = ((Sequence)dictionary[index].MyClass);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!(dataGridView1.Rows[i].Cells[1].Value == _cl.OrdSeq[i]))
                    break;
                if (i == dataGridView1.Rows.Count - 1)
                    point++;
            }
            CheckPoint();
            SetQuest();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {// Задание с открытым ответом

            if (textBox3.Text.ToLower() == ((WithOpenAnswer)dictionary[index].MyClass).Answer.ToLower())
                point++;

            CheckPoint();
            SetQuest();
        }
        private void CheckPoint()
        {
            label6.Text = string.Format("Ваш результат: {0} %", (100.0 * point / dictionary.Count).ToString("#.##"));
        }

        private void TestingForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var _cl = ((WithSelectedValue)dictionary[index].MyClass);

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                if (!_cl._Quest.Find(x => x.Answer == checkedListBox1.Items[checkedListBox1.CheckedIndices[i]]).Valid)
                {
                    break;
                }
                if (i == checkedListBox1.CheckedItems.Count - 1)
                    point++;

            }

            CheckPoint();
            SetQuest();
        }
    }
    class WithOpenAnswer
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    class WithSelectedValue
    {
        public int ID { get; set; }
        public string Questions { get; set; }
        public List<_Value> _Quest { get; set; }

    }
    class _Value
    {
        public string Answer { get; set; }
        public bool Valid { get; set; }
    }
    class Sequence
    {
        public string[] OrdSeq { get; set; }
    }
    class RelevantId
    {
        public int ID { get; set; }
        public List<string> Listfirst { get; set; }
        public List<string> ListSecond { get; set; }
    }
    
        
}

