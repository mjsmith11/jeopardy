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
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //the check will fail on AJAX postbacks and that's ok
                sessionCheck();
            }
            if (Session["Final"] != null)
            {
                //treat a daily double different than final jeopardy
                if (!IsPostBack)
                {
                    divAudio.InnerHtml = "<audio autoplay=\"autoplay\" src=\"./Resources/sounds/Jeopardy-theme-song.mp3\"></audio>";
                }
                else
                {
                    divAudio.InnerHtml = "";
                }
                if(!IsPostBack)
                    Session["AnswerTime"] = 30;
            }
            else if(!IsPostBack)
            {
                Session["AnswerTime"] = 20;
            }
            btnContinue.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Question q = null;
                if (Session["Question"] != null)
                    q = (Question)Session["Question"];

                lblCategory.Text = q.data.category;
                lblValue.Text = "$" + q.getValueForScoring().ToString();
                lblQuestionText.Text = q.data.question_text.ToUpper();

                if (q.data.image_file.Equals(""))
                {
                    divPicture.Visible = false;
                }
                else
                {
                    imgQuestion.ImageUrl = "~/Resources/question_media/"+q.data.image_file;
                    divPicture.Visible = true;
                }

                string[] choices = getAnswers(q);

                displayAnswers(choices, q);

                Session["value"] = q.getValueForScoring();

                Session["Question"] = null; //remove the session variable so this only loads once each time the gameboard passes in a question.

                btnAnswer1.Enabled = true;
                btnAnswer2.Enabled = true;
                btnAnswer3.Enabled = true;
                btnAnswer4.Enabled = true;

                lblTime.Text = "Remaining Time: " + Session["AnswerTime"] + " Seconds";
                Timer2.Enabled = false;
            }
        }

        private string[] getAnswers(Question q)
        {
            string[] answers = new string[4];
            answers[0] = q.data.correct_answer;
            answers[1] = q.data.wrong_answer_1;
            answers[2] = q.data.wrong_answer_2;
            answers[3] = q.data.wrong_answer_3;


            if (q.data.randomize_answers)
            {
                Random rnd = new Random();
                return answers.OrderBy(x => rnd.Next()).ToArray();
            }
            else
            {
                return answers;
            }
        }

        private void displayAnswers(string[] answers, Question q)
        {
            string correctAns = q.data.correct_answer;
            int correctButtonNum=0;

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

            Session["Correct"] = correctButtonNum;

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
            Timer1.Enabled = false;
            int value = (int)Session["value"];
            Session["value"] = null;

            int correctButtonNum = (int)Session["Correct"];
            Session["Correct"] = null;

            Gameboard gb = (Gameboard)Session["Gameboard"];
            if (buttonNum == correctButtonNum)
            {
                gb.increaseScore(value);
            }
            else
            {
                gb.decreaseScore(value);
            }
            Session["Gameboard"] = gb;
            showCorrect(correctButtonNum);
            btnContinue.Visible = true;
        }
        private void showCorrect(int correctButtonNum)
        {
            btnAnswer1.Enabled = false;
            btnAnswer2.Enabled = false;
            btnAnswer3.Enabled = false;
            btnAnswer4.Enabled = false;
            Button b=null;
            switch (correctButtonNum)
            {
                case 1:
                    {
                        b = btnAnswer1;
                        break;
                    }
                case 2:
                    {
                        b = btnAnswer2;
                        break;
                    }
                case 3:
                    {
                        b = btnAnswer3;
                        break;
                    }
                case 4:
                    {
                        b = btnAnswer4;
                        break;
                    }
            }
            b.BackColor = System.Drawing.Color.Green;

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            if (Session["Final"] == null)
            {
                Response.Redirect("GameBoardUI.aspx");
            }
            else
            {
                Session["Final"] = null;
                Response.Redirect("EndGame.aspx");
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int timeLeft = Int32.Parse(Session["AnswerTime"].ToString());
            if(timeLeft > 0 )
            {
                timeLeft--;
                lblTime.Text = "Remaining Time: " + timeLeft + " Seconds";
                Session["AnswerTime"] = timeLeft;

                if(timeLeft==0)
                {
                    Session["AnswerTime"] = null;
                    processAnswerChoice(0); //0 will never be correct 
                    if (Session["Final"] == null)
                    {
                        divTimeoutAudio.InnerHtml = "<audio autoplay=\"autoplay\" src=\"./Resources/sounds/qtime.wav\"></audio>";
                    }
                    //the tick of this timer after 5ms triggers the update of the answer buttons and continue button
                    Timer2.Enabled = true;
                }

            }
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            Timer2.Enabled = false;
            //this only allows this to tick once
        }

        private void sessionCheck()
        {
            if (Session["Question"] == null)
                Response.Redirect("Error.aspx");
            if (Session["Gameboard"] == null)
                Response.Redirect("Error.aspx");

        }
    }
}