using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineSweeper
{
    class ClientConnect
    {
        private int _clientID;
        private static Socket _clientSocket;
        //public bool verbonden { get; }
        private IPAddress ipAdress = IPAddress.Loopback;

        public ClientConnect()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            loopConnect();
            Thread newThread = new Thread(new ThreadStart(loopCheck));
            newThread.Start();
        }

        public ClientConnect(string ip)
        {

            if (IPAddress.TryParse(ip, out ipAdress))
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                loopConnect();
                Thread newThread = new Thread(new ThreadStart(loopCheck));
                newThread.Start();
            }
            else
            {
                loopConnect();
                Thread newThread = new Thread(new ThreadStart(loopCheck));
                newThread.Start();
            }
        }

        public void keepalive()
        {


        }

        public void loopCheck()
        {
            while(true){
            Thread.Sleep(500);
        }

        }

        private void loopConnect()
        {
            int attempts = 0;

            while (!_clientSocket.Connected && attempts < 3)
            {
                attempts++;
                Thread.Sleep(500);
                Console.WriteLine("poging " + attempts);
                try
                {
                    _clientSocket.Connect(ipAdress, 9001); // its over nine thousend
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                    attempts++;
                }
                finally
                {
                    Command command = new Command();
                    command.theCommand = commands.get_connected;
                    string json = JsonConvert.SerializeObject(command);
                    this._clientID = (send(json)).clientId;
                    Console.WriteLine("my client id = " + _clientID);

                }
            }
        }

        private Command send(string text)
        {
            String request = text;
            byte[] buffer = Encoding.ASCII.GetBytes(request);
            _clientSocket.Send(buffer);

            return receive();
        }

        private Command receive()
        {

            byte[] receivedBuf = new byte[65536];
            int rec = _clientSocket.Receive(receivedBuf);
            byte[] data = new byte[rec];
            Array.Copy(receivedBuf, data, rec);
            string receive = Encoding.ASCII.GetString(data);
            Command temp = JsonConvert.DeserializeObject<Command>(receive);
            //Console.WriteLine(temp.theCommand.ToString() + " " + temp.clientId + " " + temp.parameters.Count);
            Debug.WriteLine(temp.theCommand.ToString() + " " + temp.clientId + " " + temp.parameters.Count);
            return temp;
        }

        public void newfield(int x, int y, int numberOfBombs)
        {
            Command newBoardCommand = new Command();
            newBoardCommand.theCommand = commands.new_board;
            newBoardCommand.clientId = _clientID;
            newBoardCommand.parameters.Add(parameter.bombs, numberOfBombs);
            newBoardCommand.parameters.Add(parameter.x, x);
            newBoardCommand.parameters.Add(parameter.y, y);
            string json = JsonConvert.SerializeObject(newBoardCommand);
            send(json);

        }
        public List<ButtonPosition> getPosition(int x, int y)
        {
            Command command = new Command();
            command.theCommand = commands.get_position;
            command.clientId = _clientID;
            command.buttons.Add(new ButtonPosition(x, y));

            string json = JsonConvert.SerializeObject(command);
            Command response = send(json);
            Console.WriteLine("ontvangen {0} aantal buttens", response.buttons.Count);
            List<ButtonPosition> coordinates = new List<ButtonPosition>();
            coordinates = response.buttons;
            return coordinates;
        }

        public void disconect()
        {
            Command command = new Command();
            command.theCommand = commands.get_disconnected;
            command.clientId = _clientID;
            string json = JsonConvert.SerializeObject(command);
            Command response = send(json);
        }

    }
}
