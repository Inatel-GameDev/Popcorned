using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // a classe PlayerBehavior eh declarada como Singleton para que possa ser referenciada de forma facil durante todo o codigo
    public static PlayerBehavior Instance;

    // flag publicas utilizadas para a mecanica de parry
    public bool parringR, parringL;
    public float tolerance;

    private float timer = 0;

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
        // a cada frame sao coletados os inputs e as variaveis flags publicas sao atualizadas
        if (Input.GetMouseButtonDown(1))
        {
            if (parringR)
            {
                Debug.Log("cancel");
            }

            parringR = true;
            timer = Time.time;
        }

        if (parringR == true && (Time.time - timer) > tolerance)
        {
            Debug.Log("calma");
            parringR = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (parringL)
            {
                Debug.Log("cancel");
            }

            parringL = true;
            timer = Time.time;
        }

        if (parringL == true && (Time.time - timer) > tolerance)
        {
            Debug.Log("calma");
            parringL = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject pipocatemp = collision.gameObject;
        if (pipocatemp.name == "PopcornR(Clone)")
        {
            if (parringR)
            {
                Debug.Log("acertou");
                parringR = false;
            }
            else
            {
                Debug.Log("errou");
            }
        }

        if (pipocatemp.name == "PopcornL(Clone)")
        {
            if (parringL)
            {
                Debug.Log("acertou");
                parringL = false;
            }
            else
            {
                Debug.Log("errou");
            }
        }
    }
}
