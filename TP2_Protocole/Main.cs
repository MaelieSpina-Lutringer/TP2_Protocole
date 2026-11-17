namespace TP2_Protocole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Adresse : ");
            string adresse = Console.ReadLine();

            Console.Write("Port : ");
            int port = int.Parse(Console.ReadLine());

            ClientReseau client = new ClientReseau(adresse, port);

            try
            {
                client.Connecter();

                string accueil = client.LireLigne();
                Console.WriteLine("Serveur : " + accueil);

                while (true)
                {
                    Console.Write("> ");
                    string commande = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(commande))
                        continue;

                    client.EnvoyerLigne(commande);

                    string reponse = client.LireLigne();

                    if (reponse == null)
                    {
                        Console.WriteLine("Connexion fermée par le serveur.");
                        break;
                    }

                    Console.WriteLine(reponse);

                    if (reponse.StartsWith("221"))
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
            finally
            {
                client.Fermer();
            }
        }
    }
}