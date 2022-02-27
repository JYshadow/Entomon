using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Element : MonoBehaviour
{
    [Header("Public")]
    public string elementName;
    public int atomicNumber;
    public string symbol;
    public float atomicMass;
    public float meltingPoint;
    public float density;
    public bool discovered;

    [Header("Static")]
    public TMP_Text numberTMP;
    public TMP_Text symbolTMP;
    public Image glowImage;

    private void Start()
    {
        gameObject.name = elementName;
        numberTMP.text = atomicNumber.ToString();
        symbolTMP.text = symbol;
        meltingPoint -= 272.15f;
    }

    public void MouseEnter()    //If hovering over Element, display content
    {
        GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().DisplayTooltipElement(elementName, atomicMass, meltingPoint, density);
    }

    public void MouseExit() //If exiting Element, hide content
    {
        GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().HideTooltip();
    }
}