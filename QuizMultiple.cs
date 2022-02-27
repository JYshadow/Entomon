using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizMultiple : MonoBehaviour
{
    [Header("Public")]
    public int correctAnswer;
    public GameObject doorTrigger;
    public enum DisplayerType { Lockpad, Console, Numberlock }
    public DisplayerType displayerType;

    [Header("Static")]
    public GameObject correctBorder;

    [Header("Audio")]
    public AudioClip correctAudio;
    public AudioClip wrongAudio;

    bool completed = false;
    Transform parentTransform;
    GameObject player;
    PlayerData playerData;

    private void Awake()
    {
        parentTransform = transform.parent;
    }

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }

        correctBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void SelectAnswer(int answerNumber)
    {
        if (answerNumber == correctAnswer && completed == false)
        {
            if (doorTrigger != null)
            {
                doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
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
            else if (displayerType == DisplayerType.Numberlock)
            {
                playerData.playerStats.experience += 30f;
                completed = true;
            }

            correctBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            AudioSource.PlayClipAtPoint(correctAudio, player.transform.position, 0.35f);

            print("correct");
        }
        else if (answerNumber != correctAnswer && completed == false)
        {
            player.GetComponent<LivingEntity>().TakeDamage(200, "Shock");
            AudioSource.PlayClipAtPoint(wrongAudio, player.transform.position, 0.2f);
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
