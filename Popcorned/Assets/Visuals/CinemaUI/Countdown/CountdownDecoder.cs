using System;
using UnityEngine;

public class CountdownDecoder : MonoBehaviour
{
    public static CountdownDecoder Instance;
    public GameObject[] numbers;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (GameObject n in numbers)
            n.SetActive(false);
    }

    public void Decode(int index)
    {
        foreach (GameObject n in numbers)
            n.SetActive(false);

        try
        {
            numbers[index].SetActive(true);
        } catch (IndexOutOfRangeException)
        {
            Debug.Log("indice invalido");
        }
    }
}
