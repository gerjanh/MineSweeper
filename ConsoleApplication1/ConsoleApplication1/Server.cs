using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.statics;
using System.Net.Security;
using System.Runtime.Remoting.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MineSweeper
{
    class Server
    {

        private readonly byte[] Buffer = new byte[1024];
        private const int _bufferSize = 1024;
        private readonly TcpClient _tcpclient;
        private readonly SslStream _sslStream;
        private List<byte> _totalBuffer;


        private void ThreadLoop()
        {
           /** while (true)
            {
                try
                {
                    //new Socket().Receive(Buffer);
                   var receiveCount = _sslStream.Read(Buffer, 0, _bufferSize);

                    byte[] rawData = new byte[receiveCount];
                    Array.Copy(Buffer, 0, rawData, 0, receiveCount);
                    _totalBuffer = _totalBuffer.Concat(rawData).ToList();


                    int packetSize = Packet.getLengthOfPacket(_totalBuffer);
                    if (packetSize == -1)
                        continue;

                    JObject json = Packet.RetrieveJSON(packetSize, ref _totalBuffer);

                    if (json == null)
                        continue;

                    JToken cmd;
                    if (!json.TryGetValue("CMD", out cmd))
                    {
                        Console.WriteLine("Got JSON that does not define a command.");
                        continue;
                    }

                    var packetType = cmd.ToString().ToLower();

                    switch (packetType)
                    {
                        case "login":
                            HandleLoginPacket(json);
                            break;
                        default:
                            Console.WriteLine("Unknown packet");
                            break;
                    }




                    //_totalBuffer = _totalBuffer.Substring(packetSize + 4);
                    //_totalBuffer = String.Empty;
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Client with IP-address: " + _tcpclient.Client.LocalEndPoint + " has been disconnected.");
                    Console.WriteLine(e.Message);
                }
            }*/
        }

        private void Send(String s)
        {
            //byte[] data = Encoding.UTF8.GetBytes(s.Length.ToString("0000") + s).ToArray();

           // _sslStream.Write(Packet.CreateByteData(s));
        }

        private void HandleLoginPacket(JObject json)
        {
            //Recieve the username and password from json.
            var username = json["USERNAME"].ToString();

            JObject returnJson;
            //Code to check user/pass here

            returnJson =
                   new JObject(
                       new JProperty("CMD", "resp-login")//,
                 //      new JProperty("status", new Statuscode("ok"));
                    //   new JProperty("STATUS", Statuscode.GetCode(Statuscode.Status.InvalidUsernameOrPassword)),
                    //   new JProperty("DESCRIPTION", Statuscode.GetDescription(Statuscode.Status.InvalidUsernameOrPassword))
                       );

            //Send the result back to the client.
            Console.WriteLine(returnJson.ToString());
            Send(returnJson.ToString());
        }

        public void setupfield(){

        }
    }
}
