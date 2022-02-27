using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(MeshRenderer))]
public class LightLayer : MonoBehaviour
{
    uint rendererLayerNumber;
    int lightLayerNumber;

    Renderer[] renderers;
    Light[] lights;

    public void SetRendererLayer()
    {
        rendererLayerNumber = gameObject.GetComponent<Renderer>().renderingLayerMask;
        print("rendererLayerNumber: " + rendererLayerNumber);
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            //renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    public void SetLightLayer()
    {
        lightLayerNumber = gameObject.GetComponent<Light>().renderingLayerMask;
        print("lightLayerNumber: " + lightLayerNumber);
        lights = GetComponentsInChildren<Light>();
        print(lights.Length);

        foreach (Light light in lights)
        {
            //light.lightmapBakeType = LightmapBakeType.Realtime;
            //light.shadows = LightShadows.None;
        }
    }
}
