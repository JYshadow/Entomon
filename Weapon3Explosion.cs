using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon3Explosion : MonoBehaviour
{
    float damage;
    float nextTriggerTime;

    WeaponData.Weapon3Stats weapon3Stats;
    List<int> enemyID = new List<int>();

    void Start()
    {
        //Read WeaponData
        weapon3Stats = JsonUtility.FromJson<WeaponData.Weapon3Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/weapon3.json"));

        damage = weapon3Stats.damage;

        Destroy(gameObject, weapon3Stats.explosionLifetime);
    }

    public void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, LayerMask.GetMask("EnemyHitbox"));
        if (Time.time > nextTriggerTime)
        {
            nextTriggerTime = Time.time + 1f;
            foreach (Collider col in colliders)
            {
                if (weapon3Stats.stackable)
                {
                    col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");
                }
                if (weapon3Stats.stackable == false && enemyID.Contains(col.GetInstanceID()) == false)
                {
                    enemyID.Add(col.GetInstanceID());
                    col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");
                }
            }
            enemyID.Clear();
        }
    }
}
