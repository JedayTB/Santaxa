using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class zombieScript : hittableObject
{
   private GameObject playerReference;
    [SerializeField] private float speed = 5f;
    private Vector2 playerPos;
    private Vector2 moveDir;
    [SerializeField]
    private int damage = 1;

    public bool isBeingKnocked = false;
    private float knockTimer = 0.15f; // how long the knock lasts
    
    public int chanceToSpawn;
    void Start()
    {
        playerReference = GameObject.FindWithTag("PLAYER"); //Not optimal
        if(playerReference == null)
        {
            Debug.LogError($"{this.gameObject.name} has null player!");
        }
        
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
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PLAYER"){
            hpEventController playerHPClassRef = other.gameObject.GetComponent<hpEventController>();
            playerMoveScript moveRef = other.gameObject.GetComponent<playerMoveScript>();
            playerInvincibility playerRef = other.gameObject.GetComponent<playerInvincibility>();
            if (playerRef.isInvincible == true || moveRef.dashDuration >= 0)
            {

            }
            else if (playerRef.isInvincible == false)
            {
                playerHPClassRef.onHit(this.damage);
                playerRef.enableInvincible();
            }
        }
    }
}
