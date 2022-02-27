using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class CraftablesManager : MonoBehaviour
{
    //SavedData
    public CraftablesStats craftablesStats = new CraftablesStats();
    [System.Serializable]
    public class CraftablesStats
    {
        public List<string> unlockedCraftables = new List<string>();
    }

    private void Awake()
    {
        SaveCraftables();

        //Read SavedData
        craftablesStats = JsonUtility.FromJson<CraftablesStats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/craftablesStats.json"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
        }
    }

    private void SaveCraftables()
    {
        File.WriteAllText(Application.dataPath + "/StreamingAssets/craftablesStats.json", JsonUtility.ToJson(craftablesStats, true));
        print("craftablesStats saved");
    }
}
