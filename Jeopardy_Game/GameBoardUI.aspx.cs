using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DatabaseConnection;

namespace Jeopardy_Game
{
    public partial class GameBoardUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Gameboard gb = (Gameboard)Session["Gameboard"];
            //updateGameboard(gb);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            drawGameboard(gb);
            updateGameboard(gb);
           
            if (Session["RoundEnd"]==null)
            {
                DateTime now = DateTime.Now;
                Session["RoundEnd"] = now.AddMinutes(15);
            }
            updateRoundInfo();

        }
        

        private void Cell_Click(object sender, EventArgs e)
        {
            JeopardyButton btn = (JeopardyButton)sender;
            Gameboard gb = (Gameboard)Session["Gameboard"];
            gb.getQuestion(btn.category, btn.dollarValue).display = false;
            Session["Gameboard"] = gb;
            Question q = gb.getQuestion(btn.category, btn.dollarValue);
            Session["Question"] = q;
            if(q.wagerActive)
            {
                Response.Redirect("Wager.aspx");
            }
            else
            {
                Response.Redirect("QuestionUI.aspx");
            }


        }

        private void updateGameboard(Gameboard gb)
        {
            bool visibleQuestions = false;
            List<JeopardyButton> buttons = (List<JeopardyButton>)Session["GameBoardButtons"];
            foreach(JeopardyButton b in buttons)
            {
                if(gb.getQuestion(b.category,b.dollarValue).display)
                {
                    b.Visible = true;
                    visibleQuestions = true;
                }
                else
                {
                    b.Visible = false;
                }
            }
            Session["GameBoardButtons"] = buttons;
            if(!visibleQuestions)
            {
                Response.Redirect("EndOfRound.aspx");
            }
            updateGameInfo();
            
        }

        private void drawGameboard(Gameboard gb)
        {
            List<JeopardyButton> buttons = new List<JeopardyButton>();
            TableRow Header = new TableRow();
            tblGameboard.Rows.Add(Header);
            foreach (string category in gb.roundCategories)
            {
                TableCell cell = new TableCell();
                cell.ForeColor = System.Drawing.Color.White;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = category;
                cell.CssClass = "boardHead";
                Header.Cells.Add(cell);
            }
            foreach (int value in gb.values)
            {
                TableRow row = new TableRow();
                tblGameboard.Rows.Add(row);
                foreach (string category in gb.roundCategories)
                {
                    Question q = gb.getQuestion(category, value);
                    TableCell cell = new TableCell();

                    cell.CssClass = "boardCell";
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.VerticalAlign = VerticalAlign.Middle;
                    JeopardyButton b = new JeopardyButton();
                    b.category = category;
                    b.dollarValue = value;
                    b.Text = "$" + value.ToString();
                    cell.Controls.Add(b);
                    b.Click += Cell_Click;
                    buttons.Add(b);
                    b.CssClass = "GameBoardCell";
           
                    row.Cells.Add(cell);
                }
            }
            Session["GameBoardButtons"] = buttons;
            updateGameInfo();
        }
        private void updateGameInfo()
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            lblGameInfo.Text = "Player: "+Session["name"]+ "&nbsp;&nbsp;&nbsp;&nbsp;"+"Score: $" + gb.currentScore;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            DateTime end = (DateTime)Session["RoundEnd"];
            if(end.CompareTo(DateTime.Now)>0)
            {
                //time is remaining
                updateRoundInfo();

            }
            else
            {
                Session["RoundTimeout"] = "";
                Session["RoundEnd"]=null;
                Response.Redirect("EndOfRound.aspx");
            }
        }

        private void updateRoundInfo()
        {
            Gameboard gb = (Gameboard)Session["Gameboard"];
            DateTime end = (DateTime)Session["RoundEnd"];
            TimeSpan remaining = end.Subtract(DateTime.Now);
            string dispSeconds;
            if (remaining.Minutes < 0 || remaining.Seconds < 0)
            {
                remaining = new TimeSpan(0, 0, 0);//don't display negative
            }
            if (remaining.Seconds>9)
            {
                dispSeconds = remaining.Seconds.ToString();
            }
            else
            {
                dispSeconds = "0" + remaining.Seconds.ToString();
            }
            lblRoundInfo.Text = "Round: " + gb.currentRound + "&nbsp;&nbsp;&nbsp;&nbsp;" + "Remaining: " + remaining.Minutes + ":" + dispSeconds;
        }
    }
}