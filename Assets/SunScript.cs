using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    public float orbitSpeed = 5f; // Adjust the speed of the Sun's movement
    public Vector3 orbitCenter;  // Set the center of the orbit (e.g., Vector3.zero for the origin)
    public Vector3 orbitAxis = Vector3.up; // Axis of rotation (default is Y-axis)
    private LineRenderer lineRenderer; // Reference to the LineRenderer component
    public Renderer renderer;
    [SerializeField] Color sun = new Color();// Add a reference to the Renderer component
    void Start()

    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = sun;
    
        // Initialize the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // Rotate the Sun around the specified center and axis
        transform.RotateAround(orbitCenter, orbitAxis, orbitSpeed * Time.deltaTime);

        // Add the current position of the Sun to the LineRenderer
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
    }
}

