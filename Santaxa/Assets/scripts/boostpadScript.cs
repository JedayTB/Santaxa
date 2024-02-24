using System.Collections;
using UnityEngine;

public class boostpadScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;
    [SerializeField]
    private float boostForce = 5f;
    [SerializeField]
    private float boostDuration = 0.5f;
    [SerializeField]
    private Animator playerAnimator;
    void Start()
    {
        //Debug.LogWarning($"Uncomment below lines inside boostpad script. -Arrazola");
        
        playerRB = gameStateManager.Instance.playerRB2D;
        if(playerRB == null){
            Debug.LogError($"{this.gameObject.name} player rb2d null!");
        }
        playerAnimator = gameStateManager.Instance.playerAnimator;
        if( playerAnimator == null) {
            print("bhu");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PLAYER")){
            boostPadLogic();
        }
    }
    void boostPadLogic(){
        StartCoroutine(boostForDuration(boostDuration));
        playerAnimator.SetBool("isJumping", true);
        //boostInDirection(this.transform.up, boostForce, playerRB);
    }
    IEnumerator boostForDuration(float duration){
        while(duration >= 0){
            duration -= Time.deltaTime;
            boostInDirection(this.transform.up,boostForce, playerRB);
            yield return null;
        }
        playerRB.velocity = Vector3.zero;
        playerAnimator.SetBool("isJumping", false);
        StopCoroutine(boostForDuration(duration));
    }
    void boostInDirection(Vector3 dir, float force,  Rigidbody2D target){
        //target.AddForce(dir * force);
        target.velocity = dir * force;
    }
}
