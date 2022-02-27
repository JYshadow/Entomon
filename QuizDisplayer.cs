using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizDisplayer : MonoBehaviour
{
    [Header("Public")]
    public GameObject quizObject;
    [TextArea(5, 20)] public string lockpadDialogue;
    public float lockpadDialogueDuration;

    [HideInInspector] public bool opened;

    GameObject quizContainer;
    Help help;
    Dialogue dialogue;

    private void Start()
    {
        quizContainer = FindObjectOfType<Quiz>().gameObject.transform.Find("QuizContainer").gameObject;
        help = FindObjectOfType<Help>();
        dialogue = FindObjectOfType<Dialogue>();

        //Disable all UI Quiz-input on start
        Image[] imageComponents = GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            imageComponent.enabled = false;
        }
        TMP_Text[] textComponents = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            textComponent.enabled = false;
        }
        TMP_InputField[] textInputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField textInputField in textInputFields)
        {
            textInputField.enabled = false;
        }
    }

    public void InsertQuiz()
    {
        //Enable all UI Quiz-input on start
        Image[] imageComponents = GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            imageComponent.enabled = true;
        }
        TMP_Text[] textComponents = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            textComponent.enabled = true;
        }
        TMP_InputField[] textInputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField textInputField in textInputFields)
        {
            textInputField.enabled = true;
        }

        if (quizContainer.transform.childCount > 0)
        {
            SendMessage("RetractToParent", SendMessageOptions.DontRequireReceiver);
        }
        quizObject.transform.SetParent(quizContainer.transform);
        quizObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        quizObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
        quizObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        if (gameObject.name == "Lockpad")
        {
            help.DisplayHelp("The facility has bolstered its security. Prove you're an authorized scientist by answering this question. Correct answers reward experience-points", 10);
            if (lockpadDialogue != "")
            {
                dialogue.DisplayDialogue(lockpadDialogue, lockpadDialogueDuration);
            }
        }
        if (gameObject.name == "Console")
        {
            help.DisplayHelp("Correct answers reward experience-points", 6);
        }
    }
}
