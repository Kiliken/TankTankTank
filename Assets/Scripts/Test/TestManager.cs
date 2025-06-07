using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TestManager : MonoBehaviour
{
    public string playerSide = "<P1>";
    [SerializeField] TestMovement player;
    [SerializeField] OtherMovement playerOther;
    string playerPosString;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        
    }


    public void GetData(string s)
    {
        playerOther.movePos = ParseVector3(s);
    }


    public void SendData(Vector3 p)
    {

        playerPosString = $"({p.x.ToString("F1", CultureInfo.InvariantCulture)}, " +
              $"{p.y.ToString("F1", CultureInfo.InvariantCulture)}, " +
              $"{p.z.ToString("F1", CultureInfo.InvariantCulture)})";

        //Debug.Log(playerPosString);
        GetData(playerPosString);
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
}
