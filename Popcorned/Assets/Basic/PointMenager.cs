using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointMenager : MonoBehaviour
{
    public static PointMenager Instance;

    public int playerScore;
    public Text scoreText;
    void Awake()
    {
        Instance = this;

        playerScore = 0;
        scoreText.text = playerScore.ToString();
    }

    public void increasepoints()
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString();
    }

    public void decreasepoints()
    {
        playerScore -= 1;
        scoreText.text = playerScore.ToString();
    }
}
