using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGenerator : MonoBehaviour
{
    public GameObject meteores;
    public float creationTime = 2f;
    public float creationRange = 2f;

    void Start()
    {
        InvokeRepeating("CreateMeteorite", 0.0f, creationTime);
    }

    void Update()
    {
        
    }

    public void CreateMeteorite() {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        spawnPosition = this.transform.position + Random.onUnitSphere * creationRange;
        spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0);

        GameObject meteorite = Instantiate(meteores, spawnPosition, Quaternion.identity);
    }
}
