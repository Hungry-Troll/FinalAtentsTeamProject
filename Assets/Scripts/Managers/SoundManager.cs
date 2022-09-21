using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer titleAudioMixer;
    private static readonly string PlayLog = "PlayLog";
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";
    private int PlayLogInt;
    public Slider BGMSlider, SFXSlider;
    private float BGMSliderValue, SFXSliderValue;
    public AudioSource BGMAudioSource;
    public AudioSource[] SFXAudioArray;

    // Start is called before the first frame update
    void Start()
    {
        PlayLogInt = PlayerPrefs.GetInt(PlayLog);

        if(PlayLogInt == 0)
        {
            BGMSliderValue = 1f;
            SFXSliderValue = 1f;
            BGMSlider.value = BGMSliderValue;
            SFXSlider.value = SFXSliderValue;
            PlayerPrefs.SetFloat(BGMPref, BGMSliderValue);
            PlayerPrefs.SetFloat(SFXPref, SFXSliderValue);
            PlayerPrefs.SetInt(PlayLog, -1);
        }
        else
        {
            BGMSliderValue = PlayerPrefs.GetFloat(BGMPref);
            BGMSlider.value = BGMSliderValue;
            SFXSliderValue = PlayerPrefs.GetFloat (SFXPref);
            SFXSlider.value = SFXSliderValue;
        }
    }

    public void MasterControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }

    public void BGMControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }

    public void SFXControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }

    void OnApplicationOnFocuse(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSetting();
        }
    }

    public void UpdateSound()
    {
        BGMAudioSource.volume = BGMSlider.value;

        for (int i = 0; i < SFXAudioArray.Length; i++)
        {
            SFXAudioArray[i].volume = SFXSlider.value;
        }
    }
}
