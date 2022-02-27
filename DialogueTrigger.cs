using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Public")]
    [TextArea(5, 20)] public string dialogueToPlay;
    [TextArea(5, 20)] public string helpToPlay;
    public float dialogueDuration;
    public float helpDuration;
    public bool canRepeat;

    Dialogue dialogue;
    Help help;
    bool canPlay = true;

    private void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
        help = FindObjectOfType<Help>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerHitbox" && canPlay == true)
        {
            if (dialogueToPlay != "")
            {
                dialogue.DisplayDialogue(dialogueToPlay, dialogueDuration);

                if (canRepeat == false)
                {
                    canPlay = false;
                }
            }
            if (helpToPlay != "")
            {
                help.DisplayHelp(helpToPlay, helpDuration);
                if (canRepeat == false)
                {
                    canPlay = false;
                }
            }
        }
    }
}
