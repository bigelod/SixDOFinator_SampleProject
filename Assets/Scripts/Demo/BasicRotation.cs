using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotation : MonoBehaviour
{
    [SerializeField]
    private float thumbX = 0f;
    [SerializeField]
    private float thumbY = 0f;

    [SerializeField]
    private float rotSpd = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rotSpd = PlayerPrefs.GetFloat("rotSpd", 90f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * thumbX * rotSpd * Time.deltaTime);
    }

    public void SetThumbXY(float x, float y)
    {
        thumbX = x;
        thumbY = y;
    }

    public void SetRotSpd(float spd)
    {
        rotSpd = spd;
        PlayerPrefs.SetFloat("rotSpd", spd);
    }

    public float GetRotSpd()
    {
        return rotSpd;
    }
}
