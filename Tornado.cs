using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tornado : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource tornadeSound;

    [HideInInspector] public float damage;

    Transform target;
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    bool awake;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        path = new NavMeshPath();

        StartCoroutine(Wake());
        StartCoroutine(UpdatePath());   //Start UpdatePath one time, which will then update every x seconds
        InvokeRepeating("DealDamage", 0, 0.5f);

        tornadeSound.Play();
    }
    private IEnumerator Wake()
    {
        awake = false;
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(2); //After x seconds become awake
        navMeshAgent.speed = 0.5f;
        awake = true;
    }

    private IEnumerator UpdatePath()
    {
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);  //Find target position
            if (NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path) == true)
            {
                navMeshAgent.SetPath(path);
            }
            yield return new WaitForSeconds(0.25f); //<<<<< waitforseconds 0.25f
        }
    }

    private void DealDamage()
    {
        if (awake == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x, LayerMask.GetMask("Player"));
            if (colliders.Length > 0)
            {
                target.GetComponent<LivingEntity>().TakeDamage(damage, "Normal");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
}
