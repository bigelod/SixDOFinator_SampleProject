using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransformLocal : MonoBehaviour
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

    [SerializeField]
    private bool m_MirrorX = false;
    [SerializeField]
    private bool m_MirrorY = false;
    [SerializeField]
    private bool m_MirrorZ = false;
    [SerializeField]
    private bool m_MirrorRotX = false;
    [SerializeField]
    private bool m_MirrorRotY = false;
    [SerializeField]
    private bool m_MirrorRotZ = false;

    [SerializeField]
    private bool m_CopyPlayerHackFix = false;
    [SerializeField]
    private bool m_CopyPlayerRightHand = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_TargetTransform != null && !m_FixedUpdate)
        {
            float newX = transform.localPosition.x;
            float newY = transform.localPosition.y;
            float newZ = transform.localPosition.z;

            float rotX = transform.localEulerAngles.x;
            float rotY = transform.localEulerAngles.y;
            float rotZ = transform.localEulerAngles.z;

            float scaleX = transform.localScale.x;
            float scaleY = transform.localScale.y;
            float scaleZ = transform.localScale.z;

            if (m_CopyX)
            {
                newX = m_TargetTransform.localPosition.x;

                if (m_MirrorX) newX *= -1f;
            }

            if (m_CopyY)
            {
                newY = m_TargetTransform.localPosition.y;

                if (m_MirrorY) newY *= -1f;
            }

            if (m_CopyZ)
            {
                newZ = m_TargetTransform.localPosition.z;

                if (m_MirrorZ) newZ *= -1f;
            }

            if (m_CopyRotX)
            {
                rotX = m_TargetTransform.localEulerAngles.x;

                if (m_MirrorRotX) rotX *= -1f;
            }

            if (m_CopyRotY)
            {
                rotY = m_TargetTransform.localEulerAngles.y;

                if (m_MirrorRotY) rotY *= -1f;
            }

            if (m_CopyRotZ)
            {
                rotZ = m_TargetTransform.localEulerAngles.z;

                if (m_MirrorRotZ) rotZ *= -1f;
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

            if (m_CopyX || m_CopyY || m_CopyZ) transform.localPosition = new Vector3(newX, newY, newZ);
            if (m_CopyRotX || m_CopyRotY || m_CopyRotZ) transform.localEulerAngles = new Vector3(rotX, rotY, rotZ);
            if (m_CopyScaleX || m_CopyScaleY || m_CopyScaleZ) transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

            if (m_CopyRotationQuat) transform.localRotation = m_TargetTransform.localRotation;

            if (m_CopyPlayerHackFix)
            {
                if (m_CopyPlayerRightHand)
                {
                    transform.localEulerAngles = new Vector3(rotX + 180f, 360f - rotY, 360 - rotZ);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(rotX + 180f, rotY + 180f, 360 - rotZ);
                }   
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_TargetTransform != null && m_FixedUpdate)
        {
            float newX = transform.localPosition.x;
            float newY = transform.localPosition.y;
            float newZ = transform.localPosition.z;

            float rotX = transform.localEulerAngles.x;
            float rotY = transform.localEulerAngles.y;
            float rotZ = transform.localEulerAngles.z;

            float scaleX = transform.localScale.x;
            float scaleY = transform.localScale.y;
            float scaleZ = transform.localScale.z;

            if (m_CopyX)
            {
                newX = m_TargetTransform.localPosition.x;
            }

            if (m_CopyY)
            {
                newY = m_TargetTransform.localPosition.y;
            }

            if (m_CopyZ)
            {
                newZ = m_TargetTransform.localPosition.z;
            }

            if (m_CopyRotX)
            {
                rotX = m_TargetTransform.localEulerAngles.x;
            }

            if (m_CopyRotY)
            {
                rotY = m_TargetTransform.localEulerAngles.y;
            }

            if (m_CopyRotZ)
            {
                rotZ = m_TargetTransform.localEulerAngles.z;
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

            if (m_CopyX || m_CopyY || m_CopyZ) transform.localPosition = new Vector3(newX, newY, newZ);
            if (m_CopyRotX || m_CopyRotY || m_CopyRotZ) transform.localEulerAngles = new Vector3(rotX, rotY, rotZ);
            if (m_CopyScaleX || m_CopyScaleY || m_CopyScaleZ) transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

            if (m_CopyRotationQuat) transform.localRotation = m_TargetTransform.localRotation;

            if (m_CopyPlayerHackFix)
            {
                if (m_CopyPlayerRightHand)
                {
                    transform.localEulerAngles = new Vector3(rotX + 180f, rotY + 360f, 360 - rotZ);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(rotX + 180f, rotY + 180f, 360 - rotZ);
                }
            }
        }
    }
}
