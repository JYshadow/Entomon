using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryWeapons : MonoBehaviour
{
    [Header("Static")]
    public GameObject itemSlotsContainer;
    public GameObject itemSlotWeapon;

    [HideInInspector] public bool opened;

    Help help;
    int slotLength = 15;
    bool checkingDuplicate;

    private void Start()
    {
        help = FindObjectOfType<Help>();

        //Create UI
        for (int i = 1; i <= slotLength; i++)
        {
            RectTransform itemSlot = Instantiate(itemSlotWeapon, itemSlotsContainer.transform).GetComponent<RectTransform>();
            itemSlot.name = i.ToString();
        }

        opened = true;

        BroadcastMessage("SaveItemSlotWeaponStats");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
        }
    }

    public void InsertWeaponInInventory(GameObject weapon)
    {
        //Check if there is a duplicate
        checkingDuplicate = true;
        for (int i = 0; i < itemSlotsContainer.transform.childCount; i++)
        {
            if (itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotWeapon>().itemSlotStats.weapons.Count > 0 && itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotWeapon>().itemSlotStats.weapons[0].GetComponent<Weapon>().weaponName == weapon.GetComponent<Weapon>().weaponName)
            {
                itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotWeapon>().InsertWeaponInSlot(weapon);
                return;
            }
        }
        checkingDuplicate = false;

        //Check first empty slot
        if (checkingDuplicate == false)
        {
            for (int i = 0; i < itemSlotsContainer.transform.childCount; i++)
            {
                if (itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotWeapon>().itemSlotStats.weapons.Count == 0)
                {
                    itemSlotsContainer.transform.GetChild(i).GetComponent<ItemSlotWeapon>().InsertWeaponInSlot(weapon);
                    break;
                }
            }
        }
    }

    public void OpenInventoryWeapons()
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

        help.DisplayHelp("You can equip a primary and a secondary weapon. In combat, press 1 for primary and 2 for secondary. You can also upgrade weapons with acquired knowledge points", 10);

        //Reset Description and ActionPanel
        gameObject.transform.Find("Description").gameObject.SetActive(false);
        gameObject.transform.Find("ActionPanel").gameObject.SetActive(false);
    }

    public void CloseInventoryWeapons()
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