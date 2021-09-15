using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource DeathAudio;
    protected Collider2D coll;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    // void Update()
    //{
        
    //}
    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        coll.enabled = false;
        Anim.SetTrigger("Death");
        DeathAudio.Play();
    }
}
