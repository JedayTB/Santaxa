using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    //Dash stuff
    [SerializeField]
    private float dashChangeForce = 150f;
    private Rigidbody2D rb;
    [SerializeField]
    private float dashCount = 3f;
    [SerializeField]
    private float dashCountDown;
    private float dashDuration;

    public enum directionFacing
    {
        left = 0,
        right = 1,
        up = 2,
        down = 3
    }
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Change to get input innhere
        //and put actual movement in fixed update
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        dashCountDown += Time.deltaTime;
        dashDuration -= Time.deltaTime;

        if (dashDuration >= 0 && dashDuration < 1.5f) // I made it so there is now a hard dash duration, when the dash is over, velocity is reset
        {
            rb.velocity = Vector3.zero;
            dashDuration = 0;
            speed = 10f;
        }

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);

        if(Input.GetKeyDown(KeyCode.Space) && dashCountDown > dashCount){
            print("Should dash");
            doDash(dashChangeForce , moveDirection.normalized);
        }

        moveDirection *= speed * Time.deltaTime;
        transform.Translate(moveDirection);
    }
    void doDash(float force, Vector2 dir){
        speed = 25f;
        rb.AddForce(dir * force); // Got rid of the AddForce2D.Impuse as it allowed you do simply teleport through collision lol
        dashCountDown = 0;
        dashDuration = 2f;
    }
    private void flip(int dir)
    {
        
        Vector3 currentScale = transform.localScale; //Getting the current scale transform
       
        switch (dir){
            case (int)directionFacing.left:

                break;
            case (int)directionFacing.right:
                
                break;
            case (int)directionFacing.up:
                
                break;
            case (int)directionFacing.down:
                
                break;
            default:
                Debug.Log("Not a direction");
                break;
        }
    }
}
