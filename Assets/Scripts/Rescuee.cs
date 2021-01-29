using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuee : Interactables
{
    public int scoreAddedForRescue;

    bool hasBeenRescued;
    public override bool Interact()
    {
        if (hasBeenRescued)
        {
            return true;
        }

        hasBeenRescued = true;
        GameManager.instance.AddToScore(scoreAddedForRescue);
        Destroy(gameObject,0.1f);
        return true;
    }
}
