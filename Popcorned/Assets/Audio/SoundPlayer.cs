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
    private float beat;
    public int bpm;
    public float spawndelay;
    
    void Awake()
    {
        beat = bpm / 60f;
        partitura = new List<float>();
        src.clip = cue;
    }
    void Start()
    {
        StartCoroutine(Loopar());
    }
    private void GenSheet()
    {
        partitura.Add(0.5f);
        partitura.Add(0.5f);
        partitura.Add(0.5f);
        partitura.Add(0.5f);
        partitura.Add(1f);

        partitura.Add(spawndelay*beat);
    }
    private IEnumerator GiveAudioCue()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            src.Play();
            yield return new WaitForSeconds(partitura[i]/beat);
        }
    }
    private IEnumerator StartPopcorning()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            popcornerL.TossPopcorn();
            yield return new WaitForSeconds(partitura[i]/beat);
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
        yield return new WaitForSeconds(spawndelay*beat);
        while (true)
        {
            yield return StartCoroutine(IniciarRound());
        }
    }
}
