// Client-Side Implementation Of UDP:
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class StudentUDPClient
{
    public static void Main(string[] args)
    {
        UdpClient udpc = new UdpClient("127.0.0.1", 25565);
        IPEndPoint ep = null;
        
        // Prevent example from ending if CTL+C is pressed.


        string side = Console.ReadLine() == "1" ? "<P1>" : "<P2>";
        ConsoleKeyInfo cki;

        Console.TreatControlCAsInput = true;

        Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
        Console.WriteLine("Press the Escape (Esc) key to quit: \n");

        

        do
        {
            cki = Console.ReadKey();
            Console.Write(" --- You pressed ");
            Console.WriteLine(cki.Key.ToString());

            /*if (cki.KeyChar == 'k')
            {
                byte[] msg = Encoding.ASCII.GetBytes("S<EOF>");
                udpc.Send(msg, msg.Length);
                continue;
            }*/


            // Data to send
            byte[] msg = Encoding.ASCII.GetBytes(side + cki.Key.ToString());
            udpc.Send(msg, msg.Length);

            // received Data
            /*byte[] rdata = udpc.Receive(ref ep);
            string job = Encoding.ASCII.GetString(rdata);
            Console.WriteLine(job);*/
        } while (cki.KeyChar != 'k');
    }
}