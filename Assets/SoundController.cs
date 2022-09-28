using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioMixerGroup BGM;
    public AudioMixerGroup FPX;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

        AudioMixer audioMixer = GameManager.Resource.GetAudioMixer("TitleAudioMixer");

        AudioClip temAudio = GameManager.Resource.GetAudioSource("Low Poly Animated Dinosaurs-320k");
        // »ý¼º
        audioSource.clip = temAudio;
        AudioMixerGroup[] test = audioMixer.FindMatchingGroups("BGM");


        audioSource.outputAudioMixerGroup = test[0];

        audioSource.Play();



        //audio.
        //audio.outputAudioMixerGroup = audio123;
        //audio.GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
