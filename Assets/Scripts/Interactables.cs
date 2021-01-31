using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactables : MonoBehaviour
{
    public bool barkToInteract;

    protected Rigidbody rb;
    protected bool triggered;

    public virtual bool Interact ()
    {
        return true;
    }

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>();
        }
        rb = GetComponent<Rigidbody>();
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
