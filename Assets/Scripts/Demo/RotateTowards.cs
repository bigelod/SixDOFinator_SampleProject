using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_YawOffset = 180f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Target == null)
        {
            GameObject go = GameObject.FindWithTag("MainCamera");
            m_Target = go.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target != null)
        {
            transform.LookAt(m_Target);
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + m_YawOffset, 0f);
        }
    }
}
