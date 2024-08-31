using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public PopcornerBehavior popcornerR, popcornerL;
    public AudioSource src;
    public AudioClip cue;

    private List<float> partitura;
    
    void Awake()
    {
        partitura = new List<float>();
        src.clip = cue;
    }
    void Start()
    {
        StartCoroutine(Loopar());
    }
    private void GenSheet()
    {
        partitura.Add(1f);
        partitura.Add(0.5f);
        partitura.Add(1f);
        partitura.Add(0.25f);

        partitura.Add(0f);
    }
    private IEnumerator GiveAudioCue()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            src.Play();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator StartPopcorning()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            popcornerL.TossPopcorn();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IniciarRound()
    {
        GenSheet();

        yield return StartCoroutine(GiveAudioCue());

        yield return StartCoroutine(StartPopcorning());

        partitura.Clear();
    }

    private IEnumerator Loopar()
    {
        while (true)
        {
            yield return StartCoroutine(IniciarRound());

            yield return new WaitForSeconds(1f);
        }
    }
}
