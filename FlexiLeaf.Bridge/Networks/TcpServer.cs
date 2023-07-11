using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Giny.Core.DesignPattern;

namespace FlexiLeaf.ControlHub.Network
{
    public class TcpServer
    {
        public static Client MainClient { get; set; }
        public static Client TargetClient { get; set; }

        public static List<Client> clients = new List<Client>();

        private const int BufferSize = 8192; // Taille du tampon pour les opérations de réception/envoi
        private readonly Socket _serverSocket;

        public TcpServer()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task Start(string ipAddress, int port, bool IsControlHub)
        {
            var ipEndpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            _serverSocket.Bind(ipEndpoint);
            _serverSocket.Listen(100);

            Console.WriteLine($"Serveur TCP démarré. IP : {ipAddress}, Port : {port}");

            while (true)
            {
                var clientSocket = await _serverSocket.AcceptAsync();

                Console.WriteLine($"Nouvelle connexion cliente : {clientSocket.RemoteEndPoint}");

                // Créer une instance de la classe Client pour gérer la logique des paquets
                var client = new Client(clientSocket, IsControlHub);
                client.StartReceiving();
            }
        }
    }
}
