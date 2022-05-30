using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainMenuMusic;

    // Start is called before the first frame update
    void Start()
    {
        SoundSystem._instance.Quiet();
        SoundSystem._instance.PlayMusic(mainMenuMusic);
        
    }


}
