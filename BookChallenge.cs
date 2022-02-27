using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookChallenge : MonoBehaviour
{
    [Header("Public")]
    public GameObject doorTrigger;

    [Header("Static")]
    public Animator animator;

    PlayerData playerData;
    Dialogue dialogue;
    bool completed = false;
    string currentAnim;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        dialogue = FindObjectOfType<Dialogue>();

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }
    }

    public void OpenDoor()
    {
        if (doorTrigger != null && completed == false)
        {
            doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
            PlayAnimation("Book_move", 0.1f);
            playerData.playerStats.experience += 30f;

            dialogue.DisplayDialogue("I hear something opening.", 5f);
            print("Book");
        }
        completed = true;
    }

    private void PlayAnimation(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim) return; //If current animation is the same, do nothing
        animator.CrossFade(newAnim, fadeDuration);
        currentAnim = newAnim;  //New animation is now current animation
    }
}
