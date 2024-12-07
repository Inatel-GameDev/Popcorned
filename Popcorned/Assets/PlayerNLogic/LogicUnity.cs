using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogicUnity : MonoBehaviour
{
    // a classe LogicUnity eh declarada como Singleton para que possa ser referenciada de forma facil durante todo o codigo
    public static LogicUnity Instance;

    // dependencias
    public PopcornerBehavior popcornerR, popcornerL;
    public AudioSource src;
    public AudioClip cue;

    // controle do rng
    [Range(0, 100)] public int ReverseChance;
    [Range(0, 100)] public int ChangeSpeedChance;

    // atributos publicos
    public int bpm;
    public float spawndelay;

    // atributos privados
    const int partituraSize = 7;
    private List<Vector2>[] partitura;
    private float beat;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        beat = bpm / 60f;
        partitura = new List<Vector2>[partituraSize];

        src.clip = cue;
        for (int i = 0; i < partituraSize; i++)
            partitura[i] = new List<Vector2>();

        GenSheet();
        StartCoroutine(Loopar());
    }
    
    private IEnumerator GiveAudioCue(int index, bool reverse)
    {
        foreach (Vector2 nota in partitura[index])
        {
            if (nota.y == 0)
            {
                src.panStereo = reverse ? 1 : -1;
                src.Play();
            }

            if (nota.y == 1)
            {
                src.panStereo = reverse ? -1 : 1;
                src.Play();
            }

            yield return new WaitForSeconds(nota.x / beat);
        }
    }
    private IEnumerator StartPopcorning(int index, bool reverse)
    {
        foreach (Vector2 nota in partitura[index])
        {
            if (nota.y == 0)
                popcornerL.TossPopcorn();

            if (nota.y == 1)
                popcornerR.TossPopcorn();

            yield return new WaitForSeconds (nota.x / beat);
        }
    }
    
    private IEnumerator IniciarRound()
    {
        // rng
        int index = Random.Range(0, partituraSize);
        bool reverse = (Random.Range(0,100) < ReverseChance) ? true : false;
        bool changeSpeed = (Random.Range(0,100) <  ChangeSpeedChance) ? true : false;
        if (changeSpeed) speedUpSlowDown();

        Debug.Log($"Index = {index}");
        Debug.Log($"Reverse = {reverse}");

        yield return StartCoroutine(GiveAudioCue(index, reverse));

        yield return StartCoroutine(StartPopcorning(index, reverse));
    }
    private IEnumerator Loopar()
    {
        yield return new WaitForSeconds(spawndelay*beat);
        while (true)
            yield return StartCoroutine(IniciarRound());
    }
    
    private void speedUpSlowDown()
    {

    }
    private void GenSheet()
    {
        float roundendcue = spawndelay*beat;

        partitura[0].Add(new Vector2(1f, 0));
        partitura[0].Add(new Vector2(0.5f, 1));
        partitura[0].Add(new Vector2(0.5f, 1));
        partitura[0].Add(new Vector2(1f, 1));
        partitura[0].Add(new Vector2(0.5f, 0));
        partitura[0].Add(new Vector2(0.5f, 0));
        partitura[0].Add(new Vector2(roundendcue, 0));

        partitura[1].Add(new Vector2(1f, 1));
        partitura[1].Add(new Vector2(1f, 1));
        partitura[1].Add(new Vector2(1f, 1));
        partitura[1].Add(new Vector2(roundendcue, 1));

        partitura[2].Add(new Vector2(0.5f, 1));
        partitura[2].Add(new Vector2(0.5f, 0));
        partitura[2].Add(new Vector2(0.5f, 1));
        partitura[2].Add(new Vector2(0.5f, 0));
        partitura[2].Add(new Vector2(1f, 1));
        partitura[2].Add(new Vector2(roundendcue, 0));

        partitura[3].Add(new Vector2(1f, 0));
        partitura[3].Add(new Vector2(1f, 0));
        partitura[3].Add(new Vector2(0.5f, 1));
        partitura[3].Add(new Vector2(0.5f, 1));
        partitura[3].Add(new Vector2(roundendcue, 0));

        partitura[4].Add(new Vector2(0.5f, 0));
        partitura[4].Add(new Vector2(0.5f, 1));
        partitura[4].Add(new Vector2(0.5f, 0));
        partitura[4].Add(new Vector2(0.5f, 1));
        partitura[4].Add(new Vector2(1f, 0));
        partitura[4].Add(new Vector2(roundendcue, 1));

        partitura[5].Add(new Vector2(1f, 0));
        partitura[5].Add(new Vector2(0.5f, -1));
        partitura[5].Add(new Vector2(1f, 0));
        partitura[5].Add(new Vector2(0.5f, 1));
        partitura[5].Add(new Vector2(0.5f, 1));
        partitura[5].Add(new Vector2(roundendcue, 1));

        partitura[6].Add(new Vector2(1f, 0));
        partitura[6].Add(new Vector2(0.5f, 1));
        partitura[6].Add(new Vector2(0.5f, 1));
        partitura[6].Add(new Vector2(1f, 1));
        partitura[6].Add(new Vector2(0.5f, 0));
        partitura[6].Add(new Vector2(0.5f, 0));
        partitura[6].Add(new Vector2(roundendcue, 0));
    }
}
