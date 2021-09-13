﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public Collider2D crouchcoll;
    public Transform CellingCheck;
    public Transform zeroPoint;
    public Transform GroundCheck;
    private bool isRelife;
    public Joystick Joystick;
    public Toggle isVritualControl;
    private float horizontalmove;


    [Space]
    public float Speed;
    public float jumpforce;
    [Header("XXX")]
    public LayerMask ground;
    [HideInInspector]
    public int Cherry;

    //UI
    public Text CherryCount;

    private bool isHurt;
    private bool isGround;
    private bool isJump;
    private bool jumpPressed;

    public int JumpCount;

    [Header("Dash参数")]
    public float dashTime;
    private float dashTimeLeft;
    private float lastDash=-10;
    public float dashCoolDown;
    public float dashSpeed;

    public bool isDashing;
    public Image CDImage;

    public Button dashBtn;
    private bool isdashBtn;


    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
      
        
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (!isRelife)
        {
            if (!isHurt)
            {
                isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, ground);
                Dash();
                if (isDashing) return;
               
                Movement();
            }
            SwitchAnim();
            NewnewJump();
            
        }
       
    }
    private void Update()
    {
        if (!isRelife)
        {
            Crouch();
            PresseDash();
        }
    }
    void Movement()
    {
        if (isVritualControl.isOn)
        {
            horizontalmove = Joystick.Horizontal;
            float facedircetion = Joystick.Horizontal;
            if (horizontalmove != 0)
            {

                rb.velocity = new Vector2(horizontalmove * Speed * Time.fixedDeltaTime, rb.velocity.y);
                anim.SetFloat("running", Mathf.Abs(facedircetion));
            }
            if (facedircetion > 0)
            {

                transform.localScale = new Vector3(1, 1, 1);
            }
            if (facedircetion < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            //float horizontalmove = Input.GetAxis("Horizontal");
            //float facedircetion = Input.GetAxisRaw("Horizontal");

            //if (horizontalmove != 0)
            //{

            //    rb.velocity = new Vector2(horizontalmove * Speed * Time.fixedDeltaTime, rb.velocity.y);
            //    anim.SetFloat("running", Mathf.Abs(facedircetion));
            //}
            //if (facedircetion != 0)
            //{

            //    transform.localScale = new Vector3(facedircetion, 1, 1);
            //}
            horizontalmove = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("running", Mathf.Abs(horizontalmove));
            if (horizontalmove != 0)
            {
                rb.velocity = new Vector2(horizontalmove * Speed, rb.velocity.y);
                transform.localScale = new Vector3(horizontalmove, 1, 1); 
            }
        }

    }       
    
    void SwitchAnim()
    {
        if (isVritualControl.isOn)
        {
            if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
            {
                anim.SetBool("falling", true);
            }
            if (anim.GetBool("jumping"))
            {
                if (rb.velocity.y < 0)
                {
                    anim.SetBool("jumping", false);
                    anim.SetBool("falling", true);
                }

            }
            else if (isHurt)
            {
                anim.SetBool("hurt", true);
                anim.SetFloat("running", 0);
                if (Mathf.Abs(rb.velocity.x) < 0.1f)
                {
                    isHurt = false;
                    anim.SetBool("hurt", false);

                }
            }
            else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("falling", false);

            }
        }
        else
        {
            anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
            if (isGround)
            {
                anim.SetBool("falling", false);
            }
            else if (!isGround&&rb.velocity.y > 0)
            {
                anim.SetBool("jumping", true);
            }
            else if (rb.velocity.y < 0)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);

            }
            //if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
            //{
            //    anim.SetBool("falling", true);
            //}
            //if (anim.GetBool("jumping"))
            //{
            //    if (rb.velocity.y < 0)
            //    {
            //        anim.SetBool("jumping", false);
            //        anim.SetBool("falling", true);

            //    }

            //}
            //else if (isHurt)
            //{
            //    anim.SetBool("hurt", true);
            //    anim.SetFloat("running", 0);
            //    if (Mathf.Abs(rb.velocity.x) < 0.1f)
            //    {
            //        isHurt = false;
            //        anim.SetBool("hurt", false);
                    
            //    }
            //}
            //else if (coll.IsTouchingLayers(ground))
            //{
            //    anim.SetBool("falling", false);
               
            //}
        }
            
    }
    //void Jump() {
    //    if (isVritualControl.isOn)
    //    {
    //        if (Joystick.Vertical > 0.5f && coll.IsTouchingLayers(ground))
    //        {

    //            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
    //            jumpAudio.Play();
    //            //jumpAudio.PlayOneShot(clip);
    //            anim.SetBool("jumping", true);
    //            anim.SetBool("crouch", false);
    //            anim.SetBool("falling", false);

    //        }
    //    }
    //    else
    //    {
    //        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
    //        {
    //            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
    //            jumpAudio.Play();
    //            //jumpAudio.PlayOneShot(clip);
    //            anim.SetBool("jumping", true);
    //            anim.SetBool("crouch", false);
    //            anim.SetBool("falling", false);
    //        }
    //    }  
    //}
    /*void NewJump() {
        if (isVritualControl.isOn)
        {
            if (isGround)
            {
                extraJump = 1;

            }
            if (Joystick.Vertical > 0.5f && extraJump > 0)
            {
                rb.velocity = Vector2.up * jumpforce;
                extraJump--;
                AudioManager.Instance.JumpAudio();
                jumpAudio.Play();
                //jumpAudio.PlayOneShot(clip);
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
            }
            if (Joystick.Vertical > 0.5f && extraJump == 0 && isGround)
            {
                rb.velocity = Vector2.up * jumpforce;
                AudioManager.Instance.JumpAudio();
                //jumpAudio.Play();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
            }
        }
        else
        {
            if (isGround)
            {
                extraJump = 1;

            }
            if (Input.GetButtonDown("Jump") && extraJump > 0)
            {
                rb.velocity = Vector2.up * jumpforce;
                extraJump--;
                jumpAudio.Play();
                //jumpAudio.PlayOneShot(clip);
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
            }
            if (Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
            {
                rb.velocity = Vector2.up * jumpforce;
                jumpAudio.Play();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
            }
        }
            
    }*/
    void NewnewJump() {
        if (isVritualControl.isOn)
        {
            if (isGround)
            {
                JumpCount = 2;
                isJump = false;
            }
            if (Joystick.Vertical > 0.5f && isGround)
            {
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                JumpCount--;
                SoundManager.instance.JumpAudio();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
             
            }
            else if (Joystick.Vertical > 0.5f && JumpCount > 0 && isJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                JumpCount--;
                SoundManager.instance.JumpAudio();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
             
            }
        }
        else
        {
            if (isGround)
            {
                JumpCount = 2;
                isJump = false;
            }
            if (jumpPressed && isGround)
            {
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                JumpCount--;
                SoundManager.instance.JumpAudio();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
                jumpPressed = false;
            }
            else if (jumpPressed && JumpCount > 0 && isJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                JumpCount--;
                SoundManager.instance.JumpAudio();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
                jumpPressed = false;
            }
        }

            
    
    }
    void Crouch()
        {
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
        {
            if (isVritualControl.isOn)
            {
                if (Joystick.Vertical < -0.5f)
                {
                    crouchcoll.isTrigger = true;
                    anim.SetBool("crouch", true);
                }
                else
                {
                    anim.SetBool("crouch", false);
                    crouchcoll.isTrigger = false;
                    //anim.SetBool("idle", true);
                }
            }
            else
            {
                if (Input.GetButton("Crouch"))
                {
                    crouchcoll.isTrigger = true;
                    anim.SetBool("crouch", true);
                }
                else
                {
                    anim.SetBool("crouch", false);
                    crouchcoll.isTrigger = false;
                    //anim.SetBool("idle", true);
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Collection"&&collision.name=="Cherry")
        {

            collision.GetComponent<Animator>().Play("Get");
            SoundManager.instance.CollectionAudio();
            
        }
        else if (collision.tag == "Collection" && collision.name == "Damon")
        {
            Destroy(collision.gameObject);
            SoundManager.instance.CollectionAudio();
            //Damon++;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (anim.GetBool("falling"))
        {
            Enemies enemies  = collision.gameObject.GetComponent<Enemies>();
            if (collision.gameObject.tag == "Enemies")
            {
                enemies.JumpOn();

                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Enemies")
            {          
                //反向力
                rb.velocity = new Vector2((transform.position.x-collision.gameObject.transform.position.x) * Speed * Time.fixedDeltaTime, rb.velocity.y);
                anim.SetBool("hurt", true);
                SoundManager.instance.HurtAudio();
                isHurt = true;

            }
        }
    }
    void ReadyToDash() {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        CDImage.fillAmount = 1;
    }
    void Dash() {
        if (isDashing)
        {
            if (dashTimeLeft>0)
            {
                if (rb.velocity.y>0&&!isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalmove, jumpforce);
                    SoundManager.instance.DashAudio();
                }
                rb.velocity =new Vector2(dashSpeed * horizontalmove, rb.velocity.y);
                SoundManager.instance.DashAudio();
                dashTimeLeft -= Time.deltaTime;
                GameObjectPool.Instance.GetFormPool();
            }
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                if (!isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalmove, jumpforce);
                    SoundManager.instance.DashAudio();
                }
            }
        }
    }

    public void DashBtnOnClick() {
        if (Joystick.Vertical > 0.5f&& JumpCount > 0)
        {
            jumpPressed = true;
        }
        //冲锋
        if (Time.time >= lastDash + dashCoolDown)
        {
            ReadyToDash();
        }     
        CDImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
    }
    void PresseDash() {
        
            if (Input.GetButtonDown("Jump") && JumpCount > 0)
            {
                jumpPressed = true;
            }

            //冲锋
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (Time.time >= lastDash + dashCoolDown)
                {
                    ReadyToDash();
                }
            }
            CDImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
        }
            
    
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "BG")
        {
            anim.SetTrigger("relife");
            isRelife = true;
            this.transform.position = new Vector2(zeroPoint.position.x,zeroPoint.position.y);
           Invoke("relifeTimer", 2.0f);
            
        
        }
    }
    private void relifeTimer() {
        isRelife = false;    
        anim.SetTrigger("norelife");
    }
    public void GetItem() {
        Cherry++;
        CherryCount.text = Cherry.ToString();
    }
    }
