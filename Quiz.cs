using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [HideInInspector] public bool opened;

    public WeaponController weaponController;

    private void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();
        opened = true;
    }

    public void OpenQuiz()
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
        TMP_InputField[] textInputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField textInputField in textInputFields)
        {
            textInputField.enabled = true;
        }

        opened = true;
    }

    public void CloseQuiz()
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
            TMP_InputField[] textInputFields = GetComponentsInChildren<TMP_InputField>();
            foreach (TMP_InputField textInputField in textInputFields)
            {
                textInputField.enabled = false;
            }

            opened = false;
            BroadcastMessage("RetractToParent", SendMessageOptions.DontRequireReceiver);

            weaponController.RestoreWeapon();
        }
    }
}
