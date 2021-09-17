using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enrmy_opossum : Enemies
{
    //public Rigidbody2D rb;
    //private Collider2D Coll;
    public Collider2D viewColl;

    public Transform left, right;
    public float Speed, leftx, rightx;
    private bool Faceleft = true;
    private bool Attacking;
    private GameObject targetPlayer;
    private bool Following;
    public GameObject ChatImage;
    private bool giveup = true;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

       //rb = GetComponent<Rigidbody2D>();
       // Coll = GetComponent<Collider2D>();
        targetPlayer = GameObject.FindWithTag("Player");


        //断绝父子关系
        transform.DetachChildren();

        leftx = left.position.x;
        rightx = right.position.x;
        Destroy(left.gameObject);
        Destroy(right.gameObject);
       ChatImage.transform.SetParent(transform);
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
        //Following = coll.IsTouching(targetPlayer.GetComponent<Collider2D>());
        //Debug.Log(Following);

    }
    void Attack() {
    
        rb.velocity = new Vector2((targetPlayer.transform.position.x - transform.position.x) * Speed, transform.position.y);
        //Following = !coll.IsTouching(targetPlayer.GetComponent<Collider2D>());      
        if (!Following && giveup)
        {

            Invoke("giveupFollowing", 2.0f);
            giveup = false;

        }
    }
    void Movement()
    {
        Anim.SetTrigger("idle");
        if (Faceleft)
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;

                return;

            }
            rb.velocity = new Vector2(-Speed, transform.position.y);

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
            Following = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
        void giveupFollowing() {
        
        Anim.SetTrigger("give");
        transform.localScale = new Vector3((targetPlayer.transform.position.x - transform.position.x) >= 0 ? 1 : -1, 1, 1);
      

            ChatImage.SetActive(true);
            Invoke("unlook", 1.5f);

        Attacking = false;
    }
 
    void unlook() {
        
        ChatImage.SetActive(false);
        giveup = true;
      
    }
}
