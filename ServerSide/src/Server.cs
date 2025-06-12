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
        IPEndPoint ep = null;
        byte[] sdata;
        string p1Data = "N";
        string p2Data = "N";
        byte[] receivedData;
        string message;

        while (true)
        {


            Console.Clear();
            Console.WriteLine("<P1>" + p1Data);
            Console.WriteLine("<P2>" + p2Data);

            // Store received data from client
            receivedData = udpc.Receive(ref ep);

            message = Encoding.ASCII.GetString(receivedData);

            //string msg = ConfigurationSettings.AppSettings[studentName];
            /*if (msg == null) msg = "No such Student available for conversation";
            byte[] sdata = Encoding.ASCII.GetBytes(msg);
            udpc.Send(sdata, sdata.Length, ep);*/

            

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

           


        }
    }
}