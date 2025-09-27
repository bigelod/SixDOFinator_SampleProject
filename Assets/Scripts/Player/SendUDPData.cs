using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class SendUDPData : MonoBehaviour
{
    [SerializeField]
    private int udpPort = 7278;

    [SerializeField]
    private string targetIP = "127.0.0.1";

    private UdpClient transmitClient;

    // Start is called before the first frame update
    void Start()
    {
        transmitClient = new UdpClient();
    }

    public void SendHapticVibration(float lControllerVibration, float rControllerVibration)
    {
        string vibeData = lControllerVibration.ToString("0.00") + " " + rControllerVibration.ToString("0.00");

        SendData(vibeData);
    }

    public void SendData(string data)
    {
        byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(data);

        transmitClient.Send(serverMessageAsByteArray, serverMessageAsByteArray.Length, new IPEndPoint(IPAddress.Parse(targetIP), udpPort));
    }
}
