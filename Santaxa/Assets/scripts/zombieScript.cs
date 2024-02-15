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

        transform.Translate( moveDir * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PLAYER"){
            hpEventController playerHPClassRef = other.gameObject.GetComponent<hpEventController>();
            playerHPClassRef.onHit(this.damage);
        }
    }
}
