using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftablesButton : MonoBehaviour
{
    [Header("Public")]
    public GameObject craftableItem;
    public enum CraftableType { Weapon, Component }
    public CraftableType craftableType;

    [Header("Static")]
    public TMP_Text nameTMP;

    [HideInInspector] public bool unlocked = false;

    CraftingBench craftingBench;
    CraftablesManager craftablesManager;

    private void Start()
    {
        craftingBench = FindObjectOfType<CraftingBench>();
        craftablesManager = FindObjectOfType<CraftablesManager>();
    }

    public void DisplayName()
    {
        if (craftableType == CraftableType.Weapon)
        {
            nameTMP.text = craftableItem.GetComponent<Weapon>().weaponName;
        }
        if (craftableType == CraftableType.Component)
        {
            nameTMP.text = craftableItem.name;
        }
    }

    public void Display()
    {
        //If craftable is Weapon
        if (craftableType == CraftableType.Weapon)
        {
            //Reset all Requirements
            for (int i = 0; i < craftingBench.requirements.transform.Find("RequirementsButtonContainer").childCount; i++)
            {
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().component = null;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().icon.sprite = null;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().amount = 0;
            }

            //Display Requirements
            for (int i = 0; i < craftableItem.GetComponent<Weapon>().requirements.Length; i++)
            {
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().component = craftableItem.GetComponent<Weapon>().requirements[i].component;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().icon.sprite = craftableItem.GetComponent<Weapon>().requirements[i].component.GetComponent<Components>().sprite;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().amount = craftableItem.GetComponent<Weapon>().requirements[i].amount;
            }
            //Display other
            craftingBench.craftableDescription.transform.Find("CraftableHeader").GetComponent<TMP_Text>().text = craftableItem.GetComponent<Weapon>().weaponName;
            craftingBench.craftableDescription.transform.Find("CraftableBody").GetComponent<TMP_Text>().text = craftableItem.GetComponent<Weapon>().descriptionCraft;
            craftingBench.craftableImage.transform.Find("CraftableImageIcon").GetComponent<Image>().sprite = craftableItem.GetComponent<Weapon>().sprite;
            craftingBench.craftButton.GetComponent<CraftButton>().totalRequirements = craftableItem.GetComponent<Weapon>().requirements.Length;
            craftingBench.craftButton.GetComponent<CraftButton>().itemToCraft = craftableItem;
            craftingBench.craftButton.GetComponent<CraftButton>().itemType = "Weapon";
        }

        //If craftable is Component
        if (craftableType == CraftableType.Component)
        {
            //Reset all Requirements
            for (int i = 0; i < craftingBench.requirements.transform.Find("RequirementsButtonContainer").childCount; i++)
            {
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().component = null;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().icon.sprite = null;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().amount = 0;
            }

            //Display Requirements
            for (int i = 0; i < craftableItem.GetComponent<Components>().requirements.Length; i++)
            {
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().component = craftableItem.GetComponent<Components>().requirements[i].component;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().icon.sprite = craftableItem.GetComponent<Components>().requirements[i].component.GetComponent<Components>().sprite;
                craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().amount = craftableItem.GetComponent<Components>().requirements[i].amount;
            }

            //Display other
            craftingBench.craftableDescription.transform.Find("CraftableHeader").GetComponent<TMP_Text>().text = craftableItem.name;
            craftingBench.craftableDescription.transform.Find("CraftableBody").GetComponent<TMP_Text>().text = craftableItem.GetComponent<Components>().descriptionCraft;
            craftingBench.craftableImage.transform.Find("CraftableImageIcon").GetComponent<Image>().sprite = craftableItem.GetComponent<Components>().sprite;
            craftingBench.craftButton.GetComponent<CraftButton>().totalRequirements = craftableItem.GetComponent<Components>().requirements.Length;
            craftingBench.craftButton.GetComponent<CraftButton>().itemToCraft = craftableItem;
            craftingBench.craftButton.GetComponent<CraftButton>().itemType = "Component";
        }
    }
}
