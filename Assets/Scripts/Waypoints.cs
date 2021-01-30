using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : Interactables
{
    public float smellSpeed;
    private Transform nextWaypoint;
    public Footsteps footstepsPrefab;
    private Color smellColor;
    public TrailRenderer smellTrailPrefab;

    private TrailRenderer currentSmellTrail;
    private bool activated;

    private float trailDistance;
    private SphereCollider collider;

    private Footsteps footsteps;

    public void Init (Transform waypoint, Color color)
    {

        collider = GetComponent<SphereCollider>();
        nextWaypoint = waypoint;
        smellColor = color;
        activated = true;

        if (Random.Range(0,2) == 0)
        {
            footsteps = Instantiate(footstepsPrefab, transform.position + new Vector3(Random.Range(-collider.radius, collider.radius), 10, Random.Range(-collider.radius, collider.radius)), Quaternion.identity);
            footsteps.target = nextWaypoint;
        }
    }

    public void Deactivate()
    {
        activated = false;
        if (footsteps != null)
        {
            Destroy(footsteps.gameObject);
        }
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
        for (int i = 0; i < 2; i++)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            currentSmellTrail = Instantiate(smellTrailPrefab, player.transform.position, Quaternion.identity);
            currentSmellTrail.startColor = smellColor;
            trailDistance = Mathf.Infinity;
            float newTrailDistance = Vector3.Distance(currentSmellTrail.transform.position, nextWaypoint.position);
            
            // Because of the line wiggle, it checks whether the distance is getting greater before it destroys it. Doing it this way because the wiggle makes it miss most of the time.
            while (trailDistance - newTrailDistance >= 0)
            {
                trailDistance = newTrailDistance;
                newTrailDistance = Vector3.Distance(currentSmellTrail.transform.position, nextWaypoint.position);
                Debug.Log("NEW DIST: " + newTrailDistance + "\nOLD DIST: " + trailDistance);
                currentSmellTrail.transform.Translate((nextWaypoint.position - player.transform.position).normalized * Time.deltaTime * smellSpeed);
                yield return null;
            }

            ParticleSystem ps = currentSmellTrail.GetComponentInChildren<ParticleSystem>();
            Destroy(ps, 5);
            ps.transform.parent = null;

            Destroy(currentSmellTrail.gameObject);
            Debug.Log("Destroyed Trail");
        }
        
    }

}
