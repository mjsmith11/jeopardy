using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    public class QuestionData
    {
        /// <summary>
        /// unique numeric id of the question
        /// </summary>
        public int question_id { get; set; }
        /// <summary>
        /// text stating the question
        /// </summary>
        public string question_text { get; set; }
        /// <summary>
        /// correct answer to the question
        /// </summary>
        public string correct_answer { get; set; }
        /// <summary>
        /// first incorrect answer to the question
        /// </summary>
        public string wrong_answer_1 { get; set; }
        /// <summary>
        /// second wrong answer to the question
        /// </summary>
        public string wrong_answer_2 { get; set; }
        /// <summary>
        /// third wrong answer to the question
        /// </summary>
        public string wrong_answer_3 { get; set; }
        /// <summary>
        /// category in which the question should appear
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// difficulty of the question 1-5
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// location in book, slides, etc where question was obtained
        /// </summary>
        public string reference { get; set; }
        /// <summary>
        /// true if the question has been used in a recent game and should not be used for the next game. False if the question can be used for the next game
        /// </summary>
        public bool used { get; set; }
        /// <summary>
        /// name of the image file for picture questions, "" for non picture questions
        /// </summary>
        public string image_file { get; set; }
        /// <summary>
        /// true if it is safe to randomize the order of the question's answers. false if not
        /// </summary>
        public bool randomize_answers { get; set; }

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
        /// <param name="randomize_answers">initial value</param>
        public QuestionData(int question_id, string question_text, string correct_answer, string wrong_answer_1, string wrong_answer_2, string wrong_answer_3, string category, int level, string reference, bool used, string image_file, bool randomize_answers)
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
            this.randomize_answers = randomize_answers;
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
        /// <param name="randomize_answers">initial value</param>
        public QuestionData(string question_text, string correct_answer, string wrong_answer_1, string wrong_answer_2, string wrong_answer_3, string category, int level, string reference, bool used, string image_file, bool randomize_answers)
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
            this.randomize_answers = randomize_answers;
        }

        /// <summary>
        /// no argument constructor
        /// </summary>
        public QuestionData()
        {

        }
    }
}
