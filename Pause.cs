using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [HideInInspector] public bool opened;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6) && opened == true)
        {
            print("exit");
            Application.Quit();
        }
    }
}
