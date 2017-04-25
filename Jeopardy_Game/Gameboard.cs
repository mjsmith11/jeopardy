using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using DatabaseConnection;

namespace Jeopardy_Game
{
    public class Gameboard
    {
        public int currentRound { get; set; }
        public int currentScore { get; set; }
        private Hashtable questions;
        public List<string> roundCategories { get; }
        private List<string> gameCategories { get; set; }
        public List<int> values { get; set; }

        public Gameboard()
        {
            currentRound = 0;
            currentScore = 0;
            questions = new Hashtable();
            roundCategories = new List<string>();
            values = new List<int>();
            for(int i=100; i<=500; i+=100)
            {
                values.Add(i);
            }
        }

        public void addQuestion(Question q)
        {
            string key = q.data.category + q.value;
            questions[key] = q;
            if(!roundCategories.Contains(q.data.category))
            {
                roundCategories.Add(q.data.category);
            }
        }

        public Question getQuestion(string category, int value)
        {
            string key = category + value;
            return (Question)questions[key];
        }

        public void increaseScore(int amount)
        {
            currentScore += amount;
        }

        public void decreaseScore(int amount)
        {
            currentScore -= amount;
        }

        public void SetupNextRound()
        {
            if (currentRound == 0)
                setupFirstRound();
            else if (currentRound == 1)
                setupSecondRound();
            else if (currentRound == 2)
                setupFinalJeopardy();
            else
            {
                currentRound = -1;
            }
        }

        private void setupFirstRound()
        {
            currentRound = 1;
            bool needPicture = false;
            QuestionTable qt = new QuestionTable();
            List<int> usedQuestions = new List<int>();

            //select categories
            gameCategories = qt.getCategories();

            //randomize the order
            Random rnd = new Random();
            gameCategories = gameCategories.OrderBy(x => rnd.Next()).ToList();

            //choose questions from first 6 categories
            for(int i = 0; i<6; i++)
            {
                string currentCategory = gameCategories[i];
                List<object> categoryQuestions = qt.getUnusedQuestionsByCategory(currentCategory);
                //reset the questions if we do not have enough in this to populate this category
                if (!checkForAllLevels(categoryQuestions))
                {
                    qt.resetQuestionUsage();
                    categoryQuestions = qt.getUnusedQuestionsByCategory(currentCategory);
                }

                //choose a question for each level
                for(int j=1; j<=5; j++)
                {
                    List<object> questionsAtLevel = (from q in categoryQuestions where (((QuestionData)q).level == j) select q).ToList();
                    List<object> pictureQuestions = questionsAtLevel.Where(q => (!((QuestionData)q).image_file.Equals(""))).ToList();
                    Question question;
                    if(pictureQuestions.Count>0 && needPicture)
                    {
                        int index = rnd.Next(pictureQuestions.Count);
                        question = new Question(100 * j, (QuestionData)pictureQuestions[index]);
                        needPicture = false;
                    }
                    else
                    {
                        int index = rnd.Next(questionsAtLevel.Count);
                        question = new Jeopardy_Game.Question(100 * j, (QuestionData)questionsAtLevel[index]);
                    }
                    this.addQuestion(question);
                    usedQuestions.Add(question.data.question_id);
                }
            }

            //mark questions used
            qt.markQuestionsUsed(usedQuestions);

            //choose a daily double
            string ddCategory = roundCategories[rnd.Next(6)];
            int ddValue = values[rnd.Next(5)];
            getQuestion(ddCategory, ddValue).wagerActive = true;

        }

