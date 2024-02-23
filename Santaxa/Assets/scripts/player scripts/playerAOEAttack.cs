using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAOEAttack : MonoBehaviour
{
    public int damage = 3;
    public Vector2 size = new Vector2(6.6f, 3.6f);
    public float objectLifeTime = 1;

    SpriteRenderer sr;
    public Color color = Color.red;

    public void SetAOEAttributes(playerLoadout loadout)
    {
        this.damage = loadout.damageAOE;
        this.size = loadout.sizeAOE;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up * 2);
        transform.localScale = size;
        sr = GetComponent<SpriteRenderer>();

        color.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        objectLifeTime -= Time.deltaTime;

        if (objectLifeTime <= 0.5f)
        {
            color.a -= Time.deltaTime;
            sr.color = color;

            if (objectLifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        //print("current size " + size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            hittableObject hitThis = collision.GetComponent<hittableObject>();
            zombieScript zombie = collision.GetComponent<zombieScript>(); // i love getting component (there has to be a better way to do this)
            hitThis.onHit(damage);
            zombie.isBeingKnocked = true;
        }
    }
}