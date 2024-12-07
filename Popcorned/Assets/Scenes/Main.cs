using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // a classe Main eh declarada como Singleton para que possa ser referenciada de forma facil durante todo o codigo
    public static Main Instance;

    // atributos privados
    private int points;

    void Awake()
    {
        Instance = this;
        points = 3;
    }

    public void incrementPoints()
    {
        points++;
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
