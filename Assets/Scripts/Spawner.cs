using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public Rescuee rescueePrefab;
    public Transform[] rescueeSpawnLocations;
    public int amountOfRescueesToSpawn;

    public void OnStartDay ()
    {
        if (rescueeSpawnLocations == null)
        {
            return;
        }

        List<Transform> spawnLocations = new List<Transform>();

        for (int i = 0; i < rescueeSpawnLocations.Length; i++)
        {
            spawnLocations.Add(rescueeSpawnLocations[i]);
        }


        for (int i = 0; i < amountOfRescueesToSpawn; i++)
        {
            Transform location = spawnLocations[Random.Range(0, spawnLocations.Count)];
            spawnLocations.Remove(location);
            Instantiate(rescueePrefab, location.position, Quaternion.identity);
        }

    }
}
