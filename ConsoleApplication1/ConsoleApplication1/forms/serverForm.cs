using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper.forms
{
    public partial class serverForm : Form
    {
        private static int _cliendID = 0,lifeTime=10;
        private static List<int> _activePlayers = new List<int>();
        private static List<int> _Players = new List<int>();
        private static Dictionary<int, Socket> _sockets = new Dictionary<int, Socket>();
        private static Dictionary<int, MineSweeperField> _field = new Dictionary<int, MineSweeperField>();
        private static Dictionary<int, int> _aliveTime = new Dictionary<int, int>();

        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] _buffer = new byte[65536];
        private static string text1 = "";
        private static int port = 9000;

        public serverForm()
        {
            InitializeComponent();
            setupServer();
            setCounter();
        }

        public void keepopen()
        {
            Console.ReadLine();
           
        }

        private void setCounter()
        {
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(OnTimedEvent);
            timer1.Start();
            serverStats.Text = "";
        }

        private void startCounter()
        {
            timer1.Start();
        }

        private void OnTimedEvent(Object source, EventArgs e)
        {
            serverStats.Text = text1;
            label1.Text="number of players on this server : " +_Players.Count;
         /*   if (lifeTime < 0)
            {
                List<int> tempList = new List<int>();
                foreach (int temp in _aliveTime.Keys)
                {
                        tempList.Add(temp); 
                }
                int i=0;
                while (i < tempList.Count)
                {
                    int temp=tempList[i];
                    _aliveTime[temp]--;
                    if (_aliveTime[temp] <= 0)
                    {
                        _sockets.Remove(i);
                        _field.Remove(i);
                        _aliveTime.Remove(i);
                        text1 += "hax je bent gekicked vuile hax";
                    }
                    else i++;
                }
                lifeTime = 10;
               
            }
            else lifeTime--;//*/
        }

        private static void setupServer()
        {
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port)); // it over nine thousend
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);

        }

        private static void acceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            _field.Add(_cliendID, new MineSweeperField());
            _sockets.Add(_cliendID, socket);
            _aliveTime.Add(_cliendID,10);
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
                    text1 = "new player with id " + _cliendID + " has joined this server \r\n" + text1;
                    resp.theCommand = commands.connected;
                    resp.parameters.Add(parameter.id, _cliendID);
                    _Players.Add(_cliendID);
                    _cliendID++;
            send(resp, command.clientId);
                    break;

                case commands.new_board:
                    Console.WriteLine("er is een veld aangemaakt van {0} X {1} met {2} bommen", command.parameters[parameter.x], command.parameters[parameter.y], command.parameters[parameter.bombs]);
                    _field[command.clientId].newField(command.parameters[parameter.x], command.parameters[parameter.y], command.parameters[parameter.bombs]);
                    resp.theCommand = commands.board_made;
                    resp.clientId = command.clientId;
                    text1 = "player with id " + resp.clientId + " has made a new field of " + command.parameters[parameter.x] + " x " + command.parameters[parameter.y] + " with " + command.parameters[parameter.bombs] + " \r\n" + text1;
            send(resp, command.clientId);



                    break;

                case commands.get_position:
                    resp.theCommand = commands.position;
                    resp.clientId = command.clientId;
                    resp.buttons = _field[command.clientId].getSpot(command.buttons[0].x, command.buttons[0].y);
                    text1 = "new player with id " + resp.clientId + " asked for position " + command.buttons[0].x + " x " + command.buttons[0].y + " \r\n" + text1;
            send(resp, command.clientId);
                    //resp.buttons = msf.getSpot(command.buttons[0].x, command.buttons[0].y);
                    break;

                case commands.get_disconnected:
                    resp.theCommand = commands.position;
                    resp.clientId = command.clientId;
                    _sockets.Remove(command.clientId);
                    text1 = "new player with id " + resp.clientId + " has been disconected \r\n" + text1;
            //send(resp, command.clientId);
                    break;

                case commands.keep_alive:
                    resp.theCommand = commands.keep_alive;
                    _aliveTime[command.clientId]=10;
            send(resp, command.clientId);
                    break;

                default:
                    resp.theCommand = commands.unknown_command;
                    resp.clientId = command.clientId;
                    resp.parameters = command.parameters;
                    text1 = "new player with id " + resp.clientId + " has sended an unknown command \r\n" + text1;

                    break;

            }
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);


        }


        private static void sendToAll(string response)
        {
            foreach (Socket s in _sockets.Values)
            {
                send(response, s);
            }
        }
        private static void send(string response, Socket socket)
        {
            byte[] data = Encoding.ASCII.GetBytes(response);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            //socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            //_serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
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
