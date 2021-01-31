using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void RunAudio(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path,GetComponent<Transform>().position);
    }

    void BreathAudio(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void RunningSound(string path)
    {
        if (animator.GetFloat("Movement") >= 0.5f)
        {
            FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
        }
    }

}
