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

        public bool SetupNextRound()
        {
            if (currentRound == 0)
                return setupFirstRound();
            else if (currentRound == 1)
                return setupSecondRound();
            else if (currentRound == 2)
                return setupFinalJeopardy();
            else
            {
                currentRound = -1;
                return false;
            }
        }

        private bool setupFirstRound()
        {
            currentRound = 1;
            bool needPicture = false;
            List<int> usedQuestions = new List<int>();

            //select categories
            gameCategories = getCategories();
            if (gameCategories == null || gameCategories.Count < 13)
                return false;

            //randomize the order
            Random rnd = new Random();
            gameCategories = gameCategories.OrderBy(x => rnd.Next()).ToList();

            //choose questions from first 6 categories
            for(int i = 0; i<6; i++)
            {
                string currentCategory = gameCategories[i];
                List<object> categoryQuestions = getUnusedQuestionsByCategory(currentCategory);
                if (categoryQuestions == null)
                    return false;
                //reset the questions if we do not have enough in this to populate this category
                if (!checkForAllLevels(categoryQuestions))
                {
                    bool result = resetQuestionUsage();
                    if (result == false)
                        return false;
                    categoryQuestions = getUnusedQuestionsByCategory(currentCategory);
                    if (categoryQuestions == null)
                        return false;
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
            bool usedResult = updateUsedQuestions(usedQuestions);
            if (usedResult == false)
                return false;


            //choose a daily double
            string ddCategory = roundCategories[rnd.Next(6)];
            int ddValue = values[rnd.Next(5)];
            getQuestion(ddCategory, ddValue).wagerActive = true;

            return true;

        }

        private bool setupSecondRound()
        {
            currentRound = 2;
            questions.Clear();
            roundCategories.Clear();
            values = (from v in values select 2 * v).ToList();

            bool needPicture = false;
            List<int> usedQuestions = new List<int>();
            Random rnd = new Random();

            //choose questions from second 6 categories
            for (int i = 6; i < 12; i++)
            {
                string currentCategory = gameCategories[i];
                List<object> categoryQuestions = getUnusedQuestionsByCategory(currentCategory);
                if (categoryQuestions == null)
                    return false;
                //reset the questions if we do not have enough in this to populate this category
                if (!checkForAllLevels(categoryQuestions))
                {
                    bool result = resetQuestionUsage();
                    if (result == false)
                        return false;
                    categoryQuestions = getUnusedQuestionsByCategory(currentCategory);
                    if (categoryQuestions == null)
                        return false;
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
            bool usedResult = updateUsedQuestions(usedQuestions);
            if (usedResult == false)
                return false;

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

            return true;
        }

        private bool setupFinalJeopardy()
        {
            currentRound = 3;
            questions.Clear();
            roundCategories.Clear();
            values.Clear();

            List<int> usedQuestions = new List<int>();
            Random rnd = new Random();

            string category = gameCategories[12]; //use the 13th category
            List<object> categoryQuestions = getUnusedQuestionsByCategory(category);
            if (categoryQuestions == null)
                return false;

            List<object> candidateQuestions = (from qu in categoryQuestions where (((QuestionData)qu).level >= 4) select qu).ToList();
            if(candidateQuestions.Count() == 0)
            {
                bool result = resetQuestionUsage();
                if (result == false)
                    return false;
                categoryQuestions = getUnusedQuestionsByCategory(category);
                if (categoryQuestions == null)
                    return false;
                candidateQuestions = (from qu in categoryQuestions where (((QuestionData)qu).level >= 4) select qu).ToList();
            }

            QuestionData data = (QuestionData)candidateQuestions[rnd.Next(candidateQuestions.Count())];
            Question q = new Question(0, data);
            q.wagerActive = true;

            List<int> usedQuestion = new List<int>();
            usedQuestion.Add(q.data.question_id);
            bool usedResult = updateUsedQuestions(usedQuestion);
            if (usedResult == false)
                return false;
            questions["final"] = q;

            return true;           
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

        private List<string> getCategories()
        {
            QuestionTable qt = new QuestionTable();
            List<string> result = null;
            int attempts = 0;
            while(result == null)
            {
                result = qt.getCategories();
                attempts++;
                if (attempts >= 3)
                    break;
            }
            return result;
        }

        private List<object> getUnusedQuestionsByCategory(string category)
        {
            QuestionTable qt = new QuestionTable();
            List<object> result = null;
            int attempts = 0;
            while (result == null)
            {
                result = qt.getUnusedQuestionsByCategory(category);
                attempts++;
                if (attempts >= 3)
                    break;
            }
            return result;
        }

        private bool resetQuestionUsage()
        {
            QuestionTable qt = new QuestionTable();
            bool result = false;
            int attempts = 0;
            while (result == false)
            {
                result = qt.resetQuestionUsage();
                attempts++;
                if (attempts >= 3)
                    break;
            }
            return result;
        }

        private bool updateUsedQuestions(List<int> ids)
        {
            QuestionTable qt = new QuestionTable();
            bool result = false;
            int attempts = 0;
            while (result == false)
            {
                result = qt.markQuestionsUsed(ids);
                attempts++;
                if (attempts >= 3)
                    break;
            }
            return result;
        }
    }
}