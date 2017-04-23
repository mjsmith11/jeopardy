using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JeoparyGame
{
    public partial class Name : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            //this is your first validation
            if (string.IsNullOrWhiteSpace(nametxtbox.Text))
            {
                lblError.Text = "Name cannot be blank";
                return;
            }


            Session["name"] = nametxtbox.Text;
            //setup gameboard in session and redirect to game
        }

    }
}