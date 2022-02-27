using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EventsManager : MonoBehaviour
{
    [Header("Static")]
    public GameObject[] cutscenes;
    public Animator animator;

    [Header("Area0")]
    public GameObject dialogueTrigger1;
    public GameObject dialogueTrigger2;
    public GameObject lockpad1;
    public GameObject doorTrigger1;
    public GameObject doorTrigger2;
    public GameObject firstEnemy;
    public GameObject firstSpawner;
    public GameObject alarm1;

    [Header("Area1")]
    public GameObject area1DialogueTrigger;

    [Header("Area2")]
    public GameObject area2DialogueTrigger;

    [Header("Area3")]
    public GameObject area3DialogueTrigger;

    [Header("Audio")]
    public AudioSource alarmSound;

    [HideInInspector] public bool cutscenePlaying;

    Camera mainCamera;
    Camera declippingCamera;
    GameObject screenUI;
    InputManager inputManager;
    Player player;
    WeaponController weaponController;
    Dialogue dialogue;
    Help help;
    FirstPersonCamera firstPersonCamera;
    string currentAnim;
    bool firstWeaponCrafted = false;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        player = FindObjectOfType<Player>();
        dialogue = FindObjectOfType<Dialogue>();
        help = FindObjectOfType<Help>();
        weaponController = FindObjectOfType<WeaponController>();
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //declippingCamera = GameObject.FindGameObjectWithTag("DeclippingCamera").GetComponent<Camera>();
        screenUI = GameObject.FindGameObjectWithTag("ScreenUI");

        for (int i = 0; i < cutscenes.Length; i++)
        {
            cutscenes[i].SetActive(false);
        }

        dialogueTrigger1.SetActive(true);
        dialogueTrigger2.SetActive(false);
        lockpad1.layer = 0; //<<<<< delfault 0

        StartCoroutine(PlayerWakeUp());   //<<<<<
    }

    private void DisableCamera()
    {
        mainCamera.enabled = false;
        //declippingCamera.enabled = false;

        Image[] imageComponents = screenUI.GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            if (imageComponent.gameObject.name != "Dialogue" && imageComponent.gameObject.name != "DialogueText" && imageComponent.gameObject.name != "Help" && imageComponent.gameObject.name != "HelpText")
            {
                imageComponent.enabled = false;
            }
        }
        TMP_Text[] textComponents = screenUI.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            if (textComponent.gameObject.name != "Dialogue" && textComponent.gameObject.name != "DialogueText" && textComponent.gameObject.name != "Help" && textComponent.gameObject.name != "HelpText")
            {
                textComponent.enabled = false;
            }
        }
    }

    private void EnableCamera()
    {
        mainCamera.enabled = true;
        //declippingCamera.enabled = true;

        Image[] imageComponents = screenUI.GetComponentsInChildren<Image>();
        foreach (Image imageComponent in imageComponents)
        {
            if (imageComponent.gameObject.name != "Dialogue" && imageComponent.gameObject.name != "DialogueText" && imageComponent.gameObject.name != "Help" && imageComponent.gameObject.name != "HelpText")
            {
                imageComponent.enabled = true;
            }
        }
        TMP_Text[] textComponents = screenUI.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textComponent in textComponents)
        {
            if (textComponent.gameObject.name != "Dialogue" && textComponent.gameObject.name != "DialogueText" && textComponent.gameObject.name != "Help" && textComponent.gameObject.name != "HelpText")
            {
                textComponent.enabled = true;
            }
        }
    }

    //EVENTS
    private IEnumerator PlayerWakeUp()
    {
        Debug.Log("FirstCutscene");
        cutscenePlaying = true;
        weaponController.UnequipWeapon(true);
        DisableCamera();
        PlayCutscene("Player_wakeup", 0.0f);
        yield return null;
    }

    public IEnumerator FirstWeaponCrafted()
    {
        if (firstWeaponCrafted == false)
        {
            print("called");
            dialogueTrigger1.SetActive(false);
            dialogueTrigger2.SetActive(true);
            lockpad1.layer = 3; //3 = Interactable

            dialogue.DisplayDialogue("Not the best weapon, but it will do for now. Let's head out", 5);
            firstWeaponCrafted = true;
        }
        yield return null;
    }

    public IEnumerator FirstQuizCompleted()
    {
        cutscenePlaying = true;
        yield return new WaitForSeconds(1f);
        weaponController.storedWeapon = false;
        inputManager.CloseAll();
        player.ForceMovePlayer(cutscenes[1].transform.position - 1 * Vector3.down, cutscenes[2].transform.rotation, 2f);
        firstPersonCamera.ForceRotateCamera(cutscenes[1].transform.rotation, 2f);
        yield return new WaitForSeconds(2f);
        DisableCamera();
        PlayCutscene("Player_lookatfirstmonster", 0.0f);
    }

    public IEnumerator ExitFinalDoor()
    {
        cutscenePlaying = true;
        yield return new WaitForSeconds(1f);
        weaponController.storedWeapon = false;
        inputManager.CloseAll();
        player.ForceMovePlayer(cutscenes[2].transform.position - 1 * Vector3.down, cutscenes[2].transform.rotation, 2f);
        firstPersonCamera.ForceRotateCamera(cutscenes[2].transform.rotation, 2f);
        yield return new WaitForSeconds(2f);
        DisableCamera();
        PlayCutscene("Player_exitfinal", 0.0f);
    }

    //CUTSCENES
    private void PlayCutscene(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim)
        {
            return; //If current animation is the same, do nothing
        }
        else
        {
            animator.CrossFade(newAnim, fadeDuration);
            currentAnim = newAnim;  //New animation is now current animation
        }
    }

    public void EndCutscene(AnimationEvent myEvent)
    {
        player.transform.position = new Vector3(cutscenes[myEvent.intParameter].transform.position.x, player.transform.position.y, cutscenes[myEvent.intParameter].transform.position.z);
        player.transform.rotation = cutscenes[myEvent.intParameter].transform.rotation;
        firstPersonCamera.cameraHolder.localRotation = Quaternion.Euler(0, 0, 0);
        PlayCutscene("Player_idle", 0.0f);
        EnableCamera();
        cutscenePlaying = false;
    }

    //EVENTS
    public void PlayDialogue(AnimationEvent myEvent)
    {
        dialogue.DisplayDialogue(myEvent.stringParameter, myEvent.floatParameter);
    }

    public void PlayHelp(AnimationEvent myEvent)
    {
        help.DisplayHelp(myEvent.stringParameter, myEvent.floatParameter);
    }

    public void OpenFirstDoor()
    {
        doorTrigger1.GetComponent<DoorTrigger>().ForceOpenDoor();
    }

    public void ActivateFirstAlarm()
    {
        alarm1.GetComponent<Alarm>().alarmEnabled = true;
        alarmSound.Play();
    }

    public void SpawnFirstEnemy()
    {
        GameObject spawnedEnemy =  Instantiate(firstEnemy, firstSpawner.transform.position, firstSpawner.transform.rotation);
        spawnedEnemy.GetComponent<LivingEntity>().OnDeath += EnemyDied;
        spawnedEnemy.GetComponent<Enemy1>().seekRange = 20;
        spawnedEnemy.GetComponent<Enemy1>().movingSound.Play();
    }

    private void EnemyDied()
    {
        doorTrigger2.GetComponent<DoorTrigger>().ForceOpenDoor();
        alarm1.GetComponent<Alarm>().alarmEnabled = false;
        alarmSound.Stop();
        dialogue.DisplayDialogue("They have... mutated with the infused material. This is very bad.", 8f);
        help.DisplayHelp("Press 'T' to open the Periodic Table of Elements to see th elements you've discovered so far.", 15f);
    }

    public void AreaCleared(string areaName)
    {
        if (areaName == "Area1")
        {
            area1DialogueTrigger.SetActive(false);
        }
        if (areaName == "Area2")
        {
            area2DialogueTrigger.SetActive(false);
        }
        if (areaName == "Area3")
        {
            area3DialogueTrigger.SetActive(false);
        }
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
