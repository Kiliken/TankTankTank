using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;

/*

Data:

player,vector3sign,vector3x,vector3y,vector3z,eurlerx,eulery,eulerz

DataDigit:
0,0,00000,00000,00000,00000,00000,00000

vector3sign:
    0: + + +
    1: - - -
    2: + - -
    3: + + -
    4: - + +
    5: - - +


*/


public static class NetManager
{
    public static volatile string udpSend = "N";
    public static volatile string udpGet = "N";

    public static UdpClient udpc;

    public static void SendGetData()
    {
        Debug.Log("ThreadStarted");
        while (true)
        {
            //Debug.Log("threding");
            try
            {
                IPEndPoint ep = null;
                //byte[] msg = Encoding.ASCII.GetBytes($"{playerSide}{ParseToString(player.transform.position)}");
                byte[] msg = Encoding.ASCII.GetBytes(udpSend);
                udpc.Send(msg, msg.Length);

                byte[] rdata = udpc.Receive(ref ep);
                udpGet = Encoding.ASCII.GetString(rdata);



                //Debug.Log(udpGet.Substring(4, udpGet.Length - 4));
                //GetData(message.Substring(4, message.Length - 4));
            }
            catch (SocketException ex)
            {
                Debug.LogError("Socket Exception: " + ex.Message);
                // Handle errors gracefully, e.g., log or attempt to reconnect
            }
        }

        // Allow the thread to start
    }

    public static NetData RetriveData(string str)
    {
        NetData data = new NetData();
        char[] converter = str.ToCharArray(0, str.Length);

        if (converter[0] == 'N')
            return null;

        data.posX = float.Parse(new string(converter, 1, 6));
        data.posY = float.Parse(new string(converter, 7, 6));
        data.posZ = float.Parse(new string(converter, 13, 6));
        data.rotX = float.Parse(new string(converter, 19, 5));
        data.rotX = float.Parse(new string(converter, 24, 5));
        data.rotY = float.Parse(new string(converter, 29, 5));

        data.posX /= 100;
        data.posY /= 100;
        data.posZ /= 100;
        data.rotX /= 100;
        data.rotY /= 100;
        data.rotZ /= 100;

        /*switch (converter[1])
        {
            case '1':
                data.posX *= -1;
                data.posY *= -1;
                data.posZ *= -1;
                break;
            case '2':
                data.posY *= -1;
                data.posZ *= -1;
                break;
            case '3':
                data.posZ *= -1;
                break;
            case '4':
                data.posX *= -1;
                break;
            case '5':
                data.posX *= -1;
                data.posY *= -1;
                break;
        }*/
        return data;
    }

    /*public static NetData RetriveData(string str)
    {
        NetData data = new NetData();
        string[] parts = str.Split(',');
        if (parts.Length != 6)
        {
            Debug.LogError("Invalid Vector3 format: " + str);
            return null;
        }
        try
        {
            data.posX = float.Parse(parts[0], CultureInfo.InvariantCulture);
            data.posY = float.Parse(parts[1], CultureInfo.InvariantCulture);
            data.posZ = float.Parse(parts[2], CultureInfo.InvariantCulture);
            data.rotX = float.Parse(parts[3], CultureInfo.InvariantCulture);
            data.rotY = float.Parse(parts[4], CultureInfo.InvariantCulture);
            data.rotZ = float.Parse(parts[5], CultureInfo.InvariantCulture);
            return data;
        }
        catch (System.FormatException e)
        {
            Debug.LogError("Failed to parse Vector3: " + e.Message);
            return null;
        }
    }*/

    public static string ParseData(Transform p)
    {
        string test = "";
        test = $"+{p.position.x.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"+{p.position.y.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"+{p.position.z.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"{p.rotation.x.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"{p.rotation.y.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"{p.rotation.z.ToString("000.00", CultureInfo.InvariantCulture)}";

        //test = test.Replace("-", string.Empty);
        test = test.Replace(".", string.Empty);
        test = test.Replace("+-", "-");
		test = test.Replace("+", "0");
        
        
        //make a string of ++- +-- and swith then add the flag at the head of the test string

        return test;
    }
}


public class NetData
{
    public float posX;
    public float posY;
    public float posZ;
    public float rotX;
    public float rotY;
    public float rotZ;
}

