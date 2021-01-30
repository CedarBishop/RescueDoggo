using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private PlayerController pc;
    // Start is called before the first frame update
    void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactables>())
        {
            Interactables interactable = other.GetComponent<Interactables>();
               
            if (interactable.barkToInteract)
            {
                pc.barkableObjects.Add(other.transform);
            }
            else
            {
                pc.interactableObjects.Add(other.transform);
            }
            interactable.TriggerEnter();
            //Debug.Log("Added " + other.name + " to interactables");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactables>())
        {
            Interactables interactable = other.GetComponent<Interactables>();
            if (interactable.barkToInteract)
            {
                pc.barkableObjects.Remove(other.transform);
            }
            else
            {
                pc.interactableObjects.Remove(other.transform);
            }
            
            interactable.TriggerExit();
            //Debug.Log("Removed " + other.name + " to interactables");
        }
    }
}
