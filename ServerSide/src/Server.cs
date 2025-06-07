// Server-Side Implementation Of UDP:
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;

class StudentUDPServer
{
    public static void Main()
    {
        UdpClient udpc = new UdpClient(25565);
        Console.WriteLine("Server Started, servicing on port no. 25565");
        IPEndPoint ep = null;
        IPEndPoint player1 = null;
        IPEndPoint player2 = null;
        byte[] sdata;
        string p1Data;
        string p2Data;

        while (true)
        {

            // Store received data from client
            byte[] receivedData = udpc.Receive(ref ep);

            string message = Encoding.ASCII.GetString(receivedData);

            //string msg = ConfigurationSettings.AppSettings[studentName];
            /*if (msg == null) msg = "No such Student available for conversation";
            byte[] sdata = Encoding.ASCII.GetBytes(msg);
            udpc.Send(sdata, sdata.Length, ep);*/

            Console.WriteLine(message);

            


            if (message.Substring(0, 4) == "<P1>")
            {
                p1Data = message;
                if (player1 == null) player1 = ep;
                if (player2 == null)
                {
                    sdata = Encoding.ASCII.GetBytes(message);
                    udpc.Send(sdata, sdata.Length, player1);
                    continue;
                }
                sdata = Encoding.ASCII.GetBytes(p1Data);
                udpc.Send(sdata, sdata.Length, player2);
            }
            else if (message.Substring(0, 4) == "<P2>")
            {
                p2Data = message;
                if (player2 == null) player2 = ep;
                if (player1 == null)
                {
                    sdata = Encoding.ASCII.GetBytes(message);
                    udpc.Send(sdata, sdata.Length, player2);
                    continue;
                }
                sdata = Encoding.ASCII.GetBytes(p2Data);
                udpc.Send(sdata, sdata.Length, player1);
            }

        }
    }
}