using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    /// <summary>
    /// Web page for the user to interact with Question objects
    /// </summary>
    public partial class QuestionUI : System.Web.UI.Page
    {
        /// <summary>
        /// initialize the timer for the question and start the music for final jeopardy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Read the question from the session and display its data in the page controls
        /// Remove the question from the session
        /// Show the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// get the set of possible answers for a question
        /// </summary>
        /// <param name="q">question to get answers for</param>
        /// <returns>possible answers for the question in random order</returns>
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

        /// <summary>
        /// show the possible answers in web controls and set button number that contains the correct answer
        /// </summary>
        /// <param name="answers">Answers in the order that they should appear</param>
        /// <param name="q">Question data</param>
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

        /// <summary>
        /// process a click of first answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnswer1_Click(object sender, EventArgs e)
        {
            processAnswerChoice(1);
        }

        /// <summary>
        /// process a click of second answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnswer2_Click(object sender, EventArgs e)
        {
            processAnswerChoice(2);
        }

        /// <summary>
        /// process a click of the third answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnswer3_Click(object sender, EventArgs e)
        {
            processAnswerChoice(3);
        }

        /// <summary>
        /// process a click of the fourth answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnswer4_Click(object sender, EventArgs e)
        {
            processAnswerChoice(4);
        }

        /// <summary>
        /// stop the timer, check the answer, update the score, and show the correct answer and continue button
        /// </summary>
        /// <param name="buttonNum">number of the answer button that was clicked</param>
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

        /// <summary>
        /// disable answer buttons, turn the correct answer green
        /// </summary>
        /// <param name="correctButtonNum">number of the button displaying the correct answer</param>
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

        /// <summary>
        /// Redirect to the gameboard for first two rounds or endgame for final jeopardy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// updates the timer and process an incorrect answer when timer expires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// disable timer2.  This timer ticks to trigger an event so that the user sees the ui updates
        /// to show the correct answer and continue button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Timer2_Tick(object sender, EventArgs e)
        {
            Timer2.Enabled = false;
            //this only allows this to tick once
        }

        /// <summary>
        /// checks that Question and Gameboard exist in the Session and redirects to the error page if not
        /// </summary>
        private void sessionCheck()
        {
            if (Session["Question"] == null)
                Response.Redirect("Error.aspx");
            if (Session["Gameboard"] == null)
                Response.Redirect("Error.aspx");

        }
    }
}