using System;
using System.Collections.Generic;
using System.Text;

namespace NetWorkLib
{
    public interface IServer
    {
        void Setup(string ip,int port);

        void Start();

        void Stop();

        void Send(string sessionID, byte[] content);

        Action<string> OnConnected { get; set; }

        Action<string> OnClosed { get; set; }

        Action<string,byte[]> OnReceived { get; set; }

        ISession GetSession(string sessionID);

    }
}
