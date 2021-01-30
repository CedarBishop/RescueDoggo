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
        rescuees = new List<Rescuee>();
        if (rescueeSpawnLocations == null)
        {
            return;
        }
        rescuees.Clear();

        List<RescueeSpawnPosition> spawnLocations = new List<RescueeSpawnPosition>();

        for (int i = 0; i < rescueeSpawnLocations.Length; i++)
        {
            spawnLocations.Add(rescueeSpawnLocations[i]);
        }


        for (int i = 0; i < amountOfRescueesToSpawn; i++)
        {
            RescueeSpawnPosition location = spawnLocations[Random.Range(0, spawnLocations.Count)];
            spawnLocations.Remove(location);
            Rescuee rescuee = (Instantiate(rescueePrefab, location.transform.position, Quaternion.identity));
            rescuee.Init(location);
            rescuees.Add(rescuee);
        }

    }
}

