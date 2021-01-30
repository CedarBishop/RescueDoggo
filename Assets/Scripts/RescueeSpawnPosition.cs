using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueeSpawnPosition : MonoBehaviour
{
    public Waypoints[] pathway;

    public Color smellColor;

    public void Activate()
    {
        for (int i = 0; i < pathway.Length; i++)
        {
            pathway[i].Init((i < pathway.Length - 1) ? pathway[i + 1].transform : transform, smellColor);              
        }
    }    

    public void Deactivate ()
    {
        foreach (Waypoints item in pathway)
        {
            item.Deactivate();
            Destroy(item.gameObject);
        }
    }
}
