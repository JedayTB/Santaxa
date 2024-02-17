using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hpEventController : hittableObject
{
    //hey hey!
    private gameStateManager GSM;
    void Start()
    {
        GSM = gameStateManager.Instance;
    }
    public override void onHit(int hitDamage)
    {
        base.onHit(hitDamage);
        //print(healthPoints);
        GSM.playerOnHit(this.healthPoints);
    }
    public override void subtractHealth(int healthDelta)
    {
        this.healthPoints -= healthDelta;
        if(healthPoints <= 0){
            //end game screen here
            SceneManager.LoadScene("gameover");
        }
    }

}
