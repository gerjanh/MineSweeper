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
            Console.Title = "troll";


        Thread newThread = new Thread(new ThreadStart(loopCheck));
        newThread.Start(); 
    }

        public void keepalive()
        {


        }

        public void loopCheck() 
     {
        Thread.Sleep(500);
        this.turn = myTurn();
        string[] responses = receive().Split(';');
        if (responses[0] == "position")
        {

        }


     }

        public int getTurn()
        {
            return turn;
        }

        private  void loopConnect()
        {
            int attempts = 0;

            while (!_clientSocket.Connected && attempts < 8080)
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
                    this._clientID = int.Parse(send("get connected"));
                    //newfield(8, 8, 10);
                    Console.WriteLine(_clientID);

                }
            }
            send(_clientID + ";new board;101010");
            
            getPosition(0,1);
            
        }

        private string send(string text)
        {
            String request = text;
            byte[] buffer = Encoding.ASCII.GetBytes(request);
            _clientSocket.Send(buffer);

            return receive();
        }

        private string receive()
        {
            
            byte[] receivedBuf = new byte[1024];
            int rec = _clientSocket.Receive(receivedBuf);
            byte[] data = new byte[rec];
            Array.Copy(receivedBuf, data, rec);
            string receive = Encoding.ASCII.GetString(data);
            Console.WriteLine(receive);
            return receive;
        }

        public void newfield(int x , int y , int numberOfBombs)
        {
            int combination = 0;
            StaticCodeConverter.encode(x, y, numberOfBombs, out combination);
            send(_clientID + ";new board;" + combination);
        }
        public List<ButtonPosition> getPosition(int x , int y)
        {
            int combination = 0;
            StaticCodeConverter.encode(x, y, out combination);
            
            string response = send(_clientID + ";get:position;" + combination);
            string[] responses = response.Split(';');
            List<ButtonPosition> coordinates = new List<ButtonPosition>();

            for (int i = 1; i < responses.Length; i++)
            {
                int rx;
                int ry;
                int rpoint;
                StaticCodeConverter.decode(i,out rx,out ry,out rpoint);
                coordinates.Add(new ButtonPosition(rx,ry,rpoint));
            }
            return coordinates;
        }

        public int myTurn()
        {
            string response = send(_clientID + ";get:turn");
            string[] responses = response.Split(';');
            int turn=0;
            turn = int.Parse(responses[0]);
            return turn ;
        }

        public Boolean WhosTurn()
        {
            if (myTurn() == _clientID)
            {
                return true;
            }
            else return false;
        }

        public int getNumberOfPlayers()
        {
            int players = 0;

            string response = send(_clientID + ";get:turn");
            string[] responses = response.Split(';');
            players = int.Parse(responses[0]);

            return players;
        }

        public void disconect()
        {
            string response = send(_clientID + ";disconect");
        }

    }
}
