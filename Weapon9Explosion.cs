using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon9Explosion : MonoBehaviour
{
    [Header("Public")]
    public AudioClip explosionSound;

    WeaponData weaponData;

    private void Awake()
    {
        weaponData = FindObjectOfType<WeaponData>();
    }

    private void Start()
    {
        transform.localScale = new Vector3(weaponData.weapon9Stats.radius, weaponData.weapon9Stats.radius, weaponData.weapon9Stats.radius);

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, LayerMask.GetMask("EnemyHitbox"));
        foreach (Collider col in colliders)
        {
            col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon9Stats.damage, "Fire");
        }

        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f);

        Destroy(gameObject, 5f);
    }
/*    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }*/
}
