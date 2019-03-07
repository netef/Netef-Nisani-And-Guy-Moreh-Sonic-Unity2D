using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraAdjust : MonoBehaviour
{

    int max = 30;
    int min = 14;

    // Use this for initialization
    void Start()
    {
        GetComponent<Camera>().orthographicSize = min;
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wider")
                StartCoroutine(DelayUp());         
     
        if (col.gameObject.tag == "Thinner")        
                StartCoroutine(DelayDown());
    }

    IEnumerator DelayUp()
    {
        for (int i = min+1; i <= max; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.position = new Vector3(transform.position.x, transform.position.y+0.9f, transform.position.z);
            GetComponent<Camera>().orthographicSize = i;
        }
    }

    IEnumerator DelayDown()
    {
        for (int i = max-1; i >= min; i--)
        {
            yield return new WaitForSeconds(0.02f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.9f, transform.position.z);
            GetComponent<Camera>().orthographicSize = i;
        }
    }



}
