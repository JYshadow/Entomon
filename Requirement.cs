using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Requirement : MonoBehaviour
{
    [Header("Static")]
    public Image icon;
    public TMP_Text amountTMP;

    [HideInInspector] public GameObject component;
     public bool requirementCompleted;
    [HideInInspector] public int amount;
    [HideInInspector] public int currentAmount;

    GameObject itemSlotsContainer;
    CraftingBench craftingBench;
    Image requirementImage;

    private void Start()
    {
        requirementImage = GetComponent<Image>();
        itemSlotsContainer = FindObjectOfType<InventoryComponents>().gameObject.transform.Find("ItemSlotsContainer").gameObject;
        craftingBench = FindObjectOfType<CraftingBench>();
    }

    private void Update()
    {
        if (craftingBench.opened == true)
        {
            if (component != null)
            {
                //Check amount in inventory
                for (int i = 0; i < itemSlotsContainer.transform.childCount; i++)
                {
                    if (itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components.Count > 0 && itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components[0].name == component.name)
                    {
                        currentAmount = itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components.Count;
                        break;
                    }
                    else
                    {
                        currentAmount = 0;
                    }
                }

                //Display amount
                amountTMP.text = currentAmount.ToString() + "/" + amount.ToString();

                //Check if this requirement has been completed
                if (currentAmount >= amount)
                {
                    requirementCompleted = true;
                    amountTMP.color = new Color32(255, 255, 255, 255);
                    requirementImage.color = new Color32(0, 100, 0, 255);
                }
                if (currentAmount < amount)
                {
                    requirementCompleted = false;
                    amountTMP.color = new Color32(255, 0, 0, 255);
                    requirementImage.color = new Color32(100, 0, 0, 255);
                }
            }
            else
            {
                requirementCompleted = false;
                currentAmount = 0;
                amount = 0;
                amountTMP.text = null;
                requirementImage.color = new Color32(0, 0, 0, 255);
            }
        }
    }

    public void RemoveComponents()
    {
        if (component != null)
        {
            for (int i = 0; i < itemSlotsContainer.transform.childCount; i++)
            {
                if (itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components.Count > 0 && itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components[0].GetComponent<Components>().name == component.GetComponent<Components>().name)
                {
                    itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components.RemoveRange(0, amount);
                    break;
                }
            }
        }
    }

    public void MouseEnter()
    {
        if (component != null)
        {
            GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().DisplayTooltipName(component.name);
        }
    }

    public void MouseExit()
    {
        if (component != null)
        {
            GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().HideTooltip();
        }
    }
}
