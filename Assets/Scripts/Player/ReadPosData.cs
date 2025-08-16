using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ReadPosData : MonoBehaviour
{
    [SerializeField]
    private bool m_UDPOnly = false;

    [SerializeField]
    private CameraModeToggle m_CameraModeToggle;

    [SerializeField]
    private Transform m_PlayerHead;
    [SerializeField]
    private Transform m_PlayerHandL;
    [SerializeField]
    private Transform m_PlayerHandR;

    [SerializeField]
    private Transform m_PlayerEyeL;
    [SerializeField]
    private Transform m_PlayerEyeR;

    [SerializeField]
    private string m_DataDirectory = "";

    [SerializeField]
    private BasicMovement m_MoveScript;
    [SerializeField]
    private BasicRotation m_RotateScript;

    [SerializeField]
    private Camera m_FlatCam;
    [SerializeField]
    private Camera m_LeftEyeCam;
    [SerializeField]
    private Camera m_RightEyeCam;

    [SerializeField]
    private MeshRenderer m_OpenXRFrameIDFlat;

    private Material openXRFrameMatFlat;

    private bool dirExists = false;

    private float currIPD = 0f;
    private float currHMDQX = 0f;
    private float currHMDQY = 0f;
    private float currHMDQZ = 0f;
    private float currHMDQW = 0f;
    private float currHMDX = 0f;
    private float currHMDY = 0f;
    private float currHMDZ = 0f;
    private float currLThumbX = 0f;
    private float currLThumbY = 0f;
    private float currRThumbX = 0f;
    private float currRThumbY = 0f;
    private float currLHandQX = 0f;
    private float currLHandQY = 0f;
    private float currLHandQZ = 0f;
    private float currLHandQW = 0f;
    private float currLHandX = 0f;
    private float currLHandY = 0f;
    private float currLHandZ = 0f;
    private float currRHandQX = 0f;
    private float currRHandQY = 0f;
    private float currRHandQZ = 0f;
    private float currRHandQW = 0f;
    private float currRHandX = 0f;
    private float currRHandY = 0f;
    private float currRHandZ = 0f;
    private float currFOVH = 110f;
    private float currFOVV = 96f;
    private int currFrameID = 0;

    private bool firstStereoCheck = true;
    private bool lastStereoStatus = false;

    private bool lockFrameData = false;

    private int lastFrameID = -1;

    [SerializeField]
    private bool skipFrameWaitDebug = false;

    private bool leavingLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        if (m_MoveScript == null)
        {
            m_MoveScript = GetComponent<BasicMovement>();
        }

        if (m_RotateScript == null)
        {
            m_RotateScript = GetComponent<BasicRotation>();
        }

        if (!Directory.Exists(m_DataDirectory)) m_DataDirectory = "";

        if (m_DataDirectory == "") m_DataDirectory = PlayerPrefs.GetString("dataDirV2", "Z:/tmp/xr");

        if (m_DataDirectory == "Z:/tmp/xr" && !Directory.Exists(m_DataDirectory))
        {
            skipFrameWaitDebug = true;

            m_DataDirectory = Path.GetFullPath("./").Substring(0,1) + ":/xrtemp";
        }

        if (!Directory.Exists(m_DataDirectory)) Directory.CreateDirectory(m_DataDirectory);

        if (!Directory.Exists(m_DataDirectory))
        {
            Debug.LogError("DIRECTORY DOES NOT EXIST: " + m_DataDirectory);
            return;
        }

        string vrFile = m_DataDirectory + "/vr";

        if (!File.Exists(vrFile))
        {
            FileStream fs = File.Create(vrFile);
            fs.Close();
        }

        if (m_OpenXRFrameIDFlat == null)
        {
            GameObject obj = GameObject.FindWithTag("OpenXRFrameSquare");

            if (obj != null)
            {
                m_OpenXRFrameIDFlat = obj.GetComponent<MeshRenderer>();
            }
        }

        if (m_OpenXRFrameIDFlat != null)
        {
            openXRFrameMatFlat = m_OpenXRFrameIDFlat.material;
        }

        Application.quitting += On_Application_quitting;
    }

    private void On_Application_quitting()
    {
        string vrFile = m_DataDirectory + "/vr";

        if (File.Exists(vrFile))
        {
            File.Delete(vrFile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dirExists || Directory.Exists(m_DataDirectory))
        {
            dirExists = true;

            if (m_CameraModeToggle != null && (firstStereoCheck || m_CameraModeToggle.StereoRenderMode() != lastStereoStatus))
            {
                if (m_CameraModeToggle.StereoRenderMode())
                {
                    string sbsFile = m_DataDirectory + "/sbs";

                    if (!File.Exists(sbsFile))
                    {
                        FileStream fs = File.Create(sbsFile);
                        fs.Close();
                    }
                }
                else
                {
                    string sbsFile = m_DataDirectory + "/sbs";

                    if (File.Exists(sbsFile))
                    {
                        File.Delete(sbsFile);
                    }
                }

                firstStereoCheck = false;
                lastStereoStatus = m_CameraModeToggle.StereoRenderMode();
            }

            if (m_PlayerHandL != null)
            {
                m_PlayerHandL.transform.localPosition = new Vector3(currLHandX, currLHandY, -currLHandZ);
                Quaternion handLRot = new Quaternion(currLHandQX, currLHandQY, currLHandQZ, currLHandQW);
                //m_PlayerHandL.transform.localRotation = handLRot;
                m_PlayerHandL.localEulerAngles = new Vector3(360f - handLRot.eulerAngles.x, 360f - handLRot.eulerAngles.y, handLRot.eulerAngles.z);
            }

            if (m_PlayerHandR != null)
            {
                m_PlayerHandR.transform.localPosition = new Vector3(currRHandX, currRHandY, -currRHandZ);
                Quaternion handRRot = new Quaternion(currRHandQX, currRHandQY, currRHandQZ, currRHandQW);
                //m_PlayerHandR.transform.localRotation = handRRot;
                m_PlayerHandR.localEulerAngles = new Vector3(360f - handRRot.eulerAngles.x, 360f - handRRot.eulerAngles.y, handRRot.eulerAngles.z + 180f);
            }

            if (m_MoveScript != null)
            {
                m_MoveScript.SetThumbXY(currLThumbX, currLThumbY);
            }

            if (m_RotateScript != null)
            {
                m_RotateScript.SetThumbXY(currRThumbX, currRThumbY);
            }

            if (m_CameraModeToggle != null)
            {
                if (m_FlatCam != null && !m_CameraModeToggle.StereoRenderMode())
                {
                    if (currFOVH != 0f && currFOVV != 0f)
                    {
                        float aspectRatio = currFOVH / currFOVV;

                        m_FlatCam.fieldOfView = currFOVV;
                        m_FlatCam.aspect = aspectRatio;
                    }
                }

                if (m_LeftEyeCam != null && m_RightEyeCam != null && m_CameraModeToggle.StereoRenderMode())
                {
                    if (currFOVH != 0f && currFOVV != 0f)
                    {
                        float aspectRatio = currFOVH / currFOVV;

                        m_LeftEyeCam.fieldOfView = currFOVV;
                        m_LeftEyeCam.aspect = aspectRatio;

                        m_RightEyeCam.fieldOfView = currFOVV;
                        m_RightEyeCam.aspect = aspectRatio;
                    }
                }
            }

            if (openXRFrameMatFlat != null)
            {
                openXRFrameMatFlat.color = new Color32((byte)currFrameID, 0, 0, 1);
            }
        }
    }

    private void UpdateValue(ref float currValue, float newValue)
    {
        currValue = newValue;
    }

    private void UpdateValueInt(ref int currValue, int newValue)
    {
        currValue = newValue;
    }

    private float TryParseStr(string str)
    {
        float ans = 0f;

        float.TryParse(str, out ans);

        return ans;
    }

    private int TryParseStrToInt(string str)
    {
        int ans = 0;

        int.TryParse(str, out ans);

        return ans;
    }

    public void SetDataDir(string dataDirectory)
    {
        m_DataDirectory = dataDirectory;

        PlayerPrefs.SetString("dataDirV2", m_DataDirectory);

        dirExists = false; //Try to confirm it exists again
    }

    public void PostRenderRelease()
    {
        lockFrameData = false;
    }

    public void PreRenderRead()
    {
        if (!skipFrameWaitDebug && !leavingLevel)
        {
            while (lastFrameID == currFrameID)
            {

            }
        }

        lockFrameData = true;
        lastFrameID = currFrameID;

        //Legacy method of getting data, read from a file in the xrtemp folder
        if (dirExists && !m_UDPOnly)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(m_DataDirectory);

            foreach (string file in files)
            {
                if (file.Length > 3)
                {
                    //Not the /sbs or /vr files
                    string[] parts = Path.GetFileName(file).Split(' ');

                    if (parts[0] == "client0")
                    {
                        int i = 1;
                        int maxParts = parts.Length - 1;

                        if (maxParts >= i) UpdateValue(ref currLHandQX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLHandQY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLHandQZ, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLHandQW, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLThumbX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLThumbY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLHandX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLHandY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currLHandZ, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandQX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandQY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandQZ, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandQW, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRThumbX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRThumbY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currRHandZ, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDQX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDQY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDQZ, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDQW, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDX, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDY, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currHMDZ, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currIPD, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currFOVH, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValue(ref currFOVV, TryParseStr(parts[i]));
                        i++;
                        if (maxParts >= i) UpdateValueInt(ref currFrameID, TryParseStrToInt(parts[i]));
                    }
                }
            }
        }

        if (m_PlayerEyeL != null && m_PlayerEyeR != null)
        {
            m_PlayerEyeL.transform.localPosition = new Vector3(currIPD * 0.5f * -1f, 0f, 0f);
            m_PlayerEyeR.transform.localPosition = new Vector3(currIPD * 0.5f, 0f, 0f);
        }

        if (m_PlayerHead != null)
        {
            m_PlayerHead.transform.localPosition = new Vector3(currHMDX, currHMDY, -currHMDZ);
            Quaternion hmdRot = new Quaternion(currHMDQX, currHMDQY, currHMDQZ, currHMDQW);
            m_PlayerHead.transform.localEulerAngles = new Vector3(360f - hmdRot.eulerAngles.x, 360f - hmdRot.eulerAngles.y, hmdRot.eulerAngles.z);
        }

        if (openXRFrameMatFlat != null)
        {
            openXRFrameMatFlat.color = new Color32((byte)currFrameID, 0, 0, 1);
        }
    }

    public void ReceiveData(string data)
    {
        if (!lockFrameData)
        {
            string[] parts = data.Split(' ');

            int i = 1;
            int maxParts = parts.Length - 1;

            if (maxParts >= i) UpdateValue(ref currLHandQX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLHandQY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLHandQZ, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLHandQW, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLThumbX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLThumbY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLHandX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLHandY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currLHandZ, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandQX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandQY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandQZ, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandQW, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRThumbX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRThumbY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currRHandZ, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDQX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDQY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDQZ, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDQW, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDX, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDY, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currHMDZ, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currIPD, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currFOVH, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValue(ref currFOVV, TryParseStr(parts[i]));
            i++;
            if (maxParts >= i) UpdateValueInt(ref currFrameID, TryParseStrToInt(parts[i]));
        }
    }

    public void SetLeaving()
    {
        leavingLevel = true;
    }

    public bool LeavingLevel()
    {
        return leavingLevel;
    }
}
