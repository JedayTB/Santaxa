using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class zombieDash : MonoBehaviour
{
    Rigidbody2D rb;
    zombieScript zombie;
    private Vector2 direction;

    private float dashDuration;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        zombie = GetComponent<zombieScript>();
    }

    // Update is called once per frame
    void Update()
    {
        dashDuration -= Time.deltaTime;
        if (dashDuration >= 0 && dashDuration < 1.2f)
        {
            rb.velocity = Vector3.zero;
            dashDuration = 0;
            //speed = 10f;
        }
    }

    private void doDash() // the enemy dash is very similar to the player dash
    {
        direction = zombie.moveDir;
        //speed = 25f;
        rb.AddForce(direction * 500f);
        dashDuration = 2f;
    }

    // Instead of doing math (yuck!) there's just an empty object with a collider around the player
    // The collider represents how close the enemy must be to the player to dash
    // If the enemy touches it, they can dash!
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dashRadius")) 
        {
            doDash();
        }
    }
}
