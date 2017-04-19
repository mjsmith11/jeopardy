using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseConnection;

namespace Jeopardy_Game
{
    public class Question
    {
        /// <summary>
        /// The question value corresponding to where it should be displayed in the gameboard
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// The amount the player wagered if it is a daily double or final
        /// </summary>
        public int wager { get; set; }
        public bool wagerActive { get; set; }
        public bool display { get; set; }
        public QuestionData data { get; set; }

        public Question()
        { }

        /// <summary>
        /// Returns the wager if it is active or the value otherwise
        /// </summary>
        /// <returns>The amount that this question should affect the player's score</returns>
        public int getValueForScoring()
        {
            return wagerActive ? wager : value;
        }
    }
}