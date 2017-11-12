using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Picker
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SelectDirectory(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                folderBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void ChangeTab(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex == 0)
            {
                defaultSettingTab.Show();
            }
            else
            {
                defaultSettingTab.Hide();
            }

            if (listBox.SelectedIndex == 1)
            {
                languageTab.Show();
            }
            else
            {
                languageTab.Hide();
            }
        }
    }
}
