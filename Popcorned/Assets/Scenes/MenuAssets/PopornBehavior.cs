using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopornBehavior : MonoBehaviour
{
    private const float deadline = -8;

    private void Update()
    {
        if (transform.position.y < deadline)
            Destroy(gameObject);
    }
}
