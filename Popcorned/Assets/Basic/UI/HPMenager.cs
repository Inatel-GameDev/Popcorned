using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPMenager : MonoBehaviour
{
    public static HPMenager Instance;

    public int hp;
    public Text scoreText;
    void Awake()
    {
        Instance = this;

        hp = 3;
        scoreText.text = hp.ToString();
    }

    public void decreasepoints()
    {
        hp -= 1;
        if (hp <= 0)
        {
            ChangeScene("Menu");
        }
        scoreText.text = hp.ToString();
    }
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
