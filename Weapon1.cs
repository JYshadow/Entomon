using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon1 : WeaponWithParticle, Trigger   //This script is of trigger family
{
    [Header("Static")]
    public Transform muzzle;

    WeaponData weaponData;
    RaycastHit[] hitsCenter;
    RaycastHit[] hitsRight;
    RaycastHit[] hitsLeft;
    List<int> enemyID = new List<int>(); //make a list with all instance ID the projectile finds
    float nextShotTime;
    int extraProjectiles = 4;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();
    }

    private void Update()
    {
        Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * weaponData.weapon1Stats.range, Color.red); //forward line

        for(int i = 1; i <= extraProjectiles; i++) //for loop to create a certain amount of lines
        {
            Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + Quaternion.AngleAxis(i * weaponData.weapon1Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward * weaponData.weapon1Stats.range, Color.blue);    //right line, rotate the end by a certain axis
            Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + Quaternion.AngleAxis(i * -weaponData.weapon1Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward * weaponData.weapon1Stats.range, Color.blue);  //left line, rotate the end by a certain exis
        }

        //Particle
        var particleShape = particles[0].shape;
        particleShape.angle = weaponData.weapon1Stats.angle / 2;

        var particleMain = particles[0].main;
        particleMain.startLifetime = weaponData.weapon1Stats.range / 5;
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weaponData.weapon1Stats.msBetweenShots / 1000;

            Collider[] initialCollisions = Physics.OverlapSphere(muzzle.transform.position, 0.1f, LayerMask.GetMask("EnemyHitbox"));
            foreach (Collider initialCollision in initialCollisions)
            {
                initialCollision.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon1Stats.damage, "Normal");
            }

            //Generating raycast for left, center, and right

            hitsCenter = Physics.RaycastAll(muzzle.transform.position, muzzle.transform.forward, weaponData.weapon1Stats.range, LayerMask.GetMask("EnemyHitbox"));

            for (int i = 0; i <= extraProjectiles; i++)
            {
                hitsRight = Physics.RaycastAll(muzzle.transform.position, Quaternion.AngleAxis(i * weaponData.weapon1Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward, weaponData.weapon1Stats.range, LayerMask.GetMask("EnemyHitbox")); //Right raycasts that only hit enemies
            }

            for (int i = 0; i <= extraProjectiles; i++)
            {
                hitsLeft = Physics.RaycastAll(muzzle.transform.position, Quaternion.AngleAxis(i * -weaponData.weapon1Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward, weaponData.weapon1Stats.range, LayerMask.GetMask("EnemyHitbox")); //Left raycasts that only hit enemies
            }

            //Registering hits for left, center, and right

            for (int y = 0; y < hitsCenter.Length; y++)
            {
                if (enemyID.Contains(hitsCenter[y].collider.GetInstanceID()) == false)
                {
                    hitsCenter[y].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon1Stats.damage, "Normal");
                    enemyID.Add(hitsCenter[y].collider.GetInstanceID());
                }
            }

            for (int y = 0; y < hitsRight.Length; y++)
            {
                if (enemyID.Contains(hitsRight[y].collider.GetInstanceID()) == false)
                {
                    hitsRight[y].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon1Stats.damage, "Normal");
                    enemyID.Add(hitsRight[y].collider.GetInstanceID());
                }
            }

            for (int y = 0; y < hitsLeft.Length; y++)
            {
                if (enemyID.Contains(hitsLeft[y].collider.GetInstanceID()) == false)
                {
                    hitsLeft[y].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon1Stats.damage, "Normal");
                    enemyID.Add(hitsLeft[y].collider.GetInstanceID());
                }
            }

            enemyID.Clear();
        }
    }
}