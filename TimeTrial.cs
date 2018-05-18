using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Schokkers_Biology_Quiz
{
    public partial class TimeTrial : Form
    {
        public TimeTrial()
        {
            InitializeComponent();
        }

        QuizSelection quizSelection;
        LoadQuestion loadQuestion = new LoadQuestion();
        List<string> question;
        private string givenAnswer;
        private string answer;
        private int goodAnswers = 0;

        private int counter = 0;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();


        //========================================
        // Bij laden venster
        //========================================

        private void TimeTrial_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bij deze oefening moet je zo snel mogelijk een goed antwoord geven. Zodra je een antwoord aanklikt zal je een volgende vraag krijgen. " + "\n\r" +
                "Geef je een fout antwoord dan stopt de test en moet je opnieuw beginnen.", "Tegen de Tijd", MessageBoxButtons.OK);

            NextQuestion();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        //========================================
        // Timer
        //========================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;

            lblTime.Text = counter.ToString();
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

            rbtnAnswer1.Text = $"&1: {question[0]}";
            rbtnAnswer2.Text = $"&2: {question[1]}";
            rbtnAnswer3.Text = $"&3: {question[2]}";
            rbtnAnswer4.Text = $"&4: {question[3]}";
        }

        //========================================
        // Antwoordafhandeling
        //========================================

        private void rbtnAnswer1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAnswer1.Checked == true)
            {
                givenAnswer = question[0];
                AnswerCheck();
            }
        }

        private void rbtnAnswer2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAnswer2.Checked == true)
            {
                givenAnswer = question[1];
                AnswerCheck();
            }
        }

        private void rbtnAnswer3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAnswer3.Checked == true)
            {
                givenAnswer = question[2];
                AnswerCheck();
            }
        }

        private void rbtnAnswer4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAnswer4.Checked == true)
            {
                givenAnswer = question[3];
                AnswerCheck();
            }
        }

        //========================================
        // Antwoord Check
        //========================================

        private void AnswerCheck()
        {
            if (answer == givenAnswer)
            {
                GoodAnswer();
            }
            else
            {
                WrongAnswer();
            }
        }

        private void GoodAnswer()
        {
            NextQuestion();
            rbtnAnswer1.Checked = false;
            rbtnAnswer2.Checked = false;
            rbtnAnswer3.Checked = false;
            rbtnAnswer4.Checked = false;
            goodAnswers++;
        }

        private void WrongAnswer()
        {
            timer1.Stop();
            if (MessageBox.Show($"Helaas, dat was een fout antwoord. U heeft {goodAnswers} goede antwoorden gegeven in {counter} seconden. " +
                $"Wilt u nog eens spelen, klik dan op OK. Cancel brengt u terug bij het Entree scherm.", "Fout Antwoord", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                this.Close();
            }
            counter = 0;
            goodAnswers = 0;
            lblTime.Text = "0";
            NextQuestion();
            timer1.Start();

            rbtnAnswer1.Checked = false;
            rbtnAnswer2.Checked = false;
            rbtnAnswer3.Checked = false;
            rbtnAnswer4.Checked = false;
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

        private void mnuQuit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //========================================
        // Afluiten van Venster
        //========================================


        private void TimeTrial_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Weet u zeker dat deze test wilt afsluiten?", "Test Sluiten", MessageBoxButtons.OKCancel);

            e.Cancel = (window == DialogResult.Cancel);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
