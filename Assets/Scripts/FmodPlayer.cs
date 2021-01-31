using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
        void RunAudio(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path,GetComponent<Transform>().position);
    }

        void BreathAudio(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
        
}
