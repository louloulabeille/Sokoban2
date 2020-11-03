using System;
using System.Collections.Generic;
using Utilitaires;

namespace Sokoban
{
    public class Map : List<Elements>, ILoad, ICloneable
    {
        #region Fonction de chargement de map, clone et condition de victoire
        public List<List<char>> Load(string path, int level)
        {
            ILoad obj = new LoadFromTxt();
            return obj.Load(path, level);
        }
        public Map GetMapInit(string path, int level)
        {
            List<List<char>> listFull = new List<List<char>>(Load(path, level));
            for (int i = 0; i < listFull.Count; i++)
            {
                for (int j = 0; j < listFull[i].Count; j++)
                {
                    switch (listFull[i][j])
                    {
                        case ' ':
                            break;
                        case 'X':
                            this.Add(new Mur(j, i));
                            break;
                        case '.':
                            this.Add(new Emplacement(j, i));
                            break;
                        case '*':
                            this.Add(new Caisse(j, i));
                            break;
                        case '@':
                            this.Add(new Personnage(j, i));
                            break;
                        case '\r':                              
                            break;
                        case '&':
                            this.Add(new Emplacement(j, i));
                            this.Add(new Caisse(j, i));
                            break;
                        default:
                            throw new ApplicationException("Erreur dans le fichier map. Charactère '" + listFull[i][j] + "' non reconnu");
                    }
                }
            }       
            return this;
        }
        public static bool Win(Map map)
        {
            List<Caisse> caisseList = new List<Caisse>();
            List<Emplacement> empList = new List<Emplacement>();
            int count = 0;

            foreach (Elements elem in map)
            {
                if (elem is Caisse c) caisseList.Add(c);
                else if (elem is Emplacement e) empList.Add(e);
            }

            for (int i = 0; i < empList.Count; i++)
            {
                for (int j = 0; j < caisseList.Count; j++)
                {
                    if (caisseList[j].X == empList[i].X && caisseList[j].Y == empList[i].Y)
                    {
                        count++;
                        break;
                    }
                }
            }
            return count == empList.Count;
        }
        public object Clone()
        {
            Map ne = new Map();
            foreach (Elements elem in this)
            {
                if (elem is Personnage p) ne.Add(p.Clone() as Personnage);
                else if (elem is Caisse c) ne.Add(c.Clone() as Caisse);
                else ne.Add(elem);
            }
            return ne;
        }
        #endregion
        #region Gestion Mouvement
        public static Map OnMove(Map map, char key)
        {
            Personnage personnage = map.Find(x => x is Personnage) as Personnage;
            if (key == 'q') return Move(map, personnage,0, -1);
            else if (key == 'z') return Move(map, personnage, -1, 0);
            else if (key == 'd') return Move(map, personnage,0, 1);
            else if (key == 's') return Move(map, personnage, 1, 0);
            else return map;
        } 
        public static Map Move(Map map, Personnage p, int sens, int sens2)
        {
            bool cont = false;
            foreach (Elements elem in map)
            {
                if (p.Y + sens == elem.Y && p.X + sens2 == elem.X)
                {
                    if (elem.GetType().Name == "Mur")
                    {
                        cont = true;
                        break;
                    }
                    else if (elem is Caisse c)
                    {
                        foreach (Elements elem2 in map)
                        {
                            if (elem2 is Emplacement) continue;
                            if (p.Y + 2*sens == elem2.Y && p.X+ 2*sens2 == elem2.X)
                            {
                                if (elem2.GetType().Name == "Mur" || elem2.GetType().Name == "Caisse")
                                {
                                    cont = true;
                                    break;
                                }
                                else 
                                {
                                    c.Y+=sens;
                                    p.Y+=sens;
                                    c.X += sens2;
                                    p.X += sens2;
                                    cont = true;
                                    break;
                                }
                            }
                        }
                        if (!cont)
                        {
                            c.Y+=sens;
                            p.Y+=sens;
                            c.X += sens2;
                            p.X += sens2;
                        }
                        cont = true;
                        break;
                    }
                }
            }
            if (!cont)
            {
                p.Y += sens;
                p.X += sens2;
            }
            return map;
        }
        #endregion
    }
}

