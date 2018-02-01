using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouvOgRathApp.ClientSide.ClientControllers;

namespace Test
{
    class TestClientProgram
    {
        static void Main(string[] args)
        {
            TestCommHandler test = new TestCommHandler();
            string v =test.GetDataResponse();
            Console.WriteLine("All done\n" + v);
            Console.ReadLine();
        }
    }
}
