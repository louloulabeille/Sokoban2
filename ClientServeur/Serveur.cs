﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Utilitaires;

namespace ClientsServeur
{
    public class Serveur : IOGame
    {
        private TcpListener _socket;
        //private Thread _threadServeur;

        #region constructeur
        /// <summary>
        /// initialisation du serveur
        /// AddressFamily -> ip v4
        /// SocketType -> flue bi directionnel ne peut être que AddressFamily.InterNetwork et ProtocolType.Tcp
        /// ProtocolType -> type de connexion tcp
        /// </summary>
        /// <param name="port"> port réseau d'écoute </param>
        public Serveur(int port)
            : base(port)    /// initialisation des paramètres au vert
        {
            _socket = new TcpListener(IPAddress.Any, Port);
            this.ThreadProgramme = new Thread(new ThreadStart(ThreadServeurLoop));
            this.ThreadProgramme.Start();
            
        }
        /// <summary>
        /// idem que dessus
        /// </summary>
        /// <param name="port"> port réseau d'écoute </param>
        /// <param name="data"> données envoyées pour initialisation de la partie </param>
        public Serveur(int port, GameIOData data)
            : base(port,data)    /// initialisation des paramètres au vert
        {
            _socket = new TcpListener(IPAddress.Any, Port);
            this.ThreadProgramme = new Thread(new ThreadStart(ThreadServeurLoop));
            this.ThreadProgramme.Start();

        }
        #endregion

        #region méthode du serveur
        private void ThreadServeurLoop()
        {
            try
            {
                this._socket.Start();   // lancement de l'écoute
                bool stop = true;

                while (stop)
                {
                    byte[] buffer = new byte[1024];
                    int nbOctet;
                    string message;
                    StringBuilder sB = new StringBuilder();
                    
                    if (this.TcpClient == null)
                    {
                        this.TcpClient = this._socket.AcceptTcpClient(); // attente d'une connexion le traitement s'arrete
                    }
                    buffer = Lecture(this.TcpClient, out nbOctet);
                    message = sB.AppendFormat("{0}", Encoding.UTF8.GetString(buffer, 0, nbOctet)).ToString();
                    switch (message)
                    {
                        case "gameReady":
                            if ( !GameReady ) GameReady = true;
                            break;
                        case "stop":
                            //Envoi(this.TcpClient, MessageReseau.iCopy);
                            stop = !StopAll();
                            break;
                        case "iCopy":
                            break;
                        case "init":
                            /// traitement initialisation de la partie
                            EnvoiData(TcpClient, this.Donnee);
                            break;
                        case "reStart":
                            ///traitement de la vue pour initialiser la vue
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

        /// <summary>
        /// méthode d'arret pour le serveur 
        /// ferme le socket et TcpClient
        /// </summary>
        /// <returns></returns>
        protected override bool StopAll()
        {
            this.TcpClient.Close();
            this._socket.Stop();
            return true;
        }

        /// <summary>
        /// méthode coté serveur de la gestion du re-start
        /// </summary>
        /// <param name="data"> données de la partie coté serveur </param>
        protected override void ReStart()
        {
            this.GameReady = false;
            EnvoiData(TcpClient, this.Donnee);
            object buffer =  LectureData(TcpClient);
            this.DonneeAffichage = buffer is GameIOData b ? b : null;
            EnvoiData(TcpClient, this.Donnee);

        }
        #endregion
    }
}
