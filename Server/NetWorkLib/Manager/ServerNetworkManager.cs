using Core;
using NetworkLib;
using System;
 

namespace NetWorkLib
{
    public class ServerNetworkManager : IDisposable
    {
        protected EventManager m_eventMgr = null;

        protected ISerializer m_serializer = null;

        protected IServer m_server = null;
        protected HeartbeatServer m_heartbeat = null;


        public Action<string> onConnected { get; set; }
        public Action<string> onClosed { get; set; }

        public ServerNetworkManager(IServer server, ISerializer serializer)
        {
            m_eventMgr = new EventManager();

            m_server = server;
            m_serializer = serializer;

            m_server.OnReceived += OnReceived;
            m_server.OnConnected += OnConnected;
            m_server.OnClosed += OnClosed;
        }

        public void Init(string ip,int port)
        {
            m_serializer.Init();
            m_server.Setup(ip, port);
        }

        public void Start()
        { 
            m_server.Start();

        }

        public void Stop()
        {
            m_server.Stop();
        }

        protected virtual void OnClosed(string sessionID)
        {

            if (onClosed != null)
                onClosed.Invoke(sessionID);
        }

        protected virtual void OnConnected(string sessionID)
        {
            if (onConnected != null)
                onConnected.Invoke(sessionID);
        }


        protected virtual void OnReceived(string sessionID, byte[] data)
        {
            object obj = null;
            var protocolNum = GetProtocolNum(data);
            if (protocolNum == HeartbeatRequest.PROTOCOL_NUM)
            {
                if (m_heartbeat != null)
                {
                    obj = m_heartbeat.HandleMessage(sessionID, data);
                    OnReceivedHandle(sessionID, obj);
                }
            }
            else
            {
                obj = m_serializer.Deserialize(data);
                OnReceivedHandle(sessionID, obj);
            }
        }


        protected virtual void OnReceivedHandle(string sessionID, object data)
        {
            m_eventMgr.Send(data.GetType().Name, sessionID, data);
        }

        private ushort GetProtocolNum(byte[] data)
        {
            byte[] head = new byte[2];
            Array.Copy(data, head, 2);
            return BitConverter.ToUInt16(head, 0);
        }

        public void Dispose()
        {
            m_server.OnReceived -= OnReceived;
            m_server.OnConnected -= OnConnected;
            m_server.OnClosed -= OnClosed;
        }
    }
}
