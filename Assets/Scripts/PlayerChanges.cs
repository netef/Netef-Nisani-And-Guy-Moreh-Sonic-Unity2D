using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerChanges : MonoBehaviour
{

    public Camera playerCamera;
    public Camera signCamera;
    public GameObject sonic, superSonic;

    public Rigidbody2D rb;

    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    public static int Score = 0;
    public float velocityX = 15f;
    public float currentSpeed;


    public float startingTime;
    public float groundCheckRadius;
    public float wallCheckRadius;
    private float jump = 35f;
    private float jumpTime, jumpDelay = .5f;
    public static float Rings = 0;
    

    private bool onGround;
    private bool touchWall;
    private bool jumped;
    private bool sprinted;
    private bool flag;

    int min = 15;
    int max = 35;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        touchWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (Rings > 1 && flag)
        {
            Rings -= Time.deltaTime * 2f;
           // Mathf.Round((int)Rings);
        }

        if (((int)Rings == 50 && !flag) || ((int)Rings == 0 && flag))
        {
            Transfromation();
            flag = !flag;
        }

        if (Input.GetMouseButtonDown(0) && onGround)
        {

            rb.velocity = new Vector2(velocityX, jump);
            jumpTime = jumpDelay;

            if (!flag)
            {
                 MusicProps.PlaySound("jumpSound");
                sonic.GetComponent<Animator>().SetTrigger("Jump");
            }

            if (sprinted && !flag)
                sonic.GetComponent<Animator>().SetTrigger("Sprint");
            jumped = true;
        }

        if (touchWall)
        {
            velocityX = -velocityX;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        }

        jumpTime -= Time.deltaTime;

        if (jumpTime <= 0 && onGround && jumped)
        {
            if (!flag)
                sonic.GetComponent<Animator>().SetTrigger("Land");
            jumped = false;
        }

        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "FinishSign")
        {
            playerCamera.enabled = false;
            signCamera.enabled = true;
        }

        if (col.gameObject.tag == "Accel01")
        {
            velocityX = velocityX + 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            if(!flag)
                sonic.GetComponent<Animator>().SetTrigger("Sprint");
            sprinted = true;
        }

        if (col.gameObject.tag == "Accel02")
        {
            velocityX = velocityX - 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            if(!flag)
                sonic.GetComponent<Animator>().SetTrigger("Sprint");
            sprinted = true;
        }

        if (col.gameObject.tag == "Deccel01")
        {
            velocityX = velocityX - 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            if (!flag)
                sonic.GetComponent<Animator>().SetTrigger("Run");
            sprinted = false;
        }

        if (col.gameObject.tag == "Deccel02")
        {
            velocityX = velocityX + 10f;
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            if(!flag)
                sonic.GetComponent<Animator>().SetTrigger("Run");
            sprinted = false;
        }

        if (col.gameObject.tag == "Enemy" && (jumped || flag))
        {
            Destroy(col.gameObject, 0.5f);
            MusicProps.PlaySound("enemyDeathSound");
            col.gameObject.GetComponent<Animator>().SetTrigger("Death");
            rb.velocity = new Vector3(velocityX, rb.velocity.y + 5f, 0);
            Score += 100;
        }

        if (col.gameObject.tag == "Ring")
            MusicProps.PlaySound("ringSound");

        if (col.gameObject.tag == "Wider")
            StartCoroutine(DelayUp());

        if (col.gameObject.tag == "Thinner")
            StartCoroutine(DelayDown());

        if (col.gameObject.tag == "Boss")
        {
           MusicProps.PlaySound("enemyDeathSound");
            rb.velocity = new Vector3(velocityX, rb.velocity.y + 5f ,0);
            TheBossScript.Damage();
            HealthBarScript.health -= 20f;
        }

    }

    void Transfromation()
    {
        if (!flag)
        {
            
            MusicProps.myMusic.Stop();
            MusicProps.PlaySound("superSonicYell");
            superSonic.gameObject.SetActive(true);
            sonic.gameObject.SetActive(false);
            currentSpeed = velocityX;
            velocityX = 0;        
            StartCoroutine(DelayTransformation());
        }

        else
        {
            MusicProps.myMusic.Stop();
            MusicProps.PlaySound("theme");
            superSonic.gameObject.GetComponent<Animator>().SetTrigger("Revert");
            velocityX = 0;
            StartCoroutine(DelayTransformation());
        }
    }

    IEnumerator DelayTransformation()
    {
        if (!flag)
        {
            yield return new WaitForSeconds(1.8f);
            velocityX = currentSpeed * 1.6f;   
        }

        else
        {
            yield return new WaitForSeconds(1.2f);
            sonic.gameObject.SetActive(true);
            superSonic.gameObject.SetActive(false);
            sonic.GetComponent<Animator>().SetTrigger("Sprint");
            velocityX = currentSpeed;
        }
    }

    IEnumerator DelayUp()
    {   
            for (int i = min + 1; i <= max; i++)
            {
                yield return new WaitForSeconds(0.02f);
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y + 0.9f, playerCamera.transform.position.z);
                playerCamera.GetComponent<Camera>().orthographicSize = i;                
            }
    }

    IEnumerator DelayDown()
    {     
            for (int i = max - 1; i >= min; i--)
            {
                yield return new WaitForSeconds(0.02f);
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.9f, playerCamera.transform.position.z);
                playerCamera.GetComponent<Camera>().orthographicSize = i;                
            }
    }
}