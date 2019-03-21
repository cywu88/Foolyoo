

using System.Threading;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientGameMgr mgr = new ClientGameMgr();
            mgr.Init();

            while (true)
            {
                Thread.Sleep(10000);
            }

        }
    }
}
