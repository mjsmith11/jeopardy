using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jeopardy_Game
{
    public class GameBoardCell : TableCell, IPostBackEventHandler, INamingContainer
    {
        private static readonly object click_event = new object();

        public GameBoardCell()
        {

        }

        //public handles for adding and removing functions to be called on the click event
        public event EventHandler Click
        {
            add
            {
                Events.AddHandler(click_event, value);
            }
            remove
            {
                Events.RemoveHandler(click_event, value);
            }
        }

        protected void OnClick(EventArgs e)
        {
            EventHandler h = Events[click_event] as EventHandler;
            if (h!=null)
            {
                h(this, e);
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.ClientScript.GetPostBackEventReference(this, "custom_click"));
        }

        void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "custom_click")
            {
                this.OnClick(EventArgs.Empty);
            }
        }
    }
}