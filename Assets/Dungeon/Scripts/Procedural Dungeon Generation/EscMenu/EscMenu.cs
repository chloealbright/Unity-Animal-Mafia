using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject gearBtn;
    public GameObject escPanel;
    public GameObject resumeBtn;
    public GameObject exitBtn;
    public GameObject escConfPanel;
    public TMP_Text pauseText;

    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        gearBtn.SetActive(true);
        escPanel.SetActive(false);
        escConfPanel.SetActive(false);
        pauseText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!escPanel.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
            {
                PauseGame();
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
            {
                ResumeGame();
            }
        }
    }

    public void GearButton()
    {
        PauseGame();
    }

    public void ResumeBtn()
    {
        ResumeGame();
    }

    public void ExitBtn()
    {
        pauseText.enabled = false;
        escPanel.SetActive(false);
        escConfPanel.SetActive(true);
    }

    public void PauseGame()
    {
        pauseText.enabled = true;
        escPanel.SetActive(true);
        gearBtn.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseText.enabled = false;
        escPanel.SetActive(false);
        gearBtn.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadFarm()
    {
        SceneManager.LoadScene("Farm");
    }

    public void Confirm()
    {
        LoadFarm();
    }
    public void Refuse()
    {
        pauseText.enabled = false;
        escConfPanel.SetActive(false);
        gearBtn.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
