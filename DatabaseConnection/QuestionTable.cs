using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;

namespace DatabaseConnection
{
    /// <summary>
    /// Class for intearacting with the questions table in the database
    /// </summary>
    public class QuestionTable : DBTable
    {
        /// <summary>
        /// no argument constructor
        /// </summary>
        public QuestionTable()
        {
        }
        /// <summary>
        /// Creates a table for questions with the following attributes
        /// question_id - INT (Primary Key)
        /// question_text - string
        /// correct_answer - string
        /// wrong_answer_1 - string
        /// wrong_answer_2 - string
        /// wrong_answer_3 - string
        /// category - string
        /// level - int
        /// reference - string
        /// used -bool
        /// image_file string
        /// randomize_answers bool
        /// </summary>
        /// <returns></returns>
        public override bool createTable()
        {
            string query = "CREATE TABLE IF NOT EXISTS questions (" +
                "question_id INT NOT NULL AUTO_INCREMENT," +
                "question_text VARCHAR(256) NOT NULL," +
                "correct_answer VARCHAR(200) NOT NULL," +
                "wrong_answer_1 VARCHAR(200) NOT NULL," +
                "wrong_answer_2 VARCHAR(200) NOT NULL," +
                "wrong_answer_3 VARCHAR(200) NOT NULL," +
                "category VARCHAR(200) NOT NULL," +
                "level INT NOT NULL," +
                "reference VARCHAR(200)," +
                "used BOOL NOT NULL," +
                "image_file VARCHAR(200)," +
                "randomize_answers BOOL NOT NULL," +
                "PRIMARY KEY (question_id)" +
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
        /// <summary>
        /// Deletes one record from the question table based on key
        /// </summary>
        /// <param name="primaryKey">key of the record to delete</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public override bool deleteRecord(int primaryKey)
        {
            bool success;
            string qry = "DELETE FROM questions WHERE question_id=@q";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@q", primaryKey);
                int rowsAffected = cmd.ExecuteNonQuery();
                this.closeConnection();
                success = true;
                if (rowsAffected == 0)
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
        /// Inserts a record into the questions table with attributes from the passed object.
        /// The id in the object will be ignored and an id will be assigned by mysql's auto-increment
        /// </summary>
        /// <param name="record"></param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public override bool insertRecord(object record)
        {
            if (!(record is QuestionData))
            {
                errorMessage = "Record is not QuestionData type";
                return false;
            }
            bool success;
            string qry = "INSERT INTO questions(question_text, correct_answer, wrong_answer_1, wrong_answer_2, wrong_answer_3, category, level, reference, used, image_file, randomize_answers) VALUES(@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k)";

            if (this.openConnection())
            {
                QuestionData d = (QuestionData)record;
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@a", d.question_text);
                cmd.Parameters.AddWithValue("@b", d.correct_answer);
                cmd.Parameters.AddWithValue("@c", d.wrong_answer_1);
                cmd.Parameters.AddWithValue("@d", d.wrong_answer_2);
                cmd.Parameters.AddWithValue("@e", d.wrong_answer_3);
                cmd.Parameters.AddWithValue("@f", d.category);
                cmd.Parameters.AddWithValue("@g", d.level);
                cmd.Parameters.AddWithValue("@h", d.reference);
                cmd.Parameters.AddWithValue("@i", d.used);
                cmd.Parameters.AddWithValue("@j", d.image_file);
                cmd.Parameters.AddWithValue("@k", d.randomize_answers);
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
        /// Reads all records of the questions table. Use care calling this as it will probably have many results. The list will be empty if the table is empty.
        /// </summary>
        /// <returns>QuestionData objects for each row of questions</returns>
        public override List<object> readAll()
        {
            List<object> records;
            string qry = "SELECT * FROM questions";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                records = this.mySQLDataReaderToList(dataReader);
                dataReader.Close();
                this.closeConnection();
            }
            else
            {
                errorMessage = "Failed to open database connection";
                records = null;
            }
            return records;
        }

        /// <summary>
        /// Reads one record from the questions table
        /// </summary>
        /// <param name="primaryKey">key of the record to read</param>
        /// <returns>QuestionData object</returns>
        public override object readById(int primaryKey)
        {
            QuestionData result;
            string qry = "SELECT * FROM questions WHERE question_id=@q";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@q", primaryKey);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                List<object> resultList = this.mySQLDataReaderToList(dataReader);
                if(resultList.Count()==1)
                {
                    result = (QuestionData)resultList[0]; //since the search is by primary key, there will be at most 1 result
                }
                else
                {
                    result = null;
                    errorMessage = "No record found";
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
        /// Updates a record in the questions table. The record with question_id matching the passed object will be updated to the values in the passed object
        /// </summary>
        /// <param name="record">Updated QuestionData object</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public override bool updateRecord(object record)
        {
            if (!(record is QuestionData))
            {
                errorMessage = "Record is not QuestionData type";
                return false;
            }
            bool success;
            string qry = "UPDATE high_scores SET question_text=@a, correct_answer=@b, wrong_answer_1=@c, wrong_answer_2=@d, wrong_answer_3=@e, category=@f, level=@g, reference=@h, used=@i, image_file=@j, randomize_answers=@m WHERE question_id=@k";
            if (this.openConnection())
            {
                QuestionData d = (QuestionData)record;
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@a", d.question_text);
                cmd.Parameters.AddWithValue("@b", d.correct_answer);
                cmd.Parameters.AddWithValue("@c", d.wrong_answer_1);
                cmd.Parameters.AddWithValue("@d", d.wrong_answer_2);
                cmd.Parameters.AddWithValue("@e", d.wrong_answer_3);
                cmd.Parameters.AddWithValue("@f", d.category);
                cmd.Parameters.AddWithValue("@g", d.level);
                cmd.Parameters.AddWithValue("@h", d.reference);
                cmd.Parameters.AddWithValue("@i", d.used);
                cmd.Parameters.AddWithValue("@j", d.image_file);
                cmd.Parameters.AddWithValue("@k", d.question_id);
                cmd.Parameters.AddWithValue("@m", d.randomize_answers);
                int rowsAffected = cmd.ExecuteNonQuery();

                success = true;
                if (rowsAffected == 0)
                {
                    success = false;
                    errorMessage = "No rows were updated";
                }
                this.closeConnection();
            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }
            return success;
        }

        /// <summary>
        /// gets a count of questions that are not marked as used in the database
        /// </summary>
        /// <returns>number of unused questions or -1 in an error condition</returns>
        public int getUnusedQuestionCount()
        {
            int result;
            string query = "SELECT count(*) as c FROM questions WHERE used=0";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read()) //this is typically seen in a while loop, however in this contect the expected result is one or zero rows
                {
                    result = Int32.Parse(dataReader["c"].ToString());
                }
                else
                {
                    result = -1;
                    errorMessage = "Failed to get count";
                }
                this.closeConnection();
            }
            else
            {
                result = -1;
                errorMessage = "Failed to open database connection";
            }
            return result;
        }

        /// <summary>
        /// This method returns all questions to an unused state
        /// </summary>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public bool resetQuestionUsage()
        {
            bool success;
            string query = "UPDATE questions SET used=0";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int rowsAffected = cmd.ExecuteNonQuery();
                this.closeConnection();
                success = true;
                if (rowsAffected == 0)
                {
                    success = false;
                    errorMessage = "No rows were updated";
                }
                this.closeConnection();
            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }
            return success;
        }

        /// <summary>
        /// gets a list of categories represented by the questions in the database
        /// </summary>
        /// <returns>list of categories as strings</returns>
        public List<string> getCategories()
        {
            List<string> categories = new List<string>();
            string query = "SELECT DISTINCT category FROM questions";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    categories.Add(dataReader["category"].ToString());
                }
                this.closeConnection();

            }
            else
            {
                categories = null;
                errorMessage = "Failed to open database connection";
            }
            return categories;
        }

