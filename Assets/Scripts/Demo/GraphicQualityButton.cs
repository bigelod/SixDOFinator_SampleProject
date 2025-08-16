using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GraphicQualityButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EnabledGfx;
    [SerializeField]
    private GameObject m_DisabledGfx;

    [SerializeField]
    private TextMeshProUGUI m_DisplayText;

    [SerializeField]
    private string m_GraphicLevelName = "Low";
    [SerializeField]
    private int m_GraphicLevel = 0;

    [SerializeField]
    private AudioSource m_AudioSource;

    bool btnEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_AudioSource == null) m_AudioSource = GetComponent<AudioSource>();

        if (m_DisplayText != null)
        {
            m_DisplayText.text = m_GraphicLevelName;
        }

        int graphicLevel = PlayerPrefs.GetInt("gfxSetting", 0);

        if (graphicLevel == m_GraphicLevel)
        {
            btnEnabled = true;
            QualitySettings.SetQualityLevel(m_GraphicLevel, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currQuality = QualitySettings.GetQualityLevel();

        if (currQuality == m_GraphicLevel)
        {
            btnEnabled = true;
        }
        else
        {
            btnEnabled = false;
        }

        m_EnabledGfx.SetActive(btnEnabled);
        m_DisabledGfx.SetActive(!btnEnabled);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!btnEnabled && other.tag == "PlayerHand")
        {
            if (m_AudioSource != null)
            {
                m_AudioSource.Play();
            }

            QualitySettings.SetQualityLevel(m_GraphicLevel, true);
            PlayerPrefs.SetInt("gfxSetting", m_GraphicLevel);
        }
    }
}
