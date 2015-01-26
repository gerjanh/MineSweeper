using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace MineSweeper
{


    class Server
    {
        private static MineSweeperField msf = new MineSweeperField();
        private static int _cliendID = 0;
        private static int _turn = 0;
        private static int _numOfPlayers;
        private static List<int> _activePlayers = new List<int>();
        private static List<int> _Players = new List<int>();
        private static List<Socket> _clientSockets = new List<Socket>();
        private static Dictionary<int, Socket> _sockets = new Dictionary<int, Socket>();

        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] _buffer = new byte[16384];



        public Server()
        {
            setupServer();
            Console.Title = "server";
        }

        public void keepopen()
        {
            Console.ReadLine();
        }
        private static void setupServer()
        {
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);

        }

        private static void acceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            _sockets.Add(_cliendID++, socket);
            _numOfPlayers++;
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuf = new byte[received];
            Array.Copy(_buffer, dataBuf, received);

            string text = Encoding.ASCII.GetString(dataBuf);
            Command command = JsonConvert.DeserializeObject<Command>(text);


            String response = string.Empty;
            Command resp = new Command(); ;

            switch (command.theCommand)
            {

                case commands.get_connected:
                    resp.theCommand = commands.connected;
                    resp.parameters.Add(parameter.id, _cliendID);
                    _Players.Add(_cliendID);
                    break;

                case commands.new_board:
                    Console.WriteLine("er is een veld aangemaakt van {0} X {1} met {2} bommen", command.parameters[parameter.x], command.parameters[parameter.y], command.parameters[parameter.bombs]);
                    resp.theCommand = commands.board_made;
                    resp.clientId = command.clientId;
                    break;

                case commands.get_turn:
                    resp.theCommand = commands.turn;
                    resp.clientId = command.clientId;
                    resp.parameters.Add(parameter.player, _turn);

                    break;

                case commands.get_number_of_players:
                    resp.theCommand = commands.number_of_players;
                    resp.clientId = command.clientId;
                    resp.parameters.Add(parameter.player, _activePlayers.Count);
                    break;

                case commands.get_position:
                    resp.theCommand = commands.position;
                    resp.clientId = command.clientId;
                    resp.buttons = msf.getSpot(command.buttons[0].x, command.buttons[0].y);
                    break;

                case commands.get_disconnected:
                    resp.theCommand = commands.position;
                    resp.clientId = command.clientId;
                    _sockets.Remove(command.clientId);
                    break;

                default:
                    resp.theCommand = commands.unknown_command;
                    resp.clientId = command.clientId;
                    resp.parameters = command.parameters;

                    break;

            }
            send(resp, command.clientId);

        }

        private static void nextTurn()
        {
            _turn = _activePlayers[(_activePlayers.IndexOf(_turn) + 1) % _activePlayers.Count];
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
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
        }

        public static void send(Command mesage, int playerid)
        {
            string json = JsonConvert.SerializeObject(mesage);
            Socket socket = _sockets[playerid];
            send(json, socket);
        }


        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
