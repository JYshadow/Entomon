using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy4 : LivingEntity  //Enemy is a LivingEntity so it inherits all LivingEntity functions
{
    [Header("Public")]
    public float seekRange;
    public float minDistance;
    public float minMsBetweenAttacks;
    public float maxMsBetweenAttacks;
    [Space(10)]
    public float attack0Ratio;
    [Space(10)]
    public float attack1Damage;
    public float attack1Range;
    public int attack1Ratio;
    [Space(10)]
    public float attack2Damage;
    public float attack2Range;
    public int attack2Ratio;
    public int cloneAmount;

    [Header("Static")]
    public Animator animator;
    [Space(10)]
    public Transform attack1Muzzle;
    public GameObject attack1Area;
    [Space(10)]
    public Transform attack2Muzzle;
    public GameObject attack2Area;

    [Header("Audio")]
    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
    public AudioSource movingSound;

    public enum State { Idle, Seek, Attack };
    public State currentState;
    public List<string> attackCoroutines = new List<string>();

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    Transform target;
    LevelManager levelManager;
    CapsuleCollider capsuleCollider;
    string currentAnim;
    bool awake;
    float nextAttackTime;
    public float remainingDistance;
    bool spawnable;
    bool playerDetected = false;
/*    float undetectDuration = 5f;
    float undetectElapsed;
    bool lostSight;*/

    protected override void Start() //Override the Livingentity start
    {
        base.Start(); //run the overridden start in Livingentity as well

        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        levelManager = FindObjectOfType<LevelManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = startingHealth;
        navMeshAgent.radius = capsuleCollider.radius;
        path = new NavMeshPath();

        StartCoroutine(Wake());
        StartCoroutine(UpdatePath());   //Start UpdatePath one time, which will then update every x seconds
        InvokeRepeating("RemainingDistance", 0, 0.25f);
        InvokeRepeating("RandomDirection", 0, 0.01f);
    }

    protected override void Update()
    {
        base.Update();

/*        if (undetectElapsed < undetectDuration)
        {
            undetectElapsed += Time.deltaTime;
        }
        if (undetectElapsed >= undetectDuration)
        {
            playerDetected = false;
        }*/

        if (target != null)
        {
            //Smoothly look at target when waking up
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);

            if (awake)  //Only do things when it's awake
            {
                Vector3 targetHorizontal = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(targetHorizontal);     //Always look at target through y-rotation

                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up * 0.1f, transform.TransformDirection(Vector3.forward), out hit, 20f, (1 << LayerMask.NameToLayer("Obstacle") | (1 << LayerMask.NameToLayer("Player")))))
                {
                    if (hit.collider.tag == "PlayerHitbox")
                    {
                        playerDetected = true;
                        //lostSight = false;
                    }
/*                    else
                    {
                        if (lostSight == false)
                        {
                            lostSight = true;
                            undetectElapsed = 0;
                        }
                    }*/
                }

                if (playerDetected == true)
                {
                    //List of possible attacks based on range
                    if (remainingDistance <= attack2Range && remainingDistance > attack1Range)
                    {
                        if (!attackCoroutines.Contains("Attack0"))
                        {
                            for (int i = 0; i < attack0Ratio; i++)
                            {
                                attackCoroutines.Add("Attack0");
                            }
                        }
                    }
                    else
                    {
                        if (attackCoroutines.Contains("Attack0"))
                        {
                            attackCoroutines.Remove("Attack0");
                        }
                    }

                    if (remainingDistance <= attack2Range)
                    {
                        if (!attackCoroutines.Contains("Attack2"))
                        {
                            for (int i = 0; i < attack2Ratio; i++)
                            {
                                attackCoroutines.Add("Attack2");
                            }
                        }
                    }
                    else
                    {
                        if (attackCoroutines.Contains("Attack2"))
                        {
                            attackCoroutines.Remove("Attack2");
                        }
                    }

                    if (remainingDistance <= attack1Range)
                    {
                        if (!attackCoroutines.Contains("Attack1"))
                        {
                            for (int i = 0; i < attack1Ratio; i++)
                            {
                                attackCoroutines.Add("Attack1");
                            }
                        }
                    }
                    else
                    {
                        if (attackCoroutines.Contains("Attack1"))
                        {
                            attackCoroutines.Remove("Attack1");
                        }
                    }

                    //Actions based on range
                    if (remainingDistance <= attack2Range)
                    {
                        if (Time.time > nextAttackTime)     //If threshold is reach, do the below
                        {
                            nextAttackTime = Time.time + Random.Range(minMsBetweenAttacks, maxMsBetweenAttacks) / 1000;     //Threshold of attacking moves up x miliseconds everytime it reaches threshold

                            StartCoroutine(attackCoroutines[Random.Range(0, attackCoroutines.Count)]);
                        }

                        if (currentState != State.Attack)
                        {
                            if (remainingDistance <= minDistance)
                            {
                                navMeshAgent.speed = 0;
                            }
                            else
                            {
                                navMeshAgent.speed = moveSpeed;
                            }
                        }
                    }
                    else if (remainingDistance > attack2Range && remainingDistance <= seekRange)  //If outside attackRange but within seekRange: State = Seeking
                    {
                        currentState = State.Seek;
                        navMeshAgent.speed = moveSpeed;
                    }
                    else
                    {
                        currentState = State.Idle;
                        navMeshAgent.speed = 0;
                    }

                    //Movement speed and animation
                    if (navMeshAgent.speed > 0)
                    {
                        PlayAnimation("Enemy4_seek1", 0.25f);

                        if (movingSound.isPlaying == false)
                        {
                            movingSound.Play();
                        }
                    }
                    else
                    {
                        if (remainingDistance <= attack2Range)
                        {
                            if (currentState != State.Attack)
                            {
                                PlayAnimation("Enemy4_idle1", 0.25f);

                                movingSound.Stop();
                            }
                        }
                        else
                        {
                            if (currentState != State.Attack)
                            {
                                PlayAnimation("Enemy4_idle1", 0.25f);

                                movingSound.Stop();
                            }
                        }
                    }
                }
                else
                {
                    currentState = State.Idle;
                    navMeshAgent.speed = 0;
                }
            }
        }
    }

    private void PlayAnimation(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim) return; //If current animation is the same, do nothing
        animator.CrossFade(newAnim, fadeDuration);
        currentAnim = newAnim;  //New animation is now current animation
    }

    private IEnumerator Wake()
    {
        awake = false;
        navMeshAgent.speed = 0;
        currentState = State.Idle;
        yield return new WaitForSeconds(2); //After x seconds become awake
        awake = true;
        navMeshAgent.speed = moveSpeed;
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

    private void RemainingDistance()
    {
        remainingDistance = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            remainingDistance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
    }

    private IEnumerator Attack0()
    {
        currentState = State.Seek;
        yield return null;
    }

    private IEnumerator Attack1()
    {
        PlayAnimation("Enemy4_attack1", 0.1f);
        AudioSource.PlayClipAtPoint(attack1Sound, transform.position, 0.05f);
        currentState = State.Attack;
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(1f); //Animation duration

        currentState = State.Seek;
        yield return null;
    }

    private void Attack1Damage()
    {
        if (remainingDistance <= attack1Range)
        {
            target.gameObject.GetComponent<LivingEntity>().TakeDamage(attack1Damage * levelManager.difficultyMultiplier, "Normal");
        }
    }

    private IEnumerator Attack2()
    {
        if (cloneAmount > 0)
        {
            PlayAnimation("Enemy4_attack2", 0.1f);
            AudioSource.PlayClipAtPoint(attack2Sound, transform.position, 0.025f);
            currentState = State.Attack;
            navMeshAgent.speed = 0;
            yield return new WaitForSeconds(2f); //Animation duration

            currentState = State.Seek;
            yield return null;
        }
    }

    private void Attack2Damage()
    {
        if (cloneAmount > 0)
        {
            spawnable = true;
        }
    }

    private void RandomDirection()
    {
        if (spawnable == true)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            //Debug.DrawRay(transform.position, randomDirection * 2f, Color.red, 1f);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, randomDirection, out hit, 2f))
            {
                return;
            }
            else
            {
                cloneAmount -= 1;
                GameObject newClone = Instantiate(attack2Area, transform.position + randomDirection * 2f, Quaternion.Euler(0, Random.Range(0, 360), 0));
                newClone.GetComponent<Enemy4>().attackCoroutines.Clear();
                spawnable = false;
            }
        }
    }
}