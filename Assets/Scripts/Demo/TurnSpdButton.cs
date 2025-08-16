using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnSpdButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EnabledGfx;
    [SerializeField]
    private GameObject m_DisabledGfx;

    [SerializeField]
    private TextMeshProUGUI m_DisplayText;

    [SerializeField]
    private string m_TurnSpeedName = "Slow Turn Speed";
    [SerializeField]
    private float m_TurnSpeed = 60f;

    [SerializeField]
    private BasicRotation m_PlayerRotationScript;

    [SerializeField]
    private AudioSource m_AudioSource;

    bool btnEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();

        if (m_DisplayText != null)
        {
            m_DisplayText.text = m_TurnSpeedName;
        }

        float graphicLevel = PlayerPrefs.GetFloat("rotSpd", 90);

        if (graphicLevel == m_TurnSpeed) btnEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerRotationScript != null)
        {
            float graphicLevel = m_PlayerRotationScript.GetRotSpd();

            if (graphicLevel == m_TurnSpeed)
            {
                btnEnabled = true;
            }
            else
            {
                btnEnabled = false;
            }
        }

        m_EnabledGfx.SetActive(btnEnabled);
        m_DisabledGfx.SetActive(!btnEnabled);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!btnEnabled && other.tag == "PlayerHand" && m_PlayerRotationScript != null)
        {
            if (m_AudioSource != null)
            {
                m_AudioSource.Play();
            }

            m_PlayerRotationScript.SetRotSpd(m_TurnSpeed);
        }
    }
}
