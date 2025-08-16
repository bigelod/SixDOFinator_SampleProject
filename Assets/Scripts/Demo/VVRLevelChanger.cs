using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VVRLevelChanger : MonoBehaviour
{
    [SerializeField]
    private ReadUDPPosData udpReceiver;

    [SerializeField]
    private ReadPosData playerRPD;

    [SerializeField]
    private TextMeshProUGUI m_DisplayText;

    [SerializeField]
    private string m_LevelName = "Main Scene";
    [SerializeField]
    private string m_SceneName = "MainScene";

    [SerializeField]
    private float m_HoldTime = 2f;

    [SerializeField]
    private AudioSource m_AudioSource;

    private float currHoldTime = 0f;

    bool playerTouching = false;
    bool doingLevelTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();

        if (m_DisplayText != null)
        {
            m_DisplayText.text = m_LevelName;
        }

        if (udpReceiver == null)
        {
            GameObject go = GameObject.FindWithTag("Player");

            if (go != null)
            {
                udpReceiver = go.GetComponent<ReadUDPPosData>();
                playerRPD = go.GetComponent<ReadPosData>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRPD != null && playerRPD.LeavingLevel())
        {
            //Nothing to do
        }
        else if (doingLevelTransition)
        {
            if (playerRPD != null) playerRPD.SetLeaving();
            if (udpReceiver != null) udpReceiver.KillReceiver();

            SceneManager.LoadScene(m_SceneName);
        }
        else if (playerTouching)
        {
            currHoldTime += 1f * Time.deltaTime;

            if (currHoldTime > m_HoldTime)
            {
                doingLevelTransition = true;
            }
        }
        else
        {
            currHoldTime = 0f;
        }
    }

    public void PlayerWearingHMD()
    {
        if (!playerTouching && m_AudioSource != null)
        {
            m_AudioSource.Play();
        }

        playerTouching = true;
    }

    public void PlayerRemovedHMD()
    {
        playerTouching = false;
    }
}
