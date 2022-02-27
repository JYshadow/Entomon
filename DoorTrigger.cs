using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Public")]
    public GameObject[] doors;
    public GameObject[] lights;
    public bool stayOpen;
    public bool canTrigger;

    Light[] lightComponents;
    Renderer[] rendererComponents;

    private void Start()
    {
        lightComponents = GetComponentsInChildren<Light>();
        rendererComponents = GetComponentsInChildren<Renderer>();
    }

    public void Update()
    {
        if (canTrigger == true)
        {
            foreach (Light lightComponent in lightComponents)
            {
                lightComponent.color = Color.green;
            }

            foreach (Renderer rendererComponent in rendererComponents)
            {
                rendererComponent.materials[1].SetColor("_EmissionColor", Color.green);
            }
        }
        else
        {
            foreach (Light lightComponent in lightComponents)
            {
                lightComponent.color = Color.red;
            }

            foreach (Renderer rendererComponent in rendererComponents)
            {
                rendererComponent.materials[1].SetColor("_EmissionColor", Color.red);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitbox" && canTrigger == true && stayOpen == false)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<MovingDoor>().GetCurrentPos();
                doors[i].GetComponent<MovingDoor>().doorState = MovingDoor.DoorState.OpeningDoor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitbox" && canTrigger == true && stayOpen == false)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<MovingDoor>().GetCurrentPos();
                doors[i].GetComponent<MovingDoor>().doorState = MovingDoor.DoorState.ClosingDoor;
            }
        }
    }

    public void ForceOpenDoor()
    {
        canTrigger = true;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<MovingDoor>().GetCurrentPos();
            doors[i].GetComponent<MovingDoor>().doorState = MovingDoor.DoorState.OpeningDoor;
        }
    }
}
