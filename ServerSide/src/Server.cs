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
        byte[] sdata;
        string p1Data = "<P0>";
        string p2Data = "<P0>";

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
                if (p2Data == "<P0>")
                {
                    sdata = Encoding.ASCII.GetBytes("<P0>");
                    udpc.Send(sdata, sdata.Length, ep);
                    continue;
                }
                sdata = Encoding.ASCII.GetBytes(p2Data);
                udpc.Send(sdata, sdata.Length, ep);
            }
            else if (message.Substring(0, 4) == "<P2>")
            {
                p2Data = message;
                if (p1Data == "<P0>")
                {
                    sdata = Encoding.ASCII.GetBytes("<P0>");
                    udpc.Send(sdata, sdata.Length, ep);
                    continue;
                }
                sdata = Encoding.ASCII.GetBytes(p1Data);
                udpc.Send(sdata, sdata.Length, ep);
            }

        }
    }
}