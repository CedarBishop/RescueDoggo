using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialoguePrompt : Interactables
{
    [SerializeField] private LayerMask playerLayer;
    public GameObject dialogueAlert;

    private Rigidbody rb;

    public Vector3 zoomAmount;

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

    public void FocusObject(CinemachineVirtualCamera cvc)
    {
        cvc.LookAt = gameObject.transform;
        cvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset -= zoomAmount;
    }
}
