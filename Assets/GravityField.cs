using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public Transform planet; 
    public float gravityStrength = 10f; // Adjust the strength of gravity
    public float maxGravityDistance = 100f; // Maximum distance for gravity to have an effect
    private bool isWithinGravityField = false; // Flag to track if spaceship is within gravity field
    public Color gravityColor = Color.yellow; // Color for the gizmo
    [SerializeField]TMPro.TextMeshProUGUI GravityFieldText;
    private Coroutine textCoroutine;
    public TMPro.TextMeshProUGUI PlanetNameText;
    private Coroutine exitTextCoroutine;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
                other.GetComponent<SpaceShip>().SetInGravityField(true); // Switch to in-gravity controls
                //Debug.Log("Spaceship entered " + planet.gameObject.name + " Gravity field");
       
                Rigidbody spaceshipRb = other.GetComponent<Rigidbody>();
                if (spaceshipRb != null)
                {
                    Vector3 directionToPlanet = planet.position - other.transform.position;
                    float distance = directionToPlanet.magnitude;

                    if (distance <= maxGravityDistance)
                    {
                        isWithinGravityField = true; // Set the flag to true
                        //Debug.Log("SpaceShip in " + planet.gameObject.name + " Gravity field");
                        float gravityFactor = Mathf.Clamp01(1 - distance / maxGravityDistance);
                        spaceshipRb.AddForce(directionToPlanet.normalized * gravityStrength * gravityFactor, ForceMode.Acceleration);
                    }
                    else if (isWithinGravityField) // Only log exit when previously inside
                    {
                        isWithinGravityField = false; // Reset the flag
                        //Debug.Log("SpaceShip exited " + planet.gameObject.name + " Gravity field");
                    }
                }
        }
        //if(other.CompareTag("Meteor"))
        //{
        //    Rigidbody spaceshipRb = other.GetComponent<Rigidbody>();
        //    if (spaceshipRb != null)
        //    {
        //        Vector3 directionToPlanet = planet.position - other.transform.position;
        //        float distance = directionToPlanet.magnitude;

        //        if (distance <= maxGravityDistance)
        //        {
        //            isWithinGravityField = true; // Set the flag to true
        //                                         //Debug.Log("SpaceShip in " + planet.gameObject.name + " Gravity field");
        //            float gravityFactor = Mathf.Clamp01(1 - distance / maxGravityDistance);
        //            spaceshipRb.AddForce(directionToPlanet.normalized * gravityStrength * gravityFactor, ForceMode.Acceleration);
        //        }
        //        else if (isWithinGravityField) // Only log exit when previously inside
        //        {
        //            isWithinGravityField = false; // Reset the flag
        //                                          //Debug.Log("SpaceShip exited " + planet.gameObject.name + " Gravity field");
        //        }
        //    }
        //}
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
            
            PlanetNameText.text ="Environment: "+ planet.gameObject.name;

            
            if (textCoroutine != null)
            {
                StopCoroutine(textCoroutine);
            }
            textCoroutine = StartCoroutine(ShowGravityFieldMessage("Entering"));

        }
    }
    // Coroutine to display the text and then make it disappear
    private IEnumerator ShowGravityFieldMessage(string text)
    {
        GravityFieldText.text = text+ " "+gameObject.transform.parent.gameObject.name+" Gravity Field";
        // Fade in
        for (float alpha = 0f; alpha <= 1f; alpha += Time.deltaTime / 2f) // Fade in over 2 seconds
        {
            GravityFieldText.color = new Color(GravityFieldText.color.r, GravityFieldText.color.g, GravityFieldText.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Fade out
        for (float alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime / 2f) // Fade out over 2 seconds
        {
            GravityFieldText.color = new Color(GravityFieldText.color.r, GravityFieldText.color.g, GravityFieldText.color.b, alpha);
            yield return null;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
            // Set the planet name text to "Space"
            PlanetNameText.text = "Environment: Space ";
            other.GetComponent<SpaceShip>().SetInGravityField(false); // Switch to space controls
            Debug.Log("Spaceship exited " + planet.gameObject.name + " Gravity field");
            // Set the planet name text to "Space"
            PlanetNameText.text = "Space";

            // Start the exit message coroutine
            if (exitTextCoroutine != null)
            {
                StopCoroutine(exitTextCoroutine);
            }
            exitTextCoroutine = StartCoroutine(ShowGravityFieldMessage("Exiting"));

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = gravityColor; // Choose a color for the gizmo
        Gizmos.DrawWireSphere(planet.position, maxGravityDistance);
    }
}

