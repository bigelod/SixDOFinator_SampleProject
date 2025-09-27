using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPistol : MonoBehaviour
{
    [SerializeField]
    private bool m_LeftHand = false;

    [SerializeField]
    private Transform m_Muzzle;

    [SerializeField]
    private AudioSource m_ShootSound;

    [SerializeField]
    private GameObject m_CashFx;

    [SerializeField]
    private GameObject m_MissFx;

    [SerializeField]
    private float m_ShotCooldown = 0.15f;

    [SerializeField]
    private LayerMask m_RayHitLayermask;

    [SerializeField]
    private List<string> m_TreasureTags = new List<string>();

    private float cooldown = 0f;

    private int laserBlockerMask;

    private ReadPosData playerData;

    // Start is called before the first frame update
    void Start()
    {
        laserBlockerMask = LayerMask.NameToLayer("LaserBlocker");

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerData = player.GetComponent<ReadPosData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0f)
        {
            if (m_LeftHand)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetMouseButton((int)MouseButton.Left))
                {
                    Shoot();
                }
            }
        }
        else
        {
            cooldown -= 1f * Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (m_ShootSound != null) m_ShootSound.Play();

        if (playerData)
        {
            if (m_LeftHand)
            {
                playerData.LControllerVibration = 1f;
            }
            else
            {
                playerData.RControllerVibration = 1f;
            }
        }
        

        RaycastHit hit;

        if (Physics.Raycast(m_Muzzle.position, m_Muzzle.forward.normalized, out hit, 1000f, m_RayHitLayermask))
        {
            if (hit.transform.gameObject.layer != laserBlockerMask && m_TreasureTags.Contains(hit.transform.tag))
            {
                if (m_CashFx != null) GameObject.Instantiate(m_CashFx, hit.transform.position, Quaternion.identity);

                Destroy(hit.transform.gameObject);
            }
            else
            {
                if (m_MissFx != null) GameObject.Instantiate(m_MissFx, hit.transform.position, Quaternion.identity);
            }
        }

        cooldown = m_ShotCooldown;
    }
}
