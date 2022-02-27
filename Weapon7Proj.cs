using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon7Proj : MonoBehaviour
{
    List<int> enemyID = new List<int>(); //make a list with all instance ID the projectile finds

    public RaycastHit[] hitsCenter;

    WeaponData.Weapon7Stats weapon7Stats;

    public float time;
    public float lineRangeFront;
    public float lineRangeEnd;
    public float rayRangeFront;
    public float rayRangeEnd;
    public Vector3 dirToTarget;
    public Vector3 initialDirToTarget;
    float knockbackPower = 0.5f;
    float knockbackTime;

    void Start()
    {
        //Read weapon data
        weapon7Stats = JsonUtility.FromJson<WeaponData.Weapon7Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon7.json"));
        StartCoroutine(IncreaseLineFront());
        StartCoroutine(IncreaseRayFront());

        //Collision
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("EnemyHitbox"));
        foreach (Collider initialCollision in initialCollisions)
        {
            initialCollision.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weapon7Stats.damage, "Normal");

            initialDirToTarget = (initialCollision.transform.parent.gameObject.transform.position - transform.position).normalized;
            StartCoroutine(InitialKnockback(initialCollision, initialDirToTarget * knockbackPower));
            enemyID.Add(initialCollision.GetInstanceID());
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        hitsCenter = Physics.RaycastAll(transform.position, transform.forward, rayRangeFront, LayerMask.GetMask("EnemyHitbox"));
        Debug.DrawLine(transform.position, transform.position + transform.forward * lineRangeFront, Color.red); //forward line

        for (int i = 0; i < hitsCenter.Length; i++)
        {
            if (enemyID.Contains(hitsCenter[i].collider.GetInstanceID()) == false)
            {
                hitsCenter[i].collider.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weapon7Stats.damage, "Normal");
                dirToTarget = (hitsCenter[i].collider.transform.parent.gameObject.transform.position - transform.position).normalized;
                StartCoroutine(Knockback(hitsCenter[i], dirToTarget * knockbackPower));
                enemyID.Add(hitsCenter[i].collider.GetInstanceID());
            }
        }
    }

    IEnumerator IncreaseRayFront()
    {
        while (time <= 3)
        {
            rayRangeFront = Mathf.Lerp(0, 3, time / 3);
            yield return null;
        }
    }

    IEnumerator IncreaseLineFront()
    {
        while (time <= 3)
        {
            lineRangeFront = Mathf.Lerp(0, 3, time / 3);

            yield return null;
        }
    }

    public IEnumerator InitialKnockback(Collider collider, Vector3 direction)
    {
        Vector3 newPosition = collider.transform.parent.gameObject.transform.position + direction;

        knockbackTime = 0;
        while (knockbackTime <= 1)
        {
            collider.transform.parent.gameObject.transform.position = Vector3.Lerp(collider.transform.parent.gameObject.transform.position, newPosition, knockbackTime / 1);
            knockbackTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator Knockback(RaycastHit hit, Vector3 direction)
    {
        Vector3 newPosition = hit.collider.transform.parent.gameObject.transform.position + direction;

        knockbackTime = 0;
        while (knockbackTime <= 1)
        {
            hit.collider.transform.parent.gameObject.transform.position = Vector3.Lerp(hit.collider.transform.parent.gameObject.transform.position, newPosition, knockbackTime / 1);
            knockbackTime += Time.deltaTime;
            yield return null;
        }
    }
}