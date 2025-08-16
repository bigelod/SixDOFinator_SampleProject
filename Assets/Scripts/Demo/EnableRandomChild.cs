using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Transform> transforms = new List<Transform>();

        foreach (Transform child in transform)
        {
            transforms.Add(child);
        }

        if (transforms.Count > 0)
        {
            int index = Random.Range(0, transforms.Count);

            transforms[index].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
