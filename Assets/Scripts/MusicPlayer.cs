using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

     void Awake()
     {
        ////If any Music Player exists destroy it else continue in next scenes
        if (instance != null && instance!=this)
        {
            music = GetComponent<AudioSource>();
            Destroy(gameObject);
            
        }
        else
        {
            instance = this;

            //Continue music
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    //Play different audio according to level
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("MusicPlayer : loaded level" + level);
        music.Stop();

        if (level == 0)
        {
            music.clip = startClip;
        }
        if (level == 1)
        {
            music.clip = gameClip;
        }
        if (level == 2)
        {
            music.clip = endClip;
        }

        music.loop = true;
        music.Play();
    }





}
