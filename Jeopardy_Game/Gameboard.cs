using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace Jeopardy_Game
{
    public class Gameboard
    {
        public int currentRound { get; }
        public int currentScore { get; set; }
        private Hashtable questions;
        public List<string> categories { get; }
        public List<int> values { get; }

        public Gameboard()
        {
            currentRound = 1;
            currentScore = 0;
            questions = new Hashtable();
            categories = new List<string>();
            values = new List<int>();
            for(int i=200; i<=1000; i+=200)
            {
                values.Add(i);
            }
            populateTestQuestions();

        }

        public void addQuestion(Question q)
        {
            string key = q.data.category + q.value;
            questions[key] = q;
            if(!categories.Contains(q.data.category))
            {
                categories.Add(q.data.category);
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
                    q.data = new DatabaseConnection.QuestionData("?????", "Right", "W1", "W2", "W3", "Category #" + i, j / 100, "ref", false, "");
                    this.addQuestion(q);
                }
            }
        }
    }
}