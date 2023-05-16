using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenPanel : MonoBehaviour
{

    public GameObject fadeOutPanel;
    public float fadeWait;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeController());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeController()
    {
        if (fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
        yield return new WaitForSeconds(fadeWait);
    }
}
