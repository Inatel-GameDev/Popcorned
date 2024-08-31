using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornerBehavior : MonoBehaviour
{
    public GameObject popcornl, popcornr;

    public float RoundDelay;
    public float SpawnDelay;

    public bool direction;
    
    Coroutine coroutine;
    void Start()
    {
        coroutine = StartCoroutine(SpawnNWait());
    }

    IEnumerator SpawnNWait()
    {
        while (true)
        {
            yield return new WaitForSeconds(RoundDelay);
            SoundPlayer.Instance.GiveCue();
            yield return new WaitForSeconds(SpawnDelay);
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
}
