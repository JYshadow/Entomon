using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    [Header("Public")]
    public float moveDistance;
    public float duration;
    public enum DoorState { Idle, OpeningDoor, ClosingDoor }
    public DoorState doorState;
    public enum DoorDirection { Horizontal, Vertical, Forward }
    public DoorDirection doorDirection;

    [Header("Audio")]
    public AudioSource doorSound;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 currentPos;
    float currentTime;

    private void Start()
    {
        startPos = transform.localPosition;

        if (doorDirection == DoorDirection.Horizontal)
        {
            endPos = transform.localPosition + transform.right * moveDistance;
        }
        else if (doorDirection == DoorDirection.Vertical)
        {
            endPos = transform.localPosition + transform.up * moveDistance;
        }
        else if (doorDirection == DoorDirection.Forward)
        {
            endPos = transform.localPosition + transform.forward * moveDistance;
        }

        doorState = DoorState.Idle;
    }

    private void Update()
    {
        if (doorState == DoorState.Idle)
        {
            currentTime = 0;
        }

        if (doorState == DoorState.OpeningDoor)
        {
            OpeningDoor();
        }
        if (doorState == DoorState.ClosingDoor)
        {
            ClosingDoor();
        }
    }

    public void GetCurrentPos()
    {
        currentTime = 0;
        currentPos = transform.localPosition;

        doorSound.Play();
    }

    private void OpeningDoor()
    {
        //Increment timer once per frame
        currentTime += Time.deltaTime;
        if (currentTime > duration)
        {
            currentTime = duration;
        }

        //Lerp!
        float t = currentTime / duration;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        transform.localPosition = Vector3.Lerp(currentPos, endPos, t);

        if (transform.localPosition == endPos)
        {
            doorState = DoorState.Idle;
        }
    }

    private void ClosingDoor()
    {
        //Increment timer once per frame
        currentTime += Time.deltaTime;
        if (currentTime > duration)
        {
            currentTime = duration;
        }

        //Lerp!
        float t = currentTime / duration;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        transform.localPosition = Vector3.Lerp(currentPos, startPos, t);

        if (transform.localPosition == startPos)
        {
            doorState = DoorState.Idle;
        }
    }
}

