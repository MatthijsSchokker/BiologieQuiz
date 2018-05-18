using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schokkers_Biology_Quiz
{
    public partial class Entree : Form
    {
        //========================================
        //Dit is de initieel geopende pagina. Er volgt een welkomsttekst en van hieruit kun je naar verschillende onderdelen van het programma.
        //========================================

        public Entree()
        {
            InitializeComponent();
        }

        QuizSelection quizSelection;
        Password password;

        //========================================
        //Welkomsttekst
        //========================================

        private void Entree_Load(object sender, EventArgs e)
        {
            string introTxt = "In dit programma vindt je allerlei vragen over de biologie. Je kunt deze spelen om te oefenen voor toetsen, maar ook voor de lol spelen." +
            "\r\n\n" + "Er zijn op dit moment twee verschillende manieren om het spel te spelen:" + "\r\n\n" + "-  Vragen beantwoorden (oefenmodus)" + "\r\n" + "-  Tegen de tijd spelen" +
            "\r\n\n\n" + "Daarnaast kun je via Bestand -> Gegevens nieuwe vragen toevoegen aan het spel.";
            lblIntro.Text = introTxt;
        }

        //========================================
        // Menu Afhandeling
        //========================================

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (quizSelection == null) //als er geen venster quizSelection open is, open een nieuwe
            {
                quizSelection = new QuizSelection();
                quizSelection.FormClosed += (a, b) => quizSelection = null;
                quizSelection.Show();
            }
            if (quizSelection != null) //als er wel een venster quizSelection open is, focus daar op 
            {
                quizSelection.Focus();
            }
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            if (password == null)
            {
                password = new Password();
                password.FormClosed += (a, b) => quizSelection = null;
                password.Show();
            }
            if (password != null)
            {
                password.Focus();
            }
        }

        //========================================
        // Afluiten van Venster
        //========================================

        private void Entree_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Weet u zeker dat u wilt afsluiten?", "Programma sluiten", MessageBoxButtons.OKCancel);

            e.Cancel = (window == DialogResult.Cancel);
        }
    }

}
