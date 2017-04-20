#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    public partial class QuestionUI : System.Web.UI.Page
    {
        private int correctButtonNum;
        private int value;
        protected void Page_Load(object sender, EventArgs e)
        {
#if (DEBUG)
            populateDebugData();
#endif
            Question q = null;
            if(Session["Question"] != null)
                q = (Question)Session["Question"];

            lblCategory.Text = q.data.category;
            lblValue.Text = "$"+q.getValueForScoring().ToString();
            lblQuestionText.Text = q.data.question_text.ToUpper();

            string[] choices = randomizeAnswers(q);
            displayAnswers(choices, q);

            value = q.getValueForScoring();

            Session["Question"] = null; //remove the session variable so this only loads once each time the gameboard passes in a question.

        }

        private string[] randomizeAnswers(Question q)
        {
            string[] answers = new string[4];
            answers[0] = q.data.correct_answer;
            answers[1] = q.data.wrong_answer_1;
            answers[2] = q.data.wrong_answer_2;
            answers[3] = q.data.wrong_answer_3;

            Random rnd = new Random();
            return answers.OrderBy(x => rnd.Next()).ToArray();
        }

        private void displayAnswers(string[] answers, Question q)
        {
            string correctAns = q.data.correct_answer;

            btnAnswer1.Text = answers[0];
            if (answers[0].Equals(correctAns))
                correctButtonNum = 1;

            btnAnswer2.Text = answers[1];
            if (answers[1].Equals(correctAns))
                correctButtonNum = 2;

            btnAnswer3.Text = answers[2];
            if (answers[2].Equals(correctAns))
                correctButtonNum = 3;

            btnAnswer4.Text = answers[3];
            if (answers[3].Equals(correctAns))
                correctButtonNum = 4;

        }

        private void populateDebugData()
        {
            Question q = new Question();
            q.display = true;
            q.value = 1500;
            q.wagerActive = false;
            q.data = new DatabaseConnection.QuestionData("This is My question?", "Right", "W1", "W2", "W3", "question Category", 2, "ref", false, "");
            Session["Question"] = q;
        }

        protected void btnAnswer1_Click(object sender, EventArgs e)
        {
            processAnswerChoice(1);
        }

        protected void btnAnswer2_Click(object sender, EventArgs e)
        {
            processAnswerChoice(2);
        }

        protected void btnAnswer3_Click(object sender, EventArgs e)
        {
            processAnswerChoice(3);
        }

        protected void btnAnswer4_Click(object sender, EventArgs e)
        {
            processAnswerChoice(4);
        }

        private void processAnswerChoice(int buttonNum)
        {
            if(buttonNum == correctButtonNum)
            {
                //Correct!
            }
            else
            {
                //wrong!
            }
        }
    }
}