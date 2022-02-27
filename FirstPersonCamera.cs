using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Public")]
    public bool enableHeadbob = true;
    public float amplitude = 0.015f;
    public float frequency = 10f;
    public float mouseSensitivityX = 65;
    public float mouseSensitivityY = 55;

    [Header("Static")]
    public Transform playerBody;
    public Transform cameraHolder;
    public TMP_Text objectNameTMP;

    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public GameObject focussedObject;
    [HideInInspector] public List<GameObject> focussedOjects = new List<GameObject>();

    PlayerController playerController;
    WeaponController weaponController;
    InputManager inputManager;
    InventoryComponents inventoryComponents;
    TableOfElements tableOfElements;
    CraftablesManager craftablesManager;
    HandAnimation handAnimation;
    Camera mainCamera;
    Help help;
    PlayerData playerData;
    PlayerRotate playerRotate;
    Vector3 startPos;
    Quaternion currentRot;
    Quaternion endRot;
    //float xRotation = 0f;
    bool zooming;
    bool firstComponent = true;
    bool firstInstruction = true;
    public bool focus;
    float duration;
    float currentTime;
    bool forceRotate;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        weaponController = FindObjectOfType<WeaponController>();
        inputManager = FindObjectOfType<InputManager>();
        inventoryComponents = FindObjectOfType<InventoryComponents>();
        tableOfElements = FindObjectOfType<TableOfElements>();
        craftablesManager = FindObjectOfType<CraftablesManager>();
        handAnimation = FindObjectOfType<HandAnimation>();
        help = FindObjectOfType<Help>();
        playerData = FindObjectOfType<PlayerData>();

        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = GetComponent<Camera>();
        startPos = mainCamera.transform.localPosition;

        DisableOutlines();
    }

    private void Update()
    {
        //Check if Headbob is enabled
        if (enableHeadbob == true)
        {
            CheckMotion();
            ResetPosition();

            if (focus == true)
            {
                mainCamera.transform.LookAt(FocusTarget());
            }
        }

        //Outline interactables
        if (canInteract == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
            {
                if (hit.collider.gameObject.layer == 3) //3 = Interactable
                {
                    focussedObject = hit.collider.gameObject;
                    objectNameTMP.text = focussedObject.name;

                    if (focussedOjects.Count == 0)  //if looking at object and there is no previous object, enable outline
                    {
                        focussedOjects.Add(focussedObject);
                        if (focussedOjects[0].GetComponent<Outline>() != null)
                        {
                            focussedOjects[0].GetComponent<Outline>().enabled = true;
                        }
                    }

                    //If looking at object and there is a previous object, disable previous and add new
                    if (focussedOjects.Count > 0 && focussedObject != focussedOjects[0])
                    {
                        if (focussedOjects[0].GetComponent<Outline>() != null)
                        {
                            focussedOjects[0].GetComponent<Outline>().DisableOutline();
                        }
                        focussedOjects.Clear();
                        focussedOjects.Add(focussedObject);
                        if (focussedOjects[0].GetComponent<Outline>() != null)
                        {
                            focussedOjects[0].GetComponent<Outline>().enabled = true;
                        }
                    }

                    //Action button when focussed on object
                    if (Input.GetKeyDown(KeyCode.E) && focussedObject != null && weaponController.playingWeaponSwitchAnimation == false)
                    {
                        StartCoroutine(weaponController.UnequipWeapon(true));    //Unequip currentWeapon and store it

                        if (focussedObject.tag == "Item")
                        {  
                            //Insert components
                            for (int i = 0; i < focussedObject.GetComponent<Item>().components.Length; i++)
                            {
                                inventoryComponents.InsertComponentInInventory(focussedObject.GetComponent<Item>().components[i]);
                            }
                            //Insert elements
                            for (int i = 0; i < focussedObject.GetComponent<Item>().elements.Length; i++)
                            {
                                if (!tableOfElements.tableOfElementsStats.elementsDiscovered.Contains(focussedObject.GetComponent<Item>().elements[i]))
                                {
                                    tableOfElements.tableOfElementsStats.elementsDiscovered.Add(focussedObject.GetComponent<Item>().elements[i]);
                                    playerData.playerStats.experience += 30f;
                                }
                            }

                            //If the distance is too big, zoom in before grabbing
                            if (Vector3.Distance(mainCamera.transform.position, focussedObject.transform.position) > 1.5f)
                            {
                                StartCoroutine(Zoom());
                            }
                            StartCoroutine(GrabItem(focussedObject));

                            if (firstComponent == true)
                            {
                                help.DisplayHelp("You've dismantled an item into usable components. Press 'I' to view it in your components inventory", 10);
                                firstComponent = false;
                            }
                        }
                        if (focussedObject.tag == "CraftingInstruction")
                        {
                            craftablesManager.GetComponent<CraftablesManager>().craftablesStats.unlockedCraftables.Add(focussedObject.GetComponent<CraftingInstruction>().craftableName);

                            //If the distance is too big, zoom in before grabbing
                            if (Vector3.Distance(mainCamera.transform.position, focussedObject.transform.position) > 1.5f)
                            {
                                StartCoroutine(Zoom());
                            }
                            StartCoroutine(GrabInstruction(focussedObject));

                            if (firstInstruction == true)
                            {
                                help.DisplayHelp("You've found a crafting-instruction for a weapon or a craftable component. Head to a crafting station to craft it", 10);
                                firstInstruction = false;
                            }
                        }
                        if (focussedObject.tag == "CraftingBenchObject")
                        {
                            inputManager.ToggleCraftingBench();
                        }
                        if (focussedObject.tag == "QuizDisplayer")
                        {
                            focussedObject.GetComponent<QuizDisplayer>().InsertQuiz();
                            inputManager.ToggleQuiz();
                        }
                        if (focussedObject.tag == "BookChallenge")
                        {
                            focussedObject.GetComponent<BookChallenge>().OpenDoor();
                        }
                        if (focussedObject.tag == "FinalDoor")
                        {
                            focussedObject.GetComponent<ExitDoor>().ExitFinalDoor();
                            DisableOutlines();
                        }
                    }

                    //Holding input button
                    if (Input.GetKey(KeyCode.E) && focussedObject != null && weaponController.playingWeaponSwitchAnimation == false && focussedObject.tag == "PressButton")
                    {
                        focussedObject.GetComponent<PressButton>().ChangeTemperature();
                    }
                }
                else //If its not looking at an interactable object, disable it
                {
                    DisableOutlines();
                    objectNameTMP.text = null;
                }
            }
            else //If its not looking at anything at all, disable it
            {
                DisableOutlines();
                objectNameTMP.text = null;
            }
        }

        if (forceRotate == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > duration)
            {
                currentTime = duration;
            }

            float t = currentTime / duration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            cameraHolder.transform.localRotation = Quaternion.Lerp(currentRot, Quaternion.Euler(endRot.x, 0f, 0f), t);

            if (cameraHolder.transform.localRotation == Quaternion.Euler(0, 0f, 0f))
            {
                forceRotate = false;
            }
        }
    }

