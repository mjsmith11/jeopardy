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
                HighScoresTable hst = new HighScoresTable();
                HighScoreData hsd = new HighScoreData(gb.currentScore, Session["name"].ToString(), r.Next(100000));
                hst.updateIfQualified(hsd);
                List<object> scores = hst.readAll();
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
    }
}