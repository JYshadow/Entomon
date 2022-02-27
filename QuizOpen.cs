using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizOpen : MonoBehaviour
{
    [Header("Public")]
    public string[] correctAnswers;
    public bool numbersOnly;
    public GameObject doorTrigger;
    public bool firstQuiz;
    public enum DisplayerType { Lockpad, Console, Numberlock }
    public DisplayerType displayerType;

    [Header("Static")]
    public GameObject inputField;
    public GameObject correctBorder;

    [Header("Audio")]
    public AudioClip correctAudio;
    public AudioClip wrongAudio;

    Transform parentTransform;
    EventsManager eventsManager;
    GameObject player;
    PlayerData playerData;
    bool completed = false;

    private void Awake()
    {
        parentTransform = transform.parent;
    }

    private void Start()
    {
        eventsManager = FindObjectOfType<EventsManager>();
        playerData = FindObjectOfType<PlayerData>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }

        if (numbersOnly == true)
        {
            inputField.GetComponent<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.Digit;
        }
        else
        {
            inputField.GetComponent<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.None;
        }

        correctBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    private void Update()
    {
        if (inputField.GetComponent<TMP_InputField>().text == "")
        {
            inputField.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            inputField.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void CheckAnswer()
    {
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            if (inputField.GetComponent<TMP_InputField>().text.ToLower() == correctAnswers[i].ToLower() && completed == false)
            {
                if (doorTrigger != null && firstQuiz == false)
                {
                    doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
                }

                if (firstQuiz == true)
                {
                    StartCoroutine(eventsManager.FirstQuizCompleted());
                }

                if (displayerType == DisplayerType.Lockpad)
                {
                    playerData.playerStats.experience += 20f;
                    completed = true;
                }
                else if (displayerType == DisplayerType.Console)
                {
                    playerData.playerStats.experience += 40f;
                    completed = true;
                }

                correctBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                AudioSource.PlayClipAtPoint(correctAudio, player.transform.position, 0.35f);

                print("correct");
                break;
            }

            if (i == correctAnswers.Length - 1)
            {
                player.GetComponent<LivingEntity>().TakeDamage(200, "Shock");
                AudioSource.PlayClipAtPoint(wrongAudio, player.transform.position, 0.2f);
            }
        }
    }

    public void RetractToParent()
    {
        transform.SetParent(parentTransform);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }
}
