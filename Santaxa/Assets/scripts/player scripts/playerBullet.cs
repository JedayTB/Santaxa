using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    public int damage = 1;
    [SerializeField]
    private float objectLifeTime = 3f;
    [SerializeField]
    private int penetrationAmount;
    private int penetrationCount;
    public void setBulletAttritbutes(playerLoadout loadout){
        this.speed = loadout.speed;
        this.damage = loadout.damage;
        this.penetrationAmount = loadout.penetration;
        this.penetrationCount = penetrationAmount; // set to amt to not destory immedtiatelty
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, objectLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * this.transform.localScale.x * speed * Time.deltaTime);
        if(penetrationCount <= 0){
            GameObject.Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"collides with {other.gameObject.tag}");
        //Check if object is hittable
        if(other.gameObject.tag != "PLAYER"){
            hittableObject hitThis = other.gameObject.GetComponent<hittableObject>();
            if(hitThis != null){
                hitThis.onHit(this.damage);
                penetrationCount -= 1;
            }else{
                if(other.gameObject.CompareTag("cover")){
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
    }
}
