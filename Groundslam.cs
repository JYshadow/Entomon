using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundslam : MonoBehaviour
{
    [HideInInspector] public float damage;
    
    GameObject player;
    bool firstHit = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, LayerMask.GetMask("Player"));
        if (initialCollision.Length > 0)
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");
            firstHit = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 12 && firstHit == false) //12 = player
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");
        }
    }
}
