using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class PlayerData : MonoBehaviour
{
    public PlayerStats playerStats = new PlayerStats();
    [System.Serializable]
    public class PlayerStats
    {
        public float startingHealth;
        public float moveSpeed;
        public float experience;
        public float pickupRange;
    }

    private void Awake()
    {
        SavePlayerStats();

        //Read SavedData
        playerStats = JsonUtility.FromJson<PlayerStats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/playerStats.json"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
        }
    }

    private void SavePlayerStats()
    {
        File.WriteAllText(Application.dataPath + "/StreamingAssets/playerStats.json", JsonUtility.ToJson(playerStats, true));
        print("playerStats saved");
    }
}
