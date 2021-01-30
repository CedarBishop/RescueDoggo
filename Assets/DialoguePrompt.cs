using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePrompt : Interactables
{
    [SerializeField] private LayerMask playerLayer;
    public GameObject dialogueAlert;

    private Rigidbody rb;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    public override void TriggerEnter()
    {
        base.TriggerEnter();
        dialogueAlert.SetActive(true);
    }

    public override void TriggerExit()
    {
        base.TriggerExit();
        dialogueAlert.SetActive(false);
    }
}
