using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public GameObject gearBtn;
    public GameObject escPanel;
    public GameObject resumeBtn;
    // Start is called before the first frame update
    void Start()
    {
        gearBtn.SetActive(true);
        escPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!escPanel.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                escPanel.SetActive(true);
                gearBtn.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                escPanel.SetActive(false);
                gearBtn.SetActive(true);
            }
        }
    }

    public void GearButton()
    {
        escPanel.SetActive(true);
        gearBtn.SetActive(false);
    }

    public void ResumeBtn()
    {
        escPanel.SetActive(false);
        gearBtn.SetActive(true);
    }
}
