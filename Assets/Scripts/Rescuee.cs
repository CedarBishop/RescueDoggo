using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuee : Interactables
{
    public int scoreAddedForRescue;
    public RescueeSpawnPosition spawnPosition;
    bool hasBeenRescued;

    public override bool Interact()
    {
        if (hasBeenRescued)
        {
            return true;
        }

        hasBeenRescued = true;
        spawnPosition.Deactivate();
        Debug.Log("RESCUED " + transform.name);
        GameManager.instance.AddToScore(scoreAddedForRescue);
        Destroy(gameObject,0.1f);
        return true;
    }

    public void Init (RescueeSpawnPosition rescueeSpawnPosition)
    {
        spawnPosition = rescueeSpawnPosition;
        spawnPosition.Activate();
    }
}
