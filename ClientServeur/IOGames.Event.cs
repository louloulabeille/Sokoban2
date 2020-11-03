﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ClientsServeur
{
    public abstract partial class IOGame
    {
        #region event
        public event EventHandler EventEndGame;
        public event EventHandler EventWinGame;
        public event EventHandler EventDeconnexion;
        public event EventHandler EventGameReady;
        public event EventHandler EventWait;
        public event EventHandler EventInitialisation;
        #endregion

        #region methode des events de lancement

        /// <summary>
        /// evênement sur initialisation des jeux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEventInitialisation(object sender, EventArgsInitialisation e)
        {
            EventHandler handler = EventInitialisation;
            if (handler != null) handler(sender, e);
        }
        /// <summary>
        /// creation de EventEndGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEventEndGame(object sender, EventArgs e)
        {
            EventHandler handler = EventEndGame;
            if (handler != null) handler(sender, e);
        }

        /// <summary>
        /// creation de EventWinGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEventWinGame(object sender, EventArgs e)
        {
            EventHandler handler = EventWinGame;
            if (handler != null) handler(sender, e);
        }

        /// <summary>
        /// creation de EventDeconnexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEventDeconnexion(object sender, EventArgs e)
        {
            EventHandler handler = EventDeconnexion;
            if (handler != null) handler(sender, e);
        }

        /// <summary>
        /// event en attente initialisation du jeu quand tout est prêt pour lancer le jeu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEventGameReady(object sender, EventArgs e)
        {
            EventHandler hendler = EventGameReady;
            if (hendler != null) hendler(sender, e);
        }

        /// <summary>
        /// mise en attente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEventWait(object sender, EventArgs e)
        {
            EventHandler hendler = EventWait;
            if (hendler != null) hendler(hendler, e);
        }
        #endregion

        #region methode event

        /// <summary>
        /// Event de gestion si le partie sont prêt a jouer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IOGames_EventGameReady(object sender, EventArgs e)
        {
            if (this.GameReady)
            {
                Envoi(TcpClient, MessageReseau.gameReady);
            }
        }

        /// <summary>
        /// Methode de event arret du jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IOGames_EventDeconnexion(object sender, EventArgs e)
        {
            if (Envoi(TcpClient, MessageReseau.stop))
            {
                int nbOctet;
                byte[] retour;
                retour = Lecture(TcpClient, out nbOctet);
                if (retour.Length != 0) StopAll();
            }
            StopAll();
            Deconnexion = false;
        }

        /// <summary>
        /// gestion quand un des 2 utilisateurs
        /// ne peut plus faire de mouvments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IOGames_EventEndGame(object sender, EventArgs e)
        {
            Envoi(TcpClient, MessageReseau.reStart);
            Thread.Sleep(1);    // ca marche voir pourquoi peut être une désynchronisation entre les 2 threads
        }

        /// <summary>
        /// méthode de l'event de win la game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IOGames_EventWinGame(object sender, EventArgs e) 
        {
            Envoi(TcpClient, MessageReseau.win);
        }

        #endregion

    }
}
