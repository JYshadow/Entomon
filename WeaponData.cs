using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class WeaponData : MonoBehaviour
{
    //WeaponsStats
    public Weapon1Stats weapon1Stats = new Weapon1Stats();
    [System.Serializable]
    public class Weapon1Stats
    {
        public float damage;
        public float msBetweenShots;
        public float range;
        public float angle;
    }

    public Weapon2Stats weapon2Stats = new Weapon2Stats();
    [System.Serializable]
    public class Weapon2Stats
    {
        public float damage;
        public float msBetweenShots;
        public float projectileSpeed;
        public int sideProjectiles;
    }

    public Weapon3Stats weapon3Stats = new Weapon3Stats();
    [System.Serializable]
    public class Weapon3Stats
    {
        public float damage;
        public float msBetweenShots;
        public float projectileSpeed;
        public float explosionLifetime;
        public bool stackable;
    }

    public Weapon4Stats weapon4Stats = new Weapon4Stats();
    [System.Serializable]
    public class Weapon4Stats
    {

        public float damage;
        public float msBetweenShots;
        public float projectileSpeed;
        public float range;
        public float lifetime;
        public int maxCharges;
    }

    public Weapon5Stats weapon5Stats = new Weapon5Stats();
    [System.Serializable]
    public class Weapon5Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;
        public float range;
        public float angle;
        public float maxHeat;//
        public float warmedUpTime;//
    }

    public Weapon6Stats weapon6Stats = new Weapon6Stats();
    [System.Serializable]
    public class Weapon6Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;//
        public float timeBetweenChains;
        public float chains;//
        public float viewAngle;
        public float viewRadius;//
    }

    public Weapon7Stats weapon7Stats = new Weapon7Stats();
    [System.Serializable]
    public class Weapon7Stats
    {
        public float damage;
        public float msBetweenShots;
        public float range;
        public float angle;
    }

    public Weapon8Stats weapon8Stats = new Weapon8Stats();
    [System.Serializable]
    public class Weapon8Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;//
        public float radius;//
    }

    public Weapon9Stats weapon9Stats = new Weapon9Stats();
    [System.Serializable]
    public class Weapon9Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;//
        public float lifetime;//
        public float armingTime;//
        public float radius;//
        public float triggerRadius;
    }

    public Weapon10Stats weapon10Stats = new Weapon10Stats();
    [System.Serializable]
    public class Weapon10Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;//
        public float radius;//
    }

    public Weapon11Stats weapon11Stats = new Weapon11Stats();
    [System.Serializable]
    public class Weapon11Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;//
        public float radius;
        public float duration;//
        public float puddleRadius;//
        public float slowPercentage;//
    }

    public Weapon12Stats weapon12Stats = new Weapon12Stats();
    [System.Serializable]
    public class Weapon12Stats   //USED
    {
        public float damage;//
        public float msBetweenShots;//
        public float range;//
        public float sideProjectiles;//
    }

    private void Awake()
    {
        SaveWeaponStats();
        SaveWeaponStatsBase();   //Save base values

        //Read SavedData
        //weapon1Stats = JsonUtility.FromJson<Weapon1Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon1Stats.json"));
        //weapon2Stats = JsonUtility.FromJson<Weapon2Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon2Stats.json"));
        //weapon3Stats = JsonUtility.FromJson<Weapon3Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon3Stats.json"));
        //weapon4Stats = JsonUtility.FromJson<Weapon4Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon4Stats.json"));
        weapon5Stats = JsonUtility.FromJson<Weapon5Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5Stats.json"));
        weapon6Stats = JsonUtility.FromJson<Weapon6Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6Stats.json"));
        //weapon7Stats = JsonUtility.FromJson<Weapon7Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon7Stats.json"));
        weapon8Stats = JsonUtility.FromJson<Weapon8Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8Stats.json"));
        weapon9Stats = JsonUtility.FromJson<Weapon9Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9Stats.json"));
        weapon10Stats = JsonUtility.FromJson<Weapon10Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10Stats.json"));
        weapon11Stats = JsonUtility.FromJson<Weapon11Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11Stats.json"));
        weapon12Stats = JsonUtility.FromJson<Weapon12Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12Stats.json"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            //LoadWeaponStatsBase();   //Save base values
        }
    }

    //Saving weapon
    private void SaveWeaponStats()
    {
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon1Stats.json", JsonUtility.ToJson(weapon1Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon2Stats.json", JsonUtility.ToJson(weapon2Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon3Stats.json", JsonUtility.ToJson(weapon3Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon4Stats.json", JsonUtility.ToJson(weapon4Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5Stats.json", JsonUtility.ToJson(weapon5Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6Stats.json", JsonUtility.ToJson(weapon6Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon7Stats.json", JsonUtility.ToJson(weapon7Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8Stats.json", JsonUtility.ToJson(weapon8Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9Stats.json", JsonUtility.ToJson(weapon9Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10Stats.json", JsonUtility.ToJson(weapon10Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11Stats.json", JsonUtility.ToJson(weapon11Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12Stats.json", JsonUtility.ToJson(weapon12Stats, true));
        print("weaponStats saved");
    }

    private void SaveWeaponStatsBase()
    {
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon1StatsBase.json", JsonUtility.ToJson(weapon1Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon2StatsBase.json", JsonUtility.ToJson(weapon2Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon3StatsBase.json", JsonUtility.ToJson(weapon3Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon4StatsBase.json", JsonUtility.ToJson(weapon4Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5StatsBase.json", JsonUtility.ToJson(weapon5Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6StatsBase.json", JsonUtility.ToJson(weapon6Stats, true));
        //File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon7StatsBase.json", JsonUtility.ToJson(weapon7Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8StatsBase.json", JsonUtility.ToJson(weapon8Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json", JsonUtility.ToJson(weapon9Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10StatsBase.json", JsonUtility.ToJson(weapon10Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json", JsonUtility.ToJson(weapon11Stats, true));
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json", JsonUtility.ToJson(weapon12Stats, true));
        print("weaponStatsBase saved");
    }

    private void LoadWeaponStatsBase()
    {
        //weapon1Stats = JsonUtility.FromJson<Weapon1Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon1StatsBase.json"));
        //weapon2Stats = JsonUtility.FromJson<Weapon2Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon2StatsBase.json"));
        //weapon3Stats = JsonUtility.FromJson<Weapon3Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon3StatsBase.json"));
        //weapon4Stats = JsonUtility.FromJson<Weapon4Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon4StatsBase.json"));
        weapon5Stats = JsonUtility.FromJson<Weapon5Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5StatsBase.json"));
        weapon6Stats = JsonUtility.FromJson<Weapon6Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6StatsBase.json"));
        //weapon7Stats = JsonUtility.FromJson<Weapon7Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon7StatsBase.json"));
        weapon8Stats = JsonUtility.FromJson<Weapon8Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8StatsBase.json"));
        weapon9Stats = JsonUtility.FromJson<Weapon9Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"));
        weapon10Stats = JsonUtility.FromJson<Weapon10Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10StatsBase.json"));
        weapon11Stats = JsonUtility.FromJson<Weapon11Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"));
        weapon12Stats = JsonUtility.FromJson<Weapon12Stats>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"));
        print("WeaponStatsBase loaded");
    }
}
