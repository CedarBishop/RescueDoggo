using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : Interactables
{
    public float smellSpeed;
    private Transform nextWaypoint;
    private Color smellColor;
    private TrailRenderer smellTrailPrefab;

    private TrailRenderer currentSmellTrail;
    private bool activated;

    public void Init (Transform waypoint, Color color, TrailRenderer trail)
    {
        nextWaypoint = waypoint;
        smellColor = color;
        smellTrailPrefab = trail;
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
            Destroy(currentSmellTrail);
        }

        currentSmellTrail = Instantiate(smellTrailPrefab, transform.position, Quaternion.identity);
        currentSmellTrail.startColor = smellColor;
        StartCoroutine("CoSmellTrail");
        return true;
    }

    IEnumerator CoSmellTrail ()
    {
        while (Vector3.Distance(currentSmellTrail.transform.position, nextWaypoint.position) > 1)
        {
            transform.Translate((nextWaypoint.position - transform.position).normalized * Time.deltaTime * smellSpeed);
            yield return null;
        }
        Destroy(currentSmellTrail);
    }

}
