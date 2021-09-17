using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enrmy_opossum : Enemies
{
    public Rigidbody2D rb;
    private Collider2D Coll;
    public Collider2D viewColl;
    public LayerMask ground;
    public Transform left, right;
    public float Speed, leftx, rightx;
    private bool Faceleft = true;
    private bool Attacking; 
    private GameObject targetPlayer;
    private bool Following;
    public GameObject ChatImage;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        targetPlayer = GameObject.FindWithTag("Player");
       

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

        if (!Attacking)
        {
            Movement();
        }
        else
        {
            Attack();
        }


    }
    void Attack() {

        rb.velocity = new Vector2((targetPlayer.transform.position.x-transform.position.x) *Speed, transform.position.y);
        Following = !coll.IsTouching(targetPlayer.GetComponent<Collider2D>());

        if ( Following)
        {
            Invoke("giveupFollowing", 3.0f);
        }
    }
    void Movement()
    {
        Anim.SetBool("Giveup", Attacking);
        if (Faceleft)
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
                
                return;

            }
            rb.velocity = new Vector2(-Speed, transform.position.y);
            //if (Coll.IsTouchingLayers(ground))
            //{
            //    //Anim.SetBool("Jumping", true);
            //    rb.velocity = new Vector2(-Speed, transform.position.y);
            //}
        }
        else
        {
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
                
                return;
            }
            rb.velocity = new Vector2(Speed, transform.position.y);
            //if (Coll.IsTouchingLayers(ground))
            //{
            //    //Anim.SetBool("Jumping", true);
            //    rb.velocity = new Vector2(Speed, transform.position.y);
            //}
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Attacking = true;     
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {

    //        Attacking = false;
    //    }
    //}
    void giveupFollowing() {
        Anim.SetBool("Giveup",!Attacking);
        Attacking = false;
    }
    void look() { ChatImage.SetActive(true); }
    void unlook() { ChatImage.SetActive(false); }
}
