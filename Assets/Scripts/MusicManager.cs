using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    public float crossFadeSpeed;

    public AudioClip menuClip;
    public AudioClip gameClip;
    public AudioClip dramaticGameClip;

    private AudioSource audioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            PlayMenuMusic();
        }
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuClip;
        audioSource.Play();
    }

    public void PlayChillGameMusic ()
    {
        audioSource.clip = gameClip;
        audioSource.Play();
    }

    public void PlayDramaticGameMusic()
    {
        audioSource.clip = dramaticGameClip;
        audioSource.Play();
    }

    public void SetMusicVolume (float value)
    {
        audioSource.volume = value;
    }

    public void SetSFXVolume (float value)
    {

    }

    public float GetMusicVolume()
    {
        return audioSource.volume;
    }
}
