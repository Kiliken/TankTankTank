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

        data.posX = float.Parse(new string(converter, 2, 5));
        data.posY = float.Parse(new string(converter, 7, 5));
        data.posZ = float.Parse(new string(converter, 12, 5));
        data.rotX = float.Parse(new string(converter, 17, 5));
        data.rotX = float.Parse(new string(converter, 22, 5));
        data.rotY = float.Parse(new string(converter, 27, 5));

        data.posX /= 100;
        data.posY /= 100;
        data.posZ /= 100;
        data.rotX /= 100;
        data.rotX /= 100;
        data.rotY /= 100;

        switch (converter[1])
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
        }
        return data;
    }

    public static string ParseData(Transform p)
    {
       
        return null;
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

