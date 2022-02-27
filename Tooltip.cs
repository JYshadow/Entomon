using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [Header("Static")]
    public TMP_Text textTMP;

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        GetComponent<Image>().enabled = false;
        textTMP.enabled = false;
    }

    private void Update()
    {
        if (textTMP.enabled == true)
        {
            Canvas.ForceUpdateCanvases();
            rectTransform.sizeDelta = textTMP.gameObject.GetComponent<RectTransform>().sizeDelta;
            transform.position = new Vector3(Input.mousePosition.x + (GetComponent<RectTransform>().rect.width / 2 * 1.25f), Input.mousePosition.y - (GetComponent<RectTransform>().rect.height / 2 * 1.25f), 0);
        }
    }

    public void DisplayTooltipName(string name)
    {
        GetComponent<Image>().enabled = true;
        textTMP.enabled = true;
        textTMP.text = name;
    }

    public void DisplayTooltipElement(string elementName, float atomicMass, float meltingPoint, float density)
    {
        textTMP.enabled = true;
        if (meltingPoint == 0)
        {
            textTMP.text = "<color=#E0E300><b>" + elementName + "</color></b>" + "\n  Atomic mass: " + atomicMass + " u" + "\n  Melting point: " + "Unknown" + "\n  Density: " + density.ToString("F2") + " g/cm<sup>3<sup>";
        }
        else if (density == 0)
        {
            textTMP.text = "<color=#E0E300><b>" + elementName + "</color></b>" + "\n  Atomic mass: " + atomicMass + " u" + "\n  Melting point: " + meltingPoint.ToString("F2") + " °C" + "\n  Density: " + "Unknown";

        }
        else if (meltingPoint == 0 && density == 0)
        {
            textTMP.text = "<color=#E0E300><b>" + elementName + "</color></b>" + "\n  Atomic mass: " + atomicMass + " u" + "\n  Melting point: " + "Unknown" + "\n  Density: " + "Unknown";

        }
        else
        {
            textTMP.text = "<color=#E0E300><b>" + elementName + "</color></b>" + "\n  Atomic mass: " + atomicMass + " u" + "\n  Melting point: " + meltingPoint.ToString("F2") + " °C" + "\n  Density: " + density.ToString("F2") + " g/cm<sup>3<sup>";
        }
        transform.position = new Vector3(Input.mousePosition.x + (GetComponent<RectTransform>().rect.width / 2 * 1.25f), Input.mousePosition.y - (GetComponent<RectTransform>().rect.height / 2 * 1.25f), 0);
        GetComponent<Image>().enabled = true;
    }

    public void HideTooltip()
    {
        textTMP.text = null;
        textTMP.enabled = false;
        GetComponent<Image>().enabled = false;
    }
}
