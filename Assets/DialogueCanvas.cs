using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCanvas : MonoBehaviour
{
    public Text dialogueText;
    public Image dialogueBackgroundImage;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Deactivate();        
    }

    public void Activate (Vector3 worldPosition, string text)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueBackgroundImage.gameObject.SetActive(true);
        transform.position = worldPosition;
        dialogueText.text = text;
    }

    public void Deactivate ()
    {
        dialogueText.gameObject.SetActive(false);
        dialogueBackgroundImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.forward = (transform.position - mainCamera.transform.position).normalized;
    }
}
