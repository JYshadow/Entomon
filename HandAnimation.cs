using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip dismantlingSound;
    public AudioClip grabbingSound;

    [HideInInspector] public bool playingGrabAnimation = false;
    [HideInInspector] public Animator handAnimator;

    WeaponController weaponController;
    PlayerController playerController;
    FirstPersonCamera firstPersonCamera;
    Transform weaponPosition;
    string currentAnim;

    private void Start()
    {
        handAnimator = GetComponent<Animator>();
        weaponController = FindObjectOfType<WeaponController>();
        playerController = FindObjectOfType<PlayerController>();
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
        weaponPosition = GameObject.FindGameObjectWithTag("WeaponPosition").transform;
    }

    public void PlayHandAnimation(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim) return; //If current animation is the same, do nothing
        handAnimator.CrossFade(newAnim, fadeDuration);
        currentAnim = newAnim;  //New animation is now current animation
    }

    //General animations
    public IEnumerator GrabItemAnimation()
    {
        playingGrabAnimation = true;
        AudioSource.PlayClipAtPoint(dismantlingSound, transform.position, 0.15f);
        yield return new WaitForSeconds(0.1f);
        PlayHandAnimation("Hands_dismantling", 0.1f);
        yield return new WaitForSeconds(1.5f);

        playingGrabAnimation = false;
        PlayHandAnimation("Hands_idle", 0.0f);
        weaponController.RestoreWeapon();
    }

    public IEnumerator GrabInstructionAnimation()
    {
        playingGrabAnimation = true;
        AudioSource.PlayClipAtPoint(grabbingSound, transform.position, 0.2f);
        yield return new WaitForSeconds(0.1f);
        PlayHandAnimation("Hands_grabbing", 0.1f);
        yield return new WaitForSeconds(1.5f);

        playingGrabAnimation = false;
        PlayHandAnimation("Hands_idle", 0.0f);
        weaponController.RestoreWeapon();
    }

    public void ReleaseThrowable()
    {
        weaponPosition.GetChild(0).GetComponent<Throwable>().ReleaseProjectile();
    }

    public void HideThrowable()
    {
        weaponPosition.GetChild(0).GetComponent<Throwable>().HideModel();
    }

    public void ShowThrowable()
    {
        weaponPosition.GetChild(0).GetComponent<Throwable>().ShowModel();
    }

    public void CanShootTrue()
    {
        playerController.canShoot = true;
    }
}
