using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : Interactables
{
    public float smellSpeed;
    private Transform nextWaypoint;
    private Color smellColor;
    public TrailRenderer smellTrailPrefab;

    private TrailRenderer currentSmellTrail;
    private bool activated;

    public void Init (Transform waypoint, Color color)
    {
        nextWaypoint = waypoint;
        smellColor = color;
        activated = true;
    }

    public void Deactivate()
    {
        activated = false;
    }

    public override bool Interact()
    {
        if (!activated)
        {
            return true;
        }

        if (currentSmellTrail != null)
        {
            Destroy(currentSmellTrail.gameObject);
        }


        StartCoroutine("CoSmellTrail");
        return true;
    }

    IEnumerator CoSmellTrail ()
    {
        for (int i = 0; i < 5; i++)
        {
            currentSmellTrail = Instantiate(smellTrailPrefab, transform.position, Quaternion.identity);
            currentSmellTrail.startColor = smellColor;
            while (Vector3.Distance(currentSmellTrail.transform.position, nextWaypoint.position) > 0.1f)
            {
                currentSmellTrail.transform.Translate((nextWaypoint.position - transform.position).normalized * Time.deltaTime * smellSpeed);
                yield return null;
            }
            Destroy(currentSmellTrail.gameObject);
        }
        
    }

}
