using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboDrone : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpd = 3f;

    [SerializeField]
    private GameObject m_ExplodeObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * -1f * m_MoveSpd * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explodistick")
        {
            if (m_ExplodeObj != null)
            {
                GameObject.Instantiate(m_ExplodeObj, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
