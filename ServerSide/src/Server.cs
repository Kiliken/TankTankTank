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
    
    static volatile byte[] p1Data;
    static volatile byte[] p2Data;


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
                Console.WriteLine("[Server] Player 1: " + Encoding.ASCII.GetString(p1Data));
                Console.WriteLine("[Server] Player 2: " + Encoding.ASCII.GetString(p2Data));
            }

            if (command == "reset")
            {
                p1Data = new byte[] { 0x4E };
                p2Data = new byte[] { 0x4E };
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
        //string message;

        p1Data = new byte[] { 0x4E };
        p2Data = new byte[] { 0x4E };
		

        while (true)
        {
            //ServerLoopStart
            try
            {
                IPEndPoint ep = null;
                receivedData = udpc.Receive(ref ep);

                //message = Encoding.ASCII.GetString(receivedData);


                if (receivedData[0] == 0x41)
                {
                    p1Data = receivedData;
                    if (p2Data[0] == 0x4E)
                    {
                        sdata = Encoding.ASCII.GetBytes("N");
                        udpc.Send(sdata, sdata.Length, ep);
                        continue;
                    }
                    //sdata = Encoding.ASCII.GetBytes(p2Data);
                    udpc.Send(p2Data, p2Data.Length, ep);
                }
                else if (receivedData[0] == 0x42)
                {
                    p2Data = receivedData;
                    if (p1Data[0] == 0x4E)
                    {
                        sdata = Encoding.ASCII.GetBytes("N");
                        udpc.Send(sdata, sdata.Length, ep);
                        continue;
                    }
                    //sdata = Encoding.ASCII.GetBytes(p1Data);
                    udpc.Send(p1Data, p1Data.Length, ep);
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