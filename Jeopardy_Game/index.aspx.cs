using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    /// <summary>
    /// home page of the game that asks the user for his or her name.
    /// </summary>
    public partial class index : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Validates the user's entered name. Starts the game if it is valid and displayes an error if invalid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            Gameboard gb = new Gameboard();
            bool result = gb.SetupNextRound();
            Session["Gameboard"] = gb;
            if (result)
                Response.Redirect("GameboardUI.aspx");
            else
                Response.Redirect("Error.aspx");

        }
    }
}