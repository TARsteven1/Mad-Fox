using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemies
{
    private Rigidbody2D rb;
    //private Animator Anim;
    //private Collider2D Coll;
    //public LayerMask ground;
    public Transform top, bottom;
    public float Speed, topy, bottomy;
    private bool isUp;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
       // Coll = GetComponent<Collider2D>();
        //断绝父子关系
        transform.DetachChildren();

        topy = top.position.y;
        bottomy = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y>topy)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < bottomy)
            {
                isUp = true;
            }
        }
    }
  
}
