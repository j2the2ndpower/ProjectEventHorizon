using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeSettings : MonoBehaviour
{

    public AudioMixerGroup masterV;
    public AudioMixerGroup musicV;
    public AudioMixerGroup sfxV;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioClip settingsMusic;
    public AudioClip sfxExample;


    // Start is called before the first frame update
    void Start()
    {
        SoundSystem._instance.Quiet();
        SoundSystem._instance.PlayMusic(settingsMusic);
    }

    public void MasterChange()
    {
        float value = LogValue(masterSlider.value);
        // Debug.Log("MasterChange :" + value.ToString());
        masterV.audioMixer.SetFloat("Master", value);
    }

    public void MusicChange()
    {
        float value = LogValue(musicSlider.value);
        // Debug.Log("MasterChange :" + value.ToString());
        musicV.audioMixer.SetFloat("Music", value);
    }

    public void SFXChange()
    {
        float value = LogValue(sfxSlider.value);
        // Debug.Log("MasterChange :" + value.ToString());
        sfxV.audioMixer.SetFloat("SFX", value);
        SoundSystem._instance.PlaySFX(sfxExample);
    }

    private float LogValue(float v)
    {
        return Mathf.Log(v) * 20.0f;
    }

}
