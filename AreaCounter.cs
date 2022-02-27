using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AreaCounter : MonoBehaviour
{
    [Header("Public")]
    public GameObject doorTrigger;
    public GameObject alarmContainer;

    [HideInInspector] public List<int> totalEnemiesInAreaList = new List<int>();
    [HideInInspector] public int totalEnemiesInArea;

    EventsManager eventsManager;
    bool cleared = false;

    private void Start()
    {
        eventsManager = FindObjectOfType<EventsManager>();

        doorTrigger.GetComponent<DoorTrigger>().canTrigger = false;

        //Count total enemies in area
        for (int z = 0; z < transform.childCount; z++)
        {
            if (transform.GetChild(z).name != "AlarmContainer")
            {
                for (int i = 0; i < transform.GetChild(z).GetComponent<Spawner>().waves.Length; i++)
                {
                    totalEnemiesInAreaList.Add(transform.GetChild(z).GetComponent<Spawner>().waves[i].enemyCount);
                }
            }
        }
        totalEnemiesInArea = totalEnemiesInAreaList.Sum();
    }

    private void Update()
    {
        //If area is cleared, open door
        if (totalEnemiesInArea == 0 || transform.GetChild(1).GetComponent<Spawner>().waves.Length == 0)
        {
            if (cleared == false)
            {
                doorTrigger.GetComponent<DoorTrigger>().ForceOpenDoor();

                eventsManager.AreaCleared(gameObject.name);

                cleared = true;
            }

            if (alarmContainer != null)
            {
                alarmContainer.GetComponent<Alarm>().alarmEnabled = false;
            }
        }
        else
        {
            if (alarmContainer != null)
            {
                alarmContainer.GetComponent<Alarm>().alarmEnabled = true;
            }
        }
    }
}
