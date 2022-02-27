using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using SimpleJSON;

public class TableOfElements : MonoBehaviour
{
    [Header("Static")]
    public GameObject elementsContainer;

    [HideInInspector] public bool opened;

    //SavedData
    public TableOfElementsStats tableOfElementsStats = new TableOfElementsStats();
    [System.Serializable]
    public class TableOfElementsStats
    {
        public List<string> elementsDiscovered = new List<string>();
    }

    Help help;

    private void Awake()
    {
        SaveTableOfElements();

        //Read SavedData
        tableOfElementsStats = JsonUtility.FromJson<TableOfElementsStats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/tableOfElementsStats.json"));
    }

    private void Start()
    {
        help = FindObjectOfType<Help>();

        opened = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
        }
    }

    public void OpenTableOfElements()
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

        opened = true;

        help.DisplayHelp("Every component dismantled is made of different elements. Each new element you discover will grant you experience-points. Hover on each for more info.", 12f);

        CheckElementsDiscovered();
    }

    public void CloseTableOfElements()
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
        }
    }

    void CheckElementsDiscovered()
    {
        //Check elements glow
        for (int i = 0; i < elementsContainer.transform.childCount; i++)
        {
            if (elementsContainer.transform.GetChild(i).GetComponent<Element>() != null)
            {
                if (tableOfElementsStats.elementsDiscovered.Contains(elementsContainer.transform.GetChild(i).name))
                {
                    elementsContainer.transform.GetChild(i).GetComponent<Element>().discovered = true;
                    elementsContainer.transform.GetChild(i).GetComponent<Element>().glowImage.gameObject.SetActive(true);
                }
                else
                {
                    elementsContainer.transform.GetChild(i).GetComponent<Element>().discovered = false;
                    elementsContainer.transform.GetChild(i).GetComponent<Element>().glowImage.gameObject.SetActive(false);
                }
            }
        }
    }

    private void SaveTableOfElements()
    {
        File.WriteAllText(Application.dataPath + "/StreamingAssets/tableOfElementsStats.json", JsonUtility.ToJson(tableOfElementsStats, true));
        print("tableOfElementsStats saved");
    }
}
