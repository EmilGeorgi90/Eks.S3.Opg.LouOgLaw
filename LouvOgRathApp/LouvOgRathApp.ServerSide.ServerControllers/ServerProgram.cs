using System;

namespace LouvOgRathApp.ServerSide.ServerControllers
{
    class ServerProgram
    {

        static ServerController server;
        static void Main()
        {
            while (true)
            {
                try
                {
                    server = new ServerController();
                    server.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}