using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShock : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource trippingAudio;

    GameObject player;
    float nextShockTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trippingAudio.Play();
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 1, transform.TransformDirection(Vector3.left), out hit, 0.2f))
        {
            if (Time.time > nextShockTime)
            {
                player.GetComponent<LivingEntity>().TakeDamage(50f, "Shock");
                nextShockTime = Time.time + 0.25f;
            }
        }

        Debug.DrawRay(transform.position + Vector3.up * 1, transform.TransformDirection(Vector3.left) * 0.2f, Color.red);
    }
}
