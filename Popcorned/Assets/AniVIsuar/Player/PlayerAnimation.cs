using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // codigo que gerencia as animacoes do jogador em mais baixo nivel, maior baste das animacoes sao gerenciadas na aba 'animator'
    private SpriteRenderer fliped;
    private Animator animator;
    void Start()
    {
        fliped = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    
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
