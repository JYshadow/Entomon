using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip stingerSound;

    [HideInInspector] public float damage;

    GameObject player;
    float radius;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, transform.localScale.x / 15f, LayerMask.GetMask("Player"));
        if (initialCollision.Length > 0)
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");

            AudioSource.PlayClipAtPoint(stingerSound, transform.position, 0.05f);

            Destroy(gameObject);
        }

        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        float moveDistance = 5f * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        radius = transform.localScale.x / 15f;

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, moveDistance + 0.1f, LayerMask.GetMask("Player"), QueryTriggerInteraction.Collide))
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");

            AudioSource.PlayClipAtPoint(stingerSound, transform.position, 0.05f);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 15f);
    }
}
