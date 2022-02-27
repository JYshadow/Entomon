using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeltingChallenge : MonoBehaviour
{
    [Header("Public")]
    public GameObject doorTrigger;

    [Header("Static")]
    public Light tempLight;

    [HideInInspector] public int finished;

    GameObject worldCanvas;
    TMP_Text displayTMP;
    PlayerData playerData;
    bool completed = false;
    public float temperature;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        worldCanvas = GameObject.FindGameObjectWithTag("WorldCanvas");
        displayTMP = worldCanvas.transform.Find("MeltingDisplay").GetComponent<TMP_Text>();

        temperature = 0;

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }
    }

    private void Update()
    {
        tempLight.intensity = (temperature / 2000f) * 2f;

        displayTMP.text = temperature.ToString() + " °C";

        if (temperature < 1)
        {
            temperature = 0;
        }
        if (temperature > 1999)
        {
            temperature = 2000;
        }

        if (finished >= 3 && completed == false)
        {
            if (doorTrigger != null)
            {
                doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
                doorTrigger.GetComponent<DoorTrigger>().canTrigger = true;
                playerData.playerStats.experience += 30f;
                print("Melting");
                completed = true;
            }
        }
    }
}
