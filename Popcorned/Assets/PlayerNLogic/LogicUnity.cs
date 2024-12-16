using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

// classe que representa uma nota, utilizada para facilitar a implementacao do arquivo JSON
[System.Serializable]
public class Nota
{
    public float duration;
    public int direction;
    public Nota(float duration, int direction)
    {
        this.duration = duration;
        this.direction = direction;
    }
}

// a classe que representa uma fase, uma lista de notas, tambem utilizada para o arquivo JSON
[System.Serializable]
public class Fase
{
    public List<Nota> notas;
    public Fase()
    {
        notas = new List<Nota>();
    }
}

// a classe que representa a partitura, uma lista de fases, tambem utilizada para o arquivo JSON
[System.Serializable]
public class Partitura
{
    public List<Fase> phases;
    public Partitura()
    {
        phases = new List<Fase>();
    }
}

// classe principal
public class LogicUnity : MonoBehaviour
{
    // a classe LogicUnity eh declarada como Singleton para que possa ser referenciada de forma facil durante todo o codigo
    public static LogicUnity Instance;

    // dependencias
    public SpriteRenderer leftArrow, rightArrow, upArrow, downArrow, reverseSprite;
    public PopcornerBehavior popcornerR, popcornerL;
    public AudioSource src;
    public AudioClip cue;

    // atributos publicos
    // velocidade do jogo
        public int slowBpm;
        public int normalBpm;
        public int fastBpm;
    // o enum é utilizado para sinalizar a velocidade atual do jogo
    private enum CurrSpeed
    {
        slow, normal, fast
    } CurrSpeed currSpeed;

    public float spawndelay;
        // controle do rng
        [Range(0, 100)] public int ReverseChance;
        [Range(0, 100)] public int ChangeSpeedChance;

    // atributos privados
    private Partitura partitura;
    private float beat;

    void Awake()
    {
        Instance = this;

        leftArrow.enabled = false;
        rightArrow.enabled = false;
        upArrow.enabled = false;
        downArrow.enabled = false;
        reverseSprite.enabled = false;
    }
    void Start()
    {
        currSpeed = new CurrSpeed();
        currSpeed = CurrSpeed.normal;
        beat = (int) normalBpm / 60f;
        partitura = new Partitura();

        src.clip = cue;

        GenSheet();
        StartCoroutine(Loopar());
    }

    private IEnumerator GiveCue(int index, bool reverse)
    {
        foreach (Nota nota in partitura.phases[index].notas)
        {
            if (nota.direction == -1 ^ reverse)
            {
                src.panStereo = -1;
                StartCoroutine(ShowHide(leftArrow, 0.15f));
                src.Play();
            }
            else if (nota.direction == 1 ^ reverse)
            {
                src.panStereo = 1;
                StartCoroutine(ShowHide(rightArrow, 0.15f));
                src.Play();
            }

            yield return new WaitForSeconds(nota.duration / beat);
        }
    }

    private IEnumerator ShowHide(SpriteRenderer sprite, float duration)
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(duration);
        sprite.enabled = false;
    }

    private IEnumerator StartPopcorning(int index, bool reverse)
    {
        Fase fase = partitura.phases[index]; // Obtemos a fase correspondente
        foreach (Nota nota in fase.notas)
        {
            if (nota.direction == -1)
            {
                popcornerL.TossPopcorn(); // Ativa o efeito do lado esquerdo
            }

            if (nota.direction == 1)
            {
                popcornerR.TossPopcorn(); // Ativa o efeito do lado direito
            }

            yield return new WaitForSeconds(nota.duration / beat);
        }
    }

    private IEnumerator IniciarRound()
    {
        // rng
        int index = Random.Range(0, partitura.phases.Count);
        bool reverse = (Random.Range(0,100) < ReverseChance) ? true : false;
        bool changeSpeed = (Random.Range(0,100) <  ChangeSpeedChance) ? true : false;
        if (changeSpeed) speedUpSlowDown();

        Debug.Log($"Index = {index}");
        Debug.Log($"Reverse = {reverse}");

        if (reverse) yield return StartCoroutine(ShowHide(reverseSprite, 1f));

        yield return StartCoroutine(GiveCue(index, reverse));

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
        Debug.Log("Changing speed");
        int rng = Random.Range(0, 2);
        switch (currSpeed)
        {
            case CurrSpeed.slow:
                if (rng == 0)
                {
                    currSpeed = CurrSpeed.normal;
                    beat = normalBpm / 60f;
                }
                else
                {
                    currSpeed = CurrSpeed.fast;
                    beat = fastBpm / 60f;
                }
                StartCoroutine(ShowHide(upArrow, 1f));
                break;

            case CurrSpeed.normal:
                if (rng == 0)
                {
                    currSpeed = CurrSpeed.slow;
                    beat = slowBpm / 60f;
                    StartCoroutine(ShowHide(downArrow, 1f));
                }
                else
                {
                    currSpeed = CurrSpeed.fast;
                    beat = fastBpm / 60f;
                    StartCoroutine(ShowHide(upArrow, 1f));
                }
                break;

            case CurrSpeed.fast:
                if (rng == 0)
                {
                    currSpeed = CurrSpeed.normal;
                    beat = normalBpm / 60f;
                }
                else
                {
                    currSpeed = CurrSpeed.slow;
                    beat = slowBpm / 60f;
                }
                StartCoroutine(ShowHide(downArrow, 1f));
                break;
        }
    }

    private void GenSheet()
    {
        // Leitura do arquivo JSON
        string filePath = Path.Combine(Application.persistentDataPath, "partitura.json");
        string jsonContent = File.ReadAllText(filePath); // Lê o JSON do arquivo copiado
        if (!string.IsNullOrEmpty(jsonContent))
            partitura = JsonUtility.FromJson<Partitura>(jsonContent);


        // O final de todas as fases deve conter o tempo do final do round
        foreach (Fase fase in partitura.phases)
        {
            fase.notas[fase.notas.Count - 1].duration = spawndelay * beat;
        }
    }
}
