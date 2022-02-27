using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon4Proj : MonoBehaviour
{
    float radius;
    float projectileLifetime = 2;
    List<int> enemyID = new List<int>(); //make a list with all instance ID the projectile finds
    WeaponData.Weapon4Stats weapon4Stats;

    void Start()
    {
        //Read WeaponData
        weapon4Stats = JsonUtility.FromJson<WeaponData.Weapon4Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon4.json"));

        //Collision
        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("EnemyHitbox"));
        if (initialCollision.Length > 0)
        {
            initialCollision[0].transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weapon4Stats.damage, "Normal");    //if projectile inside an sphere, get the parent's script 'LivingEntity' of first collider and trigger TakeDamage
            Destroy(gameObject);
        }

        //Other
        Destroy(gameObject, projectileLifetime);
    }

    void FixedUpdate()  //fixedupdate for moving and physics
    {
        float moveDistance = weapon4Stats.projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        radius = transform.localScale.y / 2; //follow radius of projectile-size

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, moveDistance + 0.1f, LayerMask.GetMask("EnemyHitbox"), QueryTriggerInteraction.Collide) && enemyID.Contains(hit.collider.GetInstanceID()) == false)    //moveDistance + extra to compensate for target moving. Only damage if the instance ID does not exists in the list already
        {
            hit.collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weapon4Stats.damage, "Normal");   //get the parent's script 'LivingEntity' and trigger TakeDamage
            enemyID.Add(hit.collider.GetInstanceID());  //if it hits a hitbox, add the instance ID of the hitbox
            Destroy(gameObject);
        }
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, moveDistance + 0.1f, LayerMask.GetMask("Obstacle"), QueryTriggerInteraction.Collide))
        {
            Destroy(gameObject);
        }
    }
}
