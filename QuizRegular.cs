using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Schokkers_Biology_Quiz
{
    public partial class RegularQuiz : Form
    {
        public RegularQuiz()
        {
            InitializeComponent();
        }

        QuizSelection quizSelection;
        LoadQuestion loadQuestion = new LoadQuestion();
        List<string> question;
        private string answer;
        private string givenAnswer;

        //========================================
        // Bij laden venster
        //========================================

        private void RegularQuiz_Load(object sender, EventArgs e)
        {
            NextQuestion();
        }

        //========================================
        // vullen vraag en antwoordmogelijkheden
        //========================================
        
        private void NextQuestion()
        {
            try
            {
                question = loadQuestion.NextQuestion();

                answer = loadQuestion.CorrectAnswer();

                object picture = loadQuestion.LoadImage();
                Byte[] bytePicture = new Byte[0];
                bytePicture = (Byte[])(picture);
                MemoryStream pic = new MemoryStream(bytePicture);
                picQuestion.Image = Image.FromStream(pic);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            rbtnAnswer1.Text = "&1: " + question[0];
            rbtnAnswer2.Text = "&2: " + question[1];
            rbtnAnswer3.Text = "&3: " + question[2];
            rbtnAnswer4.Text = "&4: " + question[3];
        }

        private void btnAnswer_Click_1(object sender, EventArgs e)
        {
            //========================================
            // Controle Antwoord
            //========================================

            if (rbtnAnswer1.Checked == true)
                givenAnswer = question[0];
            if (rbtnAnswer2.Checked == true) 
                givenAnswer = question[1]; 
            if (rbtnAnswer3.Checked == true) 
                givenAnswer = question[2]; 
            if (rbtnAnswer4.Checked == true) 
                givenAnswer = question[3]; 

            if (givenAnswer == answer)
                NextQuestion();
            else { MessageBox.Show("Helaas! Dit antwoord is niet goed. Probeer het nog eens.", "Fout Antwoord", MessageBoxButtons.OK); }
        }

        //========================================
        // Menu Afhandeling
        //========================================

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (quizSelection == null)
            {
                quizSelection = new QuizSelection();
                quizSelection.FormClosed += (a, b) => quizSelection = null;
                quizSelection.Show();
            }
            if (quizSelection != null)
            {
                quizSelection.Focus();
            }
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            var window = MessageBox.Show("Weet u zeker dat u wilt afsluiten?", "Programma sluiten", MessageBoxButtons.OKCancel);

            if (window == DialogResult.OK)
            {
                Environment.Exit(1);
            }
        }

        //========================================
        // Afluiten van Venster
        //========================================

        private void RegularQuiz_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Weet u zeker dat u de huidige test wilt afsluiten?", "Test sluiten", MessageBoxButtons.OKCancel);

            e.Cancel = (window == DialogResult.Cancel);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
