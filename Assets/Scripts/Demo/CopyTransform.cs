using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [SerializeField]
    private Transform m_TargetTransform;

    [SerializeField]
    private bool m_CopyX = false;
    [SerializeField]
    private bool m_CopyY = false;
    [SerializeField]
    private bool m_CopyZ = false;

    [SerializeField]
    private bool m_CopyRotationQuat = false;

    [SerializeField]
    private bool m_CopyRotX = false;
    [SerializeField]
    private bool m_CopyRotY = false;
    [SerializeField]
    private bool m_CopyRotZ = false;

    [SerializeField]
    private bool m_CopyScaleX = false;
    [SerializeField]
    private bool m_CopyScaleY = false;
    [SerializeField]
    private bool m_CopyScaleZ = false;

    [SerializeField]
    private bool m_FixedUpdate = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_TargetTransform != null && !m_FixedUpdate)
        {
            float newX = transform.position.x;
            float newY = transform.position.y;
            float newZ = transform.position.z;

            float rotX = transform.eulerAngles.x;
            float rotY = transform.eulerAngles.y;
            float rotZ = transform.eulerAngles.z;

            float scaleX = transform.localScale.x;
            float scaleY = transform.localScale.y;
            float scaleZ = transform.localScale.z;

            if (m_CopyX)
            {
                newX = m_TargetTransform.position.x;
            }

            if (m_CopyY)
            {
                newY = m_TargetTransform.position.y;
            }

            if (m_CopyZ)
            {
                newZ = m_TargetTransform.position.z;
            }

            if (m_CopyRotX)
            {
                rotX = m_TargetTransform.eulerAngles.x;
            }

            if (m_CopyRotY)
            {
                rotY = m_TargetTransform.eulerAngles.y;
            }

            if (m_CopyRotZ)
            {
                rotZ = m_TargetTransform.eulerAngles.z;
            }

            if (m_CopyScaleX)
            {
                scaleX = m_TargetTransform.localScale.x;
            }

            if (m_CopyScaleY)
            {
                scaleY = m_TargetTransform.localScale.y;
            }

            if (m_CopyScaleZ)
            {
                scaleZ = m_TargetTransform.localScale.z;
            }

            if (m_CopyX || m_CopyY || m_CopyZ) transform.position = new Vector3(newX, newY, newZ);
            if (m_CopyRotX || m_CopyRotY || m_CopyRotZ) transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
            if (m_CopyScaleX || m_CopyScaleY || m_CopyScaleZ) transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

            if (m_CopyRotationQuat) transform.rotation = m_TargetTransform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (m_TargetTransform != null && m_FixedUpdate)
        {
            float newX = transform.position.x;
            float newY = transform.position.y;
            float newZ = transform.position.z;

            float rotX = transform.eulerAngles.x;
            float rotY = transform.eulerAngles.y;
            float rotZ = transform.eulerAngles.z;

            float scaleX = transform.localScale.x;
            float scaleY = transform.localScale.y;
            float scaleZ = transform.localScale.z;

            if (m_CopyX)
            {
                newX = m_TargetTransform.position.x;
            }

            if (m_CopyY)
            {
                newY = m_TargetTransform.position.y;
            }

            if (m_CopyZ)
            {
                newZ = m_TargetTransform.position.z;
            }

            if (m_CopyRotX)
            {
                rotX = m_TargetTransform.eulerAngles.x;
            }

            if (m_CopyRotY)
            {
                rotY = m_TargetTransform.eulerAngles.y;
            }

            if (m_CopyRotZ)
            {
                rotZ = m_TargetTransform.eulerAngles.z;
            }

            if (m_CopyScaleX)
            {
                scaleX = m_TargetTransform.localScale.x;
            }

            if (m_CopyScaleY)
            {
                scaleY = m_TargetTransform.localScale.y;
            }

            if (m_CopyScaleZ)
            {
                scaleZ = m_TargetTransform.localScale.z;
            }

            if (m_CopyX || m_CopyY || m_CopyZ) transform.position = new Vector3(newX, newY, newZ);
            if (m_CopyRotX || m_CopyRotY || m_CopyRotZ) transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
            if (m_CopyScaleX || m_CopyScaleY || m_CopyScaleZ) transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

            if (m_CopyRotationQuat) transform.rotation = m_TargetTransform.rotation;
        }
    }
}
