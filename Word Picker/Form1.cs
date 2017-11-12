using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Novacode;

namespace Word_Picker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            toolStripStatusLabel1.Text = "Ready to work.";
        }

        private void LoadDocument(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                    string[] filePath = openFileDialog1.FileName.Split('.');
                    string fileExtension = filePath[filePath.Length - 1];

                    switch (fileExtension)
                    {
                        //Read word file
                        case "docx":
                            textBox.Text = DocX.Load(openFileDialog1.FileName).Text;
                            break;

                        case "text":
                            textBox.Text = File.ReadAllText(openFileDialog1.FileName);
                            break;
                    }

                    toolStripStatusLabel1.Text = "." + fileExtension + " file successfuly loaded!";
                    ActiveForm.Text = "Word Picker - " + openFileDialog1.FileName;
                    break;

                case DialogResult.Cancel:
                    toolStripStatusLabel1.Text = "No file was selected.";
                    ActiveForm.Text = "Word Picker";
                    break;
            }
        }

        private void GetWords(object sender, EventArgs e)
        {
            wordsBox.Text = "";

            int wordLength = (int)numericUpDown1.Value;
            int numberOfWords = 0;

            toolStripStatusLabel1.Text = "Processing...";

            // Divide text into words...
            char[] punctuation = textBox.Text.Where(Char.IsPunctuation).Distinct().ToArray();
            string[] allWords = textBox.Text.Split().Select(x => x.Trim(punctuation)).ToArray();

            string inlineWords = string.Join(Environment.NewLine, allWords); ;


            // Delete numbers...
            punctuation = inlineWords.Where(Char.IsNumber).Distinct().ToArray();
            string[] words = inlineWords.Split().Select(x => x.Trim(punctuation)).ToArray();

            // Put words to the list
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length >= wordLength)
                {
                    wordsBox.Text += words[i].ToLower() + Environment.NewLine;
                    numberOfWords++;
                }
            }

            toolStripStatusLabel1.Text = string.Format("{0} words were processed.", numberOfWords);
        }

        private void EnableButton(object sender, EventArgs e)
        {
            // Check text field...
            bool textBoxPass = textBox.Text.Length > 0;

            // Check word length...
            bool numericPass = numericUpDown1.Value > 0;

            if (textBoxPass && numericPass)
            {
                getWordsButton.Enabled = true;
            }
            else
            {
                getWordsButton.Enabled = false;
            }
        }

        private void SaveFile(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                    toolStripStatusLabel1.Text = "Saving...";
                    File.WriteAllText(saveFileDialog1.FileName, wordsBox.Text);
                    toolStripStatusLabel1.Text = "Saved to: " + saveFileDialog1.FileName;
                    break;
            }
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }
    }
}
