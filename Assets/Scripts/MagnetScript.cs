using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour {

    public float PullRadius; // Radius to pull
    public float GravitationalPull; // Pull force
    public float MinRadius; // Minimum distance to pull from
    public float DistanceMultiplier; // Factor by which the distance affects force
    public Rigidbody2D rb;

    public LayerMask LayersToPull;

    // Function that runs on every physics frame
    void FixedUpdate()
    {
        Collider2D colliders = Physics2D.OverlapCircle(transform.position, PullRadius, LayersToPull);

        if(colliders.GetComponent<Rigidbody2D>() != null)
        {

        
             rb = colliders.GetComponent<Rigidbody2D>();

            //if (rb == null) break; // Can only pull objects with Rigidbody

            Vector2 direction = transform.position - colliders.transform.position;

            //if (direction.magnitude < MinRadius) continue;

            float distance = direction.sqrMagnitude * DistanceMultiplier + 1; // The distance formula

            // Object mass also affects the gravitational pull
            rb.AddForce(direction.normalized * (GravitationalPull / distance) * rb.mass * Time.fixedDeltaTime);
        }

    }

}
