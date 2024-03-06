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
        GSM.playerOnHit(this.healthValues);
        GSM.sfx.playerHitSFX();
        gameStateManager.Instance.plInvincibility.enableInvincible();
    }
    public override void subtractHealth(int healthDelta)
    {
        this.healthValues.y -= healthDelta;
        if(healthValues.y <= 0){
            //end game screen here
            SceneManager.LoadScene("gameover");
        }
    }

}
