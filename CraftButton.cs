using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour
{
    [Header("Static")]
    public TMP_Text craftTMP;

    [HideInInspector] public int totalRequirements;
    [HideInInspector] public string itemType;
    [HideInInspector] public GameObject itemToCraft;
    [HideInInspector] public List<GameObject> craftedWeapoons = new List<GameObject>();

    InventoryWeapons inventoryWeapons;
    InventoryComponents InventoryComponents;
    CraftingBench craftingBench;
    EventsManager eventsManager;
    Help help;
    Image craftButtonImage;
    bool craftable;
    bool[] completedRequirementsList;

    private void Start()
    {
        craftButtonImage = GetComponent<Image>();
        craftingBench = FindObjectOfType<CraftingBench>();
        inventoryWeapons = FindObjectOfType<InventoryWeapons>();
        InventoryComponents = FindObjectOfType<InventoryComponents>();
        eventsManager = FindObjectOfType<EventsManager>();
        help = FindObjectOfType<Help>();
        completedRequirementsList = new bool[craftingBench.requirements.transform.Find("RequirementsButtonContainer").childCount];
    }

    private void Update()
    {
        for (int i = 0; i < craftingBench.requirements.transform.Find("RequirementsButtonContainer").childCount; i++)
        {
            completedRequirementsList[i] = craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().requirementCompleted;
        }

        if (completedRequirementsList.Count(c => c) >= totalRequirements)
        {
            craftable = true;
            //craftTMP.color = new Color32(255, 255, 255, 255);
            craftButtonImage.color = new Color32(0, 150, 0, 255);
        }
        else
        {
            craftable = false;
            //craftTMP.color = new Color32(255, 0, 0, 255);
            craftButtonImage.color = new Color32(150, 0, 0, 255);
        }
    } 

    public void Craft()
    {
        if (itemToCraft != null)
        {
            if (craftable)
            {
                if (craftedWeapoons.Contains(itemToCraft))
                {
                    help.DisplayHelp("You have already crafted a " + itemToCraft.GetComponent<Weapon>().weaponName + ". Press 'U' to open your weapons inventory", 5);
                }
                else
                {
                    for (int i = 0; i < craftingBench.requirements.transform.Find("RequirementsButtonContainer").childCount; i++)
                    {
                        if (craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().component != null)
                        {
                            craftingBench.requirements.transform.Find("RequirementsButtonContainer").GetChild(i).GetComponent<Requirement>().RemoveComponents();
                        }
                    }

                    if (itemType == "Weapon")
                    {
                        inventoryWeapons.InsertWeaponInInventory(itemToCraft);
                        craftedWeapoons.Add(itemToCraft);
                        help.DisplayHelp(itemToCraft.GetComponent<Weapon>().weaponName + " crafted. Press 'U' to open your weapons inventory", 5);

                        StartCoroutine(eventsManager.FirstWeaponCrafted());
                    }

                    if (itemType == "Component")
                    {
                        InventoryComponents.InsertComponentInInventory(itemToCraft);
                    }
                }
            }
            else
            {
                help.DisplayHelp("Insufficient components to craft", 5);
            }
        }
    }
}
