using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;
using TMPro;

public class ItemSlotWeapon : MonoBehaviour
{
    [Header("Static")]
    public TMP_Text stack;
    public Image icon;

    int itemSlotIndex;
    InventoryWeapons inventoryWeapons;
    WeaponController weaponController;

    //SavedData
    public ItemSlotStats itemSlotStats = new ItemSlotStats();
    [System.Serializable]
    public class ItemSlotStats
    {
        public List<GameObject> weapons = new List<GameObject>();
    }

    private void Awake()
    {
        int.TryParse(gameObject.name, out int nameToInt);
        itemSlotIndex = nameToInt;
    }

    private void Start()
    {
        //Read SavedData
        itemSlotStats = JsonUtility.FromJson<ItemSlotStats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Inventory/itemSlotWeaponStats" + itemSlotIndex + ".json"));

        inventoryWeapons = FindObjectOfType<InventoryWeapons>();
        weaponController = FindObjectOfType<WeaponController>();
        InvokeRepeating("CheckWeaponPosition", 0, 0.2f);
    }

    //Check whether weapon is in primary or secondary position
    private void CheckWeaponPosition()
    {
        if (itemSlotStats.weapons.Count > 0)
        {
            if (weaponController.primaryWeapon != null && weaponController.primaryWeapon.GetComponent<Weapon>().weaponName == itemSlotStats.weapons[0].GetComponent<Weapon>().weaponName)
            {
                gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else if (weaponController.secondaryWeapon != null && weaponController.secondaryWeapon.GetComponent<Weapon>().weaponName == itemSlotStats.weapons[0].GetComponent<Weapon>().weaponName)
            {
                gameObject.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
    }

    public void InsertWeaponInSlot(GameObject weapon)
    {
        itemSlotStats.weapons.Add(weapon);
        stack.text = itemSlotStats.weapons.Count.ToString();
        icon.GetComponent<Image>().sprite = itemSlotStats.weapons[0].GetComponent<Weapon>().sprite;
    }

    public void Display()
    {
        if (itemSlotStats.weapons.Count > 0)
        {
            inventoryWeapons.gameObject.transform.Find("Description").gameObject.SetActive(true);
            inventoryWeapons.gameObject.transform.Find("Description").Find("Header").GetComponent<TMP_Text>().text = itemSlotStats.weapons[0].GetComponent<Weapon>().weaponName;
            inventoryWeapons.gameObject.transform.Find("Description").Find("Body").GetComponent<TMP_Text>().text = itemSlotStats.weapons[0].GetComponent<Weapon>().descriptionInventory;

            inventoryWeapons.gameObject.transform.Find("ActionPanel").gameObject.SetActive(true);
            if (itemSlotStats.weapons[0].GetComponent<Weapon>().upgradePanel != null)
            {
                inventoryWeapons.gameObject.transform.Find("ActionPanel").Find("Upgrade").gameObject.SetActive(true);
            }
            else
            {
                inventoryWeapons.gameObject.transform.Find("ActionPanel").Find("Upgrade").gameObject.SetActive(false);
            }

            inventoryWeapons.gameObject.transform.Find("ActionPanel").GetComponent<ActionPanel>().selectedWeapon = itemSlotStats.weapons[0];
        }
    }

    public void MouseEnter()
    {
        if (itemSlotStats.weapons.Count > 0)
        {
            GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().DisplayTooltipName(itemSlotStats.weapons[0].GetComponent<Weapon>().weaponName);
        }
    }

    public void MouseExit()
    {
        if (itemSlotStats.weapons.Count > 0)
        {
            GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().HideTooltip();
        }
    }

    private void SaveItemSlotWeaponStats()
    {
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Inventory/itemSlotWeaponStats" + itemSlotIndex + ".json", JsonUtility.ToJson(itemSlotStats, true));
        print("itemSlotWeaponStat saved");
    }
}
