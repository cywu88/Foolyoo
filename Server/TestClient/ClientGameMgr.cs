using Message;
using NetWorkLib;
using System;
using System.Threading.Tasks;

namespace TestClient
{
    public class ClientGameMgr
    {
        ClientNetworkManager network;

        public bool isLogin { get; private set; }

        public void Login()
        {
            CBLoginRequest req = new CBLoginRequest();
            req.Token = "TestToken";

            network.Send<CBLoginRequest, CBLoginReply>(req
            , (rep) =>
            {
                isLogin = true;
                // Toast.instance.ShowNormal("登录战场服务器成功！");
             
            });
        }

        public void InitSocket()
        {
            network = new ClientNetworkManager(new WebSocketClient(), new ProtoSerializer());

            network.Init("192.168.1.11", 8878);
            network.heartbeat.onTimeout = OnTimeout;

            //Logger.Log("Init Network (Battle) [{0}]");

            network.heartbeat.onTimeout = OnTimeout;
            network.onConnected = () =>
            {
                Login();
            };
        }

        public void Connect()
        {
            network.Connect();
        }

        public void Init()
        {
            InitSocket();

            this.Connect();
        }

       

        private void OnTimeout()
        {
            Logger.LogError("Battle Server Timeout!!!");
            //SendEvent(EventDef.SOS.HeartbeatTimeout);
            //Reconnect();
        }
    }
}
