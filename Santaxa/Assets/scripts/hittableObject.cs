
using UnityEngine;

public class hittableObject : MonoBehaviour
{
    [SerializeField]
    public int healthPoints = 1;
    //i could do the thing that could make my game engine lab2 
    //teacher happy and use a singleton reference so anything
    //can get this value without to much trouble
    //but you know what?
    //fuck you
    
    public virtual void onHit(int hitDamage){
        //Debug.Log($"{this.gameObject.name} was hit for {hitDamage}");
        subtractHealth(hitDamage);
    }   
     public virtual void subtractHealth(int healthDelta){
        this.healthPoints -= healthDelta;
        
        if(healthPoints <= 0){
            //Debug.Log($"{this.gameObject.name} has died. Now Destroying.");
            gameStateManager.waveCont.checkIfWaveOver();
            Destroy(this.gameObject);
        }
    }
}
