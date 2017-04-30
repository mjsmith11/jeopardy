using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Jeopardy_Game
{
    /// <summary>
    /// page for the user to place his or her wager
    /// </summary>
    public partial class Wager : System.Web.UI.Page
    {
        /// <summary>
        /// plays a sound effect and shows an image for final jeopardy or daily double. Shows the user the category and his or her current score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            sessionCheck();
            Gameboard gb = (Gameboard)Session["Gameboard"];
            Question q;
            
            if (gb.currentRound <= 2)
            {
                if (Session["Question"] == null)
                    Response.Redirect("Error.aspx");
                //treat a daily double different than final jeopardy
                if (!IsPostBack)
                {
                    divAudio.InnerHtml = "<audio autoplay=\"autoplay\" src=\"./Resources/sounds/dailydouble.wav\"></audio>";
                }
                else
                {
                    divAudio.InnerHtml = "";
                }
                q = (Question)Session["Question"];
                imgDailyDouble.ImageUrl = "~/Resources/images/Daily_Double.png";
            }
            else
            {
                if (!IsPostBack)
                {
                    divAudio.InnerHtml = "<audio autoplay=\"autoplay\" src=\"./Resources/sounds/final_cat.wav\"></audio>";
                }
                else
                {
                    divAudio.InnerHtml = "";
                }
                q = gb.getFinalQuestion();
                Session["Question"] = q;
                Session["Final"] = true;
                imgDailyDouble.ImageUrl = "~/Resources/images/Final_Jeopardy.png";
            }

            lblScore.Text = "Category: "+q.data.category+"&nbsp;&nbsp;&nbsp;"+"Score: $" + gb.currentScore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Validates the entered wager and redirects to the QuestionUI if valid and shows an error otherwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            if (gb.currentRound <= 2)
            {
                if(dailyDoubleValidate())
                {
                    Response.Redirect("QuestionUI.aspx");
                }
                else
                {
                    divAudio.InnerHtml = "";
                }
            }
            else
            {
                if(finalValidate())
                {
                    Response.Redirect("QuestionUI.aspx");
                }
                else
                {
                    divAudio.InnerHtml = "";
                }
            }
        }

        /// <summary>
        /// validates the entered wager according to the daily double validation flowchart and displays any errors
        /// </summary>
        /// <returns>true if valid and false if invalid</returns>
        private bool dailyDoubleValidate()
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            int score = gb.currentScore;
            int wager=0;
            string message = "";
            try
            {
                wager = Int32.Parse(tbxWager.Text);
                if (wager >= 0)
                {
                    if (!(wager <= score))
                    {
                        if (score <= 1000)
                        {
                            if(wager>1000)
                            {
                                message = "Wager must be $1000 or less";
                            }
                        }
                        else
                        {
                            message = "Wager cannot exceed score";
                        }
                    }
                }
                else
                {
                    message = "Wager must be non-negative";
                }
            }
            catch(Exception)
            {
                message = "Wager must be a non-decimal number";
            }
            if (message.Equals(""))
            {
                setWager(wager);
                return true;
            }
            else
            {
                lblError.Text = message;
                return false;
            }


        }

        /// <summary>
        /// Validates entered wager according to the final jeopardy validation flow chart and displays any errors
        /// </summary>
        /// <returns>true if valid and false if invalid</returns>
        private bool finalValidate()
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            int score = gb.currentScore;
            int wager = 0;
            string message = "";
            try
            {
                wager = Int32.Parse(tbxWager.Text);
                if (wager >= 0)
                {
                    if (wager > score)
                        message = "Wager must not exceed score";
                }
                else
                {
                    message = "Wager must be non-negative";
                }
            }
            catch (Exception)
            {
                message = "Wager must be a non-decimal number";
            }
            if (message.Equals(""))
            {
                setWager(wager);
                return true;
            }
            else
            {
                lblError.Text = message;
                return false;
            }
        }
        /// <summary>
        /// sets the wager amount on the question in the session
        /// </summary>
        /// <param name="value">wager amount to set</param>
        private void setWager(int value)
        {
            Question q = null;
            if (Session["Question"] != null)
                q = (Question)Session["Question"];
            q.wager = value;
            Session["Question"] = q;
        }

        /// <summary>
        /// checks that Gameboard exists in the session and redirects to the error page if it is missing
        /// </summary>
        private void sessionCheck()
        {
            if (Session["Gameboard"] == null)
                Response.Redirect("Error.aspx");

        }

    }
}