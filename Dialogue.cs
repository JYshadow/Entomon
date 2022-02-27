using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    TMP_Text TMPdialogue;
    Image dialogueBackground;
    string dialogueText;
    float elapsed;
    float duration;

    private void Start()
    {
        TMPdialogue = transform.GetChild(0).GetComponent<TMP_Text>();
        dialogueBackground = GetComponent<Image>();

        dialogueBackground.enabled = false;
        TMPdialogue.text = "";
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            dialogueBackground.enabled = true;
            TMPdialogue.text = dialogueText;
        }

        if (elapsed >= duration)
        {
            dialogueBackground.enabled = false;
            TMPdialogue.text = "";
        }
    }

    public void DisplayDialogue(string takenDialogueText, float takenduration)
    {
        elapsed = 0;
        dialogueText = takenDialogueText;
        duration = takenduration;
    }
}
