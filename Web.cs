using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    [Header("Static")]
    public GameObject puddle;
    public AudioClip explosionSound;

    [HideInInspector] public float damage;

    GameObject player;
    Rigidbody rb;
    float distanceToGround;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f, LayerMask.GetMask("Player"));
        if (initialCollision.Length > 0)
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
            {
                distanceToGround = hit.distance;
            }
            Instantiate(puddle, transform.position + Vector3.down * distanceToGround * 0.99f, Quaternion.identity); //Instantiate puddle on the ground

            Destroy(gameObject);
        }

        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        float moveDistance = 5f * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        rb.AddForce(new Vector3(0, -4f, 0));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 3 || col.gameObject.layer == 6 || col.gameObject.layer == 7 || col.gameObject.layer == 11)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
            {
                distanceToGround = hit.distance;
                Instantiate(puddle, transform.position + Vector3.down * distanceToGround * 0.99f, Quaternion.identity); //Instantiate puddle on the ground
            }
            else
            {
                Instantiate(puddle, transform.position + Vector3.up * 0.1f, Quaternion.identity); //Instantiate puddle on the ground
            }

            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f);

            Destroy(gameObject);
        }
        else if (col.gameObject.layer == 12) //12 = player
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
            {
                distanceToGround = hit.distance;
                Instantiate(puddle, transform.position + Vector3.down * distanceToGround * 0.99f, Quaternion.identity); //Instantiate puddle on the ground
            }
            else
            {
                Instantiate(puddle, transform.position + Vector3.up * 0.1f, Quaternion.identity); //Instantiate puddle on the ground
            }

            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.1f);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2f);
    }
}
