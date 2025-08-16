using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_RB;

    [SerializeField]
    private AudioSource m_Audio;

    // Start is called before the first frame update
    void Start()
    {
        if (m_RB == null) m_RB = GetComponent<Rigidbody>();

        if (m_Audio == null) m_Audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_RB != null && !m_RB.isKinematic)
        {
            m_Audio.pitch = Random.Range(1f, 3f);
            m_Audio.Play();
        }
    }
}
