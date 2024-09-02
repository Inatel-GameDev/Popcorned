using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornerBehavior : MonoBehaviour
{
    // objeto vazio que possui um metodo capaz de instanciar a classe 'pipoca', o spanw depende da direcao em que a pipoca deve ser atirada
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
