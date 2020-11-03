﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Utilitaires;

namespace ClientsServeur
{
    /// <summary>
    /// classe serveur
    /// </summary>
    public class Client : IOGame
    {
        //private Thread _threadClient;
        private IPAddress _adresseIP;       // adresse ip du serveur

        #region constructeur
        /// <summary>
        /// intialisation du serveur et lancement
        /// </summary>
        public Client(IPAddress adresseIP, int port)
            : base(port)
        {
            AdresseIP = adresseIP;
            this.ThreadProgramme = new Thread(new ThreadStart(Connexion));
            this.ThreadProgramme.Start();

            // allocation à la classe des ces évènements
            EventGameReady += IOGames_EventGameReady;
        }

        #region assesseur
        public IPAddress AdresseIP { get => _adresseIP; set => _adresseIP = value; }
        #endregion

        #endregion

        #region méthode 
        /// <summary>
        /// connexion au serveur
        /// </summary>
        private void Connexion()
        {
            this.TcpClient = new TcpClient(AddressFamily.InterNetwork);
            this.TcpClient.Connect(AdresseIP, Port);
            if (this.TcpClient.Connected)
            {
                if (!Envoi(this.TcpClient,MessageReseau.init))
                {
                    Deconnexion = true;
                    //throw (new ApplicationException("La connexion vers le serveur est impossible./nDéclaration de la connexion en lecture seule."));
                }
                else
                {
                    //initialisation du jeux au niveau donnée
                    object init = LectureData(TcpClient);
                    if ( init is GameIOData dataGameIOData )
                    {
                        Donnee = dataGameIOData;
                        GameReady = true;
                    }
                    else
                    {
                        Deconnexion = true;
                    }
                }
                ThreadClientLoop();
            }
            else
            {
                StopAll();
            }
        }

        /// <summary>
        /// Garder le client connecté au serveur
        /// et passe en attente
        /// </summary>
        private void ThreadClientLoop()
        {
            try
            {
                bool stop = true;
                while (stop)
                {
                    StringBuilder sB = new StringBuilder();
                    byte[] buffer = new byte[1024];
                    int nbOctet;
                    string message;

                    buffer = Lecture(this.TcpClient, out nbOctet);
                    message = sB.AppendFormat("{0}", Encoding.UTF8.GetString(buffer, 0, nbOctet)).ToString();
                    Debug.WriteLine(message);

                    switch(message)
                    {
                        case "gameReady":
                            if ( !GameReady ) { GameReady = true;}
                            break;
                        case "stop":
                            //Envoi(TcpClient, MessageReseau.iCopy);
                            stop = !StopAll();
                            break;
                        case "reStart":
                            if (!this.EndGame) { this.EndGame = true; }
                            ReStart();
                            break;
                        case "win":

                            break;
                    }
                }
            }
            catch (ArgumentNullException aNE)
            {
                Debug.WriteLine("Message: {0}", aNE.Message);
                Debug.WriteLine("Source: {0}", aNE.Source);
            }
            catch (ArgumentOutOfRangeException aOORE)
            {
                Debug.WriteLine("Message: {0}", aOORE.Message);
                Debug.WriteLine("Source: {0}", aOORE.Source);
            }
            catch (SocketException sE)
            {
                Debug.WriteLine($"Message :{sE.Message}\nCode erreur :{sE.ErrorCode}");
                Debug.WriteLine("Source: {0}", sE.Source);
            }
            catch (ObjectDisposedException oDE)
            {
                Debug.WriteLine("Message: {0}", oDE.Message);
                Debug.WriteLine("Source: {0}", oDE.Source);
            }
            catch (NotSupportedException nSE)
            {
                Debug.WriteLine("Message: {0}", nSE.Message);
                Debug.WriteLine("Source: {0}", nSE.Source);
            }
            catch (InvalidOperationException iOE)
            {
                Debug.WriteLine("Message: {0}", iOE.Message);
                Debug.WriteLine("Source: {0}", iOE.Source);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Message: {0}", e.Message);
                Debug.WriteLine("Source: {0}", e.Source);
            }
        }

        #endregion

    }
}
