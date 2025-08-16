using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHandScript : MonoBehaviour
{
    [SerializeField]
    private bool m_LeftHand = false;

    [SerializeField]
    private Transform m_IndexFinger;
    [SerializeField]
    private Transform m_Thumb;
    [SerializeField]
    private Transform m_ExtraFingers;

    [SerializeField]
    private Vector3 m_CloseIndexRot;
    [SerializeField]
    private Vector3 m_CloseThumbRot;
    [SerializeField]
    private Vector3 m_CloseExtraFingersRot;

    [SerializeField]
    private float m_AnimateSpd = 1f;

    private Vector3 startIndexRot;
    private Vector3 startThumbRot;
    private Vector3 startExtraFingerRot;

    bool indexDown = false;
    bool thumbDown = false;
    bool extraFingersDown = false;

    [SerializeField]
    private GameObject heldObject;

    private VVRHMD heldHMD;

    [SerializeField]
    private float throwForce = 10f;

    private Rigidbody heldObjectPhysics;

    private bool skipHandPosFrame = false; //Every other frame for more dramatic throws
    private Vector3 lastHandPos;

    // Start is called before the first frame update
    void Start()
    {
        if (m_IndexFinger != null && m_Thumb != null && m_ExtraFingers != null)
        {
            startIndexRot = m_IndexFinger.localEulerAngles;
            startThumbRot = m_Thumb.localEulerAngles;
            startExtraFingerRot = m_ExtraFingers.localEulerAngles;
        }
    }

    void Update()
    {
        if (heldObject == null && heldObjectPhysics != null)
        {
            if (heldObjectPhysics.isKinematic) heldObjectPhysics.isKinematic = false;

            Vector3 posDiff = transform.position - lastHandPos;

            heldObjectPhysics.AddForce(posDiff.normalized * posDiff.magnitude * throwForce, ForceMode.Impulse);

            heldObjectPhysics = null;
        }

        if (heldObject != null && heldObjectPhysics == null)
        {
            heldObjectPhysics = heldObject.GetComponent<Rigidbody>();
        }

        indexDown = false;
        thumbDown = false;
        extraFingersDown = false;

        if (m_LeftHand)
        {
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Y))
            {
                thumbDown = true;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                extraFingersDown = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                indexDown = true;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.B))
            {
                thumbDown = true;
            }

            if (Input.GetMouseButton(1))
            {
                extraFingersDown = true;
            }

            if (Input.GetMouseButton(0))
            {
                indexDown = true;
            }
        }

        if (m_IndexFinger != null && m_Thumb != null && m_ExtraFingers != null)
        {
            //Animate the hand
            if (indexDown)
            {
                m_IndexFinger.localEulerAngles = VecLerp(m_IndexFinger.localEulerAngles, m_CloseIndexRot);
            }
            else
            {
                m_IndexFinger.localEulerAngles = VecLerp(m_IndexFinger.localEulerAngles, startIndexRot);
            }

            if (thumbDown)
            {
                m_Thumb.localEulerAngles = VecLerp(m_Thumb.localEulerAngles, m_CloseThumbRot);
            }
            else
            {
                m_Thumb.localEulerAngles = VecLerp(m_Thumb.localEulerAngles, startThumbRot);
            }

            if (extraFingersDown)
            {
                m_ExtraFingers.localEulerAngles = VecLerp(m_ExtraFingers.localEulerAngles, m_CloseExtraFingersRot);
            }
            else
            {
                m_ExtraFingers.localEulerAngles = VecLerp(m_ExtraFingers.localEulerAngles, startExtraFingerRot);
            }
        }

        if (extraFingersDown && heldObject != null)
        {
            heldObject.transform.parent = transform;
            if (heldObjectPhysics != null) heldObjectPhysics.isKinematic = true;

            if (heldHMD != null)
            {
                heldHMD.SetHeld(true);
            }
        }
        else if (!extraFingersDown && heldObject != null)
        {
            if (heldHMD != null)
            {
                heldHMD.SetHeld(false);
            }

            heldObject.transform.parent = null;
            heldObject = null;
            heldHMD = null;
        }

        if (!skipHandPosFrame) lastHandPos = transform.position;
        skipHandPosFrame = !skipHandPosFrame;
    }

    private Vector3 VecLerp(Vector3 v1, Vector3 v2)
    {
        return new Vector3(Mathf.LerpAngle(v1.x, v2.x, m_AnimateSpd * Time.deltaTime), Mathf.LerpAngle(v1.y, v2.y, m_AnimateSpd * Time.deltaTime), Mathf.LerpAngle(v1.z, v2.z, m_AnimateSpd * Time.deltaTime));
    }

    private void OnTriggerStay(Collider other)
    {
        if (heldObject == null)
        {
            if (other != null && other.tag == "Grabbable")
            {
                GameObject go = other.gameObject;

                if (go != null && go.transform.parent != null)
                {
                    if (extraFingersDown)
                    {
                        heldObject = go.transform.parent.gameObject;

                        if (heldObject.tag == "MockHMD")
                        {
                            heldHMD = heldObject.GetComponent<VVRHMD>();
                        }
                        else
                        {
                            heldHMD = null;
                        }
                    }
                }
            }
        }
    }
}
