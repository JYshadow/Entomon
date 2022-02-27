using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public enum Direction { Up, Down };
    public Direction direction;

    MeltingChallenge meltingChallenge;
    float nextChangeTime;

    private void Start()
    {
        meltingChallenge = FindObjectOfType<MeltingChallenge>();
    }

    public void ChangeTemperature()
    {
        if (Time.time > nextChangeTime)
        {
            nextChangeTime = Time.time + 5 / 1000;

            if (direction == Direction.Up)
            {
                meltingChallenge.temperature += 1;
            }
            else
            {
                meltingChallenge.temperature -= 1;
            }
        }
    }
}
