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
    private List<Vector2>[] partitura; // uma lista guiada que armazena os intervalos e o sentido das pipocas/notas
    private float beat; // uma variavel intermediaria para facilitar calculos, representa a frequencia em Hz do bpm
    private bool reverse; // enquanto a variavel reverse estiver ativa, as pipocas vao spawnar no sentido oposto a dica de audio
    private int maxrounds;
    private int rounds;
    public float spawndelay; // uma variavel que define o intervalo entre o fim das dicas de audio e o spawn das pipocas
    
    void Awake()
    {
        reverse = false;
        rounds = 0;
        maxrounds = 7;
        beat = bpm / 60f;
        partitura = new List<Vector2>[maxrounds];
        src.clip = cue;

        for (int i = 0; i < maxrounds; i++)
            partitura[i] = new List<Vector2>();
    }

    // o codigo utiliza uma vasta quantidade de corrotinas para manusear metodos independente do tempo dos frames
    void Start()
    {
        // inicializacao da corrotina principal, que roda em loop
        GenSheet();
        StartCoroutine(Loopar());
    }

    // os metodos GiveAudioCue e StartPopcorning realizam a leitura do atributo 'partitura' para determinar o spawn das dicas de audio e das pipocas
    private IEnumerator GiveAudioCue()
    {
        for (int i = 0; i < partitura[rounds].Count; i++)
        {
            if (partitura[rounds][i].y == 0)
            {
                src.panStereo = -1;
                src.Play();
            }
            
            if (partitura[rounds][i].y == 1)
            {
                src.panStereo = 1;
                src.Play();
            }
            
            yield return new WaitForSeconds(partitura[rounds][i].x/beat);
        }
    }
    private IEnumerator StartPopcorning()
    {
        for (int i = 0; i < partitura[rounds].Count; i++)
        {
            if (partitura[rounds][i].y == 0)
            {
                if (!reverse)
                {
                    popcornerL.TossPopcorn();
                }
                else
                {
                    popcornerR.TossPopcorn();
                }
            }

            if (partitura[rounds][i].y == 1)
            {
                if (!reverse)
                { 
                    popcornerR.TossPopcorn();
                }
                else
                {
                    popcornerL.TossPopcorn();
                }
            }

            yield return new WaitForSeconds(partitura[rounds][i].x/beat);
        }
    }

    private IEnumerator IniciarRound() // metodo que abstrai o inicio de um 'round' no jogo, gerando uma nova partitura e iniciando o round de fato
    {
        yield return StartCoroutine(GiveAudioCue());

        yield return StartCoroutine(StartPopcorning());
    }

    private void checkspecialround()
    {
        switch (rounds)
        {
            case 3:
                speedUp();
                break;

            case 5:
                slowDown(); 
                break;
        }
    }
    private IEnumerator Loopar() // metodo que roda as corrotinas em loop
    {
        yield return new WaitForSeconds(spawndelay*beat);
        for (this.rounds = 0; this.rounds < maxrounds; this.rounds++)
        {
            checkspecialround();
            yield return StartCoroutine(IniciarRound());
        }
    }

    private void speedUp()
    {
        bpm = bpm * 2;
        beat = bpm / 60f;
        PlayerBehavior.Instance.tolerance = PlayerBehavior.Instance.tolerance/2;
    }

    private void slowDown()
    {
        bpm = bpm / 2;
        beat = bpm / 60f;
        PlayerBehavior.Instance.tolerance = PlayerBehavior.Instance.tolerance*2;
    }

    private void GenSheet() // metodo privado, ele abstrai os valores das notas (intervalo e sentido) e os armazena no atributo 'partitura'
    {
        float roundendcue = spawndelay*beat;

        partitura[0].Add(new Vector2(1f, 0));
        partitura[0].Add(new Vector2(0.5f, 1));
        partitura[0].Add(new Vector2(0.5f, 1));
        partitura[0].Add(new Vector2(1f, 1));
        partitura[0].Add(new Vector2(0.5f, 0));
        partitura[0].Add(new Vector2(0.5f, 0));
        partitura[0].Add(new Vector2(roundendcue, 0)); // o ultimo intervalo entre notas representa o intervalo entre o fim da corrotina atual e a proxima

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
