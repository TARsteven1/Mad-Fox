﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, collectionAudio,dashAudio;
    private void Awake()
    {
        instance = this;
    }

    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    public void CollectionAudio()
    {
        audioSource.clip = collectionAudio;
        audioSource.Play();

    }    public void DashAudio()
    {
        audioSource.clip = dashAudio;
        audioSource.Play();

    }
}
