using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VVRHMD : MonoBehaviour
{
    [SerializeField]
    private Transform m_StartPos;

    [SerializeField]
    private float m_ReposYPos = 0f;

    [SerializeField]
    private float m_ReposTime = 3f;

    [SerializeField]
    private GameObject m_TeleportVFX;

    [SerializeField]
    private VVRLevelChanger m_Changer;

    private bool isHeld = false;
    private float reposCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHeld && transform.position.y <= m_ReposYPos)
        {
            reposCountdown += Time.deltaTime;

            if (reposCountdown >= m_ReposTime)
            {
                if (m_TeleportVFX != null) GameObject.Instantiate(m_TeleportVFX, transform.position, Quaternion.identity);

                if (m_StartPos != null) transform.position = m_StartPos.position;
                reposCountdown = 0f;
            }
        }
        else
        {
            reposCountdown = 0f;
        }
    }

    public void SetHeld(bool _tf)
    {
        isHeld = _tf;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHead")
        {
            if (m_Changer != null) m_Changer.PlayerWearingHMD();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_Changer != null) m_Changer.PlayerRemovedHMD();
    }
}
