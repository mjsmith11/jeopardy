using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JeoparyGame
{
    public partial class Game : System.Web.UI.Page
    {

        static int count=20;
        protected void Page_Load(object sender, EventArgs e)
        {

            string field1 = (string)(Session["name"]);

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // timelbl.Text =DateTime.Now.ToString();
            count = count - 1 ;
            if(count==0)
            {
               count = 20;

                string field1 = (string)(Session["name"]);
                Session["name"] = field1;

                Response.Redirect("GameBoardUI.aspx");
            }

            if(count>0)
            timelbl.Text = count.ToString();
        }
    }
}