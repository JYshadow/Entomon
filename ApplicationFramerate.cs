using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFramerate : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;   //target framerate is 60fps to reduce microstutter
    }
}
