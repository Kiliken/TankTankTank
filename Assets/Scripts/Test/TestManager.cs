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
using Vector3 = UnityEngine.Vector3;


public class TestManager : MonoBehaviour
{
    public string playerSide = "A";
    [SerializeField] Transform player;
    [SerializeField] Transform playerOther;
    [SerializeField] Transform playerHead;
    [SerializeField] Transform playerOtherHead;
    string playerPosString;

    [SerializeField] string ip = "127.0.0.1";
    [SerializeField] int port = 25565;
    static UdpClient udpc;
    bool dataFlag = false;
    float timer = 0;
    private Thread udpDataThread;
    private bool udpRunnig = true;

    private NetData data;

    static volatile string udpSend = "N";
    static volatile string udpGet  = "N";

    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Debug.Log("DEBUG");
    }

    void Start()
    {
        udpc = new UdpClient(ip, port);
        udpc.Client.ReceiveTimeout = 1000;
        udpDataThread = new Thread(new ThreadStart(SendGetData));
        udpDataThread.Start();

        float test123 = 2.53f;
        Debug.Log(test123.ToString("000.000", CultureInfo.InvariantCulture));
        Debug.Log(CultureInfo.InvariantCulture);
    }

    // Update is called once per frame
    void Update()
    {
        udpSend = $"{playerSide}{NetManager.ParseData(player.transform, playerHead.eulerAngles.y )}";


        if (udpGet != "N")
        {
            //GetData(udpGet.Substring(1, udpGet.Length - 1));
            data = NetManager.RetriveData(udpGet);
            UpdatePosition();
        }
            

    }

    void OnDestroy()
    {
        Debug.Log("Thread shutdown");
        udpDataThread.Abort();
    }


    /*public void GetData(string s)
    {
        playerOther.movePos = ParseVector3(s);
    }*/


    public void SendData(Vector3 p)
    {

        playerPosString = $"({p.x.ToString("F1", CultureInfo.InvariantCulture)}, " +
              $"{p.y.ToString("F1", CultureInfo.InvariantCulture)}, " +
              $"{p.z.ToString("F1", CultureInfo.InvariantCulture)})";

        Debug.Log(playerPosString);
        //GetData(playerPosString);
    }

    public static string ParseToString(Vector3 p)
    {
        return $"({p.x.ToString("F1", CultureInfo.InvariantCulture)}, " +
              $"{p.y.ToString("F1", CultureInfo.InvariantCulture)}, " +
              $"{p.z.ToString("F1", CultureInfo.InvariantCulture)})";
    }


    // parse string into vector3
    public static Vector3 ParseVector3(string vectorString)
    {
        // Remove parentheses
        vectorString = vectorString.Trim('(', ')');
        // Split the string by comma
        string[] parts = vectorString.Split(',');
        if (parts.Length != 3)
        {
            Debug.LogError("Invalid Vector3 format: " + vectorString);
            return Vector3.zero;
        }
        try
        {
            float x = float.Parse(parts[0], CultureInfo.InvariantCulture);
            float y = float.Parse(parts[1], CultureInfo.InvariantCulture);
            float z = float.Parse(parts[2], CultureInfo.InvariantCulture);
            return new Vector3(x, y, z);
        }
        catch (System.FormatException e)
        {
            Debug.LogError("Failed to parse Vector3: " + e.Message);
            return Vector3.zero;
        }
    }

    private static void SendGetData()
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

    void UpdatePosition()
    {
        playerOther.position = Vector3.Lerp(playerOther.position, new Vector3(data.posX, data.posY, data.posZ), Time.deltaTime * 10f);
        playerOther.eulerAngles = new Vector3(0, data.rotBody, 0);
        playerOtherHead.eulerAngles = new Vector3(-90, data.rotHead, 0);
        //
    }

}
