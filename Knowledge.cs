using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Knowledge : MonoBehaviour
{
    [Header("Static")]
    public TMP_Text TMPknowledge;
    public GameObject levelBar;

    [Header("Audio")]
    public AudioClip levelUpSound;

    [HideInInspector] public float knowledge;
    [HideInInspector] public float playerLevel;

    PlayerData playerData;
    GameObject player;
    Help help;
    float experienceNeeded = 100;
    bool thresholdIncreased = false;
    bool firstLeveled = false;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        help = FindObjectOfType<Help>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerLevel = 0;
    }

    private void Update()
    {
        if (playerData.playerStats.experience >= experienceNeeded && thresholdIncreased == false)
        {
            knowledge += 1;
            playerLevel += 1;
            StartCoroutine(levelBar.GetComponent<Bars>().DisplayUpgradesAvailable());
            AudioSource.PlayClipAtPoint(levelUpSound, player.transform.position, 0.20f);
            if (firstLeveled == false)
            { 
                help.DisplayHelp("You've have gathered enough experience to level up, which grant you 1 knowledge point. Points can be spent to upgrade weapons", 15f);
                firstLeveled = true;
            }
            experienceNeeded += 100;
            thresholdIncreased = true;
            thresholdIncreased = false;
        }

        if (knowledge > 0)
        {
            TMPknowledge.text = "Available knowledge: " + "<size=30><color=#E0E300><b>" + knowledge.ToString() + "</size></color></b>";
        }
        else
        {
            TMPknowledge.text = "Available knowledge:  " + "<size=30><color=#E0E300><b>" + "0" + "</size></color></b>";
        }
    }
}
