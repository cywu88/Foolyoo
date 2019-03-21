using NetWorkLib;
using System;
using System.Collections.Generic;
using System.Text;


namespace Foolyoo.Server
{
    public class NetWorkManager
    { 
        /// <summary>
        /// 监听客户端
        /// </summary>
        public ServerNetworkManager forClient { get; private set; }

        public NetWorkManager()
        {
            forClient = new ServerNetworkManager(new WebSocketServer(), new ProtoSerializer());
        }

        public void Init()
        {
            InitForClient();
         
        }

        private void InitForClient()
        {
            forClient.Init("192.168.1.11", 8878);
            forClient.Start();
        }
    }
}
