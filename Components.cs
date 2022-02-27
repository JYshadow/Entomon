using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour
{
    [Header("Variables")]
    [TextArea(15, 20)] public string descriptionCraft;
    public string descriptionInventory;
    public Sprite sprite;
    public Requirements[] requirements;

    [System.Serializable]
    public class Requirements
    {
        public GameObject component;
        public int amount;
    }
}
