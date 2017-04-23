#define DEBUG
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace Jeopardy_Game
{
    public class Gameboard
    {
        public int currentRound { get; set; }
        public int currentScore { get; set; }
        private Hashtable questions;
        public List<string> categories { get; }
        public List<int> values { get; }

        public Gameboard()
        {
            currentRound = 0;
            currentScore = 0;
            questions = new Hashtable();
            categories = new List<string>();
            values = new List<int>();
            for(int i=100; i<=500; i+=100)
            {
                values.Add(i);
            }
#if(DEBUG)
            populateTestQuestions();
#endif

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

        public void SetupNextRound()
        {
            if (currentRound == 0)
                setupFirstRound();
            else if (currentRound == 1)
                setupSecondRound();
            else if (currentRound == 2)
                setupFinalJeopardy();
        }

        private void setupFirstRound()
        {
            currentRound = 1;
            throw new NotImplementedException();
        }

        private void setupSecondRound()
        {
            currentRound = 2;
            throw new NotImplementedException();
        }

        private void setupFinalJeopardy()
        {
            currentRound = 3;
            throw new NotImplementedException();
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
            getQuestion(categories[0], 200).wagerActive = true;
        }
    }
}