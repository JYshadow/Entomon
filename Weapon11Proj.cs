using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon11Proj : MonoBehaviour
{
    [Header("Static")]
    public GameObject explosion;

    Rigidbody projRigidbody;
    Transform weaponPosition;

    private void Start()
    {
        projRigidbody = GetComponent<Rigidbody>();
        projRigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
        weaponPosition = GameObject.FindGameObjectWithTag("WeaponPosition").transform;

        /*//Collision
        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("EnemyHitbox"));
        if (initialCollision.Length > 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }*/
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 8) //8 = EnemyHitbox (trigger)
        {
            GameObject weaponProjectile = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 3 || col.gameObject.layer == 6 || col.gameObject.layer == 7 || col.gameObject.layer == 11) //3 = Interactable, 6 = Obstacle, 7 = Plane, 11 = Movable (colliders)
        {
            GameObject weaponProjectile = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
