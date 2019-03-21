//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using WebSocketSharp;
//using WebSocketSharp.Server;

//namespace NetWorkLib
//{
//    public class SocketServer : ServerBase
//    {
//        const string defaultServicePath = "/default";
//        public Socket serverSocket = null;
//        public override string address { get { return "ws://{0}:{1}{2}".FormatStr(_server.Address, _server.Port, defaultServicePath); } }
       
//        public override void Setup(string ip, int myProt)
//        {
//            IPAddress ipAdress = IPAddress.Parse("127.0.0.1");
//            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            serverSocket.Bind(new IPEndPoint(ipAdress, myProt));  //绑定IP地址：端口 
    
//            //通过Clientsoket发送数据 
//            Thread myThread = new Thread(ListenClientConnect);
//            myThread.Start();
//            Console.ReadLine();
//        }

//        private void ListenClientConnect()
//        {
//            while (true)
//            {
//                Socket clientSocket = serverSocket.Accept();
//                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
//                Thread receiveThread = new Thread(ReceiveMessage);
//                receiveThread.Start(clientSocket);
//            }
//        }

//        private static void ReceiveMessage(object clientSocket)
//        {
//            Socket myClientSocket = (Socket)clientSocket;
//            while (true)
//            {
//                try
//                {
//                    //通过clientSocket接收数据 
//                    int receiveNumber = myClientSocket.Receive(result);
//                    Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                    myClientSocket.Shutdown(SocketShutdown.Both);
//                    myClientSocket.Close();
//                    break;
//                }
//            }
//        }


//        private void SendAction(string sessionID, byte[] data)
//        {
//            host.Sessions.SendToAsync(data, sessionID, null);
//        }

//        public override void Start()
//        {
//            base.Start();
//            _server.Start();
//        }

//        public override void Stop()
//        {
//            base.Stop();
//            _server.Stop();
//        }
//    }
//}
