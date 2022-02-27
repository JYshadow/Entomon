using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip sludgeSound;

    [HideInInspector] public float damage;

    GameObject player;
    Rigidbody rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, transform.localScale.x * 0.75f, LayerMask.GetMask("Player"));
        if (initialCollision.Length > 0)
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");

            AudioSource.PlayClipAtPoint(sludgeSound, transform.position, 0.05f);

            Destroy(gameObject);
        }

        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        float moveDistance = 3f * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        rb.AddForce(new Vector3(0, -1.0f, 0));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 12) //12 = player
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");

            AudioSource.PlayClipAtPoint(sludgeSound, transform.position, 0.05f);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x * 0.75f);
    }
}
