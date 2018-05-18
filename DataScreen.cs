using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schokkers_Biology_Quiz
{
    public partial class DataScreen : Form
    {
        public DataScreen()
        {
            InitializeComponent();
        }

        DatabaseConnector connector = new DatabaseConnector();
        DataSet dataS;
        private int maxR;
        private int currentR;
        bool newData = false;
        byte[] animalPicture;

        private void DataScreen_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        //========================================
        // Laden data
        //========================================

        private void LoadData() // laad data uit database
        {
            connector.conStr = Properties.Settings.Default.QuizDataString;
            dataS = connector.AnimalQuizData;

            maxR = dataS.Tables[0].Rows.Count;

            LoadFirstData();
        }

        private void LoadFirstData() // laad eerste datarow
        {
            txtAnimal.Text = dataS.Tables[0].Rows[currentR][1].ToString();
            txtType.Text = dataS.Tables[0].Rows[currentR][2].ToString();

            btnNew.Enabled = true;
            btnCancel.Enabled = false;
            btnDelete.Enabled = true;
            btnNext.Enabled = true;
            btnPrevious.Enabled = true;
            lblCounter.Text = ($"{currentR + 1} of {maxR}");
        }

        //========================================
        // Navigeren data
        //========================================

        private void btnPrevious_Click(object sender, EventArgs e) // vorige datarow
        {
            if (currentR == 0)
            {
                currentR = maxR - 1;
            }
            else
            {
                currentR--;
            }

            txtAnimal.Text = dataS.Tables[0].Rows[currentR][1].ToString();
            txtType.Text = dataS.Tables[0].Rows[currentR][2].ToString();
            lblCounter.Text = ($"{currentR + 1} of {maxR}");
        }

        private void btnNext_Click(object sender, EventArgs e) // volgende datarow
        {
            if (currentR == maxR - 1)
            {
                currentR = 0;
            }
            else
            {
                currentR++;
            }

            txtAnimal.Text = dataS.Tables[0].Rows[currentR][1].ToString();
            txtType.Text = dataS.Tables[0].Rows[currentR][2].ToString();
            lblCounter.Text = ($"{currentR + 1} of {maxR}");
        }

        //========================================
        // Toevoegen foto
        //========================================

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream memoryStream = new MemoryStream();
            imageIn.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            try
            {
                openPicture.Title = "Kies een afbeelding";
                openPicture.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openPicture.FileName = "";
                openPicture.Filter = "JPEG|*.jpg|GIF|*.gif|Bitmap|*.bmp|All files|*.*";

                if (openPicture.ShowDialog() == DialogResult.OK)
                {
                    animalPicture = ImageToByteArray(Image.FromFile(openPicture.FileName));
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                MessageBox.Show(err.StackTrace);
            }



        }

        //========================================
        // Toevoegen, verwijderen en veranderen data
        //========================================

        private void btnNew_Click(object sender, EventArgs e) // nieuwe data toevoegen
        {
            txtAnimal.Clear();
            txtType.Clear();

            newData = true;

            btnNew.Enabled = false;
            btnCancel.Enabled = true;
            btnDelete.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            lblCounter.Text = "New row";
        }

        private void btnSave_Click(object sender, EventArgs e) // data opslaan
        {
            SaveData();
        }

        private void btnDelete_Click(object sender, EventArgs e) // datarow verwijderen
        {
            DeleteData();
        }

        private void SaveData() // data opslaan
        {
            try
            {
                if (newData == true)
                {
                    DataRow newRow = dataS.Tables["animalTable"].NewRow();
                    newRow[1] = txtAnimal.Text;
                    newRow[2] = txtType.Text;
                    newRow[3] = animalPicture;
                    dataS.Tables["animalTable"].Rows.Add(newRow);
                }
                else
                {
                    DataRow changedRow = dataS.Tables["animalTable"].Rows[currentR];
                    changedRow[1] = txtAnimal.Text;
                    changedRow[2] = txtType.Text;
                    changedRow[3] = animalPicture;
                }

                connector.UpdateDatabase(dataS);

                MessageBox.Show("Data toegevoegd.");
                LoadData();
                newData = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                MessageBox.Show(err.StackTrace);
            }
        }

        private void DeleteData() // datarow verwijderen
        {
            var window = MessageBox.Show("Weet u zeker dat u deze gegevens wilt verwijderen?", "Gegevens verwijderen", MessageBoxButtons.OKCancel);

            if (window == DialogResult.OK)
            {
                try
                {
                    dataS.Tables[0].Rows[currentR].Delete();

                    connector.UpdateDatabase(dataS);

                    MessageBox.Show("De gegevens zijn verwijderd.");

                    maxR = dataS.Tables[0].Rows.Count;
                    currentR--;
                    LoadData();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        //========================================
        // Annuleren data veranderen
        //========================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            newData = false;
            LoadFirstData();
        }

        //========================================
        // Afsluiten venster
        //========================================

        private void DataScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Weet u zeker dat dit venster wilt afsluiten?", "Test Sluiten", MessageBoxButtons.OKCancel);

            e.Cancel = (window == DialogResult.Cancel);
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
