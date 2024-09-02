using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    // O objeto SoundPlayer eh um dos nucleos da logica do projeto, ele controla toda a parte de logica de gameplay associada ao ritmo do jogo
    // ele tambem eh declarado como Singleton para ser referenciado de forma facil

    public int bpm; // variavel principal do codigo, controla toda a velocidade de tudo relacionado ao ritmo

    // atributos de referencia
    public PopcornerBehavior popcornerR, popcornerL;
    public AudioSource src;
    public AudioClip cue;

    // atributos logicos
    private List<Vector2> partitura; // uma lista guiada que armazena os intervalos e o sentido das pipocas/notas
    private float beat; // uma variavel intermediaria para facilitar calculos, representa a frequencia em Hz do bpm
    public float spawndelay; // uma variavel que define o intervalo entre o fim das dicas de audio e o spawn das pipocas
    
    void Awake()
    {
        beat = bpm / 60f;
        partitura = new List<Vector2>();
        src.clip = cue;
    }

    // o codigo utiliza uma vasta quantidade de corrotinas para manusear metodos independente do tempo dos frames
    void Start()
    {
        // inicializacao da corrotina principal, que roda em loop
        StartCoroutine(Loopar());
    }
    private void GenSheet() // metodo privato, ele gera os valores das notas (intervalo e sentido) e os armazena no atributo 'partitura'
    {
        partitura.Add(new Vector2(1f, 0));
        partitura.Add(new Vector2(0.5f, 1));
        partitura.Add(new Vector2(0.5f, 1));
        partitura.Add(new Vector2(1f, 1));
        partitura.Add(new Vector2(0.5f, 0));
        partitura.Add(new Vector2(0.5f, 0));

        partitura.Add(new Vector2(spawndelay*beat, 0)); // o ultimo intervalo entre notas representa o intervalo entre o fim da corrotina atual e a proxima
    }

    // os metodos GiveAudioCue e StartPopcorning realizam a leitura do atributo 'partitura' para determinar o spawn das dicas de audio e das pipocas
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

    private IEnumerator IniciarRound() // metodo que abstrai o inicio de um 'round' no jogo, gerando uma nova partitura e iniciando o round de fato
    {
        GenSheet();

        yield return StartCoroutine(GiveAudioCue());

        yield return StartCoroutine(StartPopcorning());

        partitura.Clear();
    }

    private IEnumerator Loopar() // metodo que roda as corrotinas em loop
    {
        yield return new WaitForSeconds(spawndelay*beat);
        while (true)
        {
            yield return StartCoroutine(IniciarRound());
        }
    }
}
