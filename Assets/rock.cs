using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    public float lifetimeMinutes = 0f; // Lifetime in minutes (0 = infinite)
    public float elapsedTime = 0f;

    void FixedUpdate()
    {
        // Check if a lifetime is set
        if (lifetimeMinutes > 0)
        {
            elapsedTime += Time.deltaTime; // Incremenhtdzhzdt elapsed time

            // Destroy the rock after the specified lifetime
            if (elapsedTime >= lifetimeMinutes * 60f)
            {
                Destroy(gameObject);
            }
        }
    }
}
