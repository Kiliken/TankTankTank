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
using System.Diagnostics;
using System.Threading;
using Vector3 = UnityEngine.Vector3;
using Debug = UnityEngine.Debug;
using static UnityEngine.GraphicsBuffer;


public class TestManager : MonoBehaviour
{
    [Header("Players")]
    public char playerSide = 'A';
    [SerializeField] Transform player;
    [SerializeField] Transform playerOther;
    [SerializeField] Transform playerHead;
    [SerializeField] Transform playerOtherHead;
    [SerializeField] TankMovement2 TankConnection;
    [SerializeField] Cannon playerOtherTurret;
    string playerPosString;

    [Header("Turret")]
    public bool shooting = false;
    private bool shot = false;
    private float shootCD = 3f;
    private float shootCDTimer = 0f;
    private byte reggaetonCheck = 0x00;

    [Header("Client")]
    [SerializeField] string ip = "127.0.0.1";
    [SerializeField] int port = 25565;
    static UdpClient udpc;
    bool dataFlag = false;
    float timer = 0;
    private Thread udpDataThread;
    private bool udpRunnig = true;

    private NetData data;

    static volatile byte[] udpSend = new byte[] { 0x4E };
    static volatile byte[] udpGet  = new byte[] { 0x4E };

    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Debug.Log("DEBUG");

        if (GetArg("-ip") != null)
        {
            ip = GetArg("-ip");
        }
        if (GetArg("-port") != null)
        {
            port = int.Parse(GetArg("-port"));
        }

        if (GetArg("-player") != null)
        {
            playerSide = char.Parse(GetArg("-player"));
        }
    }

    void Start()
    {
        player.position = (playerSide == 'A' ? new Vector3(-28.2f, 1.4f, 52f) : new Vector3(28.2f, 1.4f, -52f));
        player.eulerAngles = (playerSide == 'A' ? new Vector3(0, -197f, 0) : new Vector3(0, -22f, 0));


        udpc = new UdpClient(ip, port);
        udpc.Client.ReceiveTimeout = 1000;
        udpDataThread = new Thread(new ThreadStart(SendGetData));
        udpDataThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        udpSend = NetManager.ParseByte(playerSide, player.transform, playerHead.eulerAngles.y, TankConnection.shooting);
        //Debug.Log(Encoding.ASCII.GetString(udpSend));

        if (udpGet[0] != 0x4E)
        {
            //GetData(udpGet.Substring(1, udpGet.Length - 1));
            data = NetManager.RetriveByte(udpGet);
            UpdatePosition();

            if((byte)(data.shootingFlag - reggaetonCheck) != 0)
            {
                playerOtherTurret.Fire();
                reggaetonCheck++;

                if (reggaetonCheck >= 0x10)
                    reggaetonCheck -= 0x10;
            }

            /*if (data.isShooting)
            {
                if (!shot)
                {
                    playerOtherTurret.Fire();
                    //sinceFire = 0f;
                    //returned = false;
                    shot = true;
                    Debug.Log("enemy shot");
                }

                if (shootCDTimer < shootCD)
                {
                    shootCDTimer += Time.deltaTime;
                }
                else
                {
                    shot = false;
                    shootCDTimer = 0f;
                    Debug.Log("enemy can shoot");
                }
            }else shot = false;*/
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
                //byte[] msg = Encoding.ASCII.GetBytes(udpSend);
                udpc.Send(udpSend, udpSend.Length);

                //byte[] rdata = udpc.Receive(ref ep);
                //udpGet = Encoding.ASCII.GetString(rdata);
                udpGet = udpc.Receive(ref ep);

                stopWatch.Stop();
                timeSpan = stopWatch.ElapsedMilliseconds;
                if (timeSpan < 33)
                    Thread.Sleep((int)(33 - timeSpan));



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

    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }

}
