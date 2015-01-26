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
        private static int _cliendID=0;
        private static int _turn=0;
        private static int _numOfPlayers;
        private static List<int> _activePlayers=new List<int>();
        private static List<int> _Players = new List<int>();
        private static List<Socket> _clientSockets = new List<Socket>();
        private static Dictionary<int, Socket> _sockets = new Dictionary<int, Socket>();

        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] _buffer = new byte[1024];
        
        

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
           // _clientSockets.Add(socket);
           // _cliendID++;
            _numOfPlayers++;
            // client toegevoegd
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
                    resp.theCommand=commands.connected;
                    resp.parameters.Add("id", _cliendID);
                    _Players.Add(_cliendID);
                        break;

                case commands.new_board:
                        Console.WriteLine("er is een veld aangemaakt van {0} X {1} met {2} bommen", command.parameters["x"], command.parameters["y"], command.parameters["bombs"]);
                     resp.theCommand=commands.board_made;
                     resp.clientId = command.clientId;

                     msf.newField(command.parameters["x"], command.parameters["y"], command.parameters["bombs"]);

                    
                        List<ButtonPosition> temp =msf.getSpot(0, 1);

                    int[,] field = msf.getfield();
                        for (int i = 0; i < 5; i++)
                        {
                            for (int x = 0; x < 5; x++)
                            {
                                Console.Write(" " + field[x, i]);
                            }
                            Console.WriteLine("");
                        }

                            foreach (ButtonPosition t in temp)
                            {
                                Console.WriteLine("waarde {0} op {1} X {2}", t.point, t.x, t.y);
                            }
                    
                        break;

                case commands.get_turn:
                        resp.theCommand = commands.turn;
                        resp.clientId = command.clientId;
                        resp.parameters.Add("player",_turn);

                        break;

                case commands.get_number_of_players:
                        resp.theCommand = commands.number_of_players;
                        resp.clientId = command.clientId;
                        resp.parameters.Add("players",_activePlayers.Count);
                    break;

                case commands.get_position :

                    break;

                default:
                    resp.theCommand = commands.unknown_command;
                    resp.clientId = command.clientId;
                    resp.parameters = command.parameters;
                    
                    break;

            }
            send(resp, command.clientId);


            /*
                switch (command.TheCommand)
                {

                    case "get connected":
                        resp = new Command("");
                        resp.ClientId = _cliendID;
                        _Players.Add(_cliendID);

                        send(response, socket);
                        Console.WriteLine("connected");
                        _cliendID++;
                        break;

                    default:
                        int x;
                        int y;
                        int numOfBombs;
                        int point;
                        int combination;

                        switch (temp[1].ToLower())
                        {

                            case "get":
                                switch (temp[2].ToLower())
                                {
                                    case "number of players":
                                        response = _numOfPlayers + ";";
                                        send(response, socket);
                                        break;
                                    case "turn":
                                        response = _turn + ";";
                                        send(response, socket);
                                        break;
                                    case "position":
                                        combination = int.Parse(temp[3]);
                                        StaticCodeConverter.decode(combination, out x, out y);
                                        Console.WriteLine("position " + x + " " + y);
                                        List<int> spots = msf.getSpot(x, y);
                                        response = "position" + ";";

                                        foreach (int i in spots)
                                        {
                                            response = i + ";";
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
                                            nextTurn();
                                        }
                                        else
                                        {
                                            sendToAll(response);
                                            int tempturn = _turn;
                                            nextTurn();
                                            _activePlayers.Remove(tempturn);
                                        }


                                        break;
                                    default:
                                        Console.WriteLine(temp[2]);
                                        break;
                                }
                                break;
                            case "new board":
                                int num = int.Parse(command.Parameters["bombs"]);
                                combination = int.Parse(temp[2]);
                                StaticCodeConverter.decode(combination, out x, out y, out numOfBombs);
                                msf.newField(x, y, numOfBombs);
                                response = "new field made of;" + x + ";" + y + ";" + numOfBombs + ";";
                                send(response, socket);
                                _activePlayers = _Players;

                                break;

                            case "disconect":
                                _numOfPlayers--;
                                _activePlayers.Remove(int.Parse(temp[0]));
                                _Players.Remove(int.Parse(temp[0]));
                                if (int.Parse(temp[0])==_turn){
                                    nextTurn();
                                }
                                break;
                            default:

                                break;
                        }
                        break;
                }  //*/
        }

        private static void nextTurn()
        {
            _turn =_activePlayers[(_activePlayers.IndexOf(_turn)+1)%_activePlayers.Count];
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

        public static void send(Command mesage,int playerid)
        {
            string json = JsonConvert.SerializeObject(mesage);
            Socket socket = _sockets[playerid];
            send (json,socket);
        }


        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
