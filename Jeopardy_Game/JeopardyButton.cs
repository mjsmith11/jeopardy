using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    public class JeopardyButton : Button
    {
        public int dollarValue { get; set; }
        public string category { get; set; }
    }
}