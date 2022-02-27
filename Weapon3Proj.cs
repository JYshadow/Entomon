using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon3Proj : MonoBehaviour
{
    public GameObject explosion;

    float projectileLifetime = 2;
    WeaponData.Weapon3Stats weapon3Stats;

    void Start()
    {
        //Read WeaponData
        weapon3Stats = JsonUtility.FromJson<WeaponData.Weapon3Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon3.json"));

        //Collision
        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("EnemyHitbox"));
        if (initialCollision.Length > 0)
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        //Other
        StartCoroutine(InstantiateAfterLifetime(projectileLifetime));
    }

    void FixedUpdate()  //fixedupdate for moving and physics
    {
        float moveDistance = weapon3Stats.projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);

        RaycastHit hit;
        float radius;
        radius = transform.localScale.y / 2; //follow radius of projectile-size

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, moveDistance + 0.1f, LayerMask.GetMask("EnemyHitbox"), QueryTriggerInteraction.Collide))    //moveDistance + extra to compensate for target moving. Only damage if the instance ID does not exists in the list already
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, moveDistance + 0.1f, LayerMask.GetMask("Obstacle"), QueryTriggerInteraction.Collide))    //moveDistance + extra to compensate for target moving. Only damage if the instance ID does not exists in the list already
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator InstantiateAfterLifetime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
