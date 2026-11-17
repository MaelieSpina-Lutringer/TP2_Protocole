using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace TP2_Protocole
{
    public class ClientReseau
    {
        private readonly string adresse;
        private readonly int port;

        private TcpClient client;
        private NetworkStream flux;
        private StreamReader lecteur;
        private StreamWriter ecrivain;

        public bool Connecte => client != null && client.Connected;

        public ClientReseau(string adresse, int port)
        {
            this.adresse = adresse;
            this.port = port;
        }

        public void Connecter()
        {
            client = new TcpClient();
            client.Connect(adresse, port);

            flux = client.GetStream();

            lecteur = new StreamReader(flux, new UTF8Encoding(false));

            ecrivain = new StreamWriter(flux, new UTF8Encoding(false))
            {
                NewLine = "\n",
                AutoFlush = true
            };
        }

        public void EnvoyerLigne(string ligne)
        {
            ecrivain.WriteLine(ligne);
        }

        public string LireLigne()
        {
            return lecteur.ReadLine();
        }

        public void Fermer()
        {
            lecteur?.Close();
            ecrivain?.Close();
            flux?.Close();
            client?.Close();
        }
    }
}