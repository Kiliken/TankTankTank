// A C# Program for Server
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {

class Program {

// Main Method
static void Main(string[] args)
{
    ExecuteServer();
}

public static void ExecuteServer()
{
    // Establish the local endpoint 
    // for the socket. Dns.GetHostName
    // returns the name of the host 
    // running the application.
    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
    IPAddress ipAddr = ipHost.AddressList[0];
    IPAddress myIp = IPAddress.Parse("127.0.0.1");
    IPEndPoint localEndPoint = new IPEndPoint(myIp, 25565);

    // Creation TCP/IP Socket using 
    // Socket Class Constructor
    Socket listener = new Socket(myIp.AddressFamily,
                 SocketType.Stream, ProtocolType.Tcp);

    try {
        
        // Using Bind() method we associate a
        // network address to the Server Socket
        // All client that will connect to this 
        // Server Socket must know this network
        // Address
        listener.Bind(localEndPoint);

        // Using Listen() method we create 
        // the Client list that will want
        // to connect to Server
        listener.Listen(10);

        while (true) {
            
            Console.WriteLine("Waiting connection ... ");

            // Suspend while waiting for
            // incoming connection Using 
            // Accept() method the server 
            // will accept connection of client
            Socket clientSocket = listener.Accept();

            // Data buffer
            byte[] bytes = new Byte[1024];
            string data = null;

            while (true) {

                int numByte = clientSocket.Receive(bytes);
                
                data += Encoding.ASCII.GetString(bytes,
                                           0, numByte);
                                           
                if (data.IndexOf("<EOF>") > -1)
                    break;
            }

            Console.WriteLine("Text received -> {0} ", data);
            byte[] message = Encoding.ASCII.GetBytes("Test Server");

            // Send a message to Client 
            // using Send() method
            clientSocket.Send(message);
                    int i = 0;
                    do
                    {
                        i++;
                        data = "";
                        while (true)
                        {

                            int numByte = clientSocket.Receive(bytes);

                            data += Encoding.ASCII.GetString(bytes,
                                                       0, numByte);

                            if (data.IndexOf("<EOF>") > -1)
                                break;
                        }
                Console.WriteLine(data);
                    }
                    while (data != "S<EOF>");
        
            // Close client Socket using the
            // Close() method. After closing,
            // we can use the closed Socket 
            // for a new Client Connection
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
    
    catch (Exception e) {
        Console.WriteLine(e.ToString());
    }
}
}
}
/*
    public static void Main()
    {
        ConsoleKeyInfo cki;
        // Prevent example from ending if CTL+C is pressed.
        Console.TreatControlCAsInput = true;

        Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
        Console.WriteLine("Press the Escape (Esc) key to quit: \n");
        do
        {
            cki = Console.ReadKey();
            Console.Write(" --- You pressed ");
            if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
            if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
            if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
            if (cki.Key.ToString() == "d")
                Console.WriteLine("1");
            Console.WriteLine(cki.Key.ToString());
            
        } while (cki.Key != ConsoleKey.Escape);
    }
*/