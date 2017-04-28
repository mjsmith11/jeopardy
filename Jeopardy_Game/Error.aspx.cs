using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    /// <summary>
    /// Page to inform the user of an error condition and allow them to continue from the beginning of the game.
    /// </summary>
    public partial class Error : System.Web.UI.Page
    {
        /// <summary>
        /// clear the session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }

        /// <summary>
        /// send user to the home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}