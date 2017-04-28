using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    /// <summary>
    /// page to handle transitioning between rounds of the game or to the end of the game.
    /// </summary>
    public partial class EndOfRound : System.Web.UI.Page
    {
        /// <summary>
        /// Play sound effect if round timed out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// set the text of the socre label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            sessionCheck();
            Gameboard gb = (Gameboard)Session["Gameboard"];
            lblScore.Text = Session["name"].ToString()+": $" + gb.currentScore;
        }

        /// <summary>
        /// Setup the gameboard for the next round and redirect to the appropriate page to begin the next round.
        /// May redirect to EndGame.aspx if the player does not qualify to play final jeopardy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            bool result = gb.SetupNextRound();
            Session["Gameboard"] = gb;
            if (result)
            {
                if (gb.currentRound == 2)
                {
                    Response.Redirect("GameBoardUI.aspx");
                }
                else if (gb.currentRound == 3 && gb.currentScore > 0)
                {
                    Response.Redirect("Wager.aspx");
                }
                else
                {
                    Response.Redirect("EndGame.aspx");
                }
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }


        /// <summary>
        /// Checking the session for name and Gameboard and raising an Error if either is null
        /// </summary>
        private void sessionCheck()
        {
            if (Session["name"] == null)
                Response.Redirect("Error.aspx");
            if (Session["Gameboard"] == null)
                Response.Redirect("Error.aspx");

        }
    }
}