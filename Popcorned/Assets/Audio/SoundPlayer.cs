using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;

    public AudioSource src;
    public AudioClip cue;

    private void Awake()
    {
        Instance = this;
    }

    public void GiveCue()
    {
        src.clip = cue;
        src.Play();
    }
}
