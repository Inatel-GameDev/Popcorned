using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // a classe Main eh declarada como Singleton para que possa ser referenciada de forma facil durante todo o codigo
    public static Main Instance;

    // atributos publicos
    public int pointLimit;
    public int startingPoints;

    // atributos privados
    private int points;

    private void Awake()
    {
        Instance = this;
        points = startingPoints;
    }

    private void Start()
    {
        CountdownDecoder.Instance.Decode(points);
    }

    public void incrementPoints()
    {
        points += (points < pointLimit) ? 1 : 0;
        CountdownDecoder.Instance.Decode(points);
    }

    public void decrementPoints()
    {
        points--;
        if (points < 0)
            LoadStart();
        else
            CountdownDecoder.Instance.Decode(points);
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("Menu");
    }
}
