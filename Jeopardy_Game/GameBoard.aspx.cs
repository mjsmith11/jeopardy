using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    public partial class GameBoard : System.Web.UI.Page
    {
        private int[] values = { 100, 200, 300, 400, 500 };
        private const int NUM_CATEGORIES = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TableRow Header = new TableRow();
                tblGameboard.Rows.Add(Header);
                for(int i=1; i<=NUM_CATEGORIES; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = "category" + i;
                    Header.Cells.Add(cell);
                }
                foreach(int value in values)
                {
                    TableRow row = new TableRow();
                    tblGameboard.Rows.Add(row);
                    for(int i=0; i<NUM_CATEGORIES; i++)
                    {
                        TableCell cell = new TableCell();
                        cell.Text = "$"+value.ToString();
                        row.Cells.Add(cell);
                    }
                }                
            }
        }
    }
}