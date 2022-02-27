using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public GameObject selectedWeapon;

    WeaponController weaponController;
    UpgradePanel upgradePanel;
    InputManager inputManager;
    GameObject upgradesManager;

    private void Awake()
    {
        weaponController = FindObjectOfType<WeaponController>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        inputManager = FindObjectOfType<InputManager>();
        upgradesManager = GameObject.FindGameObjectWithTag("UpgradesManager");
    }

    private void Start()
    {
        Image[] imageComponents = upgradesManager.GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            imageComponent.enabled = false;
        }
        TMP_Text[] textComponents = upgradesManager.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            textComponent.enabled = false;
        }
    }

    public void EquipPrimary()
    {
        //If primary weapon is already in primary, do nothing
        if (weaponController.primaryWeapon != null && weaponController.primaryWeapon.GetComponent<Weapon>().weaponName == selectedWeapon.GetComponent<Weapon>().weaponName)
        {
            return;
        }

        //If primary weapon is already in secondary, remove from secondary
        StartCoroutine(weaponController.UnequipWeapon(false));    //Unequip from currentWeapon
        if (weaponController.secondaryWeapon != null && weaponController.secondaryWeapon.GetComponent<Weapon>().weaponName == selectedWeapon.GetComponent<Weapon>().weaponName)
        {
            weaponController.secondaryWeapon = null;
            weaponController.primaryWeapon = selectedWeapon;
        }
        else
        {
            weaponController.primaryWeapon = selectedWeapon;
        }
    }

    public void EquipSecondary()
    {
        //If secondary weapon is already in secondary, do nothing
        if (weaponController.secondaryWeapon != null && weaponController.secondaryWeapon.GetComponent<Weapon>().weaponName == selectedWeapon.GetComponent<Weapon>().weaponName)
        {
            return;
        }

        //If secondary weapon is already in primary, remove from primary
        StartCoroutine(weaponController.UnequipWeapon(false));
        if (weaponController.primaryWeapon != null && weaponController.primaryWeapon.GetComponent<Weapon>().weaponName == selectedWeapon.GetComponent<Weapon>().weaponName)
        {
            weaponController.primaryWeapon = null;
            weaponController.secondaryWeapon = selectedWeapon;
        }
        else
        {
            weaponController.secondaryWeapon = selectedWeapon;
        }
    }

    public void OpenUpgradePanel()
    {
        //Replace and open current UpgradePanel
        /*        if (upgradePanel.transform.Find("UpgradePanelContainer").childCount > 0)
                {
                    Destroy(upgradePanel.transform.Find("UpgradePanelContainer").GetChild(0).gameObject);
                }
                GameObject weaponUpgradePanel = Instantiate(selectedWeapon.GetComponent<Weapon>().upgradePanel, upgradePanel.transform.Find("UpgradePanelContainer"));
                weaponUpgradePanel.transform.Find("Header").GetChild(0).GetComponent<TMP_Text>().text = "- Upgrade " + selectedWeapon.GetComponent<Weapon>().weaponName + " -";
                inputManager.ToggleUpgradePanel();*/
        if (upgradePanel.transform.Find("UpgradePanelContainer").childCount > 0)
        {
            upgradePanel.transform.Find("UpgradePanelContainer").GetChild(0).SetParent(upgradesManager.transform);
        }

        if (selectedWeapon.GetComponent<Weapon>().weaponName == "Propane torch")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon5").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }
        else if (selectedWeapon.GetComponent<Weapon>().weaponName == "Stun gun")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon6").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }
        else if (selectedWeapon.GetComponent<Weapon>().weaponName == "Saltwater bomb")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon8").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }
        else if (selectedWeapon.GetComponent<Weapon>().weaponName == "Explosive Trap")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon9").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }
        else if (selectedWeapon.GetComponent<Weapon>().weaponName == "Dry ice bomb")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon10").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }
        else if (selectedWeapon.GetComponent<Weapon>().weaponName == "Glue bomb")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon11").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }
        else if (selectedWeapon.GetComponent<Weapon>().weaponName == "Boric acid spray")
        {
            upgradesManager.transform.Find("UpgradePanelWeapon12").SetParent(upgradePanel.transform.Find("UpgradePanelContainer"), true);
            inputManager.ToggleUpgradePanel();
        }

        upgradePanel.transform.Find("UpgradePanelContainer").GetChild(0).localPosition = Vector3.zero;
        inputManager.ToggleUpgradePanel();
    }
}
