using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private EventInstance _instance;

    Dictionary<string, string> _soundsDictionary;

    private void Awake()
    {
        instance = this;
        SetSoundsDictionary();
    }

    private void SetSoundsDictionary()
    {
        _soundsDictionary = new Dictionary<string, string>();
        _soundsDictionary.Add("Almost Time", "event:/SFX/Almost Time");
        _soundsDictionary.Add("Cannon", "event:/SFX/Canonn");
        _soundsDictionary.Add("Click", "event:/SFX/Click");
        _soundsDictionary.Add("Entangle", "event:/SFX/Entangle");
        _soundsDictionary.Add("Rope Extend", "event:/SFX/Rope Flying");
        _soundsDictionary.Add("Rope Impact", "event:/SFX/Rope Impact");
        _soundsDictionary.Add("Rope Reel", "event:/SFX/Rope Reel");
        _soundsDictionary.Add("Score Hit 4", "event:/SFX/Score Hit 4");
        _soundsDictionary.Add("Score Hit 2", "event:/SFX/Score Hit 2");
        _soundsDictionary.Add("Score Hit 3", "event:/SFX/Score Hit 3");
        _soundsDictionary.Add("Seagull", "event:/SFX/Seagull");
        _soundsDictionary.Add("Time Up", "event:/SFX/Time's Up");
    }

    public static void PlayOneShot(string SoundName)
    {
        RuntimeManager.PlayOneShot(instance._soundsDictionary[SoundName]);
    }

    public void StartAudioLoop(string SoundName)
    {
        _instance = RuntimeManager.CreateInstance(instance._soundsDictionary[SoundName]);
    }

    public void StopAudioLoop() 
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
