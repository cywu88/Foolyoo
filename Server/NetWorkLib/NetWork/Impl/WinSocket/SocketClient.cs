using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace NetWorkLib
{
    public class SocketClient : ClientBase
    {
        public Socket _socket = null;

        IPEndPoint endpoint;

        public override void Setup(string host, int port)
        {
            base.Setup(host, port);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Blocking = false;
            _socket.NoDelay = true;

            IPAddress address = null;
            if (!IPAddress.TryParse(host, out address))
            {
                IPHostEntry hostinfo = Dns.GetHostEntry(host);
                IPAddress[] aryIP = hostinfo.AddressList;
                address = aryIP[0];
            }

            this.endpoint = new IPEndPoint(address, port);



            //_socket.Connected += (a, b) => { base.OnConnected(); };
            //_socket.OnClose += (a, b) => { base.OnClosed(); };
            //_socket.OnMessage += (a, b) => { base.OnReceived(b.RawData); };
        }
        public override string address { get { return "123"; } }
        public override void Send(byte[] data)
        {
            base.Send(data);
            SendData(data, 0, data.Length);
        }

        public void SendData(byte[] data, int offset, int length)
        {
            int sent = 0;
            int thisSent = 0;

            while ((length - sent) > 0)
            {
                thisSent = _socket.Send(data, offset + sent, length - sent, SocketFlags.None);
                sent += thisSent;
            }
        }

        public override void Connect()
        {
            base.Connect();

            _socket.BeginConnect("192.168.1.11", 8878, _end_connect, _socket);

             
        }

        private void _end_connect(IAsyncResult result)
        {
            var so = result.AsyncState as Socket;

            try
            {
                so.EndConnect(result);
                if (so.Connected)
                {
                    
                }
                else
                {
                     
                }
            }
            catch (SocketException err)
            {
                if (err.SocketErrorCode == SocketError.TimedOut)
                {
                    int a = 11;
                }
                else
                {
                    int a = 11;
                }
            }
            catch (Exception err)
            {
                int a = 11;
            }

            base.OnConnected();

        }


        public override void Close()
        {
            base.Close();
            _socket.Close();
        }

    }
}
