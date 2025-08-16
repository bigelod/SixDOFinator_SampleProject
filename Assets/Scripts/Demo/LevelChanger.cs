using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private ReadUDPPosData udpReceiver;

    [SerializeField]
    private ReadPosData playerRPD;

    [SerializeField]
    private GameObject m_GrowGfx;
    [SerializeField]
    private Vector3 m_GrowGfxDefaultScale = Vector3.one;
    [SerializeField]
    private Vector3 m_GrowGfxMaxScale = Vector3.one;

    [SerializeField]
    private TextMeshProUGUI m_DisplayText;

    [SerializeField]
    private string m_LevelName = "Main Scene";
    [SerializeField]
    private string m_SceneName = "MainScene";

    [SerializeField]
    private float m_HoldTime = 3f;

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
        if (doingLevelTransition)
        {
            m_GrowGfx.transform.localScale = m_GrowGfxMaxScale;
            m_GrowGfx.SetActive(true);

            if (playerRPD != null) playerRPD.SetLeaving();
            if (udpReceiver != null) udpReceiver.KillReceiver();

            SceneManager.LoadScene(m_SceneName);
        }
        else if (playerTouching)
        {
            currHoldTime += 1f * Time.deltaTime;

            if (m_GrowGfx != null)
            {
                m_GrowGfx.transform.localScale = Vector3.Lerp(m_GrowGfxDefaultScale, m_GrowGfxMaxScale, currHoldTime / m_HoldTime);
                m_GrowGfx.SetActive(true);
            }

            if (currHoldTime > m_HoldTime)
            {
                doingLevelTransition = true;
            }
        }
        else
        {
            currHoldTime = 0f;

            if (m_GrowGfx != null)
            {
                m_GrowGfx.transform.localScale = m_GrowGfxDefaultScale;
                m_GrowGfx.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHand")
        {
            if (!playerTouching && m_AudioSource != null)
            {
                m_AudioSource.Play();
            }

            playerTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerHand")
        {
            playerTouching = false;
        }
    }
}
