using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    public GameObject dialoguePanel;


    [SerializeField]
    private TextAsset inkJSON;
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        bubble.SetActive(false);
        panel.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            panel.SetActive(true);
            bubble.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            panel.SetActive(false);
            bubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
