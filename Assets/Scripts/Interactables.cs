using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactables : MonoBehaviour
{
    Rigidbody rb;

    public virtual bool Interact ()
    {
        return true;
    }

    private void Awake()
    {
        rb.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
