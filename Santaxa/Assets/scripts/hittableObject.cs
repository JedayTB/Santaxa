using UnityEngine;

public class hittableObject : MonoBehaviour
{
    [SerializeField]
    //Is a vector2 to store base hp value.
    //for use in HP Bars.
    protected Vector2 healthValues = new Vector2(1,1);
    
    [SerializeField]
    protected ParticleSystem deathParticles;
    
    [SerializeField]
    protected AudioSource audioSRC;
    [SerializeField]
    protected AudioClip[] hitFX;
    void Start()
    {
        audioSRC = AudioSource.FindAnyObjectByType<AudioSource>();
        if(audioSRC == null){
            Debug.LogError("AUDIO SOURCE NULL");
        }
    }
    public virtual void onHit(int hitDamage){
        //Debug.Log($"{this.gameObject.name} was hit for {hitDamage}");
        subtractHealth(hitDamage);
    }   
     public virtual void subtractHealth(int healthDelta){
        this.healthValues.y -= healthDelta;
        //playRndHitSfx();
        Debug.LogWarning($"should play {this.gameObject.name} hit sfx here");
        if(healthValues.y <= 0){
            print("why am i being run");
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
    protected virtual void playRndHitSfx(){
        int rndNum = Random.Range(0, hitFX.Length);
        audioSRC.clip = hitFX[rndNum];
        audioSRC.Play();
    }
}
