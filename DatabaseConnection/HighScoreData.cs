using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    /// <summary>
    /// A data type for working with high scores in the database
    /// </summary>
    public class HighScoreData
    {
        /// <summary>
        /// Unique id of the high score record
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// the score acheived
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// name of the player acheiving the score
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// identifier of the game in which the score was acheived
        /// </summary>
        public int gameID { get; set; }

        /// <summary>
        /// 4 argument constuctor
        /// </summary>
        /// <param name="ID">initial value for ID</param>
        /// <param name="score">initial value for score</param>
        /// <param name="name">initial value for name</param>
        /// <param name="gameID">initial value for gameID</param>
        public HighScoreData(int ID, int score, string name, int gameID)
        {
            this.ID = ID;
            this.score = score;
            this.name = name;
            this.gameID = gameID;
        }


        public HighScoreData(int score, string name, int gameID)
        {
            this.ID = 0;
            this.score = score;
            this.name = name;
            this.gameID = gameID;

        }
    }
}
