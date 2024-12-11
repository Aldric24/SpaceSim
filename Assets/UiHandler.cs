using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    private const float CameraRotationSpeed = 5.0f; // Constant for camera rotation speed
    [SerializeField] GameObject camera;
    [SerializeField] GameObject ControlPanel;
  
    [SerializeField] GameObject main_menu;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the camera in the x-axis
        camera.transform.Rotate(Vector3.right * CameraRotationSpeed * Time.deltaTime);
    }

    public void OnBackButtonClick()
    {
        ControlPanel.SetActive(false);
        main_menu.SetActive(true);
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Planet System");
    }

    public void OnControlsButtonClick()
    {
        main_menu.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
