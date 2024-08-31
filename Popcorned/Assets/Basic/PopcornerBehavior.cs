using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornerBehavior : MonoBehaviour
{
    public GameObject popcornl, popcornr;
    public bool direction;

    public void TossPopcorn()
    {
        if (direction)
        {
            Instantiate(popcornl, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(popcornr, transform.position, transform.rotation);
        }
    }
}
