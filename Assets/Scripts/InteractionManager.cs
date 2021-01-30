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
            pc.interactableObjects.Add(other.transform);
            other.GetComponent<Interactables>().TriggerEnter();
            Debug.Log("Added " + other.name + " to interactables");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactables>())
        {
            pc.interactableObjects.Remove(other.transform);
            other.GetComponent<Interactables>().TriggerExit();
            Debug.Log("Removed " + other.name + " to interactables");
        }
    }
}
