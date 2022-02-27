using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon5 : WeaponWithParticle, Trigger   //This script is of trigger family
{
    [Header("Static")]
    public Transform muzzle;
    public float heat = 0;

    [Header("Audio")]
    public AudioSource warmingupSound;
    public AudioSource torchSound;
    public AudioSource overheatedSound;

    WeaponData weaponData;
    List<int> enemyID = new List<int>(); //make a list with all instance ID the projectile finds
    RaycastHit[] hitsCenter;
    RaycastHit[] hitsRight;
    RaycastHit[] hitsLeft;
    float heatMultiplier = 0;
    float nextShotTime;
    int extraProjectiles = 4;
    float scale = 13;
    float nextAddHeatTime;
    float nextReduceHeatTime;
    float warmUpTimer = 0;
    float overheatTimer = 0;
    float overheatedTime = 5;
    bool canWarmUp = true;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();

        //Particles
        var particleMainFire = particles[0].main;
        particleMainFire.startLifetime = new ParticleSystem.MinMaxCurve(0.8f * weaponData.weapon5Stats.range / scale, weaponData.weapon5Stats.range / scale);
        particleMainFire.startDelay = weaponData.weapon5Stats.warmedUpTime;

        var particleLimitVelocityFire = particles[0].limitVelocityOverLifetime;
        particleLimitVelocityFire.limit = weaponData.weapon5Stats.range;

        var particleMainSmoke = particles[1].GetComponent<ParticleSystem>().main;
        particleMainSmoke.startLifetime = new ParticleSystem.MinMaxCurve(0.8f * weaponData.weapon5Stats.range / scale, weaponData.weapon5Stats.range / scale);
        particleMainSmoke.startDelay = weaponData.weapon5Stats.warmedUpTime * 0.8f * 2f;

        var particleLimitVelocitySmoke = particles[2].GetComponent<ParticleSystem>().limitVelocityOverLifetime;
        particleLimitVelocitySmoke.limit = weaponData.weapon5Stats.range * 1.1f;
    }

    private void Update()
    {
        //Debug
        Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * weaponData.weapon5Stats.range, Color.red); //forward line
        for (int i = 1; i <= extraProjectiles; i++) //for loop to create a certain amount of lines
        {
            Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + Quaternion.AngleAxis(i * weaponData.weapon5Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward * weaponData.weapon5Stats.range, Color.blue);    //right line, rotate the end by a certain axis
            Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + Quaternion.AngleAxis(i * -weaponData.weapon5Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward * weaponData.weapon5Stats.range, Color.blue);  //left line, rotate the end by a certain exis
        }

        //Heat
        if (heat < 0)
        {
            heat = 0;
        }

        if (heat > weaponData.weapon5Stats.maxHeat)
        {
            heat = weaponData.weapon5Stats.maxHeat;
            StartCoroutine(Overheat());
        }

        if (playable == true)
        {
            if (Time.time > nextReduceHeatTime && heat < 100)
            {
                nextReduceHeatTime = Time.time + 0.1f;

                heat = heat - 2f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                canWarmUp = true;
                warmUpTimer = 0;
                heatMultiplier = 0;

                warmingupSound.Stop();
                torchSound.Stop();
                //StopParticle();
            }

            if (Input.GetMouseButton(0))    //Start warmup only on button down, not continuous detection
            {
                if (canWarmUp == true)
                {
                    StartCoroutine(WarmUp());
                    canWarmUp = false;
                }

                if (warmingupSound.isPlaying == false && warmUpTimer < weaponData.weapon5Stats.warmedUpTime)
                {
                    warmingupSound.Play();
                }
                if (torchSound.isPlaying == false && warmUpTimer >= weaponData.weapon5Stats.warmedUpTime)
                {
                    torchSound.Play();
                }
            }

        }
    }
    public void Shoot()
    {
        if (playable == true && warmUpTimer >= weaponData.weapon5Stats.warmedUpTime)
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + weaponData.weapon5Stats.msBetweenShots / 1000;

                //Generating raycast for left, center, and right
                hitsCenter = Physics.RaycastAll(muzzle.transform.position, muzzle.transform.forward, weaponData.weapon5Stats.range, LayerMask.GetMask("EnemyHitbox"));

                for (int i = 0; i <= extraProjectiles; i++)
                {
                    hitsRight = Physics.RaycastAll(muzzle.transform.position, Quaternion.AngleAxis(i * weaponData.weapon5Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward, weaponData.weapon5Stats.range, LayerMask.GetMask("EnemyHitbox")); //Right raycasts that only hit enemies
                }

                for (int i = 0; i <= extraProjectiles; i++)
                {
                    hitsLeft = Physics.RaycastAll(muzzle.transform.position, Quaternion.AngleAxis(i * -weaponData.weapon5Stats.angle / extraProjectiles / 2, Vector3.up) * muzzle.transform.forward, weaponData.weapon5Stats.range, LayerMask.GetMask("EnemyHitbox")); //Left raycasts that only hit enemies
                }

                //Registering hits for left, center, and right
                for (int y = 0; y < hitsCenter.Length; y++)
                {
                    if (enemyID.Contains(hitsCenter[y].collider.GetInstanceID()) == false)
                    {
                        hitsCenter[y].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon5Stats.damage, "Fire");
                        enemyID.Add(hitsCenter[y].collider.GetInstanceID());
                    }
                }

                for (int y = 0; y < hitsRight.Length; y++)
                {
                    if (enemyID.Contains(hitsRight[y].collider.GetInstanceID()) == false)
                    {
                        hitsRight[y].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon5Stats.damage, "Fire");
                        enemyID.Add(hitsRight[y].collider.GetInstanceID());
                    }
                }

                for (int y = 0; y < hitsLeft.Length; y++)
                {
                    if (enemyID.Contains(hitsLeft[y].collider.GetInstanceID()) == false)
                    {
                        hitsLeft[y].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon5Stats.damage, "Fire");
                        enemyID.Add(hitsLeft[y].collider.GetInstanceID());
                    }
                }

                enemyID.Clear();
            }

            if (Time.time > nextAddHeatTime)
            {
                nextAddHeatTime = Time.time + 0.1f;

                heat = heat + 1 + heatMultiplier;
                heatMultiplier = heatMultiplier + 0.05f;
            }
        }
    }

    private IEnumerator WarmUp()
    {
        while (warmUpTimer < weaponData.weapon5Stats.warmedUpTime)
        {
            warmUpTimer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Overheat()
    {
        canWarmUp = true;
        warmUpTimer = 0;
        heatMultiplier = 0;
        StopParticle();

        warmingupSound.Stop();
        torchSound.Stop();

        while (overheatTimer < overheatedTime)
        {
            overheatTimer = overheatTimer + Time.deltaTime;
            playable = false;

            if (overheatedSound.isPlaying == false)
            {
                overheatedSound.Play();
            }

            yield return null;
        }

        if (overheatTimer >= overheatedTime)
        {
            heat = 50;
            playable = true;
            overheatTimer = 0;

            overheatedSound.Stop();
        }
    }
}