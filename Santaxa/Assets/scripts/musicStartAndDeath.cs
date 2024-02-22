using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicStartAndDeath : MonoBehaviour
{
    AudioSource music;

    public AudioClip[] songs;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.clip = songs[0];
        music.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (music.isPlaying == false)
        {
            SwitchToLoop(1);
        }
    }

    private void SwitchToLoop(int song)
    {
        music.clip = songs[song];
        music.Play();
        music.loop = true;
    }
}
