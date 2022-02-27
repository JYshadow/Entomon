using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSlam : MonoBehaviour
{
    GameObject player;
    public float damage;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 7f, LayerMask.GetMask("Player"));
        foreach (Collider col in colliders)
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "normal");
        }

        Destroy(gameObject, 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x * 7f);
    }
}
