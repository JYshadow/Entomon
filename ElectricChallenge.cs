using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricChallenge : MonoBehaviour
{
    [Header("Public")]
    public GameObject doorTrigger;

    [Header("Static")]
    public ParticleSystem shockParticleSystem;

    PlayerData playerData;
    bool completed = false;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }
    }

    public void CompleteChallenge()
    {
        if (doorTrigger != null && completed == false)
        {
            doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
            shockParticleSystem.Play();
            playerData.playerStats.experience += 30f;
            print("Electric");
        }
        completed = true;
    }
}
