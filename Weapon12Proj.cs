using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon12Proj : MonoBehaviour
{
    [Header("Static")]
    public GameObject impact;
    public AudioClip hitSound;

    public bool canPlayHitSound;

    WeaponData weaponData;

    private void Awake()
    {
        canPlayHitSound = false;
    }

    private void Start()
    {
        //Read WeaponData
        weaponData = FindObjectOfType<WeaponData>();

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, LayerMask.GetMask("EnemyHitbox"));
        if (initialCollisions.Length > 0)
        {
            initialCollisions[0].transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon12Stats.damage, "Acid");
            Instantiate(impact, transform.localPosition, transform.rotation);

            if (canPlayHitSound == true)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, 0.35f);
            }

            Destroy(gameObject);
        }

        Destroy(gameObject, weaponData.weapon12Stats.range);
    }

    private void Update()  //fixedupdate for moving and physics
    {
        float moveDistance = 20 * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    private void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + 0.1f))
        {
            if (hit.collider.gameObject.layer == 8)  //8 = EnemyHitbox
            {
                hit.collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon12Stats.damage, "Acid");
                Instantiate(impact, transform.localPosition, transform.rotation);

                if (canPlayHitSound == true)
                {
                    AudioSource.PlayClipAtPoint(hitSound, transform.position, 0.35f);
                }

                Destroy(gameObject);
            }

            if (hit.collider.gameObject.layer == 6 || hit.collider.gameObject.layer == 7 || hit.collider.gameObject.layer == 11)    //6 = Obstacle, 7 = Plane, 8 = Movable
            {
                Instantiate(impact, transform.localPosition, transform.rotation);

                if (canPlayHitSound == true)
                {
                    AudioSource.PlayClipAtPoint(hitSound, transform.position, 0.35f);
                }

                Destroy(gameObject);            
            }
        }
    }
}
