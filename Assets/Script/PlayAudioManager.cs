using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioManager : MonoBehaviour
{
    //public static PlayAudioManager instance;

    public AudioSource audioSource;

    public AudioClip jumpAudio,hurtAudio,collectionAudio;

    //public AudioClip AudioClip;
    
  //  private void Awake() { instance = this; }
    //private void Awake()
    //{
    //    //if (Instance !=null)
    //    //{
    //        Instance = this;
    //   // }
        
    //}
    //public void PlaySound(string name) {
    //    AudioClip clip = Resources.LoadAudioClip(name);
    //    audioSource.PlayOneShot(clip);
    //}
    public void playJumpAudio() {
        //AudioClip clip = Resources.LoadAudioClip("");
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }   
    public void HurtAudio() {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }   
    public void CollectionAudio() {
        audioSource.clip = collectionAudio;
        audioSource.Play();
    }


}
