using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AffichageGame
{
    public partial class Menu : Form
    {  
        public Menu()
        {
            InitializeComponent();
            // Chargement du path
            string path = Resource.File;
            string cd = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(cd).Parent.Parent.Parent.Parent;
            // Init des var global
            Globals.Di = di.FullName;
            Globals.Theme = "/Lucas";
            //Init image et taille du form
            SetBackground();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnSolo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sokoban.Map map = new Sokoban.Map();
            map.GetMapInit(Globals.Di +"/"+ Resource.File, 1);
            DialogResult result = new AffichageGraphique(map, 1).ShowDialog();
            this.Show();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnThemes_Click(object sender, EventArgs e)
        {
            this.Hide();
            DialogResult result = new ThemesForm().ShowDialog();
            SetBackground();
            this.WindowState = FormWindowState.Maximized;
            this.Show();
        }

        private void SetBackground()
        {
            this.BackgroundImage = new Bitmap(Globals.Di + Globals.Theme + "/background.png");
            BackgroundImageLayout = ImageLayout.Stretch;

        }
    }
    // Global var de directory et theme
    public static class Globals
    {
        private static string theme;
        private static string di;

        public static string Theme { get => theme; set => theme = value; }
        public static string Di { get => di; set => di = value; }
    }
}
