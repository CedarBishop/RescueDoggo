using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : Interactables
{
    public float smellSpeed;
    private Transform nextWaypoint;
    public Footsteps footstepsPrefab;
    private Gradient smellColor;
    public SmellFX smellFXPrefab;
    public ParticleSystem meshGlow;

    private bool activated;
    private bool trailIsActive;

    private float trailDistance;
    private SphereCollider collider;

    SmellFX currentSmellFX;

    private Footsteps footsteps;

    public void Init (Transform waypoint, Gradient color)
    {
        collider = GetComponent<SphereCollider>();
        nextWaypoint = waypoint;
        smellColor = color;
        activated = true;

        if (meshGlow != null)
        {
            ParticleSystem glow = Instantiate(meshGlow, transform.position, Quaternion.identity);
            Color extractedColor = color.colorKeys[0].color;
            glow.startColor = new Color(extractedColor.r, extractedColor.g, extractedColor.b, 0.05f);
            glow.transform.parent = transform;
            glow.Play();
        }

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

        if (trailIsActive)
        {
            return true;
        }

        StartCoroutine("CoSmellTrail");
        return true;
    }

    IEnumerator CoSmellTrail ()
    {
        trailIsActive = true;
        for (int i = 0; i < 1; i++)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (currentSmellFX != null)
            {
                Destroy(currentSmellFX);
            }
            currentSmellFX = Instantiate(smellFXPrefab, player.transform.position, Quaternion.identity);
            currentSmellFX.SetColor(smellColor);
            trailDistance = Mathf.Infinity;
            float newTrailDistance = Vector3.Distance(currentSmellFX.transform.position, nextWaypoint.position);
            
            // Because of the line wiggle, it checks whether the distance is getting greater before it destroys it. Doing it this way because the wiggle makes it miss most of the time.
            while (trailDistance - newTrailDistance >= 0)
            {
                trailDistance = newTrailDistance;
                newTrailDistance = Vector3.Distance(currentSmellFX.transform.position, nextWaypoint.position);
                currentSmellFX.transform.Translate((nextWaypoint.position - player.transform.position).normalized * Time.deltaTime * smellSpeed);
                yield return null;
            }


            Destroy(currentSmellFX.gameObject);
            Debug.Log("Destroyed Trail");           
        }

        if (nextWaypoint.GetComponent<Waypoints>())
        {
            nextWaypoint.GetComponent<Waypoints>().SmellTrailChain();
        }
        trailIsActive = false;
    }

    public void SmellTrailChain ()
    {
        if (trailIsActive)
        {
            return;
        }

        StartCoroutine("CoSmellTrailChain");
    }

    IEnumerator CoSmellTrailChain()
    {
        trailIsActive = true;
        PlayerController player = FindObjectOfType<PlayerController>();
        if (currentSmellFX != null)
        {
            Destroy(currentSmellFX);
        }
        currentSmellFX = Instantiate(smellFXPrefab, transform.position + Vector3.up, Quaternion.identity);
        currentSmellFX.SetColor(smellColor);
        trailDistance = Mathf.Infinity;
        float newTrailDistance = Vector3.Distance(currentSmellFX.transform.position, nextWaypoint.position);

        // Because of the line wiggle, it checks whether the distance is getting greater before it destroys it. Doing it this way because the wiggle makes it miss most of the time.
        while (trailDistance - newTrailDistance >= 0)
        {
            trailDistance = newTrailDistance;
            newTrailDistance = Vector3.Distance(currentSmellFX.transform.position, nextWaypoint.position);
            //currentSmellFX.transform.forward = (nextWaypoint.position - player.transform.position).normalized;
            currentSmellFX.transform.Translate((nextWaypoint.position - (transform.position + Vector3.up)).normalized * Time.deltaTime * smellSpeed);
            yield return null;
        }

        Destroy(currentSmellFX.gameObject);
        trailIsActive = false;
    }

}
