using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ReadUDPPosData : MonoBehaviour
{
    //Based on this tutorial by Chiad Dogan: https://cihaddogan.medium.com/udp-communication-between-unity-and-matlab-simulink-d4a62921936d

    [SerializeField]
    private ReadPosData posData;

    [SerializeField]
    private int udpPort = 7872;

    private UdpClient udpClient;
    private Thread udpReadThread;


    void Start()
    {
        if (posData == null) posData = GetComponent<ReadPosData>();

        Initialize();
    }

    public void Initialize()
    {
        udpReadThread = new Thread(new ThreadStart(ReceiveData));
        udpReadThread.IsBackground = true;
        udpReadThread.Start();
    }

    private void ReceiveData()
    {
        udpClient = new UdpClient(udpPort);

        while (true)
        {
            try
            {
                IPEndPoint recieveFromAnyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref recieveFromAnyIP);

                string returnData = Encoding.ASCII.GetString(data);

                if (posData != null)
                {
                    posData.ReceiveData(returnData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    public void KillReceiver()
    {
        try
        {
            udpReadThread.Abort();
            udpReadThread = null;
            udpClient.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        try
        {
            udpReadThread.Abort();
            udpReadThread = null;
            udpClient.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
