using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingMetal : MonoBehaviour
{
    [Header("public")]
    public float meltingTemperature;

    MeltingChallenge meltingChallenge;
    Vector3 startPos;
    Vector3 endPos;
    float currentTime;
    float duration = 10f;
    bool enabledMelting = true;

    private void Start()
    {
        meltingChallenge = FindObjectOfType<MeltingChallenge>();

        startPos = transform.localPosition;
        endPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.335f, transform.localPosition.z);
    }

    private void Update()
    {
        if (meltingChallenge.temperature >= meltingTemperature -25f && meltingChallenge.temperature <= meltingTemperature +25f)
        {
            //Increment timer once per frame
            currentTime += Time.deltaTime;
            if (currentTime > duration)
            {
                currentTime = duration;
            }

            //Lerp!
            float t = currentTime / duration;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
        }

        if (transform.localPosition == endPos && enabledMelting == true)
        {
            meltingChallenge.finished += 1;
            enabledMelting = false;
        }
    }
}
