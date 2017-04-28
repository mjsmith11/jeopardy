using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseConnection;

namespace Jeopardy_Game
{
    public partial class EndGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Random r = new Random();
                Gameboard gb = (Gameboard)Session["Gameboard"];
                HighScoreData hsd = new HighScoreData(gb.currentScore, Session["name"].ToString(), r.Next(100000));
                updateHighScores(hsd);
                List<object> scores = getHighScores();
                if (scores == null)
                    Response.Redirect("Error.aspx");
                displayHighScores(scores);
                lblScore.Text = "Your Score: $" + gb.currentScore;
                Session.RemoveAll();
            }
        }

        protected void btnPlayAgain_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        private void displayHighScores(List<object> highScores)
        {
            string html = "<table class='highScores'>";

            foreach(object hs in highScores)
            {
                HighScoreData h = (HighScoreData)hs;
                html += "<tr>";
                html += "<td>" + h.name + "</td>";
                html += "<td>$" + h.score + "</td>";
                html += "</tr>";
            }

            html += "</table>";

            divHighScoreArea.InnerHtml = html;
        }

        /// <summary>
        /// Try up to 3 times to get high scores
        /// </summary>
        /// <returns>List of all high scores or null if all three attempts fail</returns>
        private List<object> getHighScores()
        {
            HighScoresTable hst = new HighScoresTable();
            hst.errorMessage = "";
            bool hadSuccess = false;
            int attempts = 0;
            
            while(!hadSuccess)
            {
                List<object> result = hst.readAll();
                if(hst.errorMessage.Equals(""))
                {
                    return result;
                }
                attempts++;
                if (attempts >= 3)
                    break;
            }
            return null;
        }

        /// <summary>
        /// Make up to 3 times to executeUpdateIfQualified and quietly return if all 3 attempts fail
        /// </summary>
        /// <param name="hsd">High Score Data</param>
        private void updateHighScores(HighScoreData hsd)
        {
            HighScoresTable hst = new HighScoresTable();
            bool success = hst.updateIfQualified(hsd);
            int attempts = 1;
            while(!success)
            {
                success = hst.updateIfQualified(hsd);

                attempts++;
                if (attempts >= 3)
                    break;
            }
        }
    }
}