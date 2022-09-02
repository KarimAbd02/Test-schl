using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public enum CategoryQuest
    {
        /// <summary>
        /// Задание с открытым ответом!
        /// </summary>
        Category_1,
        /// <summary>
        /// Задание с выбором ответа!
        /// </summary>
        Category_2,
        /// <summary>
        /// Задание на упорядочивание последовательности!
        /// </summary>
        Category_3,
        /// <summary>
        /// Задание на устоновление соответствия!
        /// </summary>
        Category_4
    }
    public class Questions
    {
        public int Index { get; set; }
        public int numbertheme { get; set; }
        public CategoryQuest CategoryObj { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
