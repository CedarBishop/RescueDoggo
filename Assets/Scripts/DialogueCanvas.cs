using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DialogueCanvas : MonoBehaviour
{
    public Text dialogueText;
    public Image dialogueBackgroundImage;

    public bool isActive = false;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Deactivate();
        transform.forward = (transform.position - mainCamera.transform.position).normalized;
    }

    public void ToggleActivation(Vector3 worldPosition, string text)
    {
        if (!isActive)
        {
            Activate(worldPosition, text);
            isActive = true;
        }
        else
        {
            Deactivate();
            isActive = false;
        }
    }

    public void Activate (Vector3 worldPosition, string text)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueBackgroundImage.gameObject.SetActive(true);
        transform.position = worldPosition;
        dialogueText.text = text;
        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
        cvc.LookAt = transform;
        transform.forward = (transform.position - cvc.transform.position).normalized;
    }

    public void Deactivate ()
    {
        dialogueText.gameObject.SetActive(false);
        dialogueBackgroundImage.gameObject.SetActive(false);
        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
        cvc.LookAt = FindObjectOfType<PlayerController>().gameObject.transform;
        transform.forward = (transform.position - cvc.transform.position).normalized;
    }
}
