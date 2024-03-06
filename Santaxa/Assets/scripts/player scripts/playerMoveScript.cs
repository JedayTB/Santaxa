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
    private const float dashCoolDown = 3f;
    [SerializeField]
    private float dashCountDown;
    public float dashDuration;
    private Animator anim;
    private SpriteRenderer ownSprite;
    
    private Vector2 moveDirection;
    [SerializeField]
    private bool determineIfMove;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dashCountDown = dashCoolDown; //to dash right away
    }
    void Update()
    {
        //Change to get input innhere
        //and put actual movement in fixed update
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        dashCountDown += Time.deltaTime;
        dashDuration -= Time.deltaTime;

        if (dashDuration >= 0 && dashDuration < 1.5f)
        {
            // I made it so there is now a hard dash duration,
            // when the dash is over, velocity is reset
            rb.velocity = Vector3.zero;
            dashDuration = 0;
            speed = 10f;
        }

        moveDirection = new Vector2(horizontalInput, verticalInput);

        if (Input.GetKeyDown(KeyCode.Space) && dashCountDown >= dashCoolDown)
        {
            doDash(dashChangeForce, moveDirection.normalized);
            gameStateManager.Instance.playerOnDash(dashCoolDown);
        }
        flipSprite();
        moveDirection *= speed * Time.deltaTime;
        determineIfMove = moveDirection.x != 0 || moveDirection.y != 0;
        anim.SetBool("isWalking", determineIfMove);
        transform.Translate(moveDirection);
    }
    
    void doDash(float force, Vector2 dir){
        speed = 25f;
        rb.AddForce(dir * force); // Got rid of the AddForce2D.Impuse as it allowed you do simply teleport through collision lol
        dashCountDown = 0;
        dashDuration = 2f;
    }
    void flipSprite()
    {
        if (horizontalInput > 0) // right
        {
            ownSprite.flipX = false;
        }
        else if (horizontalInput < 0) // left
        {
            ownSprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dashEnemy"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
