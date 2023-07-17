using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager instance;
    public AudioSource musicSource;
    public AudioSource soundSource;

    /*private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }*/

    public void StartMusic(AudioClip song)
    {
        Debug.Log("SoundManager StartMusic()");
        musicSource.clip = song;
        musicSource.Play();
    }

    public void StartMusic()
    {
        Debug.Log("SoundManager StartMusic()");
        if(musicSource.clip!= null)
            musicSource.Play();
        else
            Debug.Log("SoundManager StartMusic() Clip == null!!!");
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }
}
