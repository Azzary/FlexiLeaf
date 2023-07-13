using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Network.Packets.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub.Network
{
    public class Client
    {
        private readonly Socket _clientSocket;
        private readonly byte[] _buffer;
        public string ID { get; set; }
        private readonly bool IsControlHub = false;

        public Client(Socket clientSocket, bool isControlHub = false)
        {
            IsControlHub = isControlHub;
            ID = Guid.NewGuid().ToString();
            _clientSocket = clientSocket;
            _buffer = new byte[32768];
            OnConnect();
        }

        public void StartReceiving()
        {
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private readonly List<byte> buffer = new();

        private async void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var bytesRead = _clientSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    var receivedData = new byte[bytesRead];
                    Array.Copy(_buffer, receivedData, bytesRead);
                    if (!IsControlHub)
                    {
                        if(this == TcpServer.TargetClient)
                            await TcpServer.MainClient.Send(receivedData);
                        _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
                        return;
                    }
                    buffer.AddRange(receivedData);
                    Read:
                    if (buffer.Count <= sizeof(int))
                        return;
                    int packetSize = BitConverter.ToInt32(buffer.ToArray(), 0);

                    if (buffer.Count >= packetSize + sizeof(int))
                    {
                        var byteRemove = buffer.GetRange(0, sizeof(int));
                        buffer.RemoveRange(0, sizeof(int));
                        var received = PacketSerializer.Deserialize(buffer.ToArray());
                        if(!PacketHandler.ExecuteHandler(received, this, false) && TcpServer.TargetClient != null && this != TcpServer.TargetClient)
                        {
                            byteRemove.AddRange(buffer.GetRange(0, packetSize));
                            await TcpServer.TargetClient.Send(byteRemove.ToArray());
                        }
                        buffer.RemoveRange(0, packetSize);
                        
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
                Console.WriteLine($"Client déconnecté : {ID}");
                OnDisconnect();
            }

        }

        public async void OnConnect()
        {
            if (!IsControlHub)
            {
                TcpServer.clients.Add(this);
                if (TcpServer.MainClient != null)
                    await TcpServer.MainClient.Send(new UpdateClientListPacket(TcpServer.clients.Select(client => client.ID).ToList()));

            }
        }

        public async void OnDisconnect()
        {
            _clientSocket.Close();
            if (!IsControlHub)
            {
                TcpServer.clients.Remove(this);
                if (TcpServer.MainClient != null)
                    await TcpServer.MainClient.Send(new UpdateClientListPacket(TcpServer.clients.Select(client => client.ID).ToList()));
            }
            else
            {
                if(TcpServer.MainClient == this)
                {
                    TcpServer.MainClient = null;
                    if(TcpServer.TargetClient != null)
                    {
                        await TcpServer.TargetClient.Send(new ScreenPacket(false, 1, 1));
                        TcpServer.TargetClient = null;
                    }
                }
            }
        }

        private async Task Send(byte[] sendData)
        {
            try
            {
                await _clientSocket.SendAsync(sendData, SocketFlags.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur d'envoi : {ex}");
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
