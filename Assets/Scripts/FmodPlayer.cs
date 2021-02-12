using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour

{
    private Animator animator;
    private float distance = 0.2f;
    private float Material;
    public LayerMask TerrainCheck;

    private void Start()
    {
        animator = GetComponent<Animator>();
     }

    private void FixedUpdate()
    {
        MaterialCheck();
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.green);
    }

    void MaterialCheck()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, distance, TerrainCheck);

        if (hit.collider)
        {
            if (hit.collider.tag == "Material: Snow")
                Material = 0f;
            // Debug.Log("we hit snow");
            else if (hit.collider.tag == "Material: Water")
                Material = 1f;
            // Debug.Log("we hit water");
            else
                Material = 0f;
        }
    }

    void RunningSound(string path)
    {
        if (animator.GetFloat("Movement") > 0.75f)
        {
            FMOD.Studio.EventInstance running = FMODUnity.RuntimeManager.CreateInstance(path);
            running.setParameterByName("Material", Material);
            running.start();
            running.release();
        }
    }


    void SniffingSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void JumpingSound(string path)
    {
        FMOD.Studio.EventInstance jumping = FMODUnity.RuntimeManager.CreateInstance(path);
        jumping.setParameterByName("Material", Material);
        jumping.start();
        jumping.release();
    }

    void LandingSound(string path)
    {
        FMOD.Studio.EventInstance landing = FMODUnity.RuntimeManager.CreateInstance(path);
        landing.setParameterByName("Material", Material);
        landing.start();
        landing.release();
    }
}
