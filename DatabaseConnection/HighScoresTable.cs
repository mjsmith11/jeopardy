using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DatabaseConnection
{
    public class HighScoresTable : DBTable
    {
        /// <summary>
        /// the number of high scores to track
        /// </summary>
        public static int NUM_HIGH_SCORES = 3;

        /// <summary>
        /// No argument constructor
        /// </summary>
        public HighScoresTable()
        {
        }

        /// <summary>
        /// Create a table for high scores with the following attributes
        /// high_score_id - INT (Primary Key)
        /// high_score - INT
        /// high_score_name - string
        /// high_score_game INT
        /// </summary>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public override bool createTable()
        {
            string query = "CREATE TABLE IF NOT EXISTS high_scores (" +
                "high_score_id INT NOT NULL AUTO_INCREMENT," +
                "high_score INT NOT NULL," +
                "high_score_name varchar(50) NOT NULL," +
                "high_score_game INT," +
                "PRIMARY KEY (high_score_id)" +
                ")";
            bool success;
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.closeConnection();
                success = true;
            }
            else
            {
                this.errorMessage = "Failed to open database connection";
                success = false;
            }

            return success;
        }

        public override bool deleteRecord(int primaryKey)
        {
            bool success;
            string qry = "DELETE FROM high_scores WHERE high_score_id=@h";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@h", primaryKey);
                int rowsAffected = cmd.ExecuteNonQuery();
                this.closeConnection();
                success = true;
                if(rowsAffected==0)
                {
                    success = false;
                    errorMessage = "No rows were deleted";
                }
            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }
            return success;
        }

        /// <summary>
        /// Inserts a record into the database. This method will ignore the ID property of the passed object and assign an available unique id
        /// </summary>
        /// <param name="record">HighScoresTable object to be added</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public override bool insertRecord(object record)
        {
            if(!(record is HighScoreData))
            {
                errorMessage = "Record is not HighScoreData type";
                return false;
            }
            bool success;
            string qry = "INSERT INTO high_scores(high_score, high_score_name, high_score_game) VALUES(@a, @b, @c)";
            
            if(this.openConnection())
            {
                HighScoreData d = (HighScoreData)record;
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@a", d.score);
                cmd.Parameters.AddWithValue("@b", d.name);
                cmd.Parameters.AddWithValue("@c", d.gameID);
                int rowsAffected = cmd.ExecuteNonQuery();
                this.closeConnection();
                success = true;

                if (rowsAffected == 0)
                {
                    success = false;
                    errorMessage = "No rows were inserted";
                }

            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }

            return success;
        }
        /// <summary>
        /// reads the entire high score table
        /// </summary>
        /// <returns>List of HighScoreData objects. Each object in the list is a record from the table</returns>
        public override List<object> readAll()
        {
            List<object> records = new List<object>();
            string qry = "SELECT * FROM high_scores";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    records.Add(new HighScoreData(Int32.Parse(dataReader["high_score_id"].ToString()), Int32.Parse(dataReader["high_score"].ToString()), dataReader["high_score_name"].ToString(), Int32.Parse(dataReader["high_score_game"].ToString())));
                }
                dataReader.Close();
                this.closeConnection();
            }
            else
            {
                errorMessage = "Failed to open database connection";
            }
            return records;
        }
        /// <summary>
        /// Attempts to select one record from the database
        /// </summary>
        /// <param name="primaryKey">high_score_id of the record to select</param>
        /// <returns>The selected record as a HighScoreData object</returns>
        public override object readById(int primaryKey)
        {
            HighScoreData result;
            string qry = "SELECT * FROM high_scores WHERE high_score_id=@h";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@h", primaryKey);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if(dataReader.Read()) //this condition is typically seen in a while structure, but do to the WHERE clause, there will be either 1 or 0 results
                {
                    result = new HighScoreData(Int32.Parse(dataReader["high_score_id"].ToString()), Int32.Parse(dataReader["high_score"].ToString()), dataReader["high_score_name"].ToString(), Int32.Parse(dataReader["high_score_game"].ToString()));
                }
                else
                {
                    result = null;
                    errorMessage = "No matching record found";
                }
                dataReader.Close();
                this.closeConnection();
            }
            else
            {
                result = null;
                errorMessage = "Failed to open database connection";
            }
            return result;
        }

        /// <summary>
        /// Updates the record whose primary key matches the id of the passed object and sets all attributes to match the passed object
        /// </summary>
        /// <param name="record">A HighScoreData object representing how the record should be updated</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public override bool updateRecord(object record)
        {
            if (!(record is HighScoreData))
            {
                errorMessage = "Record is not HighScoreData type";
                return false;
            }
            bool success;
            string qry = "UPDATE high_scores SET high_score=@a, high_score_name=@b, high_score_game=@c WHERE high_score_id=@d";
            if(this.openConnection())
            {
                HighScoreData data = (HighScoreData)record;
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@a", data.score);
                cmd.Parameters.AddWithValue("@b", data.name);
                cmd.Parameters.AddWithValue("@c", data.gameID);
                cmd.Parameters.AddWithValue("@d", data.ID);
                int rowsAffected = cmd.ExecuteNonQuery();

                success = true;
                if(rowsAffected == 0)
                {
                    success = false;
                    errorMessage = "No rows were updated";
                }
            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }
            return success;
        }

        /// <summary>
        /// This will updated the high scores table considering the high score data passed in. This can include adding the passed
        /// high score data to the table and removing a score from the table that has been beaten.
        /// </summary>
        /// <param name="data">Data to consider for the high score table</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public bool updateIfQualified(HighScoreData data)
        {
            bool success;
            if(this.openConnection())
            {
                string query = "SELECT COUNT(*) as count, MIN(high_score) as min_score FROM high_scores;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int count, min_score;
                dataReader.Read();
                count = Int32.Parse(dataReader["count"].ToString());
                if (count == 0)
                {
                    min_score = Int32.MaxValue;
                }
                else
                {
                    min_score = Int32.Parse(dataReader["min_score"].ToString());
                }
        
                dataReader.Close();
                this.closeConnection();

                success = true;
                if(count<NUM_HIGH_SCORES)
                {
                    //if there are less records in the data than the number of high scores we are maintaining
                    success &= this.insertRecord(data); //success is true only if it was already true and insertRecord returns true
                }
                else if(data.score>min_score)
                {
                    //if the table contains at least the number of scores we are tracking, but this score exceeds the lowest one
                    success &= this.insertRecord(data); //success is true only if it was already true and insertRecord returns true
                    success &= this.deleteBeatenHighScore(min_score); //success is true only if it was already true and insertRecord returns true
                }

            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }

            return success;
        }

        /// <summary>
        /// Deletes exactly one record with the given score. There is no guarantee of which record will be deleted if multiple records exist with the passed score
        /// </summary>
        /// <param name="beatenScore">High score that has been beaten and needs deleted</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public bool deleteBeatenHighScore(int beatenScore)
        {
            bool success;
            string query = "DELETE FROM high_scores WHERE high_score = @s LIMIT 1";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@s", beatenScore);
                int rowsAffected = cmd.ExecuteNonQuery();
                this.closeConnection();
                success = true;
                if (rowsAffected != 1)
                {
                    success = false;
                    errorMessage = "Delete did not affect exactly one row";
                }
            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }
            return success;
        }
    }
}
