using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteShower : MonoBehaviour
{
    public Transform planet; 
    public GameObject meteorPrefab; 
    public int numberOfMeteors = 20; // Number of meteors in the shower
    public float showerDuration = 10f; // Duration of the meteor shower in seconds
    public float spawnRadius = 100f; // Radius around the planet to spawn meteors
    public float minSpeed = 50f; // Minimum speed of the meteors
    public float maxSpeed = 100f; // Maximum speed of the meteors

    private bool isShowerActive = false;
    void Start()
    {
        // Start the meteor shower
        StartShower();
    }
    public void StartShower()
    {
        if (!isShowerActive)
        {
            isShowerActive = true;
            StartCoroutine(SpawnMeteors());
        }
    }

    IEnumerator SpawnMeteors()
    {
       while (true) 
       {
            // Calculate random spawn position
            Vector3 spawnDirection = Random.onUnitSphere; // Random direction in a sphere
            Vector3 spawnPosition = planet.position + spawnDirection * spawnRadius;

            // Instantiate a meteor
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
            float min=Random.Range(2, 10);
            meteor.GetComponent<rock>().lifetimeMinutes = min; 
            meteor.GetComponent<PlanetOrbit>().enabled = false; 
            
            meteor.transform.rotation = Random.rotation;
            meteor.transform.localScale = Vector3.one * Random.Range(10f, 35f);
            // Add velocity towards the planet
            Rigidbody rb = meteor.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float speed = Random.Range(minSpeed, maxSpeed);
                rb.velocity = -spawnDirection * speed; // Move towards the planet's center
            }

           

            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f)); // Randomize spawn interval
       }
 
    }
}
