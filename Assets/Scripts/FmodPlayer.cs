using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void RunAudio(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path,GetComponent<Transform>().position);
    }
}
