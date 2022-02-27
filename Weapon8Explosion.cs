using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon8Explosion : MonoBehaviour
{
    [Header("Public")]
    public AudioClip explosionSound;

    WeaponData weaponData;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();

        transform.localScale = new Vector3(weaponData.weapon8Stats.radius, weaponData.weapon8Stats.radius, weaponData.weapon8Stats.radius);

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, LayerMask.GetMask("EnemyHitbox"));
        foreach (Collider col in colliders)
        {
            col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon8Stats.damage, "Water");
        }

        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f);

        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }
}
