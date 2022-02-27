using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon7 : WeaponWithParticle, Trigger   //This script is of trigger family
{
    public GameObject projectile;
    public Transform muzzle;

    float nextShotTime;
    public int charge = 0;

    public float time;


    WeaponData.Weapon7Stats weapon7Stats;

    void Start()
    {
        //Read weapon data
        weapon7Stats = JsonUtility.FromJson<WeaponData.Weapon7Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon7.json"));
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation);
        }
    }

    public void Shoot() //Charging
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weapon7Stats.msBetweenShots / 1000;

            charge = charge + 1;
        }
    }
}