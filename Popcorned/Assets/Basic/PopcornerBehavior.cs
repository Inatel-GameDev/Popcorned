using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornerBehavior : MonoBehaviour
{
    public GameObject popcornl, popcornr;

    public float spawnrate = 1;
    public bool direction;

    private float timer = 0;
    void Start()
    {

    }

    void Update()
    {
        if (timer >= spawnrate)
        {
            SoundPlayer.Instance.GiveCue();
            if (direction)
            {
                Instantiate(popcornl,transform.position,transform.rotation);
            }
            else
            {
                Instantiate(popcornr, transform.position, transform.rotation);
            }
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
