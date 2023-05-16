using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscConf : MonoBehaviour
{
    public GameObject escConfPanel;
    public GameObject escPanel;
    public GameObject exitBtn;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        escConfPanel.SetActive(false);
        //escPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ExitBtn()
    {
        escPanel.SetActive(false);
        escConfPanel.SetActive(true);
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
        escConfPanel.SetActive(false);
    }
}

