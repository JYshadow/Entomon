using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;
using System;

public class Weapon4 : MonoBehaviour, Trigger
{
    public Transform muzzle;
    public GameObject projectile;
    public GameObject turret;
    public GameObject model;

    bool placeable;
    float placeableRange = 0.5f;
    float nextShotTime;
    float wakingTime = 2;
    bool placed = false;
    bool awake;
    bool empty;
    Material material1;
    Material material2;
    ChargesManager chargesManager;
    WeaponController weaponController;
    WeaponData.Weapon4Stats weapon4Stats;

    void Start()
    {
        //Read WeaponData
        weapon4Stats = JsonUtility.FromJson<WeaponData.Weapon4Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon4.json"));

        //Other
        weaponController = FindObjectOfType<WeaponController>();
        chargesManager = FindObjectOfType<ChargesManager>();

        material1 = model.transform.GetChild(0).GetComponent<Renderer>().material;
        material2 = model.transform.GetChild(1).GetComponent<Renderer>().material;

        StartCoroutine(Waking());
        StartCoroutine(Emptying());
    }

    void Update()
    {
        //Check if turret is placeable
        if (placed == false)  //only check if it has not been placed
        {
            Collider[] obstacleColliders = Physics.OverlapSphere(transform.position, placeableRange, (LayerMask.GetMask("Obstacle") | LayerMask.GetMask("Interactable")));

            if (obstacleColliders.Length > 0)
            {
                placeable = false;
                material1.color = Color.red;
                material2.color = Color.red;

            }
            else
            {
                placeable = true;
                material1.color = Color.yellow;
                material2.color = Color.yellow;
            }
        }

        //Find closest enemy and shoot
        if (placed == true && awake == true && empty == false)
        {
            float nearest = Mathf.Infinity;
            Collider nearestEnemy = null;

            foreach (Collider enemyCollider in Physics.OverlapSphere(transform.position, weapon4Stats.range, LayerMask.GetMask("EnemyHitbox")))
            {
                float distSqr = (enemyCollider.transform.position - transform.position).sqrMagnitude;
                if (distSqr < nearest)
                {
                    nearest = distSqr;
                    nearestEnemy = enemyCollider;
                }
            }

            if (nearestEnemy != null)
            {
                transform.LookAt(nearestEnemy.gameObject.transform.position);
                material1.color = new Color(1, 0.58824f, 0);
                material2.color = new Color(1, 0.58824f, 0);

                if (Time.time > nextShotTime) //shooting
                {
                    nextShotTime = Time.time + weapon4Stats.msBetweenShots / 1000;

                    Instantiate(projectile, muzzle.position, muzzle.rotation);
                }
            }
            else
            {
                material1.color = Color.yellow;
                material2.color = Color.yellow;
            }
        }
    }

    public void Shoot() //place turret instead of shoot
    {
        if (chargesManager.weapon4Charges > 0 && placeable == true)
        {
            chargesManager.weapon4Charges = chargesManager.weapon4Charges - 1;

            GameObject placedTurret = Instantiate(turret, transform.position, transform.rotation) as GameObject;
            placedTurret.GetComponent<Weapon4>().placed = true;  //will be overidden if 'placed' is set to false in void Start
            placedTurret.GetComponent<Weapon4>().placeable = false;
            placedTurret.layer = 3; //give pickable layer

            if (chargesManager.weapon4Charges == 0)
            {
                Destroy(weaponController.currentWeapon); //destroy it after placing the last turret
            }
        }
    }

    IEnumerator Waking()
    {
        awake = false;
        material1.color = Color.black;
        material2.color = Color.black;
        yield return new WaitForSeconds(wakingTime);
        awake = true;
    }

    IEnumerator Emptying()
    {
        empty = false;
        yield return new WaitForSeconds(wakingTime + weapon4Stats.lifetime);
        empty = true;
        material1.color = Color.black;
        material2.color = Color.black;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weapon4Stats.range);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, placeableRange);
    }
}
