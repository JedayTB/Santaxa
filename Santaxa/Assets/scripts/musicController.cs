using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    AudioSource music;

    public AudioClip[] songs;

    private bool isBetweenWave = false;

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
        if (music.isPlaying == false && isBetweenWave == false)
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

    public void SwitchToBetween()
    {
        isBetweenWave = true;
        music.clip = songs[2];
        music.Play();
    }

    public void SwitchToMain()
    {
        isBetweenWave = false;
        music.clip = songs[0];
        music.Play();
        music.loop = false;
    }
}
