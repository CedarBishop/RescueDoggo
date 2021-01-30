using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuee : Interactables
{
    public int scoreAddedForRescue;
    public RescueeSpawnPosition spawnPosition;
    bool hasBeenRescued;
    private string rescueeName;

    public override bool Interact()
    {
        if (hasBeenRescued)
        {
            return true;
        }

        hasBeenRescued = true;
        spawnPosition.Deactivate();
        Debug.Log("RESCUED " + rescueeName);
        GameManager.instance.RescuedPerson(scoreAddedForRescue, rescueeName);
        Destroy(gameObject,0.1f);
        return true;
    }

    public void Init (RescueeSpawnPosition rescueeSpawnPosition, string name)
    {
        spawnPosition = rescueeSpawnPosition;
        rescueeName = name;
        spawnPosition.Activate();
    }
}
