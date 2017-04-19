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
            Gameboard gb;
            if (!IsPostBack)
            {
                gb = new Gameboard();
                Session["Gameboard"] = gb;
            }
            else
            {
                gb = (Gameboard)Session["Gameboard"];
            }
            drawGameboard(gb);
                
            
        }
        

        private void Cell_Click(object sender, EventArgs e)
        {
            JeopardyButton btn = (JeopardyButton)sender;
            Gameboard gb = (Gameboard)Session["Gameboard"];
            gb.getQuestion(btn.category, btn.dollarValue).display = false;
            updateGameboard(gb);
            Session["Gameboard"] = gb;


        }

        private void updateGameboard(Gameboard gb)
        {
           
            List<JeopardyButton> buttons = (List<JeopardyButton>)Session["GameBoardButtons"];
            foreach(JeopardyButton b in buttons)
            {
                if(gb.getQuestion(b.category,b.dollarValue).display)
                {
                    b.Visible = true;
                }
                else
                {
                    b.Visible = false;
                }
            }
            Session["GameBoardButtons"] = buttons;
        }

        private void drawGameboard(Gameboard gb)
        {
            List<JeopardyButton> buttons = new List<JeopardyButton>();
            TableRow Header = new TableRow();
            tblGameboard.Rows.Add(Header);
            foreach (string category in gb.categories)
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
                foreach (string category in gb.categories)
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
        }
    }
}