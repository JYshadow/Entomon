using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalChallenge : MonoBehaviour
{
    [Header("Public")]
    public GameObject doorTrigger;

    bool completed = false;

    private void Start()
    {
        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitbox" && completed == false)
        {
            if (doorTrigger != null)
            {
                doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
                doorTrigger.GetComponent<DoorTrigger>().canTrigger = true;
                print("Elemental");
            }
            completed = true;
        }
    }
}
