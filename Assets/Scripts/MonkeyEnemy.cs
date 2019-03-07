using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyEnemy : MonoBehaviour {

    Rigidbody2D rb;
    public float velocityY = -3f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "ClimbTR")
        {
            velocityY = -velocityY;
            rb.velocity = new Vector2(rb.velocity.x, velocityY);

        }
    }
}
