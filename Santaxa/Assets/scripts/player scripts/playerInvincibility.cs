using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this does NOT include the dash invincibility
public class playerInvincibility : MonoBehaviour
{
    public bool isInvincible = false;
    private float invincibleTime = 0;
    private float flickerTimer = 0.15f;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        invincibleTime -= Time.deltaTime;
        flickerTimer -= Time.deltaTime;
        if (invincibleTime <= 0)
        {
            disableInvincible();
        }

        if (flickerTimer < 0 && isInvincible) 
        {
            sr.enabled = !sr.enabled;
            flickerTimer = 0.15f;
        }
    }

    public void enableInvincible()
    {
        isInvincible = true;
        invincibleTime = 1.5f;
    }

    public void disableInvincible() 
    {
        isInvincible = false;
    } 
}
