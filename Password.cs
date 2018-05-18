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
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        DataScreen dataScreen;
        private int counter = 2;
        private string password = "";

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                Environment.Exit(0);
                MessageBox.Show("U heeft teveel pogingen gebruikt om het wachtwoord in te vullen", "Wachtwoord incorrect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (txtPassword.Text == password)
                {
                    if (dataScreen == null)
                    {
                        dataScreen = new DataScreen();
                        dataScreen.FormClosed += (a, b) => dataScreen = null;
                        dataScreen.Show();
                    }
                    if (dataScreen != null)
                    {
                        dataScreen.Focus();
                    }

                    this.Close();
                }
                else if (counter == 2)
                {
                    MessageBox.Show($"Dit wachtwoord is incorrect. U heeft nog {counter} pogingen over.", "Incorrect wachtwoord");
                    counter--;
                }
                else if (counter == 1)
                {
                    MessageBox.Show($"Dit wachtwoord is incorrect. U heeft nog {counter} poging over. Als uw volgende poging incorrect is sluit het programma uit veiligheidsoverwegingen.", "Incorrect wachtwoord");
                    counter--;
                }
            }
        }
    }
}
