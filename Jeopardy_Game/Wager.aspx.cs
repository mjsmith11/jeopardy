using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Jeopardy_Game
{
    public partial class Wager : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            Question q;
            
            if (gb.currentRound <= 2)
            {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

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
        
        private void setWager(int value)
        {
            Question q = null;
            if (Session["Question"] != null)
                q = (Question)Session["Question"];
            q.wager = value;
            Session["Question"] = q;
        }


    }
}