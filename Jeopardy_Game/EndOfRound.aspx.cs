using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    public partial class EndOfRound : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["RoundTimeout"] != null)
            { 
                if (!IsPostBack)
                {
                    divAudio.InnerHtml = "<audio autoplay=\"autoplay\" src=\"./Resources/sounds/end_of_rnd.wav\"></audio>";
                    Session["RoundTimeout"] = null;
                }
                else
                {
                    divAudio.InnerHtml = "";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            lblScore.Text = Session["name"].ToString()+": $" + gb.currentScore;
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            gb.SetupNextRound();
            Session["Gameboard"] = gb;
            if(gb.currentRound==2)
            {
                Response.Redirect("GameBoardUI.aspx");
            }
            else if(gb.currentRound==3 && gb.currentScore>0)
            {
                Response.Redirect("Wager.aspx");
            }
            else
            {
                Response.Redirect("EndGame.aspx");
            }
        }
    }
}