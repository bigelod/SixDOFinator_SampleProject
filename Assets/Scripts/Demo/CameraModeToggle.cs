using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeToggle : MonoBehaviour
{    
    [SerializeField]
    private bool m_EnableStereoRender = false;

    [SerializeField]
    private GameObject m_2DCamera;
    [SerializeField]
    private GameObject m_StereoCameraL;
    [SerializeField]
    private GameObject m_StereoCameraR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleRenderMode();
        }

        if (m_StereoCameraL != null && m_StereoCameraR != null & m_2DCamera != null)
        {
            if (m_EnableStereoRender)
            {
                m_2DCamera.SetActive(false);
                m_2DCamera.tag = "Camera2D";

                m_StereoCameraL.SetActive(true);
                m_StereoCameraR.SetActive(true);
                m_StereoCameraR.tag = "MainCamera";
            }
            else
            {
                m_StereoCameraL.SetActive(false);
                m_StereoCameraR.SetActive(false);
                m_StereoCameraR.tag = "CameraR";

                m_2DCamera.SetActive(true);
                m_2DCamera.tag = "MainCamera";
            }
        }
    }

    public void ToggleRenderMode()
    {
        m_EnableStereoRender = !m_EnableStereoRender;
    }

    public bool StereoRenderMode()
    {
        return m_EnableStereoRender;
    }
}