        private void setupSecondRound()
        {
            currentRound = 2;
            questions.Clear();
            roundCategories.Clear();
            values = (from v in values select 2 * v).ToList();

            bool needPicture = false;
            QuestionTable qt = new QuestionTable();
            List<int> usedQuestions = new List<int>();
            Random rnd = new Random();

            //choose questions from second 6 categories
            for (int i = 6; i < 12; i++)
            {
                string currentCategory = gameCategories[i];
                List<object> categoryQuestions = qt.getUnusedQuestionsByCategory(currentCategory);
                //reset the questions if we do not have enough in this to populate this category
                if (!checkForAllLevels(categoryQuestions))
                {
                    qt.resetQuestionUsage();
                    categoryQuestions = qt.getUnusedQuestionsByCategory(currentCategory);
                }

                //choose a question for each level
                for (int j = 1; j <= 5; j++)
                {
                    List<object> questionsAtLevel = (from q in categoryQuestions where (((QuestionData)q).level == j) select q).ToList();
                    List<object> pictureQuestions = questionsAtLevel.Where(q => (!((QuestionData)q).image_file.Equals(""))).ToList();
                    Question question;
                    if (pictureQuestions.Count > 0 && needPicture)
                    {
                        int index = rnd.Next(pictureQuestions.Count);
                        question = new Question(200 * j, (QuestionData)pictureQuestions[index]);
                        needPicture = false;
                    }
                    else
                    {
                        int index = rnd.Next(questionsAtLevel.Count);
                        question = new Jeopardy_Game.Question(200 * j, (QuestionData)questionsAtLevel[index]);
                    }
                    this.addQuestion(question);
                    usedQuestions.Add(question.data.question_id);
                }
            }

            //mark questions used
            qt.markQuestionsUsed(usedQuestions);

            //choose a daily double
            string ddCategory = roundCategories[rnd.Next(6)];
            int ddValue = values[rnd.Next(5)];
            getQuestion(ddCategory, ddValue).wagerActive = true;

            //choose a second daily double that is different than the first one
            string dd2Category = ddCategory;
            int dd2Value = ddValue;
            while((dd2Category.Equals(ddCategory))&&(dd2Value==ddValue))
            {
                dd2Category = roundCategories[rnd.Next(6)];
                dd2Value = values[rnd.Next(5)];
            }
            getQuestion(dd2Category, dd2Value).wagerActive = true;
        }

        private void setupFinalJeopardy()
        {
            currentRound = 3;
            questions.Clear();
            roundCategories.Clear();
            values.Clear();

            QuestionTable qt = new QuestionTable();
            List<int> usedQuestions = new List<int>();
            Random rnd = new Random();

            string category = gameCategories[12]; //use the 13th category
            List<object> categoryQuestions = qt.getUnusedQuestionsByCategory(category);

            List<object> candidateQuestions = (from qu in categoryQuestions where (((QuestionData)qu).level >= 4) select qu).ToList();
            if(candidateQuestions.Count() == 0)
            {
                qt.resetQuestionUsage();
                categoryQuestions = qt.getUnusedQuestionsByCategory(category);
                candidateQuestions = (from qu in categoryQuestions where (((QuestionData)qu).level >= 4) select qu).ToList();
            }

            QuestionData data = (QuestionData)candidateQuestions[rnd.Next(candidateQuestions.Count())];
            Question q = new Question(0, data);
            q.wagerActive = true;
            questions["final"] = q;            
        }

        public Question getFinalQuestion()
        {
            return (Question)questions["final"];
        }
        private bool checkForAllLevels(List<object> questionList)
        {
            //check for each level
            for(int i=1; i<=5; i++)
            {
                List<object> questionsAtLevel = (from q in questionList where (((QuestionData)q).level == i) select q).ToList();
                if (questionsAtLevel.Count == 0)
                    return false;
            }
            return true;
        }

        private void populateTestQuestions()
        {
            for(int i=0; i<6; i++)
            {
                for(int j=200; j<=1000; j+=200)
                {
                    Question q = new Question();
                    q.display = true;
                    q.value = j;
                    q.wagerActive = false;
                    q.data = new DatabaseConnection.QuestionData("?????", "Right", "W1", "W2", "W3", "Category #" + i, j / 100, "ref", false, "",true);
                    this.addQuestion(q);
                }
            }
            getQuestion(roundCategories[0], 200).wagerActive = true;
        }
    }
}