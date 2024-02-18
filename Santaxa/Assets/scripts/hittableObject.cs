using UnityEngine;

public class hittableObject : MonoBehaviour
{
    [SerializeField]
    //Is a vector2 to store base hp value.
    //for use in HP Bars.
    protected Vector2 healthValues = new Vector2(1,1);

    
    public virtual void onHit(int hitDamage){
        //Debug.Log($"{this.gameObject.name} was hit for {hitDamage}");
        subtractHealth(hitDamage);
    }   
     public virtual void subtractHealth(int healthDelta){
        this.healthValues.y -= healthDelta;
        
        if(healthValues.y <= 0){
            //Debug.Log($"{this.gameObject.name} has died. Now Destroying.");
            gameStateManager.waveCont.checkIfWaveOver();
            Destroy(this.gameObject);
        }
    }
}
