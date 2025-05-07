using System;

using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to spawn
    public Transform[] spawnPoints; // Array of spawn points

    public int numberOfObjectsToSpawn = 5; // Number of objects to spawn

    void Start()
    {
        // Check if there are enough spawn points
        if (spawnPoints.Length < numberOfObjectsToSpawn)
        {
            Debug.LogWarning("Not enough spawn points available.");
            return;
        }
        
        // Randomly select spawn points
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Get a random index within the spawnPoints array
            int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

            // Get the random spawn point transform
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the object at the selected spawn point
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Remove the selected spawn point from the array to avoid spawning multiple objects at the same point
            // This step is optional if you want to allow multiple objects at the same spawn point
            // You can remove this line if you want to spawn multiple objects at the same spawn point
            spawnPoints[randomIndex] = spawnPoints[spawnPoints.Length - 1];
            Array.Resize(ref spawnPoints, spawnPoints.Length - 1);
        }
    }
}
