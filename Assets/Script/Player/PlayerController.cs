using System.Collections;
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
    public Image BtnCDImage;
    public Image IconCDImage;


    public Button dashBtn;

    private int JumpClock;
   



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
            if (!isHurt) { 

               
                isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, ground);
               
                Dash();
                if (isDashing) return;
               
                Movement();
                NewnewJump();
            }
            SwitchAnim();
            

            //Debug.Log(rb.velocity.y + "XXXX" + Time.deltaTime);
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
            float horizontalvmove = Joystick.Horizontal;
            anim.SetFloat("running", Mathf.Abs(horizontalvmove));
            if (horizontalvmove != 0)
            {
                rb.velocity = new Vector2(horizontalvmove * Speed, rb.velocity.y);
                transform.localScale = new Vector3((horizontalvmove>=0?1:-1) ,1, 1);

            }         
        }
        else
        {
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
        //if (isVritualControl.isOn)
        //{
        //    if (isHurt)
        //    {
        //        anim.SetBool("hurt", true);
        //        anim.SetFloat("running", 0);
        //        if (Mathf.Abs(rb.velocity.x) < 0.1f)
        //        {
        //            isHurt = false;
        //            anim.SetBool("hurt", false);
        //        }
        //    }
        //    //anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
        //    if (isGround)
        //    {
                
        //        anim.SetBool("falling", false);
        //         if (rb.velocity.y < 0) {
                    
        //            anim.SetBool("falling", false); }
        //    }
        //    //else if (!isGround && rb.velocity.y > 0)
        //    //{
        //    //    anim.SetBool("jumping", true);
        //    //}
        //    else
        //    {
        //        if (rb.velocity.y < 0) {
                  
        //            anim.SetBool("falling", true);
        //        anim.SetBool("jumping", false);
        //    }
        //        else if ( rb.velocity.y > 0)
        //        {
        //            anim.SetBool("jumping", true);
        //        }
        //    }
          
        //}
       // else
       //{
            if(isHurt)
            {
                anim.SetBool("hurt", true);
                anim.SetFloat("running", 0);
                if (Mathf.Abs(rb.velocity.x) < 0.1f)
                {
                    isHurt = false;
                    anim.SetBool("hurt", false);
                }
            }
           // anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
            if (isGround)
            {
                anim.SetBool("falling", false);
                if (rb.velocity.y < 0)
                {
                    anim.SetBool("falling", false);
                }

            }
            else
            {
                if (rb.velocity.y < 0)
                {
                    
                anim.SetFloat("running", 0);
                anim.SetBool("falling", true);
               anim.SetBool("jumping", false);
                }
                else if (rb.velocity.y > 0)
                {
                    anim.SetBool("jumping", true);
                }
           // }        
        
       }

    }
    void NewnewJump() {
        if (isVritualControl.isOn)
        {
            if (isGround)
            {
                JumpCount = 2;
                isJump = false;
               
                if (Joystick.Vertical > 0.5f|| JumpClock > 0)
                {
                    isJump = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                    JumpCount--;
                    SoundManager.instance.JumpAudio();
                    anim.SetBool("jumping", true);
                    anim.SetBool("crouch", false);
                    anim.SetBool("falling", false);
                    JumpClock--;

                }

            }        
           else if (Joystick.Vertical > 0.5f&& JumpCount > 0 && isJump && JumpClock > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                JumpCount--;
                SoundManager.instance.JumpAudio();
                anim.SetBool("jumping", true);
                anim.SetBool("crouch", false);
                anim.SetBool("falling", false);
                JumpClock = 0;
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
        if (collision.tag=="Collection")
        {

            collision.GetComponent<Animator>().Play("Get");
            SoundManager.instance.CollectionAudio();
            JumpCount++;


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

                rb.velocity = new Vector2(rb.velocity.x, jumpforce );
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Enemies")
            {
                //反向力   
                rb.velocity = new Vector2((transform.position.x-collision.gameObject.transform.position.x) * Speed*1.2f, rb.velocity.y);
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
        BtnCDImage.fillAmount = 1;
       IconCDImage.fillAmount = 1;
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
    public void JumpBtnOnClick() {
      
            JumpClock++;
                     
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
        BtnCDImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
        IconCDImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
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
            BtnCDImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
            IconCDImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;


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
    public void GetItemCount() {
        Cherry++;
        CherryCount.text = Cherry.ToString();
    }
    }
