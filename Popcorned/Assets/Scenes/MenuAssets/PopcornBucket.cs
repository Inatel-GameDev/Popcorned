using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornBucket : MonoBehaviour
{
    public GameObject popcorn;
    [Range(0, 100)] public int spawnChance;

    private GameObject plane;
    private Vector3 rotationSpeed = new Vector3(0, 0, 50);

    private void Start()
    {
        plane = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime); // animação de rotacionar o balde
        bool spawn = (Random.Range(0, 100) < spawnChance) ? true : false;
        if (spawn) spawnPopcorn();
    }

    private void spawnPopcorn()
    {
        Vector3 popcornSpawn = new Vector3(plane.transform.position.x, plane.transform.position.y + 0.4f, plane.transform.position.z);
        Instantiate(popcorn, popcornSpawn, Quaternion.identity);
    }
}
