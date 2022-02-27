using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    [Header("Static")]
    public GameObject pauseMenu;

    [HideInInspector] public bool respawning;

    InventoryWeapons inventoryWeapons;
    InventoryComponents inventoryComponents;
    TableOfElements tableOfElements;
    UpgradePanel upgradePanel;
    CraftingBench craftingBench;
    Quiz quiz;

    FirstPersonCamera firstPersonCamera;
    PlayerController playerController;
    Player player;
    WeaponController weaponController;
    HandAnimation handAnimation;
    EventsManager eventsManager;

    private void Start()
    {
        craftingBench = FindObjectOfType<CraftingBench>();
        inventoryWeapons = FindObjectOfType<InventoryWeapons>();
        inventoryComponents = FindObjectOfType<InventoryComponents>();
        tableOfElements = FindObjectOfType<TableOfElements>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        quiz = FindObjectOfType<Quiz>();

        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
        playerController = FindObjectOfType<PlayerController>();
        player = FindObjectOfType<Player>();
        weaponController = FindObjectOfType<WeaponController>();
        handAnimation = FindObjectOfType<HandAnimation>();
        eventsManager = FindObjectOfType<EventsManager>();

        //Close all UI
        craftingBench.CloseCraftingBench();
        tableOfElements.CloseTableOfElements();
        upgradePanel.CloseUpgradePanel();
        inventoryComponents.CloseInventoryComponents();
        inventoryWeapons.CloseInventoryWeapons();
        quiz.CloseQuiz();

        Image[] imageComponents = pauseMenu.GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            imageComponent.enabled = false;
        }
        TMP_Text[] textComponents = pauseMenu.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            textComponent.enabled = false;
        }
    }

    private void Update()
    {
        //If UI is opened or handAnimation is playing, do not move
        if (pauseMenu.GetComponent<Pause>().opened || inventoryWeapons.opened || inventoryComponents.opened || tableOfElements.opened || upgradePanel.opened || craftingBench.opened || quiz.opened)
        {
            Cursor.lockState = CursorLockMode.None;
            firstPersonCamera.DisableOutlines();

            firstPersonCamera.canInteract = false;
            playerController.canInteract = false;
            player.canInteract = false;
            weaponController.canInteract = false;

            //Close all
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAll();
                pauseMenu.GetComponent<Pause>().opened = false;

                Image[] imageComponents = pauseMenu.GetComponentsInChildren<Image>();
                foreach (Image imageComponent in imageComponents)
                {
                    imageComponent.enabled = false;
                }
                TMP_Text[] textComponents = pauseMenu.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text textComponent in textComponents)
                {
                    textComponent.enabled = false;
                }
            }
        }
        else if (handAnimation.playingGrabAnimation == true || eventsManager.cutscenePlaying == true || respawning == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonCamera.canInteract = false;
            playerController.canInteract = false;
            player.canInteract = false;
            weaponController.canInteract = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonCamera.canInteract = true;
            playerController.canInteract = true;
            player.canInteract = true;
            weaponController.canInteract = true;

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                Image[] imageComponents = pauseMenu.GetComponentsInChildren<Image>();
                foreach (Image imageComponent in imageComponents)
                {
                    imageComponent.enabled = true;
                }
                TMP_Text[] textComponents = pauseMenu.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text textComponent in textComponents)
                {
                    textComponent.enabled = true;
                }
                pauseMenu.GetComponent<Pause>().opened = true;
            }
        }

        //If craftingBench or quiz is opened, do not register key input
        if (craftingBench.opened == false && quiz.opened == false)
        {
            //Inventory Weapons
            if (Input.GetKeyDown(KeyCode.U) && eventsManager.cutscenePlaying == false)
            {
                if (inventoryWeapons.opened == false)
                {
                    inventoryWeapons.OpenInventoryWeapons();    //Open
                    inventoryComponents.CloseInventoryComponents();
                    tableOfElements.CloseTableOfElements();
                    upgradePanel.CloseUpgradePanel();
                    craftingBench.CloseCraftingBench();
                    quiz.CloseQuiz();

                    playerController.StopParticle();
                }
                else
                {
                    inventoryWeapons.CloseInventoryWeapons();
                }
            }

            //Inventory Components
            if (Input.GetKeyDown(KeyCode.I) && eventsManager.cutscenePlaying == false)
            {
                if (inventoryComponents.opened == false)
                {
                    inventoryWeapons.CloseInventoryWeapons();
                    inventoryComponents.OpenInventoryComponents();    //Open
                    tableOfElements.CloseTableOfElements();
                    upgradePanel.CloseUpgradePanel();
                    craftingBench.CloseCraftingBench();
                    quiz.CloseQuiz();

                    playerController.StopParticle();
                }
                else
                {
                    inventoryComponents.CloseInventoryComponents();
                }
            }

            //Table of Elements
            if (Input.GetKeyDown(KeyCode.T) && eventsManager.cutscenePlaying == false)
            {
                if (tableOfElements.opened == false)
                {

                    inventoryWeapons.CloseInventoryWeapons();
                    inventoryComponents.CloseInventoryComponents();
                    tableOfElements.OpenTableOfElements();  //Open
                    craftingBench.CloseCraftingBench();
                    upgradePanel.CloseUpgradePanel();
                    quiz.CloseQuiz();

                    playerController.StopParticle();
                }
                else
                {
                    tableOfElements.CloseTableOfElements();
                }
            }
        }
    }

    //UI without key inputs
    public void ToggleCraftingBench()
    {
        if (craftingBench.opened == false)
        {
            inventoryComponents.CloseInventoryComponents();
            inventoryWeapons.CloseInventoryWeapons();
            tableOfElements.CloseTableOfElements();
            craftingBench.OpenCraftingBench();  //Open
            upgradePanel.CloseUpgradePanel();
            quiz.CloseQuiz();
        }
    }

    public void ToggleUpgradePanel()
    {
        if (upgradePanel.opened == false)
        {
            inventoryWeapons.CloseInventoryWeapons();
            inventoryComponents.CloseInventoryComponents();
            tableOfElements.CloseTableOfElements();
            craftingBench.CloseCraftingBench();
            upgradePanel.OpenUpgradePanel();    //Open
            quiz.CloseQuiz();
        }
    }

    public void ToggleQuiz()
    {
        if (quiz.opened == false)
        {
            inventoryComponents.CloseInventoryComponents();
            inventoryWeapons.CloseInventoryWeapons();
            tableOfElements.CloseTableOfElements();
            craftingBench.CloseCraftingBench();
            upgradePanel.CloseUpgradePanel();
            quiz.OpenQuiz();    //Open
        }
    }

    public void CloseAll()
    {
        craftingBench.CloseCraftingBench();
        tableOfElements.CloseTableOfElements();
        inventoryComponents.CloseInventoryComponents();
        inventoryWeapons.CloseInventoryWeapons();
        upgradePanel.CloseUpgradePanel();
        quiz.CloseQuiz();

        weaponController.RestoreWeapon();
    }
}
