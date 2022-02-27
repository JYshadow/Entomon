using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using UnityEditor;

public class Weapon6 : WeaponWithParticle, Trigger   //This script is of trigger family
{
    [Header("Static")]
    public GameObject projectile;
    public GameObject muzzle;

    WeaponData weaponData;
    float nextShotTime;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();        
        var particleMain = particles[0].main;
        particleMain.startLifetime = weaponData.weapon6Stats.viewRadius / 7;

        //var particleShape = particles[0].shape;
        //particleShape.angle = weaponData.weapon6Stats.viewAngle / 2;
    }

    private void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + DirFromAngle(-weaponData.weapon6Stats.viewAngle / 2, false) * weaponData.weapon6Stats.viewRadius);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(30, transform.up) * transform.forward, Color.red);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(-30, transform.up) * transform.forward, Color.red);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + weaponData.weapon6Stats.msBetweenShots / 1000;

            GameObject firstProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation);
            firstProjectile.GetComponent<Weapon6Proj>().firstChain = true;
            firstProjectile.GetComponent<Weapon6Proj>().chains = weaponData.weapon6Stats.chains;
            firstProjectile.GetComponent<Weapon6Proj>().source = muzzle;

            StartCoroutine(PowerboxRaycast());
        }
    }

    private IEnumerator PowerboxRaycast()
    {
        RaycastHit hit;

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("PowerHitbox"));
        if (initialCollision.Length > 0)
        {
            initialCollision[0].gameObject.GetComponent<ElectricChallenge>().CompleteChallenge();     
        }
        else if (Physics.Raycast(muzzle.transform.position, transform.TransformDirection(Vector3.forward), out hit, weaponData.weapon6Stats.viewRadius, LayerMask.GetMask("PowerHitbox")))
        {
            hit.collider.gameObject.GetComponent<ElectricChallenge>().CompleteChallenge();
        }
        yield return null;
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, weaponData.weapon6Stats.viewRadius);
    }

    private IEnumerator SpawnFirstChain()
    {
        if (playerController.firstShot == true)
        {
            yield return new WaitForSeconds(0.5f);
        }



        playerController.firstShot = false;
        yield return null;
    }*/
}