using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;

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