/*    private void FixedUpdate()
    {
        if (canInteract == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * 5 * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * 5 * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            cameraHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }*/

    public void ForceRotateCamera(Quaternion takenEndRot, float takenDuration)
    {
        currentTime = 0;
        currentRot = cameraHolder.transform.localRotation;
        endRot = takenEndRot;
        duration = takenDuration;
        forceRotate = true;
        print("cameraforcemoved");
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, cameraHolder.position.y, transform.position.z);
        pos += cameraHolder.forward * 50f;
        return pos;
    }

    private void CheckMotion()
    {
        if (canInteract == true)
        {
            if (playerController.horizontalMovement != 0 || playerController.verticalMovement != 0)
            {
                PlayMotion(FootStepMotion());
            }
        }
    }

    private void PlayMotion(Vector3 motion)
    {
        mainCamera.transform.localPosition += motion;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude;
        return pos;
    }

    private void ResetPosition()
    {
        if (zooming == false)
        {
            if (mainCamera.transform.localPosition == startPos) return;
            {
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, startPos, 3f * Time.deltaTime);
            }
        }
    }

    public void DisableOutlines()
    {
        if (focussedOjects.Count > 0)   //If not looking to an object (no raycast hit), disable the outline of previous object if there is one
        {
            if (focussedOjects[0].GetComponent<Outline>() != null)
            {
                focussedOjects[0].GetComponent<Outline>().DisableOutline();
            }
            focussedOjects.Clear();
        }
    }

    private IEnumerator GrabItem (GameObject itemToGrab)
    {
        StartCoroutine(handAnimation.GrabItemAnimation());
        yield return new WaitForSeconds(1.25f);
        Destroy(itemToGrab);
        focussedOjects.Clear();
        focussedObject = null;
        objectNameTMP.text = null;
    }

    private IEnumerator GrabInstruction(GameObject instructionToGrab)
    {
        StartCoroutine(handAnimation.GrabInstructionAnimation());
        yield return new WaitForSeconds(0.75f);
        Destroy(instructionToGrab);
        focussedOjects.Clear();
        focussedObject = null;
        objectNameTMP.text = null;
    }

    private IEnumerator Zoom()
    {
        float zoomDistance = Vector3.Distance(mainCamera.transform.position, focussedObject.transform.position) * 0.4f;
        float currentTime = 0;
        float duration = 0.1f;
        Vector3 currentPosition = mainCamera.transform.localPosition;
        Vector3 newPosition = mainCamera.transform.localPosition + Vector3.forward * zoomDistance;

        while (currentTime < duration)
        {
            zooming = true;
            currentTime += Time.deltaTime;
            mainCamera.transform.localPosition = Vector3.Lerp(currentPosition, newPosition, currentTime / duration);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        zooming = false;
    }
}
