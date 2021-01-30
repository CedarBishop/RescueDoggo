using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialoguePrompt : Interactables
{
    [SerializeField] private LayerMask playerLayer;
    public GameObject dialogueAlert;

    public string text = "<MISSING TEXT>";

    private Rigidbody rb;

    private void Awake()
    {
        if (!GetComponent<Rigidbody>())
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    public override bool Interact()
    {

        dialogueAlert.SetActive(false);
        UIManager.instance.ToggleDialogueCanvas(transform.position + Vector3.up * 3, text);
        return true;
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
        UIManager.instance.DeactivateDialogueCanvas();
    }
}
