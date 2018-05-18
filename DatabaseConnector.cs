using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Schokkers_Biology_Quiz
{
    class DatabaseConnector
    {
        private SqlConnection myConn;
        private SqlDataAdapter sqlDataAdapter;
        private string connectionString;

        public string conStr
        {
            set { connectionString = value; }
        }

        public System.Data.DataSet AnimalQuizData
        {
            get { return AnimalQuestions(); }
        }

        private System.Data.DataSet AnimalQuestions()
        {
            using (myConn = new SqlConnection(connectionString))
            {
                myConn.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * From animalTable", myConn);

                System.Data.DataSet dataS = new System.Data.DataSet();

                sqlDataAdapter.Fill(dataS, "animalTable");

                return dataS;
            }
        }

      public void UpdateDatabase(System.Data.DataSet dataS)
        {
            using (myConn = new SqlConnection(connectionString))
            {
                myConn.Open();
                sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter("Select * From animalTable", myConn);

                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Update(dataS, "animalTable");
            }
        }

    }
}
