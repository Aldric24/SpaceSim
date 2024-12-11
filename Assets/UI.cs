using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject UIPanel;
    [SerializeField]TextMeshProUGUI timescaletext;
    [SerializeField]TextMeshProUGUI timescaleUItext;
    [SerializeField] int timeScaleint = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScaleint;
        timescaletext.text = "Sim Time: "+timeScaleint+"X";
        timescaleUItext.text = timeScaleint + "X";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }
    void PauseGame()
    {

        UIPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }
    public void increaseTimescale()
    {
        timeScaleint++;
        timescaletext.text = "Sim Time: " + timeScaleint + "X";
        timescaleUItext.text = timeScaleint + "X";
        
    }
    public void DecreaseTimescale()
    {
        timeScaleint--;
        timescaletext.text = "Sim Time: " + timeScaleint + "X";
        timescaleUItext.text = timeScaleint + "X";
        
    }
    void ResumeGame()
    {
        Time.timeScale = timeScaleint;

        UIPanel.SetActive(false);
    }
    public void OnbuttonclickMainMenu()
    {
       SceneManager.LoadScene("MainMenu");
    }

}
