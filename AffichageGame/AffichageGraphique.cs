using System;
using System.Collections.Generic;
using System.Drawing;
using Sokoban;
using System.Windows.Forms;
using System.IO;


namespace AffichageGame
{

    public partial class AffichageGraphique : Form
    {
        public Map mapLv;
        public Map mapActuel;
        public int lv;
        public char orientation = 'q';
        public char newOrientation = 'q';
        public List<Image> imgList; // Image des sprites
        public int move=0; // Sprite a afficher (0-2)

        public AffichageGraphique(Map map, int level)
        {
            InitializeComponent();
            
            lv = level;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
            mapActuel = map;
            mapLv = map.Clone() as Map;
            this.KeyPreview = true;
            this.Afficher3(map);
            listBox1.SelectedIndex = lv - 1;
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'z' || e.KeyChar == 's' || e.KeyChar == 'q' || e.KeyChar == 'd')
            {
                Map x = mapActuel.Clone() as Map;
                mapActuel = Map.OnMove(mapActuel, char.Parse(e.KeyChar.ToString()));
                newOrientation = e.KeyChar;
                this.Afficher4(x);

                if (Map.Win(mapActuel))
                {
                    string path = Resource.File;
                    MessageBox.Show($"OUHOUHOUH You won The Gameeeee!!!!!!\nNombre de coup joués {Caisse.nbMouvement}");
                    lv++;
                    listBox1.SelectedIndex = lv - 1;
                    Caisse.nbMouvement = 0;
                    Map map = new Map();
                    mapActuel = mapLv = map.GetMapInit(Globals.Di + "/" + path, lv);
                    this.Afficher3(map);
                }
                this.KeyPreview = true;
                orientation = newOrientation;
            }
        }

        public void Afficher3(Map map)
        {
            Reset();
            foreach (Elements elem in map)
            {
                Image image = new Bitmap(40,40); 
                PictureBox img = new PictureBox();
                img.Size = new Size(40, 40);

                if (elem is Personnage) image = getImg()[0];
                else if (elem is Mur) image = Image.FromFile(Globals.Di + Globals.Theme + "/wall.png");
                else if (elem is Caisse) image = Image.FromFile(Globals.Di + Globals.Theme + "/caisse.png");
                else if (elem is Emplacement) image = Image.FromFile(Globals.Di + Globals.Theme + "/emp.png");

                image = new Bitmap(image, 40, 40);
                img.Image = image;
                img.Location = new Point(elem.X * 40, elem.Y * 40);
                panel1.Controls.Add(img);

                if (elem is Caisse) img.BringToFront();
                else if (elem is Emplacement) img.SendToBack();
            }
        }

        public void Afficher4(Map map)
        {
            Personnage p = null;
            Personnage p2 = null;
            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].Y != mapActuel[i].Y || map[i].X != mapActuel[i].X)
                {
                    if (map[i] is Personnage)
                    {
                        p = map[i] as Personnage;
                        p2 = mapActuel[i] as Personnage;
                    }
                    else
                    {
                        Control x = panel1.GetChildAtPoint(new Point(map[i].X * 40, map[i].Y * 40));
                        x.Location = new Point(mapActuel[i].X * 40, mapActuel[i].Y * 40);
                        x.BringToFront();
                    }
                }
            }
            if (p != null)
            {
                PictureBox z = panel1.GetChildAtPoint(new Point(p.X * 40, p.Y * 40)) as PictureBox;
                if (orientation != newOrientation) imgList = getImg();
                move = move == 2 ? 0 : move+1;
                z.Image = imgList[move];
                z.Location = new Point(p2.X * 40, p2.Y * 40);
                z.BringToFront();
            }
        }

        #region reset

        private void Reset()
        {
            panel1.Controls.Clear();
        }
        #endregion reset

        private List<Image> getImg()
        {
            List<Image> toReturn = new List<Image>();
            if (newOrientation == 'z') 
            {
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/haut.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/haut1.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/haut2.png"), 40, 40));
                return toReturn; 
            }
            else if (newOrientation == 'q') 
            {
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/gauche.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/gauche1.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/gauche2.png"), 40, 40));
                return toReturn; 
            }
            else if (newOrientation == 's') 
            {
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/bas.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/bas1.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/bas2.png"), 40, 40));
                return toReturn; 
            }
            else 
            {
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/droite.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/droite1.png"), 40, 40));
                toReturn.Add(new Bitmap(Image.FromFile(Globals.Di + Globals.Theme + "/droite2.png"), 40, 40));
                return toReturn;
            }
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
            Afficher3(mapLv);
            mapActuel = mapLv.Clone() as Map;
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Reset();
            lv = listBox1.SelectedIndex + 1;
            Caisse.nbMouvement = 0;
            Map x = new Map();
            x.GetMapInit(Globals.Di + "\\" + Resource.File, lv);
            mapActuel = mapLv = x.Clone() as Map;
            Afficher3(x);
        }
    }
}
