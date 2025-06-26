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
using System.ComponentModel;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
/*

Data:

player,vector3x,vector3y,vector3z,eurlerbody,eulerhead,shootingflag

DataBytes:
1(byte),4(float),4(float),4(float),4(float),4(float),1(bool)

*/


public static class NetManager
{
    public static volatile string udpSend = "N";
    public static volatile string udpGet = "N";

    public static UdpClient udpc;

    private static void SendGetData()
    {
        long timeSpan = 0;
        Stopwatch stopWatch = new Stopwatch();
        Debug.Log("ThreadStarted");
        
        while (true)
        {
            //Debug.Log("threding");
            try
            {
                IPEndPoint ep = null;
                stopWatch.Reset();
                stopWatch.Start();
                //byte[] msg = Encoding.ASCII.GetBytes($"{playerSide}{ParseToString(player.transform.position)}");
                byte[] msg = Encoding.ASCII.GetBytes(udpSend);
                udpc.Send(msg, msg.Length);

                byte[] rdata = udpc.Receive(ref ep);
                udpGet = Encoding.ASCII.GetString(rdata);

                stopWatch.Stop();
                timeSpan = stopWatch.ElapsedMilliseconds;
                if (timeSpan < 45)
                    Thread.Sleep((int)(45 - timeSpan));



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
        data.rotBody = float.Parse(new string(converter, 19, 5));
        data.rotHead = float.Parse(new string(converter, 24, 5));
        data.rotZ = float.Parse(new string(converter, 29, 5));

        data.posX /= 100;
        data.posY /= 100;
        data.posZ /= 100;
        data.rotBody /= 100;
        data.rotHead /= 100;
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
    
    public static NetData RetriveByte(byte[] bytes)
    {
        NetData data = new NetData();
        //char[] converter = str.ToCharArray(0, str.Length);

        if (bytes[0] == 0x4E)
            return null;
        
        data.posX = BitConverter.ToSingle(bytes, 1);
        data.posY = BitConverter.ToSingle(bytes, 5);
        data.posZ = BitConverter.ToSingle(bytes, 9);
        data.rotBody = BitConverter.ToSingle(bytes, 13);
        data.rotHead = BitConverter.ToSingle(bytes, 17);
        data.isShooting = BitConverter.ToBoolean(bytes, 21);
        
        return data;
    }

    public static byte[] ParseByte(char side, Transform p, float r, bool shotFlag)
    {
        byte[] test = new byte[0];

        test = test.Concat(new byte[] { (byte)side }).ToArray();
        test = test.Concat(BitConverter.GetBytes(p.position.x)).ToArray();
        test = test.Concat(BitConverter.GetBytes(p.position.y)).ToArray();
        test = test.Concat(BitConverter.GetBytes(p.position.z)).ToArray();
        test = test.Concat(BitConverter.GetBytes(p.eulerAngles.y)).ToArray();
        test = test.Concat(BitConverter.GetBytes(r)).ToArray();
        test = test.Concat(BitConverter.GetBytes(shotFlag)).ToArray();



        //make a string of ++- +-- and swith then add the flag at the head of the test string

        return test;
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

    public static string ParseData(Transform p, float r)
    {
        string test = "";
        test = $"+{p.position.x.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"+{p.position.y.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"+{p.position.z.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"{p.eulerAngles.y.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"{r.ToString("000.00", CultureInfo.InvariantCulture)}" +
               $"{p.eulerAngles.z.ToString("000.00", CultureInfo.InvariantCulture)}";

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
    public float rotBody;
    public float rotHead;
    public float rotZ;
    public bool isShooting;
}

