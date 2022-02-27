using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon3 : MonoBehaviour, Trigger   //This script is of trigger family
{
    public Transform muzzle;
    public GameObject projectile;

    float nextShotTime;
    WeaponData.Weapon3Stats weapon3Stats;

    void Start()
    {
        //Read WeaponData
        weapon3Stats = JsonUtility.FromJson<WeaponData.Weapon3Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon3.json"));
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weapon3Stats.msBetweenShots / 1000;

            Instantiate(projectile, muzzle.position, muzzle.rotation);
        }
    }
}