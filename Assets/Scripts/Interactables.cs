using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactables : MonoBehaviour
{
    Rigidbody rb;

    public virtual bool Interact ()
    {
        return true;
    }

    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
