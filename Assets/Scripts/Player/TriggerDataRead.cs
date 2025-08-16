using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDataRead : MonoBehaviour
{
    [SerializeField]
    private ReadPosData m_PlayerDataReader;

    [SerializeField]
    private bool m_TriggerRead = true;
    [SerializeField]
    private bool m_TriggerStop = true;

    private void OnPreRender()
    {
        if (m_TriggerRead && m_PlayerDataReader != null)
        {
            m_PlayerDataReader.PreRenderRead();
        }
    }

    private void OnPostRender()
    {
        if (m_TriggerStop && m_PlayerDataReader != null)
        {
            m_PlayerDataReader.PostRenderRelease();
        }
    }
}
