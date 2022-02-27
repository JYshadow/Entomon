using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon2 : MonoBehaviour, Trigger   //This script is of trigger family
{
    public Transform muzzle;
    public GameObject projectile;

    float rotationOffset = 10;
    float nextShotTime;

    WeaponData.Weapon2Stats weapon2Stats;

    void Start()
    {
        //Read WeaponData
         weapon2Stats = JsonUtility.FromJson<WeaponData.Weapon2Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon2.json"));
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weapon2Stats.msBetweenShots / 1000;

            Instantiate(projectile, muzzle.position, Quaternion.AngleAxis(0, transform.up) * muzzle.rotation);  //middle

            for (int i = 0; i <= weapon2Stats.sideProjectiles; i++)
            {
                Instantiate(projectile, muzzle.position, Quaternion.AngleAxis(i * -rotationOffset, transform.up) * muzzle.rotation);    //left
                Instantiate(projectile, muzzle.position, Quaternion.AngleAxis(i * rotationOffset, transform.up) * muzzle.rotation); //right
            }
        }
    }
}