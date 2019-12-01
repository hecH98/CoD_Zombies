using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip roundStart, roundEnd, monjita;
    private static AudioSource player;
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    public static void startRound()
    {
        player.clip = roundStart;
        player.Play();
    }

    public static void endRound()
    {
        player.clip = roundEnd;
        player.Play();
    }

    public static void Monja()
    {
        player.clip = monjita;
        player.Play();
    }
}
