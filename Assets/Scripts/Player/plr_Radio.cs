using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plr_Radio : MonoBehaviour
{
    public AudioSource randomSound;
    public AudioClip[] audioSources;

    void Start()
    {
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play();
    }
}
