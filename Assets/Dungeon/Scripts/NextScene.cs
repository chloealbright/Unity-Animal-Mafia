using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    // 2nd option private Vector2 targetPosition; // store player position for next scene
    //MAYBE DELETE IF WE CAN POSITION WITHOUT 
    public Dungeon_VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    //Dungeon_VectorValue startingPosition; //for scene transition player position
    //Dungeon_VectorValue startingPosition;

    // Start is called before the first frame update
    void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
        
    }

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

            //transform.position = startingPosition.initialValue;

            transform.position = playerPosition;

            // playerStorage.targetValue = playerPosition;


            //transform.position = startingPosition.initialValue; //Update for Scene Change Player Position

            //playerStorage.initialValue = playerPosition;
            //2nd Option playerPosition =  targetPosition;
            //OLD playerStorage.initialValue = playerPosition;
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
