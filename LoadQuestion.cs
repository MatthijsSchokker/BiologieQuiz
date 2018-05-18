using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Schokkers_Biology_Quiz
{
    class LoadQuestion
    {        
        private List<int> selectedTopics;

        string conString;
        DataSet dataS;

        Random rnd = new Random();
        int maxR;
                
        private List<string> answers = new List<string>();
        private List<int> currentQ = new List<int>();
        private object picture = new Object();
        private int currentA;
        
        public List<string> NextQuestion()
        {
            currentQ.Clear();
            answers.Clear();

            if (dataS == null)
            {
                //========================================
                // Connectie met vragendatabase
                //========================================

                DatabaseConnector connector = new DatabaseConnector();

                conString = Properties.Settings.Default.QuizDataString;

                connector.conStr = conString;

                dataS = connector.AnimalQuizData;

                //========================================
                // opstellen parameters
                //========================================

                maxR = dataS.Tables[0].Rows.Count;
            }

            //========================================
            // bepalen antwoordmogelijkheden
            //========================================

            while (currentQ.Count < 4)
            {
                int rndA = rnd.Next(0, maxR - 1);
                if (!currentQ.Contains(rndA))
                {
                    currentQ.Add(rndA);
                }
            }
            
            foreach (int i in currentQ)
            {
                answers.Add(dataS.Tables[0].Rows[i][1].ToString());
            }

            //========================================
            // kies het goede antwoord
            //========================================

            int cA = rnd.Next(0, 3);

            currentA = currentQ[cA];

            return answers;
        }
                    
        public string CorrectAnswer()
        {
            int cA = currentQ.IndexOf(currentA);
            return answers[cA];
        }

        public object LoadImage()
        {
            picture = (Byte[])(dataS.Tables[0].Rows[currentA][3]);
            return picture;
        }
    }
}
