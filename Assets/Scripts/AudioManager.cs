using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private EventInstance _instance;

    Dictionary<string, EventReference> _soundsDictionary;

    private void Awake()
    {
        /***
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        ***/
        instance = this;
        /***
        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ***/
    }

    public static void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void StartAudioLoop(string Event)
    {
        _instance = RuntimeManager.CreateInstance(Event);
    }

    public void StopAudioLoop(string Event) 
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
