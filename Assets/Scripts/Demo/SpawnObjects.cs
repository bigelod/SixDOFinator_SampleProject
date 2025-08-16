using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;

    [SerializeField]
    private float m_SpawnAwayFromPlayerDist = 10f;

    [SerializeField]
    private int m_MaxObjectSpawn = 10;

    [SerializeField]
    private float m_MinCooldown = 5f;

    [SerializeField]
    private float m_MaxCooldown = 25f;

    [SerializeField]
    private GameObject m_SpawnObject;

    [SerializeField]
    private string m_ObjectTag = "Enemy";

    private float spawnCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Player == null)
        {
            GameObject go = GameObject.FindWithTag("MainCamera");

            m_Player = go.GetComponent<Transform>(); 
        }

        spawnCooldown = Random.Range(m_MinCooldown, m_MaxCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldown > 0f)
        {
            spawnCooldown -= Time.deltaTime;
        }

        if (m_Player != null && m_SpawnObject != null && spawnCooldown <= 0f) 
        {
            int enemyCount = GameObject.FindGameObjectsWithTag(m_ObjectTag).Length;

            float spawnChance = Random.Range(0f, 1000f);

            if (enemyCount < m_MaxObjectSpawn && spawnChance >= 400f && spawnChance <= 600f)
            {
                if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(m_Player.position.x, 0f, m_Player.transform.position.z)) > m_SpawnAwayFromPlayerDist)
                {
                    GameObject.Instantiate(m_SpawnObject, transform.position, Quaternion.identity);

                    spawnCooldown = Random.Range(m_MinCooldown, m_MaxCooldown);
                }
            }
        }
    }
}
