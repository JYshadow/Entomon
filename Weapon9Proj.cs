using System.Collections;
using UnityEngine;

public class Weapon9Proj : MonoBehaviour
{
    [Header("Static")]
    public GameObject explosion;
    public AudioClip plofSound;
    public GameObject balloonArming;
    public GameObject balloonArmed;

    WeaponData weaponData;
    bool armed = false;

    private void Awake()
    {
        weaponData = FindObjectOfType<WeaponData>();
    }

    private void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = weaponData.weapon9Stats.triggerRadius;
        StartCoroutine(Arming());
        StartCoroutine(DelayedDestroy());
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 7) //7 = Plane
        {
            AudioSource.PlayClipAtPoint(plofSound, transform.position, 0.05f);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == 8) //8 = EnemyHitbox (trigger)
        {
            if (armed == true)
            {
                GameObject weaponProjectile = Instantiate(explosion, transform.position, transform.rotation);
                //weaponProjectile.transform.localScale = weaponProjectile.transform.localScale * weaponPosition.localScale.x;
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private IEnumerator Arming()
    {
        balloonArming.SetActive(true);
        balloonArmed.SetActive(false);
        yield return new WaitForSeconds(weaponData.weapon9Stats.armingTime);
        armed = true;
        balloonArming.SetActive(false);
        balloonArmed.SetActive(true);
    }

    public IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(weaponData.weapon9Stats.lifetime);
        GameObject weaponProjectile = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }

    /*    private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, weaponData.weapon6Stats.viewRadius / 2* 1.5f);
        }*/
}
