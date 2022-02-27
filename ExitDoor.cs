using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject doorTrigger;

    EventsManager eventsManager;

    private void Start()
    {
        eventsManager = FindObjectOfType<EventsManager>();

        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;
        }
    }

    public void ExitFinalDoor()
    {
        if (doorTrigger != null)
        {
            doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();
            StartCoroutine(eventsManager.ExitFinalDoor());
        }
    }
}
