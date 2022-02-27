using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon10Explosion : MonoBehaviour
{
    [Header("Public")]
    public AudioClip explosionSound;

    [HideInInspector] public List<GameObject> slowedEnemies = new List<GameObject>();

    WeaponData weaponData;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();

        transform.localScale = new Vector3(weaponData.weapon10Stats.radius, weaponData.weapon10Stats.radius, weaponData.weapon10Stats.radius);

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, LayerMask.GetMask("EnemyHitbox"));
        foreach (Collider col in colliders)
        {
            col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon10Stats.damage, "Cold");

            if (col.gameObject.layer == 8 && !slowedEnemies.Contains(col.transform.parent.gameObject)) //8 = EnemyHitbox (trigger)
            {
                slowedEnemies.Add(col.transform.parent.gameObject);
                col.transform.parent.gameObject.GetComponent<LivingEntity>().Slow(30f);
            }
        }

        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.25f);

        StartCoroutine(DestroyAndUnslow());
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

    private IEnumerator DestroyAndUnslow()
    {
        yield return new WaitForSeconds(2f);
        foreach (GameObject slowedEnemy in slowedEnemies)
        {
            slowedEnemy.GetComponent<LivingEntity>().ResetMovespeed(30f);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }
}
