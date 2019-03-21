using System;
using NetWorkLib;

namespace RedStone
{
    public class ProxyBase
    {
        public virtual ClientNetworkManager network
        {
            get { return null; }
        }

     
        public virtual void SendMessage<T>(T msg)
        {
            network.Send(msg);
        }

        public virtual void SendMessage<TSend, TReply>(TSend msg, Action<TReply> action)
        {
            network.Send(msg, action);
        }

        public virtual void RegisterMessage<T>(Action<T> callback)
        {
            network.RegisterNetwork(callback);
        }

        public virtual void OnInit()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnDestroy()
        {

        }
    }
}
