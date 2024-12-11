using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public Transform sun; // Assign the Sun object in the Inspector
    public float orbitSpeed = 5f; // Adjust the orbital speed
    public float gravitationalConstant = 10f; // Adjust the strength of gravity
    public Color gravityColor = Color.yellow;
    public Renderer renderer; 
    [SerializeField] float distanceToSun = 10f;


    void Start()
    {

        renderer = GetComponent<Renderer>();

    }
    void FixedUpdate()
    {
        distanceToSun = Vector3.Distance(transform.position, sun.position);
        // Calculate the gravitational force direction
        Vector3 directionToSun = sun.position - transform.position;

        // Apply gravitational force to the planet
        GetComponent<Rigidbody>().AddForce(directionToSun * gravitationalConstant, ForceMode.Force);

        // Calculate the tangential velocity to maintain orbit
        Vector3 tangentialVelocity = Vector3.Cross(directionToSun, transform.up).normalized * orbitSpeed;
        GetComponent<Rigidbody>().velocity = tangentialVelocity;

        // Change the material color of the planet
        renderer.material.color = gravityColor;
    }

    void OnDrawGizmosSelected()
    {
        if (sun != null)
        {
            // Calculate the distance to the Sun
            float distanceToSun = Vector3.Distance(transform.position, sun.position);

            // Get the orbit plane normal (assuming the orbit is in the XZ plane)
            Vector3 orbitPlaneNormal = transform.up;

            // Draws a wireframe circle gizmo to represent the orbit radius
            Gizmos.color = gravityColor;
            DrawCircle(sun.position, orbitPlaneNormal, distanceToSun, 50); // 50 segments in the circle
        }
    }

    void DrawCircle(Vector3 center, Vector3 normal, float radius, int segments)
    {
        float angleIncrement = 360f / segments;
        Vector3 previousPoint = center + Quaternion.AngleAxis(0f, normal) * Vector3.forward * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleIncrement * i;
            Vector3 point = center + Quaternion.AngleAxis(angle, normal) * Vector3.forward * radius;
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
