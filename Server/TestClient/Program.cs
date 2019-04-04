

using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //ClientGameMgr mgr = new ClientGameMgr();
            //mgr.Init();

            //while (true)
            //{
            //    Thread.Sleep(10000);
            //}




            var str1 = "^[\xAC00-\xD7A3]+$";
            System.Console.WriteLine("str1:" + str1);


            var match  = new Regex(str1);
            var roleName = "거기있니";

            if (match.IsMatch(roleName))
            {
                int a = 100;
            }


            var str2 = "^[\xac00-\xd7a3_a-zA-Z0-9]+$";

            System.Console.WriteLine("str2:" + str2);

            var match2 = new Regex(str2);
            if (match2.IsMatch(roleName))
            {
                int a = 100;
            }
            int b = 1000;

             
            var str3 = "^[\\\xac00-\xd7a3_a-zA-Z0-9]+$";

            str3 = str3.Replace("\\", "");
            System.Console.WriteLine("str3:" + str3);



            var match3 = new Regex(str3);
            
            if (match3.IsMatch(roleName))
            {
                int a2 = 100;
            }



        }
    }
}
