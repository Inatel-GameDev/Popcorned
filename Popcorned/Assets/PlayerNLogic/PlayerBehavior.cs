using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;

    public bool parringR, parringL;
    public float tolerance;
    public float invincibilityDuration = 0.5f; // Tempo de invencibilidade em segundos

    private float timer = 0;
    private bool invincible = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        parringR = false;
        parringL = false;
    }

    void Update()
    {
        if (invincible) return; // Se estiver invencível, ignora os inputs

        if (Input.GetMouseButtonDown(1))
        {
            if (parringR)
            {
                Main.Instance.decrementPoints();
                Debug.Log("cancel");
                StartCoroutine(InvincibilityCoroutine()); // Inicia invencibilidade
            }
            parringR = true;
            timer = Time.time;
        }

        if (parringR && (Time.time - timer) > tolerance)
        {
            Main.Instance.decrementPoints();
            Debug.Log("calma");
            StartCoroutine(InvincibilityCoroutine()); // Inicia invencibilidade
            parringR = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (parringL)
            {
                Main.Instance.decrementPoints();
                Debug.Log("cancel");
                StartCoroutine(InvincibilityCoroutine()); // Inicia invencibilidade
            }
            parringL = true;
            timer = Time.time;
        }

        if (parringL && (Time.time - timer) > tolerance)
        {
            Main.Instance.decrementPoints();
            Debug.Log("calma");
            StartCoroutine(InvincibilityCoroutine()); // Inicia invencibilidade
            parringL = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincible) return; // Se estiver invencível, ignora colisões

        GameObject pipocatemp = collision.gameObject;
        if (pipocatemp.name == "PopcornR(Clone)")
        {
            if (parringR)
            {
                Main.Instance.incrementPoints();
                Debug.Log("acertou");
                parringR = false;
            }
            else
            {
                Main.Instance.decrementPoints();
                Debug.Log("errou");
                StartCoroutine(InvincibilityCoroutine()); // Inicia invencibilidade
            }
        }

        if (pipocatemp.name == "PopcornL(Clone)")
        {
            if (parringL)
            {
                Main.Instance.incrementPoints();
                Debug.Log("acertou");
                parringL = false;
            }
            else
            {
                Main.Instance.decrementPoints();
                Debug.Log("errou");
                StartCoroutine(InvincibilityCoroutine()); // Inicia invencibilidade
            }
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        invincible = true;
        Debug.Log("Invencível por " + invincibilityDuration + " segundos");
        yield return new WaitForSeconds(invincibilityDuration);
        invincible = false;
        Debug.Log("Invencibilidade acabou");
    }
}
