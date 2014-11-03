using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace MineSweeper
{


    class Server
    {
        private static int _cliendID=0;
        private static int _turn=0;
        private static int _numOfPlayers;
        private static List<int> _activePlayers=new List<int>();
        private static List<int> _Players=new List<int>();

        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> _clientSockets = new List<Socket>();
        private static byte[] _buffer = new byte[1024];
        private static MineSweeperField msf = new MineSweeperField();
        
        public void main(){
            setupServer();
        }
        
        private static void setupServer()
        {
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 100));
            _serverSocket.Listen(1);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);

        }

        private static void acceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            _clientSockets.Add(socket);
            _cliendID++;
            _numOfPlayers++;
            // client toegevoegd
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuf = new byte[received];
            Array.Copy(_buffer, dataBuf, received);

            string text = Encoding.ASCII.GetString(dataBuf);
            String response = string.Empty;

            string[] temp = text.Split(';') ;

            switch(temp[0].ToLower()){

                case "get connected":
                    _cliendID++;
                    response = _cliendID+"";
                    _Players.Add(_cliendID);
                    send(response, socket);
                    break;

                default:
                    int x;
                    int y;
                    int numOfBombs;
                    int point;
                    int combination;

                    switch(temp[1].ToLower()){
                        case "get":
                            switch (temp[2].ToLower())
                            {
                                case "number of players":
                                    response = _numOfPlayers+";";
                                    send(response, socket);
                                    break;
                                case "turn":
                                    response = _turn+";";
                                    send(response, socket);
                                    break;
                                case "position;":
                                    combination = int.Parse(temp[3]);
                                    StaticCodeConverter.decode(combination, out x, out y);
                                    List<int> spots = msf.getSpot(x,y);
                                    response = "position" + ";";
                                    foreach (int i in spots)
                                    {
                                        response = i+";";
                                    }
                                    

                                    if (spots.Count == 1)
                                    {
                                        foreach (int i in spots)
                                        {
                                            if (i / 10000 < 0)
                                            {
                                                send(response, socket);
                                            }
                                            else
                                            {
                                                sendToAll(response);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        sendToAll(response);
                                    }


                                    break;
                            default :

                                break;
                            }
                            break;
                        case "new board":
                            
                            combination = int.Parse(temp[2]);
                            StaticCodeConverter.decode(combination, out x, out y, out numOfBombs);
                            msf.newField(x, y, numOfBombs);
                            response = "new field made of;"+x+";"+y+";"+numOfBombs+";";
                            send(response, socket);
                            _activePlayers = _Players;

                            break;

                        case "disconect":
                            _numOfPlayers--;
                            _activePlayers.Remove(int.Parse(temp[0]));
                            _Players.Remove(int.Parse(temp[0]));
                            break;
                        default :

                            break;
                }
                    break;
            }
        }

        private static void sendToAll(string response)
        {
            foreach (Socket s in _clientSockets)
            {
                send(response, s);
            }
        }
        private static void send(string response, Socket socket)
        {
            byte[] data = Encoding.ASCII.GetBytes(response);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
        }

        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
