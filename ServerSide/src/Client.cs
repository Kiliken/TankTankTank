// A C# program for Client
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client {

class Program {

// Main Method
static void Main(string[] args)
{
    ExecuteClient();
}

// ExecuteClient() Method
static void ExecuteClient()
{

    try {
        
        // Establish the remote endpoint 
        // for the socket. This example 
        // uses port 11111 on the local 
        // computer.
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipHost.AddressList[0];
        IPAddress myIp = IPAddress.Parse("127.0.0.1");
        IPEndPoint localEndPoint = new IPEndPoint(myIp, 25565);

        // Creation TCP/IP Socket using 
        // Socket Class Constructor
        Socket sender = new Socket(myIp.AddressFamily,
                   SocketType.Stream, ProtocolType.Tcp);

        try {
            
            // Connect Socket to the remote 
            // endpoint using method Connect()
            sender.Connect(localEndPoint);

            // We print EndPoint information 
            // that we are connected
            Console.WriteLine("Socket connected to -> {0} ",
                          sender.RemoteEndPoint.ToString());

            // Creation of message that
            // we will send to Server
            byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
            int byteSent = sender.Send(messageSent);
            
        ConsoleKeyInfo cki;
        // Prevent example from ending if CTL+C is pressed.
        Console.TreatControlCAsInput = true;

        Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
        Console.WriteLine("Press the Escape (Esc) key to quit: \n");
                    do
                    {
                        cki = Console.ReadKey();
                        Console.Write(" --- You pressed ");
                        Console.WriteLine(cki.Key.ToString());

                        if (cki.KeyChar == 'k')
                        {
                            messageSent = Encoding.ASCII.GetBytes("S<EOF>");
                            byteSent = sender.Send(messageSent);
                            continue;
                        }

                        messageSent = Encoding.ASCII.GetBytes(" --- You pressed " + cki.Key.ToString() + "<EOF>");
                        byteSent = sender.Send(messageSent);
            
            
        } while (cki.KeyChar != 'k');

            // Data buffer
                    byte[] messageReceived = new byte[1024];

            // We receive the message using 
            // the method Receive(). This 
            // method returns number of bytes
            // received, that we'll use to 
            // convert them to string
            int byteRecv = sender.Receive(messageReceived);
            Console.WriteLine("Message from Server -> {0}", 
                  Encoding.ASCII.GetString(messageReceived, 
                                             0, byteRecv));



            Console.ReadKey();

            // Close Socket using 
                    // the method Close()
                    sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        
        // Manage of Socket's Exceptions
        catch (ArgumentNullException ane) {
            
            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        }
        
        catch (SocketException se) {
            
            Console.WriteLine("SocketException : {0}", se.ToString());
        }
        
        catch (Exception e) {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
    }
    
    catch (Exception e) {
        
        Console.WriteLine(e.ToString());
    }
}
}
}