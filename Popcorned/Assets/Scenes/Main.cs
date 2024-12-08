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

    void Awake()
    {
        Instance = this;
        points = startingPoints;
    }

    public void incrementPoints()
    {
        points += (points < pointLimit) ? 1 : 0;
    }

    public void decrementPoints()
    {
        points--;
        if (points < 0)
            LoadStart();
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("Menu");
    }
}
