using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rescuee : Interactables
{
    public int scoreAddedForRescue;
    public RescueeSpawnPosition spawnPosition;
    bool hasBeenRescued;
    private string rescueeName;

    public GameObject dialogueAlert;
    public Text textObj;

    private CameraFade camFade;

    public string[] dialogue = { "<MISSING TEXT>" };

    private void Awake()
    {
        camFade = FindObjectOfType<CameraFade>();
    }

    public override bool Interact()
    {
        Debug.Log("RESCUED " + rescueeName);
        if (hasBeenRescued)
        {
            return true;
        }

        hasBeenRescued = true;

        spawnPosition.Deactivate();
        GameManager.instance.RescuedPerson(scoreAddedForRescue, rescueeName);

        StartCoroutine("WaitForSeconds");
        
        return true;
    }

    public void Init (RescueeSpawnPosition rescueeSpawnPosition, string name)
    {
        spawnPosition = rescueeSpawnPosition;
        rescueeName = name;
        spawnPosition.Activate();
    }

    public IEnumerator WaitForSeconds()
    {
        // Set text
        textObj.text = dialogue[Random.Range(0, dialogue.Length)];
        yield return new WaitForSeconds(3f);    // Let dialogue sit there for a bit

        // Fade screen to Black...
        camFade.fade = true;
        camFade.curtainAlpha = 0;

        // Fade screen to Transparent...
        yield return new WaitForSeconds(camFade.fadeTime);  // Wait for Fade to Black to finish
        Destroy(gameObject, 2.01f);
        yield return new WaitForSeconds(2);  // Wait on back for a bit
        camFade.curtainAlpha = 1;
        camFade.fade = false;   // Set fade to clear to start


        yield return true;
    }
}
