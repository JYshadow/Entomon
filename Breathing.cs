using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing : MonoBehaviour
{
    Animator animator;
    string currentAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PlayBreathingAnimation("Breathing_soft", 0.0f);
    }

    private void PlayBreathingAnimation(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim) return; //if current animation is the same, do nothing
        animator.CrossFade(newAnim, fadeDuration);
        currentAnim = newAnim;  //new animation is now current animation
    }
}
