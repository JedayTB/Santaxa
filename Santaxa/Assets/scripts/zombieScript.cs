using UnityEngine;

public class zombieScript : hittableObject
{
   private GameObject playerReference;
    [SerializeField] private float speed = 5f;
    private Vector2 playerPos;
    public Vector2 moveDir;
    [SerializeField]
    private int damage = 1;

    public bool isBeingKnocked = false;
    private float knockTimer = 0.15f; // how long the knock lasts

    private float wallCheckTimer = 0.1f;
    private bool isTurned = false;
    public LayerMask coverLayer;
    
    public int chanceToSpawn;
  
    private playerMoveScript moveRef;
    private playerInvincibility plInvince;

    private Animator enemyAnimator;
    void Start()
    {
        playerReference = gameStateManager.Instance.playerReference;
        if(playerReference == null)
        {
            Debug.LogError($"{this.gameObject.name} has null player!");
        }
        /*
            hpEventController playerHPClassRef = other.gameObject.GetComponent<hpEventController>();
            playerMoveScript moveRef = other.gameObject.GetComponent<playerMoveScript>();
            playerInvincibility playerRef = other.gameObject.GetComponent<playerInvincibility>();
        */
        enemyAnimator = gameObject.GetComponent<Animator>();
        if(enemyAnimator == null){
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

        wallCheckTimer -= Time.deltaTime;
        if (wallCheckTimer <= 0) // if we wanna make it look cleaner we COULD do it every frame
        {
            CheckForWall();
            wallCheckTimer = 0.1f;
        }
        bool movingCheck = moveDir.x > 0.1f && moveDir.y > 0.1f;
        enemyAnimator.SetBool("isMoving", movingCheck);
    }

    // Does a raycast in the direction of the player
    // If it hits something, it rotates the enemy by 90 degrees until that raycast is no longer hitting something
    // This mostly works, but can cause issues when the player is in a weird spot or when the enemy is in a corner
    private void CheckForWall()
    {
        RaycastHit2D hitInfoPlayer = Physics2D.Raycast(transform.position, moveDir, 1f, coverLayer);
        //RaycastHit2D hitInfoForward = Physics2D.Raycast(transform.position, Vector2.right, 1f, coverLayer);
        Debug.DrawRay(transform.position, moveDir, Color.red);
        //Debug.DrawRay(transform.position, Vector2.right, Color.red);
        if (hitInfoPlayer.collider != null && isTurned == false)
        {
            isTurned = true;
            Quaternion tempDirection = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f);

            transform.rotation = tempDirection;
        }
        else if (hitInfoPlayer.collider == null && isTurned)
        {
            Quaternion tempDirection = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            transform.rotation = tempDirection;
            isTurned = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PLAYER"){
            if (plInvince.isInvincible == false && moveRef.dashDuration <= 0)
            {
                gameStateManager.Instance.playerHpEvent.onHit(this.damage);
                //plInvince.enableInvincible();
            }
        }
    }
}
