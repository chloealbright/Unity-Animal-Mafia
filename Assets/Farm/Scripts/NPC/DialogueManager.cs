using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private Story currentStory;

    public bool dialogueIsPlaying {  get; private set; }

    public GameObject contButton;

    private static DialogueManager instance;

    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than on in the Scene");
        }
        instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {

        contButton.SetActive(false);
        dialogueIsPlaying = false;
        dialoguePanel .SetActive(false);
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        //get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        contButton.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        contButton.SetActive(false);
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More Choices were given thn the UI can support");
        }

        int index = 0;
        //enable and initialize the choices up tp the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            contButton.SetActive(false);
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining choices the UI supports and make sure they're hidden
        for(int i = index; i < choices.Length; i++)
        {
            contButton.SetActive(true);
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
