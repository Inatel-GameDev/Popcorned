using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer fliped;
    private Animator animator;
    void Start()
    {
        fliped = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fliped.flipX = true;
            animator.SetTrigger("Punching");
        }

        if (Input.GetMouseButtonDown(1))
        {
            fliped.flipX = false;
            animator.SetTrigger("Punching");
        }
    }
}
