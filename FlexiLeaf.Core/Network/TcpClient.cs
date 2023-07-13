using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Network.Packets.IO;
using System.Runtime.CompilerServices;
using Giny.Core.DesignPattern;

namespace FlexiLeaf.Core.Network
{
    public class TcpClient : Singleton<TcpClient>
    {
        private const int BufferSize = 32768; // Taille du tampon pour les opérations de réception/envoi
        private readonly Socket _clientSocket;
        private readonly byte[] _buffer;

        public TcpClient()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _buffer = new byte[BufferSize];
        }

        public async Task Connect(string ipAddress, int port, string password = "")
        {
            var ipEndpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            await _clientSocket.ConnectAsync(ipEndpoint);
            await Send(new LoginPacket(password));
            StartReceiving();
        }

        private void StartReceiving()
        {
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private List<byte> buffer = new List<byte>();

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var bytesRead = _clientSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    var receivedData = new byte[bytesRead];
                    Array.Copy(_buffer, receivedData, bytesRead);
                    buffer.AddRange(receivedData);
                Read:
                    if (buffer.Count <= sizeof(int))
                        return;
                    int packetSize = BitConverter.ToInt32(buffer.ToArray(), 0);

                    if (buffer.Count >= packetSize + sizeof(int))
                    {
                        buffer.RemoveRange(0, sizeof(int));
                        var received = PacketSerializer.Deserialize(buffer.ToArray());
                        buffer.RemoveRange(0, packetSize);
                        PacketHandler.ExecuteHandler(received, this);
                        if (buffer.Count > 0)
                            goto Read;
                    }
                    _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
                }
                else
                {
                    throw new SocketException();
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Deconnection");
            }

        }

        public async Task Send(Packet message)
        {
            try
            {
                var sendData = PacketSerializer.Serialize(message);
                await _clientSocket.SendAsync(sendData, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Erreur d'envoi : {ex.SocketErrorCode}");
            }
        }
    }
}
