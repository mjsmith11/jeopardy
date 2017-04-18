using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseConnection;

namespace Jeopardy_Game
{
    public class Question
    {

        public int value { get; set; }
        public int wager { get; set; }
        public bool wagerActive { get; set; }
        public bool display { get; set; }
        public QuestionData data { get; set; }

        public Question()
        { }
    }
}