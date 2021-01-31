using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    public AudioClip menuIntroClip;
    public AudioClip menuClip;
    public AudioClip gameClip;
    public AudioClip dramaticGameClip;

    private AudioSource audioSource;

    private float introDelay;

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
        introDelay = menuIntroClip.length;
        StartCoroutine("CoMenuMusic");
    }

    public void PlayChillGameMusic ()
    {
        StopCoroutine("CoMenuMusic");

        audioSource.clip = gameClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayDramaticGameMusic()
    {
        StopCoroutine("CoMenuMusic");

        audioSource.clip = dramaticGameClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetMusicVolume (float value)
    {
        audioSource.volume = value / 2;
    }

    public void SetSFXVolume (float value)
    {

    }

    public float GetMusicVolume()
    {
        return audioSource.volume * 2;
    }

    IEnumerator CoMenuMusic ()
    {
        audioSource.loop = false;
        audioSource.clip = menuIntroClip;
        audioSource.Play();

        yield return new WaitForSeconds(introDelay);

        audioSource.clip = menuClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
