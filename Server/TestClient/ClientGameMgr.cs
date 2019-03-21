using Message;
using NetWorkLib;
using System;
using System.Threading.Tasks;

namespace TestClient
{
    public class ClientGameMgr
    {
        ClientNetworkManager forClient;

        public bool isLogin { get; private set; }

        public void Login()
        {
            CBLoginRequest req = new CBLoginRequest();
            req.Token = "TestToken";

            forClient.Send<CBLoginRequest, CBLoginReply>(req
            , (rep) =>
            {
                isLogin = true;
                // Toast.instance.ShowNormal("登录战场服务器成功！");
             
            });
        }

        public void Init()
        {
            forClient = new ClientNetworkManager(new WebSocketClient(), new ProtoSerializer());

            forClient.Init("127.0.0.1", 6638);

            this.Login();

        }
    }
}
