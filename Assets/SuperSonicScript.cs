using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperSonicScript : MonoBehaviour
{

    public AudioSource jumpSound;
    public AudioClip killSound;
    public AudioClip RingSound;


    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    public int Rings = 0;
    public int Score = 0;

    public float groundCheckRadius;
    public float wallCheckRadius;



    private float velocityX = 15f;
    private float jump = 35f;
    private float jumpTime, jumpDelay = .5f;

    private bool onGround;
    private bool touchWall;
    private bool jumped;
    private bool sprinted;

    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // jumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        touchWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (Input.GetMouseButtonDown(0) && onGround)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(velocityX, jump);
            jumpTime = jumpDelay;
            anim.SetTrigger("Jump");
            if (sprinted)
                anim.SetTrigger("Sprint");
            jumped = true;


        }

        if (touchWall)
        {
            velocityX = -velocityX;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        jumpTime -= Time.deltaTime;
        if (jumpTime <= 0 && onGround && jumped)
        {
            anim.SetTrigger("Land");
            jumped = false;
        }


        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }






    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Accel01")
        {
            velocityX = velocityX + 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            anim.SetTrigger("Sprint");
            sprinted = true;
        }

        if (col.gameObject.tag == "Accel02")
        {
            velocityX = velocityX - 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            anim.SetTrigger("Sprint");
            sprinted = true;
        }

        if (col.gameObject.tag == "Deccel01")
        {
            velocityX = velocityX - 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            anim.SetTrigger("Run");
            sprinted = false;
        }

        if (col.gameObject.tag == "Deccel02")
        {
            velocityX = velocityX + 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            anim.SetTrigger("Run");
            sprinted = false;
        }

        if (col.gameObject.tag == "Enemy" && jumped)
        {
            Destroy(col.gameObject);
            AudioSource.PlayClipAtPoint(killSound, transform.position, 10);
            rb.velocity = new Vector2(velocityX, rb.velocity.y + 5f);
            Score += 100;
        }

        if (col.gameObject.tag == "Ring")
        {
            AudioSource.PlayClipAtPoint(RingSound, transform.position, 100);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 30, 100, 25), "Score: " + Score);
        GUI.Box(new Rect(0, 0, 100, 25), "Rings: " + Rings);
    }



}