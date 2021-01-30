using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactables : MonoBehaviour
{
    Rigidbody rb;
    protected bool triggered;

    public virtual bool Interact ()
    {
        return true;
    }

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
    }

    public virtual void TriggerEnter()
    {
        triggered = true;
    }
    public virtual void TriggerExit()
    {
        triggered = false;
    }
}
