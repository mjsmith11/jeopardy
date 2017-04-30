using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    /// <summary>
    /// Add dollar value and question category to buttons
    /// </summary>
    public class JeopardyButton : Button
    {
        /// <summary>
        /// value of the question attached to this button
        /// </summary>
        public int dollarValue { get; set; }
        /// <summary>
        /// category of the question attached to this button
        /// </summary>
        public string category { get; set; }
    }
}