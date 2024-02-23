using System.Collections;
using UnityEngine;

public class boostpadScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;
    [SerializeField]
    private float boostForce = 25;
    [SerializeField]
    private float boostDuration = 1f;
    
    void Start()
    {
        playerRB = gameStateManager.Instance.playerRB2D;
        if(playerRB == null){
            Debug.LogError($"{this.gameObject.tag} player rb2d null!");
        }else{
            Debug.LogWarning("Not configured to get player reference from GSM find a Fix!");
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
    }
    IEnumerator boostForDuration(float duration){
        while(duration >= 0){
            duration -= Time.deltaTime;
            boostInDirection(this.transform.up,boostForce, playerRB);
            yield return null;
        }
        playerRB.velocity = Vector2.zero;
        StopCoroutine(boostForDuration(duration));
    }
    void boostInDirection(Vector3 dir, float force,  Rigidbody2D target){
        target.AddForce(dir * force);
    }
}
