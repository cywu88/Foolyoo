using System;
using System.Threading;

namespace Foolyoo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Console.Title = "Foolyoo.Server";

            //ConfigLogger();

            GameManager gMgr = new GameManager();
            gMgr.Start();

            while (true)
            {
                Thread.Sleep(10000);
            }
        }

        //private static void ConfigLogger()
        //{
        //    AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        //    {
        //        Exception excp = (Exception)e.ExceptionObject;
        //        Logger.Log(excp.Message + "\n" + excp.StackTrace);
        //    };
        //    Logger.SetFilePath("MainServer.log");
        //}
    }
}
