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

    public LayerMask coverLayer;
    private bool isWall;
    private bool bounceActive;

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
        bounceActive = playerShootLogic.bouncyBool;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
 
        if (penetrationCount <= 0){
            GameObject.Destroy(this.gameObject);
        }

        checkForWall();
    }

    // The way the bounce works is by using raycasts to check if there is a wall to the right and left (because I genuinely have no idea how else to figure out what side it is hitting)
    // If the colliders don't collide, the bounce will execute as if it was colliding with a ceiling, and vice versa
    // Because of this, right now corners and angled walls are a bit broken :(
    private void checkForWall()
    {
        RaycastHit2D leftRay = Physics2D.Raycast(transform.position, Vector2.left,  0.5f, coverLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, coverLayer);

        if (leftRay.collider == null && rightRay.collider == null)
        {
            isWall = false;
        }
        if (leftRay.collider != null || rightRay.collider != null) // this may seem redundent but i'm like 17% sure it made a difference when testing so i'm leaving it
        {
            isWall = true;
        }
    }

    private void flipDirection()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        if (isWall)
        {
            Quaternion newRotation = Quaternion.Euler(0, 0, 360 - currentRotation);
            transform.rotation = newRotation;
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(0, 0, 180 - currentRotation);
            transform.rotation= newRotation;
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
                    if (bounceActive) // love a good triple nested if (ik it could be simplier but honestly this is funny)
                    {
                        flipDirection();
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                    
                }
            }
        }
    }
}
