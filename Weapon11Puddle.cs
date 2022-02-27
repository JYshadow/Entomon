using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
public class Weapon11Puddle : MonoBehaviour
{
    public List<GameObject> slowedEnemies = new List<GameObject>();

    WeaponData weaponData;
    ParticleSystem.MainModule particlePuddle;

    private void Awake()
    {
        weaponData = FindObjectOfType<WeaponData>();
        particlePuddle = GetComponent<ParticleSystem>().main;
    }

    private void Start()
    {
        transform.localScale = new Vector3(weaponData.weapon11Stats.puddleRadius, weaponData.weapon11Stats.puddleRadius, weaponData.weapon11Stats.puddleRadius);
        particlePuddle.startLifetime = weaponData.weapon11Stats.duration;

        StartCoroutine(DestroyPuddle());
    }

    private void Update()
    {
        for (var i = slowedEnemies.Count - 1; i > -1; i--)
        {
            if (slowedEnemies[i] == null)
            {
                slowedEnemies.RemoveAt(i);
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == 8 && !slowedEnemies.Contains(col.transform.parent.gameObject)) //8 = EnemyHitbox (trigger)
        {
            if (col != null && col.transform.parent != null && col.transform.parent.gameObject != null && col.transform.parent.gameObject.GetComponent<LivingEntity>() != null)
            {
                col.transform.parent.gameObject.GetComponent<LivingEntity>().Slow(weaponData.weapon11Stats.slowPercentage);
                slowedEnemies.Add(col.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 8 && slowedEnemies.Contains(col.transform.parent.gameObject)) //8 = EnemyHitbox (trigger)
        {
            slowedEnemies.Remove(col.transform.parent.gameObject);
            col.transform.parent.gameObject.GetComponent<LivingEntity>().ResetMovespeed(weaponData.weapon11Stats.slowPercentage);
        }
    }

    private IEnumerator DestroyPuddle()
    {
        yield return new WaitForSeconds(weaponData.weapon11Stats.duration);
        foreach (GameObject slowedEnemy in slowedEnemies)
        {
            slowedEnemy.GetComponent<LivingEntity>().ResetMovespeed(weaponData.weapon11Stats.slowPercentage);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }
}
