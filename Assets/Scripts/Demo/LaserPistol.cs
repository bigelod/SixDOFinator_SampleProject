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

    private SendUDPData dataSender;
    private float LHapticData = 0f;
    private float RHapticData = 0f;
    private int waitFrames = 72;
    bool waitFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        laserBlockerMask = LayerMask.NameToLayer("LaserBlocker");

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            dataSender = player.GetComponent<SendUDPData>();
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

        if (dataSender != null)
        {
            dataSender.SendHapticVibration(LHapticData, RHapticData);
        }

        if (waitFrame)
        {
            waitFrames -= 1;

            if (waitFrames <= 0)
            {
                LHapticData = 0f;
                RHapticData = 0f;
                waitFrame = false;
            }
        }
    }

    void Shoot()
    {
        if (m_ShootSound != null) m_ShootSound.Play();

        if (m_LeftHand)
        {
            LHapticData = 1f;
        }
        else
        {
            RHapticData = 1f;
        }

        waitFrames = 72;
        waitFrame = true;

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
