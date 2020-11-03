using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AffichageGame
{
    public partial class ThemesForm : Form
    {
        public ThemesForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button x = sender as Button;
            Globals.Theme = "/"+x.Text;
            this.Close();
        }
    }
}
