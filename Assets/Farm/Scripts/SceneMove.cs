using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public bool triggered = false;

    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
    //level move zoned enter, if collider is a player
    //move game to another scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        //can use other.GetComponent<Player>() to see if the game object has a player component
        //tags work too
        if (other.tag == "Player" && !other.isTrigger)
        {
            //player entered, so move level
            print("Switching scene to" + sceneBuildIndex);
            playerStorage.initialValue = playerPosition;
            triggered = true;
            StartCoroutine(FadeController());
            //SceneManager.LoadScene(sceneBuildIndex);
        }

    }

    public IEnumerator FadeController()
    {
        if(fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }

}
