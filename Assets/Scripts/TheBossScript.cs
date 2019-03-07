using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBossScript : MonoBehaviour {

    public Rigidbody2D rb;
    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject rightWall;

    public GameObject rightPlatforms;
    public GameObject leftPlatforms;

    private float counterUntilUpdate;
    private float counterUntilPlatform;

    private bool right = true;
    private bool left = true;

    public Animator anim;


    private float velocityX = 0;

    public static float health;





    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        health = 100f;
        TurnRight();
    }

    // Update is called once per frame
    void Update () {

        counterUntilUpdate -= Time.deltaTime;

        if (counterUntilUpdate <= 0)
        {
            anim.SetTrigger("Run");
            rb.velocity = new Vector3(velocityX, rb.velocity.y, rb.velocity.y);
        }

        counterUntilPlatform -= Time.deltaTime;

        if (counterUntilPlatform<=0)
        {
            if (right)
            {
                rightPlatforms.SetActive(false);
                right = false;
            }
            if (left)
            {
                leftPlatforms.SetActive(false);
                left = false;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject , 2f);
            Destroy(rightWall);
        }
            
        

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "BossRight")
        {
            rightPoint.SetActive(false);
            rightPlatforms.SetActive(true);
            leftPoint.SetActive(true);
            counterUntilPlatform = 3.2f;
            right = true;
            TurnLeft();
        }

        if (col.tag == "BossLeft")
        {
            leftPoint.SetActive(false);
            leftPlatforms.SetActive(true);
            rightPoint.SetActive(true);
            counterUntilPlatform = 3.2f;
            left = true;
            TurnRight();
        }
    }

    void TurnLeft()
    {
        transform.localScale = new Vector3(21.75031f, transform.localScale.y, transform.localScale.z);
        counterUntilUpdate = 3.2f;
        rb.velocity = new Vector3(0, 0, 0);
        velocityX = -36f;
    }

    void TurnRight()
    {
        transform.localScale = new Vector3(-21.75031f, transform.localScale.y, transform.localScale.z);
        counterUntilUpdate = 3.2f;
        rb.velocity = new Vector3(0, 0, 0);
        velocityX = 36f;
    }

    public static void Damage()
    {
        health -= 20f;
        Debug.Log(health);
    }
}
