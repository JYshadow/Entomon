using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon9 : Weapon, Trigger, Throwable   //This script is of trigger family
{
    [Header("Static")]
    public GameObject projectile;

    [Header("Audio")]
    public AudioSource pressSoundSingle;

    float nextShotTime;
    WeaponData weaponData;
    PlayerController playerController;

    private void Awake()
    {
        weaponData = FindObjectOfType<WeaponData>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weaponData.weapon9Stats.msBetweenShots / 1000;

            playerController.ResetAnimationToIdle();

            pressSoundSingle.Play();
        }
    }

    public void ReleaseProjectile()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    public void HideModel()
    {
        transform.Find("Model").gameObject.SetActive(false);
    }

    public void ShowModel()
    {
        transform.Find("Model").gameObject.SetActive(true);
    }
}