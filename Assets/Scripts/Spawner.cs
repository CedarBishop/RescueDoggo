using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public Rescuee rescueePrefab;
    public RescueeSpawnPosition[] rescueeSpawnLocations;
    public int amountOfRescueesToSpawn;


    public List<Rescuee> rescuees;

    public void OnStartDay()
    {
        SpawnRescuees();
    }

    void SpawnRescuees()
    {
        if (rescueeSpawnLocations == null)
        {
            return;
        }

        rescuees = new List<Rescuee>();

        List<RescueeSpawnPosition> spawnLocations = new List<RescueeSpawnPosition>();

        if (rescueeSpawnLocations.Length == 1)
        {
            spawnLocations.Add(rescueeSpawnLocations[0]);
        }
        else
        {
            for (int i = 0; i < rescueeSpawnLocations.Length; i++)
            {
                spawnLocations.Add(rescueeSpawnLocations[i]);
            }
        }


        for (int i = 0; i < amountOfRescueesToSpawn; i++)
        {
            RescueeSpawnPosition location;
            if (spawnLocations.Count == 1)
            {
                location = spawnLocations[0];
            }
            else
            {
                location = spawnLocations[Random.Range(0, spawnLocations.Count)];
            }
            
            spawnLocations.Remove(location);
            Rescuee rescuee = (Instantiate(rescueePrefab, location.transform.position, Quaternion.identity));
            rescuee.Init(location);
            rescuees.Add(rescuee);
        }

    }
}

