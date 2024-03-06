using UnityEngine;

public class hittableObject : MonoBehaviour
{
    [SerializeField]
    //Is a vector2 to store base hp value.
    //for use in HP Bars.
    protected Vector2 healthValues = new Vector2(1,1);
    
    [SerializeField]
    protected ParticleSystem deathParticles;

    public virtual void onHit(int hitDamage){
        //Debug.Log($"{this.gameObject.name} was hit for {hitDamage}");
        subtractHealth(hitDamage);
    }   
     public virtual void subtractHealth(int healthDelta){
        this.healthValues.y -= healthDelta;
        Debug.LogWarning($"should play {this.gameObject.name} hit sfx here");
        if(healthValues.y <= 0){
            //Debug.Log($"{this.gameObject.name} has died. Now Destroying.");
            gameStateManager.waveCont.checkIfWaveOver();
            if(deathParticles != null)
            {
                ParticleSystem temp = Instantiate(deathParticles);
                temp.transform.position = this.transform.position;
                
            }
            else
            {
                print($"{this.gameObject.name} does not have particles");
            }
            OnDestroy();
        }
    }
    protected virtual void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
