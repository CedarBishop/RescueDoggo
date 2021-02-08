using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private Animator animator;
    FMOD.Studio.EventInstance SFXVolumeTestEvent;
    FMOD.Studio.Bus SFXSounds;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus Ambience;

    public float SFXVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public float AmbienceVolume = 0.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        SFXSounds = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        Ambience = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Other/SFXTestBark");
    }

    void Update()
    {
        SFXSounds.setVolume(SFXVolume);
        Music.setVolume(MusicVolume);
        Ambience.setVolume(AmbienceVolume);
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
        FMOD.Studio.PLAYBACK_STATE Pbstate;
        SFXVolumeTestEvent.getPlaybackState(out Pbstate);
         if (Pbstate != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void AmbienceVolumeLevel(float newAmbienceVolume)
    {
        AmbienceVolume = newAmbienceVolume;
    }

    void RunningSound(string path)
    {
        if (animator.GetFloat("Movement") >0.75f)
        {
            FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
        }        
    }

    void SniffingSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void JumpingSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void LandingSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
