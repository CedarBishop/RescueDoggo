using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactables
{
    bool isInteractedWith;
    public override bool Interact()
    {
        isInteractedWith = !isInteractedWith;
        return !isInteractedWith;
    }
}
