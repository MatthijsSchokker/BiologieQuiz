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
    public partial class QuizSelection : Form
    {
        public QuizSelection()
        {
            InitializeComponent();
        }

        RegularQuiz regularQuiz;
        TimeTrial timeTrial;

        //========================================
        // geselecteerde test openen
        //========================================

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (radioRegular.Checked == true)
            {
                if (regularQuiz == null)
                {
                    regularQuiz = new RegularQuiz();
                    regularQuiz.FormClosed += (a, b) => timeTrial = null;
                    regularQuiz.Show();
                }
                if (regularQuiz != null)
                {
                    regularQuiz.Focus();
                }

                this.Close();
            }

            if (radioTime.Checked == true)
            {
                if (timeTrial == null)
                {
                    timeTrial = new TimeTrial();
                    timeTrial.FormClosed += (a, b) => timeTrial = null;
                    timeTrial.Show();
                }
                if (timeTrial != null)
                {
                    timeTrial.Focus();
                }

                this.Close();
            }

            if (radioRegular.Checked == false && radioTime.Checked == false)
            {
                MessageBox.Show("Welke test wilt u starten?", "Geen keus ontvangen", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        //========================================
        // Afsluiten nieuwe test dialoog
        //========================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
