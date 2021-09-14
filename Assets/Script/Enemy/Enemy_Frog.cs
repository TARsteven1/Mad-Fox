using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemies
{
    public Rigidbody2D rb;
   // private Animator Anim;
    private Collider2D Coll;
    public LayerMask ground;
    public Transform left, right;
    public float Speed,JumpForce,leftx,rightx;
    private bool Faceleft=true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
       // Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        //断绝父子关系
        transform.DetachChildren();

        leftx = left.position.x;
        rightx = right.position.x;
        Destroy(left.gameObject);
        Destroy(right.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement();
        SwitchAnim();
    }
    void Movement()
    {
        if (Faceleft)
        {        
            if (transform.position.x <leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
                return;
               
            }
            if (Coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
        }
        else
        {
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
                return;
            }
            if (Coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }         
        }
    }
    void SwitchAnim()
    {
        //anim.SetBool("idle", false);
        //if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        //{
        //    anim.SetBool("falling", true);
        //}
        if (Anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                Anim.SetBool("Jumping", false);
                Anim.SetBool("Falling", true);

            }

        }
        //else if (isHurt)
        //{
        //    anim.SetBool("hurt", true);
        //    anim.SetFloat("running", 0);
        //    if (Mathf.Abs(rb.velocity.x) < 0.1f)
        //    {
        //        isHurt = false;
        //        anim.SetBool("hurt", false);
        //        anim.SetBool("idle", true);
        //    }
        //}
        else if (Coll.IsTouchingLayers(ground) &&Anim.GetBool("Falling"))
        {
            Anim.SetBool("Falling", false);
            //Anim.SetBool("Idle", true);
        }
    }

}
