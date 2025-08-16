using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimFrameStart : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private string AnimationName = "";

    // Start is called before the first frame update
    void Start()
    {
        if (m_Animator == null)
        {
            m_Animator = GetComponent<Animator>();
        }

        if (m_Animator != null)
        {
            m_Animator.Play(AnimationName, -1, Random.Range(0f, 1f));
            m_Animator.speed = Random.Range(0.3f, 0.8f);
        }

        transform.localEulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
