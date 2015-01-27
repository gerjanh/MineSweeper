using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private int turn;

        public ClientConnect()
        {
            loopConnect();
            Thread newThread = new Thread(new ThreadStart(loopCheck));
            newThread.Start();
        }

        ClientConnect cc;
        /*public void alive(ClientConnect ClientConnect)
        {
            this.cc = ClientConnect;
            while (true)
            {

            }
        }*/

        public void keepalive()
        {


        }

        public void loopCheck()
        {
            Thread.Sleep(500);
            this.turn = whosTurn();
        }

        private void loopConnect()
        {
            int attempts = 0;

            while (!_clientSocket.Connected && attempts < 100)
            {
                attempts++;
                Thread.Sleep(500);
                Console.WriteLine("poging " + attempts);
                try
                {
                    _clientSocket.Connect(IPAddress.Loopback, 8080);
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

            byte[] receivedBuf = new byte[16384];
            int rec = _clientSocket.Receive(receivedBuf);
            byte[] data = new byte[rec];
            Array.Copy(receivedBuf, data, rec);
            string receive = Encoding.ASCII.GetString(data);
            Command temp = JsonConvert.DeserializeObject<Command>(receive);
            Console.WriteLine(temp.theCommand.ToString() + " " + temp.clientId + " " + temp.parameters.Count);

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

        public int whosTurn()
        {
            Command command = new Command();
            command.theCommand = commands.get_turn;
            command.clientId = _clientID;
            string json = JsonConvert.SerializeObject(command);
            Command response = send(json);

            return response.parameters[parameter.player];
        }

        public Boolean myTurn()
        {
            if (whosTurn() == _clientID)
            {
                return true;
            }
            else return false;
        }

        public int getNumberOfPlayers()
        {
            Command command = new Command();
            command.theCommand = commands.get_number_of_players;
            command.clientId = _clientID;
            string json = JsonConvert.SerializeObject(command);
            Command response = send(json);
            return response.parameters[parameter.player];
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
