using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    public class QuestionData
    {
        public int question_id { get; set; }
        public string question_text { get; set; }
        public string correct_answer { get; set; }
        public string wrong_answer_1 { get; set; }
        public string wrong_answer_2 { get; set; }
        public string wrong_answer_3 { get; set; }
        public string category { get; set; }
        public int level { get; set; }
        public string reference { get; set; }
        public bool used { get; set; }
        public string image_file { get; set; }

        /// <summary>
        /// 11 parameter constructor
        /// </summary>
        /// <param name="question_id">initial value</param>
        /// <param name="question_text">initial value</param>
        /// <param name="correct_answer">initial value</param>
        /// <param name="wrong_answer_1">initial value</param>
        /// <param name="wrong_answer_2">initial value</param>
        /// <param name="wrong_answer_3">initial value</param>
        /// <param name="category">initial value</param>
        /// <param name="level">initial value</param>
        /// <param name="reference">initial value</param>
        /// <param name="used">initial value</param>
        /// <param name="image_file">initial value</param>
        public QuestionData(int question_id, string question_text, string correct_answer, string wrong_answer_1, string wrong_answer_2, string wrong_answer_3, string category, int level, string reference, bool used, string image_file)
        {
            this.question_id = question_id;
            this.question_text = question_text;
            this.correct_answer = correct_answer;
            this.wrong_answer_1 = wrong_answer_1;
            this.wrong_answer_2 = wrong_answer_2;
            this.wrong_answer_3 = wrong_answer_3;
            this.category = category;
            this.level = level;
            this.reference = reference;
            this.used = used;
            this.image_file = image_file;
        }

        /// <summary>
        /// 10 parameter constructor that sets question_id to 0
        /// </summary>
        /// <param name="question_text">initial value</param>
        /// <param name="correct_answer">initial value</param>
        /// <param name="wrong_answer_1">initial value</param>
        /// <param name="wrong_answer_2">initial value</param>
        /// <param name="wrong_answer_3">initial value</param>
        /// <param name="category">initial value</param>
        /// <param name="level">initial value</param>
        /// <param name="reference">initial value</param>
        /// <param name="used">initial value</param>
        /// <param name="image_file">initial value</param>
        public QuestionData(string question_text, string correct_answer, string wrong_answer_1, string wrong_answer_2, string wrong_answer_3, string category, int level, string reference, bool used, string image_file)
        {
            this.question_id = 0;
            this.question_text = question_text;
            this.correct_answer = correct_answer;
            this.wrong_answer_1 = wrong_answer_1;
            this.wrong_answer_2 = wrong_answer_2;
            this.wrong_answer_3 = wrong_answer_3;
            this.category = category;
            this.level = level;
            this.reference = reference;
            this.used = used;
            this.image_file = image_file;
        }

        /// <summary>
        /// no argument constructor
        /// </summary>
        public QuestionData()
        {

        }
    }
}
