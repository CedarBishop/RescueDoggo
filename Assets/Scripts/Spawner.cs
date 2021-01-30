using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public RescueeNames rescueeNames;
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

        if (amountOfRescueesToSpawn > spawnLocations.Count)
        {
            amountOfRescueesToSpawn = spawnLocations.Count;
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

            rescuee.Init(location, rescueeNames.names[Random.Range(0,rescueeNames.names.Length)]);
            rescuees.Add(rescuee);
        }

    }

    [System.Serializable]
    public class RescueeNames
    {
        public string[] names;
    }
}