        /// <summary>
        /// Returns a list of all questions that have not been used for a given category.
        /// </summary>
        /// <param name="category">Category to select questions from</param>
        /// <returns>List of question objects</returns>
        public List<object> getUnusedQuestionsByCategory(string category)
        {
            List<object> records;
            string qry = "SELECT * FROM questions WHERE category = @c AND used=0";
            if (this.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.Parameters.AddWithValue("@c", category);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                records = this.mySQLDataReaderToList(dataReader);
                dataReader.Close();
                this.closeConnection();
            }
            else
            {
                errorMessage = "Failed to open database connection";
                records = null;
            }
            return records;
        }
        /// <summary>
        /// sets the used attribute to true for a set of questions
        /// </summary>
        /// <param name="ids">ids of the questions to update </param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public bool markQuestionsUsed(List<int> ids)
        {

            bool success=true;            
            if (this.openConnection())
            {
                foreach (int id in ids)
                {
                    string query = "UPDATE questions SET used=1 WHERE question_id=@a";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@a", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected==0)
                    {
                        success = false;
                        errorMessage = "One or more updates failed";
                    }
                    
                }
                this.closeConnection();
               
            }
            else
            {
                success = false;
                errorMessage = "Failed to open database connection";
            }
            return success;
        }
        

        /// <summary>
        /// Parses a CSV file with no headings and the following columns in order: question_text, correct_answer, wrong_answer1, wrong_answer2,
        /// wrong_answer3, category, level, reference. Each row of the file is inserted as a record in the database that has not been used. This 
        /// method does not support picture questions or questions with answers that cannot be in random order
        /// </summary>
        /// <param name="csvPath">Path to csv file to import</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public bool importRegularQuestionsFromCSV(string csvPath)
        {
            bool result = true; //assume its good unless we see something bad
            TextFieldParser parser = new TextFieldParser(@csvPath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while(!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                string question = fields[0];
                string correct = fields[1];
                string wrong1 = fields[2];
                string wrong2 = fields[3];
                string wrong3 = fields[4];
                string category = fields[5];
                int level = Int32.Parse(fields[6]);
                string reference = fields[7];

                QuestionData q = new QuestionData(question, correct, wrong1, wrong2, wrong3, category, level, reference, false, "", true);
                result &= this.insertRecord(q); //this will change result from true to false but will not change it from false to true
            }
            return result;
        }
        /// <summary>
        /// Creates a List of QuestionData objects read from a MySqlDataReader. The reader must be executed and closed external to this method.
        /// This method readers executed on SELECT * queries on the questions table as it looks for all attributes of the question table.
        /// </summary>
        /// <param name="rdr">Executed data reader</param>
        /// <returns>List of QuestionData objects</returns>
        public List<object> mySQLDataReaderToList(MySqlDataReader rdr)
        {
            List<object> list = new List<object>();
            while (rdr.Read())
            {
                int id = Int32.Parse(rdr["question_id"].ToString());
                string questionText = rdr["question_text"].ToString();
                string correct = rdr["correct_answer"].ToString();
                string wrong1 = rdr["wrong_answer_1"].ToString();
                string wrong2 =rdr["wrong_answer_2"].ToString();
                string wrong3 = rdr["wrong_answer_3"].ToString();
                string category = rdr["category"].ToString();
                int level = Int32.Parse(rdr["level"].ToString());
                string reference = rdr["reference"].ToString();
                bool used = Boolean.Parse(rdr["used"].ToString());
                string image = rdr["image_file"].ToString();
                bool rand = Boolean.Parse(rdr["randomize_answers"].ToString());

                list.Add(new QuestionData(id, questionText, correct, wrong1, wrong2, wrong3, category, level, reference, used, image, rand));
            }
            return list;
        }
    }
}
