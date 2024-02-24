using System.Collections;
using UnityEngine;


// this does NOT include the dash invincibility
public class playerInvincibility : MonoBehaviour
{
    public bool isInvincible = false;
    private float invincibleTime = 1.5f;

    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void enableInvincible()
    {
        print("being run");
        isInvincible = true;
        StartCoroutine(flickerPlayer(invincibleTime));
    }
    IEnumerator flickerPlayer(float duration){
        while(duration >= 0){
            duration -= Time.deltaTime;
            //print(duration);
            print(duration % 0.25f);
            if(duration % 0.25f <= 0.075){
                print("flip");
                sr.enabled = !sr.enabled;
            }
            yield return null;
        }
        sr.enabled = true;
        isInvincible = false;
        StopCoroutine(flickerPlayer(duration));
    }
}
