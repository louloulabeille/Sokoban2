using Microsoft.VisualBasic;
using System;
using System.Diagnostics;

namespace Sokoban
{
    public class Personnage : Elements, ICloneable
    {
        #region Champs privés

        #endregion Champs privés

        #region propriétés 

        #endregion propriétés 

        #region Méthode
        public object Clone()
        {
            Personnage toReturn = new Personnage
            {
                X = this.X,
                Y = this.Y,
                Content = this.Content
            };
            return toReturn;
        }
        #endregion Méthode

        #region Constructeur
        public Personnage(int x, int y) : base(x, y)
        {
            Content = '@';
        }

        public Personnage()
        {

        }
        #endregion Constructeur

        #region Evenement





        #endregion Evenement

    }

}
