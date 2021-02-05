using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private Animator animator;
    FMOD.Studio.EventInstance SFXVolumeTestEvent;
    FMOD.Studio.Bus SFXSounds;

    float SFXVolume = 0.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        SFXSounds = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Other/SFXTestBark");
    }

    void Update()
    {
        SFXSounds.setVolume(SFXVolume);
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
