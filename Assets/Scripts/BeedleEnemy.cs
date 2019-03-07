using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeedleEnemy : MonoBehaviour
{
    public Rigidbody2D myBody;

    
    public float velocityX = -5f;
    public float velocityY = -2f;

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = new Vector2(velocityX, velocityY);
        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(3f);
        velocityX *= -1;
        velocityY *= -1;
    }

}
