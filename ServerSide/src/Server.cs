// Server-Side Implementation Of UDP:
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;
using System.Threading;

class BasicUdpServer
{
    static int port = 25565;
    static UdpClient udpc = new UdpClient(port);
    
    static volatile string p1Data = "N";
    static volatile string p2Data = "N";


    public static void Main()
    {
        Thread udpServerThread;
        udpServerThread = new Thread(new ThreadStart(ServerLoop));
        udpServerThread.Start();
        string command = "";
        Console.WriteLine("[Server] started on port " + port);
        do
        {
            Console.Write("[Server] ");
            command = Console.ReadLine();

            if (command == "data")
            {
                Console.WriteLine("[Server] Player 1: " + p1Data);
                Console.WriteLine("[Server] Player 2: " + p2Data);
            }

            if (command == "reset")
            {
                p1Data = "N";
                p2Data = "N";
                Console.WriteLine("[Server] data resetted ");
            }

            if (command == "clear")
            {
                Console.Clear();
                Console.WriteLine("[Server] console cleared ");
            }
                

        } while (command != "close");

        udpServerThread.Abort();
        Console.WriteLine("[Server] closed");
    }
    
    private static void ServerLoop()
    {
        byte[] sdata;
        byte[] receivedData;
        string message;

        while (true)
        {
            //ServerLoopStart
            try
            {
                IPEndPoint ep = null;
                receivedData = udpc.Receive(ref ep);

                message = Encoding.ASCII.GetString(receivedData);


                if (message.Substring(0, 1) == "A")
                {
                    p1Data = message;
                    if (p2Data == "N")
                    {
                        sdata = Encoding.ASCII.GetBytes("N");
                        udpc.Send(sdata, sdata.Length, ep);
                        continue;
                    }
                    sdata = Encoding.ASCII.GetBytes(p2Data);
                    udpc.Send(sdata, sdata.Length, ep);
                }
                else if (message.Substring(0, 1) == "B")
                {
                    p2Data = message;
                    if (p1Data == "N")
                    {
                        sdata = Encoding.ASCII.GetBytes("N");
                        udpc.Send(sdata, sdata.Length, ep);
                        continue;
                    }
                    sdata = Encoding.ASCII.GetBytes(p1Data);
                    udpc.Send(sdata, sdata.Length, ep);
                }

                //Debug.Log(udpGet.Substring(4, udpGet.Length - 4));
                //GetData(message.Substring(4, message.Length - 4));
            }
            catch (SocketException ex)
            {
                
                // Handle errors gracefully, e.g., log or attempt to reconnect
            }
        }
    }
}