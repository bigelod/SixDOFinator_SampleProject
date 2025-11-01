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

    public void SendHapticVibration(float lControllerVibration, float rControllerVibration, string sbsFlag = "0")
    {
        //V0.2 now takes L Vibration, R Vibration, VR flag, SBS flag, target FOV W, target FOV H
        float lCV = Mathf.Max(0f, lControllerVibration);
        float rCV = Mathf.Max(0f, rControllerVibration);

        if (lCV + rCV == 0)
        {
            //Technically we don't need to send any data and WinlatorXR will automatically tween vibration strength back to 0 itself for each controller, to make it fade out smoothly
            SendData("0 0 1 " + sbsFlag + " 104.5 104.5");
        }
        else
        {
            string vibeData = lCV.ToString("0.000") + " " + rCV.ToString("0.000");

            SendData(vibeData + " 1 " + sbsFlag + " 104.5 104.5");
        }
    }

    public void SendData(string data)
    {
        byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(data);

        transmitClient.Send(serverMessageAsByteArray, serverMessageAsByteArray.Length, new IPEndPoint(IPAddress.Parse(targetIP), udpPort));
    }
}
