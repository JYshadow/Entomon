using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableChallenge : MonoBehaviour
{
    [Header("Public")]
    public GameObject doorTrigger;

    [Header("Static")]
    public ParticleSystem particleSystemPuddle;
    public ParticleSystem particleSystemSparks;

    PlayerData playerData;
    Dialogue dialogue;
    bool completed = false;
    //bool shockable = false;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        dialogue = FindObjectOfType<Dialogue>();

        particleSystemPuddle.Stop();
        particleSystemSparks.Stop();

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }

        //InvokeRepeating("Shock", 0, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SaltWaterBomb" && completed == false)
        {
            if (doorTrigger != null)
            {
                doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
                doorTrigger.GetComponent<DoorTrigger>().canTrigger = true;
                completed = true;
                StartCoroutine(PlayPuddle());
                playerData.playerStats.experience += 30f;
                print("Cable");
                dialogue.DisplayDialogue("That connect the circuit", 5f);
            }
        }
    }

    private IEnumerator PlayPuddle()
    {
        particleSystemPuddle.Play();
        yield return new WaitForSeconds(1f);
        //shockable = true;
        particleSystemPuddle.Pause();
        particleSystemSparks.Play();
    }

    /*private void Shock()
    {
        if (shockable == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.localPosition, transform.localScale.x / 2);
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    if (col.gameObject.layer == 8 || col.gameObject.layer == 12)    //8 = Enemyhitbox, 12 = Playerhitbox
                    {
                        col.transform.parent.gameObject.GetComponent<LivingEntity>().TakeDamage(1f);
                    }
                }
            }
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, transform.localScale.x / 2);
    }
}
