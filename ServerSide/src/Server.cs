using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class Server
{
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

    public struct Vector2
    {
        float x  = 0f { get; set; }
        float y  = 0f { get; set; }
        
        public Vector2(float X, float Y)
{

}
    }
}
