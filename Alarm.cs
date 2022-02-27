using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [Header("Public")]
    public float startIntensity;
    public float endIntensity;
    public Color enmissionStartColor;
    public Color emissionEndColor;
    public Color lightStartColor;
    public Color lightEndColor;
    public bool flashing;
    
    [HideInInspector] public bool alarmEnabled;

    Light[] lightComponents;
    Renderer[] rendererComponents;

    private void Start()
    {
        lightComponents = GetComponentsInChildren<Light>();
        rendererComponents = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (alarmEnabled)
        {
            if (flashing == true)
            {
                foreach (Light lightComponent in lightComponents)
                {
                    lightComponent.color = lightEndColor;
                    lightComponent.intensity = Mathf.PingPong(Time.time, endIntensity);
                }

                foreach (Renderer rendererComponent in rendererComponents)
                {
                    rendererComponent.materials[1].SetColor("_EmissionColor", Color.Lerp(Color.black, emissionEndColor, Mathf.PingPong(Time.time, 0.5f)));
                }
            }
            else
            {
                foreach (Light lightComponent in lightComponents)
                {
                    lightComponent.color = lightEndColor;
                    lightComponent.intensity = endIntensity;
                }

                foreach (Renderer rendererComponent in rendererComponents)
                {
                    rendererComponent.materials[1].SetColor("_EmissionColor", emissionEndColor);
                }
            }
        }
        else
        {
            foreach (Light lightComponent in lightComponents)
            {
                lightComponent.color = lightStartColor;
                lightComponent.intensity = startIntensity;
            }

            foreach (Renderer rendererComponent in rendererComponents)
            {
                rendererComponent.materials[1].SetColor("_EmissionColor", enmissionStartColor);
            }
        }
    }
}
