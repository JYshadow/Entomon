using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon12 : Weapon, Trigger
{
    public Transform muzzle;
    public GameObject projectile;

    [Header("Audio")]
    public AudioSource pressSoundSingle;

    float rotationOffset = 5f;
    float nextShotTime;

    WeaponData weaponData;
    HandAnimation handAnimation;

    private void Start()
    {
        //Read WeaponData
        weaponData = FindObjectOfType<WeaponData>();
        handAnimation = FindObjectOfType<HandAnimation>();
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weaponData.weapon12Stats.msBetweenShots / 1000;

            GameObject middleProj = Instantiate(projectile, muzzle.position, Quaternion.AngleAxis(0, transform.up) * muzzle.rotation);  //middle
            middleProj.GetComponent<Weapon12Proj>().canPlayHitSound = true;

            if (weaponData.weapon12Stats.sideProjectiles > 0)
            {
                for (int i = 1; i < (weaponData.weapon12Stats.sideProjectiles + 1); i++)
                {
                    Instantiate(projectile, muzzle.position, Quaternion.AngleAxis(i * -rotationOffset, transform.up) * muzzle.rotation);    //left
                    Instantiate(projectile, muzzle.position, Quaternion.AngleAxis(i * rotationOffset, transform.up) * muzzle.rotation); //right
                }
            }

            StartCoroutine(PressAndRelease());

            pressSoundSingle.Play();
        }
    }

    private IEnumerator PressAndRelease()
    {
        handAnimation.PlayHandAnimation("Hands_weapon12_press", 0.1f);
        yield return new WaitForSeconds(0.25f);
        handAnimation.PlayHandAnimation("Hands_weapon12_release", 0.1f);
    }
}
