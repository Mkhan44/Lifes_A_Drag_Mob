using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using UnityEngine.UI;
public class Audio_Controller : MonoBehaviour
{
    public Slider globalVolumeSlider;
    public Slider globalMusicVolumeSlider;
    public Slider globalSFXVolumeSlider;

    //Strings we will use here and in pause menu to ensure that we save the player's volume preferences.
    string globalVolumeKey = "Global_Volume";
    string MusicVolumeKey = "Music_Volume";
    string SFXVolumeKey = "SFX_Volume";

    float globalVolumePref;
    float MusicVolumePref;
    float SFXVolumePref;
    // Start is called before the first frame update
    void Start()
    {
        globalVolumePref = PlayerPrefs.GetFloat(globalVolumeKey);
        MusicVolumePref = PlayerPrefs.GetFloat(MusicVolumeKey);
        SFXVolumePref = PlayerPrefs.GetFloat(SFXVolumeKey);

       /*
        globalVolumeSlider.value = EazySoundManager.GlobalVolume;
        globalMusicVolumeSlider.value = EazySoundManager.GlobalMusicVolume;
        globalSFXVolumeSlider.value = EazySoundManager.GlobalSoundsVolume;
        */

        globalVolumeSlider.value = globalVolumePref;
        globalMusicVolumeSlider.value = MusicVolumePref;
        globalSFXVolumeSlider.value = SFXVolumePref;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlobalVolumeChanged()
    {
        EazySoundManager.GlobalVolume = globalVolumeSlider.value;
    }

    public void GlobalMusicVolumeChanged()
    {
        EazySoundManager.GlobalMusicVolume = globalMusicVolumeSlider.value;
    }

    public void GlobalSoundVolumeChanged()
    {
        EazySoundManager.GlobalSoundsVolume = globalSFXVolumeSlider.value;
    }

    public void setValues()
    {
        PlayerPrefs.SetFloat(globalVolumeKey, EazySoundManager.GlobalVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, EazySoundManager.GlobalMusicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, EazySoundManager.GlobalSoundsVolume);

    }
}
