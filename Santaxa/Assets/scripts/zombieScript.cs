using UnityEditor;
using UnityEngine;

public class zombieScript : hittableObject
{
    public static int numOfEnemies = 0;
    private GameObject playerReference;
    [SerializeField] private float speed = 5f;
    private Vector2 playerPos;
    public Vector2 moveDir;
    [SerializeField]
    private int damage = 1;

    public bool isBeingKnocked = false;
    private float knockTimer = 0.15f; // how long the knock lasts

    private float wallCheckTimer = 0.1f;
    public bool isTurned = false;
    public LayerMask coverLayer;

    public int chanceToSpawn;

    private playerMoveScript moveRef;
    private playerInvincibility plInvince;

    // trust me we are cooking
    private Vector2[] rotations = new Vector2[4] { Vector2.right, Vector2.down, Vector2.left, Vector2.up };
    private int direction = 0;

    private Animator enemyAnimator;
    [Tooltip("This is the identifier of the enemy")]
    [SerializeField]
    private int enemyNumber = 0;
    void Start()
    {
        enemyNumber = numOfEnemies;
        numOfEnemies++;

        playerReference = gameStateManager.Instance.playerReference;
        if (playerReference == null)
        {
            Debug.LogError($"{this.gameObject.name} has null player!");
        }
        /*
            hpEventController playerHPClassRef = other.gameObject.GetComponent<hpEventController>();
            playerMoveScript moveRef = other.gameObject.GetComponent<playerMoveScript>();
            playerInvincibility playerRef = other.gameObject.GetComponent<playerInvincibility>();
        */
        enemyAnimator = gameObject.GetComponent<Animator>();
        if (enemyAnimator == null)
        {
            Debug.LogError($"{this.gameObject.name} Null Animator! or doesn't have one.");
        }

        moveRef = gameStateManager.Instance.plMoveScript;
        plInvince = gameStateManager.Instance.plInvincibility;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerReference.transform.position;
        moveDir = playerReference.transform.position - this.transform.position;
        moveDir.Normalize();

        if (isTurned == true)
        {
            transform.Translate(rotations[direction] * speed * Time.deltaTime);
        }
        else
        {
            if (isBeingKnocked)
            {
                transform.Translate(-moveDir * speed * 4 * Time.deltaTime);
                knockTimer -= Time.deltaTime;

                if (knockTimer <= 0f)
                {
                    knockTimer = 0.15f;
                    isBeingKnocked = false;
                }

            }
            else
            {
                transform.Translate(moveDir * speed * Time.deltaTime);
            }
        }

        wallCheckTimer -= Time.deltaTime;
        if (wallCheckTimer <= 0) // if we wanna make it look cleaner we COULD do it every frame
        {
            CheckForWall();
            wallCheckTimer = 0.2f;
        }
        bool movingCheck = moveDir.x > 0.1f && moveDir.y > 0.1f;
        enemyAnimator.SetBool("isMoving", movingCheck);
    }

    /// <summary>
    /// Wall Detection V2!!
    /// Shoots a raycast in 4 directions (up, right, down, left. In that order)!
    /// There are 2 arrays, one with all the rays (raycasts), and one with all the directions (rotations)
    /// The number positions in rotations[] represent the same direction as the raycasts, but offset 90 degress to the right
    /// EX. raycast[0] casts a ray up, rotations[0] is Vector2.right
    /// With this, we can use the info from the collisions to determine where we want to move to not be colliding with the wall
    /// For simplicity, the enemy always tries to move 90 degrees to the right (cuz I have no idea how to determine the shorter path LOL)
    /// When a collision happens, we take the raycast that is colliding (raycasts[x]), and set x to a variable called direction, which will change the direction in Update
    /// EX. If raycasts[1] is colliding, that's on the right, so we set the direction to 1. direction then gets used to determine the rotation, by making it rotations[direction], which is down
    /// sorry if this doesn't really make sense i am sick lmao but this works better (they still do get stuck but like I just don't know enough to make it perfect lol)
    /// </summary>
    private void CheckForWall()
    {
        RaycastHit2D[] raycasts = new RaycastHit2D[4] { Physics2D.Raycast(transform.position, Vector2.up, 1.3f, coverLayer),
                                                        Physics2D.Raycast(transform.position, Vector2.right, 1.3f, coverLayer),
                                                        Physics2D.Raycast(transform.position, Vector2.down, 1.3f, coverLayer),
                                                        Physics2D.Raycast(transform.position, Vector2.left, 1.3f, coverLayer)};

        Debug.DrawRay(transform.position, Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.right, Color.red);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
        Debug.DrawRay(transform.position, Vector2.left, Color.red);

        for (int x = 0; x < 4; x++)
        {
            if (raycasts[x].collider != null)
            {
                isTurned = true;
                direction = x;
                break;
            }
        }

        if (direction < 3) // Another check to help them not get stuck on corners
        {
            if (raycasts[direction + 1].collider != null)
            {
                direction++;
            }
        }

        if (raycasts[0].collider == null && raycasts[1].collider == null && raycasts[2].collider == null && raycasts[3].collider == null) // shit if but idc lol!!!!
        {
            isTurned = false;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PLAYER")
        {
            if (plInvince.isInvincible == false && moveRef.dashDuration <= 0)
            {
                gameStateManager.Instance.playerHpEvent.onHit(this.damage);
                //plInvince.enableInvincible();
            }
        }
    }
    protected override void OnDestroy()
    {
        gameStateManager.waveCont.enemyArray.Remove(this);
        base.OnDestroy();
    }
}
