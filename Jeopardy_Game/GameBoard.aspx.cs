using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Jeopardy_Game
{
    public partial class GameBoard : System.Web.UI.Page
    {
        private int[] values = { 100, 200, 300, 400, 500 };
        private const int NUM_CATEGORIES = 6;
        private Hashtable cells;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cells = new Hashtable();
                TableRow Header = new TableRow();
                tblGameboard.Rows.Add(Header);
                for(int i=1; i<=NUM_CATEGORIES; i++)
                {
                    TableCell cell = new TableCell();
                    cells[i] = new Hashtable();
                    ((Hashtable)cells[i])["Header"] = cell;
                    cell.ForeColor = System.Drawing.Color.White;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.VerticalAlign = VerticalAlign.Middle;
                    cell.Text = "category" + i;
                    Header.Cells.Add(cell);
                }
                foreach(int value in values)
                {
                    TableRow row = new TableRow();
                    tblGameboard.Rows.Add(row);
                    for(int i=1; i<=NUM_CATEGORIES; i++)
                    {
                        //GameBoardCell cell = new GameBoardCell();
                        //cell.Click += Cell_Click;
                        TableCell cell = new TableCell();
                        Button b = new Button();
                        b.Text = "$" + value.ToString();
                        //b.
                        b.Click += Cell_Click;
                        Page.Controls.Add(b);
                        cell.Controls.Add(b);
                        ((Hashtable)cells[i])[value] = cell; 
                        //cell.Text = "$"+value.ToString();
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.VerticalAlign = VerticalAlign.Middle;
                        row.Cells.Add(cell);
                    }
                }
                Session["rows"] = tblGameboard.Rows;
            }
            else
            {
                TableRowCollection myrows = (TableRowCollection)Session["rows"];
                Response.Write("before loop");
                for(int i=0; i<myrows.Count; i++)
                {
                    tblGameboard.Rows.Add(myrows[i]);
                    Response.Write(i);
                }
            }

        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Response.Write("HELLO!");
        }

        protected TableCell getCell(int category, int value)
        {
            return ((TableCell)((Hashtable)cells[category])[value]);
        }

    }
}