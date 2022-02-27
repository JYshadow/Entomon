using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryComponents : MonoBehaviour
{
    [Header("Static")]
    public GameObject itemSlotsContainer;
    public GameObject itemSlotComponent;

    [HideInInspector] public bool opened;

    Help help;
    DropsManager dropsManager;
    int slotLength = 40;
    bool checkingDuplicate;

    private void Awake()
    {
    }

    private void Start()
    {
        help = FindObjectOfType<Help>();
        dropsManager = FindObjectOfType<DropsManager>();

        //Create UI
        for (int i = 1; i <= slotLength; i++)
        {
            RectTransform itemSlot = Instantiate(itemSlotComponent, itemSlotsContainer.transform).GetComponent<RectTransform>();
            itemSlot.name = i.ToString();
        }

        opened = true;

        BroadcastMessage("SaveItemSlotComponentStats");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))    //Save all ItemSlotsInChildren
        {
        }

        if (Input.GetKeyDown(KeyCode.F4))    //Save all ItemSlotsInChildren
        {
            for (int z = 0; z < 20; z++)
            {
                for (int i = 0; i < dropsManager.droppool.Length; i++)
                {
                    for (int y = 0; y < dropsManager.droppool[i].GetComponent<Item>().components.Length; y++)
                    {
                        InsertComponentInInventory(dropsManager.droppool[i].GetComponent<Item>().components[y]);
                    }
                }
            }
            print("Filled inventory");
        }
    }

    public void InsertComponentInInventory(GameObject component)
    {
        //Check if there is a duplicate
        checkingDuplicate = true;
        for (int i = 0; i < itemSlotsContainer.transform.childCount; i++)
        {
            if (itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components.Count > 0 && itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components[0].name == component.name)
            {
                itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().InsertComponentInSlot(component);
                return;
            }
        }
        checkingDuplicate = false;

        //Check first empty slot
        if (checkingDuplicate == false)
        {
            for (int i = 0; i < itemSlotsContainer.transform.childCount; i++)
            {
                if (itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().itemSlotStats.components.Count == 0)
                {
                    itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotComponent>().InsertComponentInSlot(component);
                    break;
                }
            }
        }


    }

    public void OpenInventoryComponents()
    {
        Image[] imageComponents = GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            imageComponent.enabled = true;
        }
        TMP_Text[] textComponents = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            textComponent.enabled = true;
        }

        opened = true;

        help.DisplayHelp("Items you find will be dismantled into components and put into your components inventory. Components are used to craft weapons and other components at a crafting station", 10f);
    }

    public void CloseInventoryComponents()
    {
        if (opened == true)
        {
            Image[] imageComponents = GetComponentsInChildren<Image>();
            foreach (Image imageComponent in imageComponents)
            {
                imageComponent.enabled = false;
            }
            TMP_Text[] textComponents = GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text textComponent in textComponents)
            {
                textComponent.enabled = false;
            }

            opened = false;
        }
    }
}