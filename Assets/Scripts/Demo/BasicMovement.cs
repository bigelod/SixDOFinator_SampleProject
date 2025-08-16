using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_Head;

    [SerializeField]
    private bool useRawThumbXY = false;

    [SerializeField]
    private float thumbX = 0f;
    [SerializeField]
    private float thumbY = 0f;

    [SerializeField]
    private float moveSpd = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleInputMode();
        }

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        if (m_Head != null)
        {
            forward = new Vector3(m_Head.forward.x, 0f, m_Head.forward.z);
            right = new Vector3(m_Head.right.x, 0f, m_Head.right.z);
        }

        if (!useRawThumbXY)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                thumbY = 1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                thumbY = -1f;
            }
            else
            {
                thumbY = 0f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                thumbX = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                thumbX = 1f;
            }
            else
            {
                thumbX = 0f;
            }
        }

        transform.Translate((forward * thumbY + right * thumbX) * moveSpd * Time.deltaTime, Space.World);
    }

    public void SetThumbXY(float x, float y)
    {
        if (useRawThumbXY)
        {
            thumbX = x;
            thumbY = y;
        }
    }

    public void ToggleInputMode()
    {
        useRawThumbXY = !useRawThumbXY;
    }
}
