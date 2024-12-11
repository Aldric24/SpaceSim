using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    public float thrustForce = 10f;
    public float spaceRotationSpeed = 50f; // Sensitivity for space rotation
    public float gravityRotationSpeed = 30f; // Sensitivity for gravity rotation
    public float boosterForce = 50f;
    public float camerarotation = 50f;
    public Slider thrustSlider; // Reference to the UI Slider object
    public Slider SpacerotationSpeedSlider;
    public Slider GravityrotationSpeedSlider;
    public Slider camerarotationslider;
    public Slider booster;// Reference to the UI Slider object for rotation speed
    private Rigidbody rb;
    public float velocity;
    private bool isInGravityField = false;
    public Transform camera;
    [SerializeField] private Quaternion initialCameraRotation;
    [SerializeField] TextMeshProUGUI VelcoityText;// Store initial camera rotation
    [SerializeField] TextMeshProUGUI ControlsText;
    [SerializeField] TextMeshProUGUI DistanceText;
    [SerializeField] Transform reset;
    public float raycastDistance = 1000f;
    public float raycastInterval = 0.5f;

    void Start()
    {
        reset = transform;
        rb = GetComponent<Rigidbody>();
        // Set initial slider values (optional)
        thrustForce = thrustSlider.value;
        spaceRotationSpeed = SpacerotationSpeedSlider.value;
        gravityRotationSpeed = GravityrotationSpeedSlider.value;
        camerarotation = camerarotationslider.value;
        boosterForce = booster.value;
        
        StartCoroutine(RaycastToPlanet());

    }

    void FixedUpdate()
    {
        
        
        velocity = rb.velocity.magnitude;
        DisplayVelocity();
        if (isInGravityField)
        {
            HandleGravityControls();
        }
        else
        {
            HandleSpaceControls();
        }
        
    }

    void HandleGravityControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(transform.right * gravityRotationSpeed); // Pitch down
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(-transform.right * gravityRotationSpeed); // Pitch up
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.forward * gravityRotationSpeed); // Roll left
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-transform.forward * gravityRotationSpeed); // Roll right
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(-transform.up * gravityRotationSpeed); // Yaw left
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(transform.up * gravityRotationSpeed); // Yaw right
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * thrustForce); // Thrust forward
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(-transform.forward * thrustForce); // Thrust backward
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * boosterForce); // Vertical thrust
        }
        if(Input.GetKey(KeyCode.R))
        {
            ReturnSpaceShip();
        }
        // Mouse for camera control in gravity
        float horizontalMouseInput = Input.GetAxis("Mouse X");
        float verticalMouseInput = Input.GetAxis("Mouse Y");
        camera.transform.Rotate(Vector3.up, horizontalMouseInput * camerarotation * Time.deltaTime);
        camera.transform.Rotate(Vector3.left, verticalMouseInput * camerarotation * Time.deltaTime);
    }

    void HandleSpaceControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * thrustForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * thrustForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * thrustForce);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * thrustForce);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(-transform.forward * spaceRotationSpeed); // Roll left
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(transform.forward * spaceRotationSpeed); // Roll right
        }

        // Rotation using the mouse with spaceRotationSpeed
        float horizontalMouseInput = Input.GetAxis("Mouse X");
        float verticalMouseInput = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up, horizontalMouseInput * camerarotation * Time.deltaTime);
        transform.Rotate(Vector3.left, verticalMouseInput * camerarotation * Time.deltaTime);
    }

    // Called when the thrust slider value changes
    public void OnThrustSliderChanged()
    {
        thrustForce = thrustSlider.value;
    }

    // Called when the rotation speed slider value changes
    public void OnSpaceRotationSpeedSliderChanged()
    {
        spaceRotationSpeed = SpacerotationSpeedSlider.value;
    }
    public void OnGravityRotationSpeedSliderChanged()
    {
        gravityRotationSpeed = GravityrotationSpeedSlider.value;
    }

    // Called when the camera rotation slider value changes
    public void OnCameraRotationSliderChanged()
    {
        camerarotation = camerarotationslider.value;
    }

    // Called when the booster slider value changes
    public void OnBoosterSliderChanged()
    {
        boosterForce = booster.value;
    }

    void DisplayVelocity()
    {
        
        float speed = rb.velocity.magnitude;

        // Determine forward/backward direction
        float forwardSpeed = Vector3.Dot(transform.forward, rb.velocity);

        // Display the velocity with + or - sign
        string velocityString = (forwardSpeed > 0 ? "+" : "-") + speed.ToString("F1") + " Km/s";
        VelcoityText.text = "Velocity: " + velocityString;
    }

    public void SetInGravityField(bool inGravity)
    {
        isInGravityField = inGravity;

        if (!inGravity)
        {
            // Reset camera to initial rotation when switching to space controls
            ControlsText.text = "Space Controls Active";
            camera.rotation = initialCameraRotation;
        }
        else
        {
            ControlsText.text = "Planet Controls Active";
        }
    }

    IEnumerator RaycastToPlanet()
    {
        while (true)
        {
            RaycastHit hit;
            // Use camera's forward direction for raycast
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, raycastDistance))
            {
                
                float distanceToPlanet = hit.distance;
                Debug.Log("Raycast hit " + hit.transform.name);
                Debug.Log("Distance to planet: " + distanceToPlanet);
                DistanceText.text = hit.transform.name+" Distance: " + distanceToPlanet.ToString("F1") + " Km";
            }
            yield return new WaitForSeconds(raycastInterval);
        }
    }

    
    void DrawRaycastGizmo()
    {
        Gizmos.color = Color.red;
        // Use camera's forward direction for gizmo
        Gizmos.DrawRay(camera.transform.position, camera.transform.forward * raycastDistance);
    }

    void OnDrawGizmosSelected()
    {
        DrawRaycastGizmo();
    }
    

    void ReturnSpaceShip()
    {
        // Reset the spaceship's position and rotation
        transform.position = reset.position;
        transform.rotation = reset.rotation;

        // Reset the spaceship's velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}

