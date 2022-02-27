using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon11Explosion : MonoBehaviour
{
    [Header("Public")]
    public AudioClip explosionSound;

    [Header("Static")]
    public GameObject puddle;

    WeaponData weaponData;
    float distanceToGround;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();

        transform.localScale = new Vector3(weaponData.weapon11Stats.radius, weaponData.weapon11Stats.radius, weaponData.weapon11Stats.radius);

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, LayerMask.GetMask("EnemyHitbox"));
        foreach (Collider col in colliders)
        {
            col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon11Stats.damage, "Normal");
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
        {
            distanceToGround = hit.distance;
        }

        Instantiate(puddle, transform.position + Vector3.down * distanceToGround * 0.99f, Quaternion.identity); //Instantiate puddle on the ground

        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.25f);

        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }
}
