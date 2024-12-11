using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteRing : MonoBehaviour
{
    public Transform planet; 
    public GameObject meteoritePrefab; 
    public int numberOfMeteorites = 100; // Number of meteorites in the ring
    public float ringRadius = 50f; // Radius of the ring
    public float ringWidth = 10f; // Width of the ring
    public float rotationSpeed = 20f; // Rotation speed of the ring
    public float meteoriteOrbitSpeed = 10f;
    void Start()
    {
       
        CreateRing();
    }

    void CreateRing()
    {
        for (int i = 0; i < numberOfMeteorites; i++)
        {
            // Calculate random position within the ring
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(ringRadius - ringWidth / 2f, ringRadius + ringWidth / 2f);
            Vector3 position = Quaternion.AngleAxis(angle, transform.up) * Vector3.forward * distance;

            // Instantiate a meteorite
            GameObject meteorite = Instantiate(meteoritePrefab, planet.position + position, Quaternion.identity);
            meteorite.GetComponent<PlanetOrbit>().sun = planet;
            meteorite.GetComponent<rock>().lifetimeMinutes = 0;
            // Make the meteorite a child of the planet (optional)
            meteorite.transform.parent = planet;

            // Add some random rotation and scale (optional)
            meteorite.transform.rotation = Random.rotation;
            meteorite.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (planet != null)
        {
            
            Gizmos.color = Color.red;
            DrawTorus(planet.position, transform.up, ringRadius, ringWidth, 50);
        }
    }

    // Helper function to draw a torus gizmo
    void DrawTorus(Vector3 center, Vector3 normal, float radius, float tubeRadius, int segments)
    {
        for (int i = 0; i < segments; i++)
        {
            float angle = 2f * Mathf.PI * i / segments;
            Vector3 offset = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, normal) * Vector3.forward * radius;
            Gizmos.DrawWireSphere(center + offset, tubeRadius);
        }
    }
}
