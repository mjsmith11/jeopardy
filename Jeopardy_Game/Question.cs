using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseConnection;

namespace Jeopardy_Game
{
    /// <summary>
    /// contains data about questions used during game play
    /// </summary>
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
        /// <summary>
        /// true if a wager is being used for this question false otherwise
        /// </summary>
        public bool wagerActive { get; set; }
        /// <summary>
        /// true if the question should be displayed on the gameboard and false otherwise
        /// </summary>
        public bool display { get; set; }
        /// <summary>
        /// database data for this question
        /// </summary>
        public QuestionData data { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public Question()
        { }

        /// <summary>
        /// initializes default values
        /// </summary>
        /// <param name="value">value for this question in dollars</param>
        /// <param name="data">database data for this question</param>
        public Question(int value, QuestionData data)
        {
            this.value = value;
            this.wager = 0;
            this.wagerActive = false;
            this.display = true;
            this.data = data;
        }

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