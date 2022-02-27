using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon8 : Weapon, Trigger, Throwable   //This script is of trigger family
{
    [Header("Static")]
    public Transform muzzle;
    public GameObject projectile;

    [Header("Audio")]
    public AudioSource pressSoundSingle;

    float nextShotTime;
    WeaponData weaponData;
    PlayerController playerController;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weaponData.weapon8Stats.msBetweenShots / 1000;

            playerController.ResetAnimationToIdle();

            pressSoundSingle.Play();
        }
    }

    public void ReleaseProjectile()
    {
        Instantiate(projectile, muzzle.position, muzzle.rotation);
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