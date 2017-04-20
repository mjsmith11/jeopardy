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

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this is your first validation
            if (string.IsNullOrWhiteSpace(nametxtbox.Text))
            {
                Label3error.Text = "Name shouldn't be blank";
                return;
            }


            Session["name"] = nametxtbox.Text;
            Response.Redirect("CategoryPage.aspx");
        }

        protected void nametxtbox_TextChanged(object sender, EventArgs e)
        {
            Label3error.Text = string.Empty;
        }
    }
}