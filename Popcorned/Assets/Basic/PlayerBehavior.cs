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
            parringR = true;
            timer = Time.time;
        }

        if (parringR == true && (Time.time - timer) > tolerance)
        {
            parringR = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            parringL = true;
            timer = Time.time;
        }

        if (parringL == true && (Time.time - timer) > tolerance)
        {
            parringL = false;
        }
    }
}
