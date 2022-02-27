using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingBench : MonoBehaviour
{   
    [Header("Static")]
    public GameObject craftables;
    public GameObject craftableDescription;
    public GameObject craftableImage;
    public GameObject requirements;
    public GameObject craftButton;

    [HideInInspector] public bool opened;

    CraftablesManager craftablesManager;
    WeaponController weaponController;
    Help help;

    private void Start()
    {
        craftablesManager = FindObjectOfType<CraftablesManager>();
        weaponController = FindObjectOfType<WeaponController>();
        help = FindObjectOfType<Help>();

        opened = true;
    }

    public void OpenCraftingBench()
    {
        //Open and enable all image and text components
        opened = true;

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

        //Check if craft has been unlocked and display if it is
        for (int i = 0; i < craftables.transform.Find("CraftablesScrollMenu").GetChild(0).childCount; i++)
        {
            if (craftablesManager.craftablesStats.unlockedCraftables.Contains(craftables.transform.Find("CraftablesScrollMenu").GetChild(0).GetChild(i).gameObject.name))
            {
                craftables.transform.Find("CraftablesScrollMenu").GetChild(0).GetChild(i).gameObject.SetActive(true);
                craftables.transform.Find("CraftablesScrollMenu").GetChild(0).GetChild(i).GetComponent<CraftablesButton>().DisplayName();
            }
            else
            {
                craftables.transform.Find("CraftablesScrollMenu").GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }

        //Display all active crafts
        for (int i = 0; i < craftables.transform.Find("CraftablesScrollMenu").GetChild(0).childCount; i++)
        {
            if (craftables.transform.Find("CraftablesScrollMenu").GetChild(0).GetChild(i).gameObject.activeSelf == true)
            {
                craftables.transform.Find("CraftablesScrollMenu").GetChild(0).GetChild(i).GetComponent<CraftablesButton>().Display();
                return;
            }
        }

        help.DisplayHelp("Every crafting station lets you crafting weapons (purple) and components (blue).", 8);
    }

    public void CloseCraftingBench()
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

            weaponController.RestoreWeapon();
        }
    }
}
