using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    TMP_Text TMPhelp;
    Image helpBackground;
    public float elapsed;
    string helpText;
    float duration;

    private void Start()
    {
        TMPhelp = transform.GetChild(0).GetComponent<TMP_Text>();
        helpBackground = GetComponent<Image>();

        helpBackground.enabled = false;
        TMPhelp.text = "";
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            helpBackground.enabled = true;
            TMPhelp.text = helpText;
        }

        if (elapsed >= duration)
        {
            helpBackground.enabled = false;
            TMPhelp.text = "";
        }
    }


    public void DisplayHelp(string takenHelpText, float takenduration)
    {
        elapsed = 0;
        helpText = takenHelpText;
        duration = takenduration;
    }
}
