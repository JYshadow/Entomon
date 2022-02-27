using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Weapon6Proj : MonoBehaviour
{
    [Header("Public")]
    public AudioClip shockSound;

    [Header("Static")]
    public ParticleSystem particleElectricity;
    public ParticleSystem particleSpark;
    [HideInInspector] public List<int> enemyID = new List<int>();
    [HideInInspector] public bool firstChain;
    [HideInInspector] public float chains;
    [HideInInspector] public GameObject source;

    List<Collider> enemiesInArea = new List<Collider>();
    WeaponData weaponData;
    Collider randomEnemyInArea;
    LineRenderer linerenderer;
    float time;
    float lineDuration;
    //float lineWidthStart;
    //float lineWidthEnd;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();
        linerenderer = GetComponent<LineRenderer>();

        linerenderer.positionCount = 1;
        linerenderer.SetPosition(0, transform.position);
        lineDuration = weaponData.weapon6Stats.timeBetweenChains * 2f;

        if (firstChain == true)
        {
            StartCoroutine(FindFirstEnemy());
            particleElectricity.Play();
            particleElectricity.Stop();
            particleSpark.Stop();
        }
        else
        {
            StartCoroutine(FindNextEnemy());
            particleElectricity.Play();
            particleSpark.Play();

            AudioSource.PlayClipAtPoint(shockSound, transform.position, 0.35f);
        }

        if (chains == 0)
        {
            particleElectricity.Stop();
        }
    }

    private void Update()
    {
        transform.position = source.transform.position;
        if (randomEnemyInArea != null)
        {
            transform.LookAt(randomEnemyInArea.transform.position);
        }
        linerenderer.SetPosition(0, transform.position);

        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * weaponData.weapon6Stats.viewRadius / 2, Color.red);
    }

    private IEnumerator FindFirstEnemy()
    {
        /*        foreach (Collider enemyCollider in Physics.OverlapSphere(transform.position, weaponData.weapon6Stats.viewRadius, LayerMask.GetMask("EnemyHitbox"))) //Find random enemy within field of view
                {
                    Transform target = enemyCollider.transform;    //get transform of every collider in radius
                    Vector3 dirToTarget = (target.position - transform.position).normalized;    //get direction of every target in radius
                    if (Vector3.Angle(transform.forward, dirToTarget) < weaponData.weapon6Stats.viewAngle / 2)  //if direction of the target is within viewAngle
                    {
                        float dstToTarget = Vector3.Distance(transform.position, target.position);  //calculate the distance to the target

                        if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("Obstacle")))
                        {
                            enemiesInArea.Add(enemyCollider);
                        }
                    }
                }*/

        RaycastHit hit;

        Vector3 upRayRotation = Quaternion.AngleAxis(30, transform.up) * transform.forward;
        Vector3 downRayRotation = Quaternion.AngleAxis(-30, transform.up) * transform.forward;
        Vector3 leftRayRotation = Quaternion.AngleAxis(25, transform.right) * transform.forward;
        Vector3 rightRayRotation = Quaternion.AngleAxis(-25, transform.right) * transform.forward;

        if (!Physics.Raycast(transform.position, transform.forward, out hit, weaponData.weapon6Stats.viewRadius / 2, LayerMask.GetMask("Obstacle")))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, weaponData.weapon6Stats.viewRadius / 2, LayerMask.GetMask("EnemyHitbox")) || Physics.Raycast(transform.position, upRayRotation, out hit, weaponData.weapon6Stats.viewRadius / 2, LayerMask.GetMask("EnemyHitbox")) || Physics.Raycast(transform.position, downRayRotation, out hit, weaponData.weapon6Stats.viewRadius / 2, LayerMask.GetMask("EnemyHitbox")) || Physics.Raycast(transform.position, leftRayRotation, out hit, weaponData.weapon6Stats.viewRadius / 2, LayerMask.GetMask("EnemyHitbox")) || Physics.Raycast(transform.position, rightRayRotation, out hit, weaponData.weapon6Stats.viewRadius / 2, LayerMask.GetMask("EnemyHitbox")))
            {
                enemiesInArea.Add(hit.collider);
            }
        }

        if (enemiesInArea.Count > 0)
        {
            randomEnemyInArea = enemiesInArea[Random.Range(0, enemiesInArea.Count)];    //get random enemy in area
            randomEnemyInArea.gameObject.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon6Stats.damage, "Shock");    //random enemy takes damage
            GameObject nextProjectile = Instantiate(gameObject, randomEnemyInArea.transform.position, randomEnemyInArea.transform.rotation) as GameObject;
            nextProjectile.GetComponent<Weapon6Proj>().firstChain = false;
            nextProjectile.GetComponent<Weapon6Proj>().source = randomEnemyInArea.gameObject;
            nextProjectile.GetComponent<Weapon6Proj>().enemyID.Add(randomEnemyInArea.GetInstanceID());
            linerenderer.positionCount = 2;
            linerenderer.SetPosition(1, randomEnemyInArea.transform.position);
            StartCoroutine(ThinnenLine());
            Destroy(gameObject, weaponData.weapon6Stats.timeBetweenChains * 4);
        }
        else
        {
            Destroy(gameObject);
        }
        yield return null;
    }

    private IEnumerator FindNextEnemy()
    {
        yield return new WaitForSeconds(weaponData.weapon6Stats.timeBetweenChains);

        if (chains > 0)
        {
            foreach (Collider enemyCollider in Physics.OverlapSphere(transform.position, weaponData.weapon6Stats.viewRadius / 2 * 1.5f, LayerMask.GetMask("EnemyHitbox")))
            {
                if (enemyID.Contains(enemyCollider.GetInstanceID()) == false)
                {
                    enemiesInArea.Add(enemyCollider);
                }
            }

            if (enemiesInArea.Count > 0)
            {
                randomEnemyInArea = enemiesInArea[Random.Range(0, enemiesInArea.Count)];    //get random enemy in area
                randomEnemyInArea.gameObject.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(weaponData.weapon6Stats.damage, "Shock");    //random enemy takes damage
                GameObject nextProjectile = Instantiate(gameObject, randomEnemyInArea.transform.position, randomEnemyInArea.transform.rotation) as GameObject;
                nextProjectile.GetComponent<Weapon6Proj>().firstChain = false;
                nextProjectile.GetComponent<Weapon6Proj>().chains = chains - 1;
                nextProjectile.GetComponent<Weapon6Proj>().source = randomEnemyInArea.gameObject;
                nextProjectile.GetComponent<Weapon6Proj>().enemyID.Add(randomEnemyInArea.GetInstanceID());
                linerenderer.positionCount = 2;
                linerenderer.SetPosition(1, randomEnemyInArea.transform.position);
                StartCoroutine(ThinnenLine());

                Destroy(gameObject, weaponData.weapon6Stats.timeBetweenChains * 4);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, weaponData.weapon6Stats.timeBetweenChains * 4);
        }
    }

    private IEnumerator ThinnenLine()
    {
        lineDuration = time + lineDuration;

        while (time <= lineDuration)
        {
            linerenderer.startWidth = Mathf.Lerp(1f, 0f, time / lineDuration);
            linerenderer.endWidth = Mathf.Lerp(0.5f, 0f, time / lineDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }

/*    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponData.weapon6Stats.viewRadius / 2* 1.5f);
    }*/
}
