using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// also i tried to have it play on death but i have no idea how to because this bitch ass program WONT LET ME OH MY GOD I GIVE UP!!!
// lets just pretend there's a death sound lol ahhaha!!!
public class SFXController : MonoBehaviour
{
    AudioSource audio;
    [SerializeField]
    private AudioClip[] hitFX;


    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void playerHitSFX()
    {
        int rndNum = Random.Range(0, hitFX.Length);
        audio.clip = hitFX[rndNum];
        audio.Play();
    }

    public void enemyHitSFX()
    {
        audio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision) // dumbass fucking stupid shit I DIDNT WANNA DO THIS BUT LITERALLY NOTHING ELSE WORKED AAAAAAAAA
    {
        if (gameObject.CompareTag("enemy") && collision.gameObject.CompareTag("bullet"))
        {
            audio.Stop();
            audio.Play();
        }
    }
}
