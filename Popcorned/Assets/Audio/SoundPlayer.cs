using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public PopcornerBehavior popcornerR, popcornerL;
    public AudioSource src;
    public AudioClip cue;

    private List<Vector2> partitura;
    private float beat;
    public int bpm;
    public float spawndelay;
    
    void Awake()
    {
        beat = bpm / 60f;
        partitura = new List<Vector2>();
        src.clip = cue;
    }
    void Start()
    {
        StartCoroutine(Loopar());
    }
    private void GenSheet()
    {
        partitura.Add(new Vector2(1f, 0));
        partitura.Add(new Vector2(0.5f, 1));
        partitura.Add(new Vector2(0.5f, 1));
        partitura.Add(new Vector2(1f, 1));
        partitura.Add(new Vector2(0.5f, 0));
        partitura.Add(new Vector2(0.5f, 0));

        partitura.Add(new Vector2(spawndelay*beat, 0));
    }
    private IEnumerator GiveAudioCue()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            if (partitura[i].y == 0)
            {
                src.panStereo = -1;
            }
            else
            {
                src.panStereo = 1;
            }
            src.Play();
            yield return new WaitForSeconds(partitura[i].x/beat);
        }
    }
    private IEnumerator StartPopcorning()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            if (partitura[i].y == 0)
            {
                popcornerL.TossPopcorn();
            }
            else
            {
                popcornerR.TossPopcorn();
            }

            yield return new WaitForSeconds(partitura[i].x/beat);
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
