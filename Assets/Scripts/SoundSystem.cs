using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem _instance;
    public AudioSource musicSource, sfxSource;

    // Singleton Work
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Quiet()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            // Debug.Log("SoundSystem.PlaySFX()");
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("PlaySFX was given a null clip");
        }
    }

    public void PlayMusic(AudioClip music)
    {
        if (music != null)
        {
            // Debug.Log("SoundSystem.PlayMusic()");
            musicSource.clip = music;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("PlayMusic was given a null clip");
        }
    }
}
