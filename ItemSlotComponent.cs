using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlotComponent : MonoBehaviour
{
    [Header("Static")]
    public TMP_Text stack;
    public Image icon;

    int itemSlotIndex;

    //SavedData
    public ItemSlotStats itemSlotStats = new ItemSlotStats();
    [System.Serializable]
    public class ItemSlotStats
    {
        public List<GameObject> components = new List<GameObject>();
    }

    private void Awake()
    {
        int.TryParse(gameObject.name, out int nameToInt);
        itemSlotIndex = nameToInt;
    }

    private void Start()
    {
        //Read SavedData
        itemSlotStats = JsonUtility.FromJson<ItemSlotStats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Inventory/itemSlotComponentStats" + itemSlotIndex + ".json"));
    }

    private void Update()
    {
        if (itemSlotStats.components.Count > 0)
        {
            stack.text = itemSlotStats.components.Count.ToString();
            icon.GetComponent<Image>().sprite = itemSlotStats.components[0].GetComponent<Components>().sprite;      
        }
        else
        {
            stack.text = "";
            icon.GetComponent<Image>().sprite = null;
        }
    }

    public void InsertComponentInSlot(GameObject component)
    {
        itemSlotStats.components.Add(component);
    }

    public void MouseEnter()
    {
        if (itemSlotStats.components.Count > 0)
        {
            GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().DisplayTooltipName(itemSlotStats.components[0].name);
        }
    }

    public void MouseExit()
    {
        if (itemSlotStats.components.Count > 0)
        {
            GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().HideTooltip();
        }
    }

    private void SaveItemSlotComponentStats()
    {
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Inventory/itemSlotComponentStats" + itemSlotIndex + ".json", JsonUtility.ToJson(itemSlotStats, true));
        print("itemSlotComponentStat saved");
    }
}
