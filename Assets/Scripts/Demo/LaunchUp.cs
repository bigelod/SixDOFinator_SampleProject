using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchUp : MonoBehaviour
{
    [SerializeField]
    private float launchSpdMin = 50f;

    [SerializeField]
    private float launchSpdMax = 75f;

    [SerializeField]
    private float destroyBelowY = -20f;

    [SerializeField]
    private Rigidbody m_RB;

    [SerializeField]
    private bool m_RandomTorque;

    [SerializeField]
    private float m_MaxTorque = 50f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_RB == null) m_RB = GetComponent<Rigidbody>();

        if (m_RB != null)
        {
            m_RB.AddForce(Vector3.up * Random.Range(launchSpdMin, launchSpdMax), ForceMode.Impulse);

            if (m_RandomTorque)
            {
                Vector3 torque = Vector3.zero;
                torque.x = Random.Range(-1f * m_MaxTorque, m_MaxTorque);
                torque.y = Random.Range(-1f * m_MaxTorque, m_MaxTorque);
                torque.z = Random.Range(-1f * m_MaxTorque, m_MaxTorque);

                m_RB.AddTorque(torque);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < destroyBelowY)
        {
            Destroy(gameObject);
        }
    }
}
