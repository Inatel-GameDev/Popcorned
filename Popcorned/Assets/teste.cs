using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SequenciaFuncoes : MonoBehaviour
{
    void Start()
    {
        // Iniciar a sequência em loop
        StartCoroutine(LoopExecutarSequencia());
    }

    // Coroutine para Funcao1
    private IEnumerator Funcao1()
    {
        Debug.Log("Início de Funcao1");

        // Simula algum trabalho com uma espera
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Fim de Funcao1");
    }

    // Coroutine para Funcao2
    private IEnumerator Funcao2()
    {
        Debug.Log("Início de Funcao2");

        // Simula outro trabalho com uma espera
        yield return new WaitForSeconds(3.0f);

        Debug.Log("Fim de Funcao2");
    }

    // Coroutine para gerenciar a sequência das funções
    private IEnumerator ExecutarSequencia()
    {
        // Executa Funcao1 e espera sua conclusão
        yield return StartCoroutine(Funcao1());

        // Depois que Funcao1 terminar, executa Funcao2
        yield return StartCoroutine(Funcao2());

        Debug.Log("Sequência completa");
    }

    // Coroutine para manter a sequência em loop
    private IEnumerator LoopExecutarSequencia()
    {
        while (true) // Loop infinito
        {
            yield return StartCoroutine(ExecutarSequencia());

            // Adiciona uma espera opcional entre cada loop, se necessário
            yield return new WaitForSeconds(1.0f); // Espera 1 segundo antes de reiniciar a sequência
        }
    }
}
