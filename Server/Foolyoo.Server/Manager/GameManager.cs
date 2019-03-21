using System;
using System.Collections.Generic;
using System.Text;

namespace Foolyoo.Server
{
    public class GameManager
    {
        public void Start()
        {
            NetWorkManager netMgr = new NetWorkManager();
            netMgr.Init();
        }
    }
}
