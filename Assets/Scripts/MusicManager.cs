using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    private float introDelay;

    [FMODUnity.EventRef]
    public string MenuMusicEventPath;
    [FMODUnity.EventRef]
    public string MainMusicEventPath;
    FMOD.Studio.EventInstance MenuMusicInst;
    FMOD.Studio.EventInstance MainMusicInst;
    FMOD.Studio.EventInstance Ambience;

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
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            MenuMusicInst = FMODUnity.RuntimeManager.CreateInstance(MenuMusicEventPath);
            MenuMusicInst.start();
            MenuMusicInst.release();
        }

        Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/blizzard");
        Ambience.start();
        Ambience.release();

    }

    public void PlayMenuMusic()
    {
        MenuMusicInst = FMODUnity.RuntimeManager.CreateInstance(MenuMusicEventPath);
        // MenuMusicInst.start();
        MenuMusicInst.release();
        Ambience.setParameterByName("Intensity", 0f);
    }

    public void PlayChillGameMusic()
    {
        StopCoroutine("CoMenuMusic");
        MenuMusicInst.release();
        MenuMusicInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MainMusicInst = FMODUnity.RuntimeManager.CreateInstance(MainMusicEventPath);
        MainMusicInst.start();
        Ambience.setParameterByName("Intensity", 0f);
        MainMusicInst.setParameterByName("Intensity", 0f);
        MainMusicInst.release();
    }

    public void PlayDramaticGameMusic()
    {
        StopCoroutine("CoMenuMusic");
        Ambience.setParameterByName("Intensity", 1f);
        MainMusicInst.setParameterByName("Intensity", 1f);
    }

    public void SetMusicVolume (float value)
    {
        //audioSource.volume = value / 2;
    }

    public void SetSFXVolume (float value)
    {
        //FMOD stuff
    }

   // public float GetMusicVolume()
   // {
       // return audioSource.volume * 2;

    //}
    IEnumerator CoMenuMusic()

{
    yield return new WaitForSeconds(introDelay);
}

}

