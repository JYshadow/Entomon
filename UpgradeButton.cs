using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJSON;
using System.IO;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    WeaponData weapondata;
    Knowledge knowledge;
    UpgradePanel upgradePanel;
    public bool upgraded;

    private void Start()
    {
        weapondata = FindObjectOfType<WeaponData>();
        knowledge = FindObjectOfType<Knowledge>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        //gameObject.transform.GetChild(0).gameObject.SetActive(false);   //Glow disabled
    }

    public void MouseEnter(string upgradeName)    //If hovering over Element, display content
    {
        GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().DisplayTooltipName(upgradeName);
    }

    public void MouseExit() //If exiting Element, hide content
    {
        GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>().HideTooltip();
    }

    public void Update()
    {
        if (upgradePanel.opened == true)
        {
            if (upgraded == true)
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;    //Glow enabled
            }
            else
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;    //Glow enabled
            }
        }
    }

    //WEAPON5
    public void Weapon5Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5StatsBase.json"))["damage"] * increase;
            weapondata.weapon5Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon5MaxHeat(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5StatsBase.json"))["maxHeat"] * increase;
            weapondata.weapon5Stats.maxHeat += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon5WarmupTime(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5StatsBase.json"))["warmedUpTime"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon5StatsBase.json"))["warmedUpTime"] / (1 + increase));
            weapondata.weapon5Stats.warmedUpTime -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    //WEAPON6
    public void Weapon6Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6StatsBase.json"))["damage"] * increase;
            weapondata.weapon6Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon6Shootspeed(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6StatsBase.json"))["msBetweenShots"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6StatsBase.json"))["msBetweenShots"] / (1 + increase));
            weapondata.weapon6Stats.msBetweenShots -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon6Chains(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            weapondata.weapon6Stats.chains += increase;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon6Range(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon6StatsBase.json"))["viewRadius"] * increase;
            weapondata.weapon6Stats.viewRadius += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    //WEAPON8
    public void Weapon8Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8StatsBase.json"))["damage"] * increase;
            weapondata.weapon8Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon8Shootspeed(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8StatsBase.json"))["msBetweenShots"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8StatsBase.json"))["msBetweenShots"] / (1 + increase));
            weapondata.weapon8Stats.msBetweenShots -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon8Radius(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon8StatsBase.json"))["radius"] * increase;
            weapondata.weapon8Stats.radius += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    //WEAPON9
    public void Weapon9Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["damage"] * increase;
            weapondata.weapon9Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon9Dropspeed(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["msBetweenShots"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["msBetweenShots"] / (1 + increase));
            weapondata.weapon9Stats.msBetweenShots -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon9Duration(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["lifetime"] * increase;
            weapondata.weapon9Stats.lifetime += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon9ArmingTime(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["armingTime"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["armingTime"] / (1 + increase));
            weapondata.weapon9Stats.armingTime -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon9Radius(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon9StatsBase.json"))["radius"] * increase;
            weapondata.weapon9Stats.radius += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    //WEAPON10
    public void Weapon10Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10StatsBase.json"))["damage"] * increase;
            weapondata.weapon10Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon10Throwspeed(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10StatsBase.json"))["msBetweenShots"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10StatsBase.json"))["msBetweenShots"] / (1 + increase));
            weapondata.weapon10Stats.msBetweenShots -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon10Radius(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon10StatsBase.json"))["radius"] * increase;
            weapondata.weapon10Stats.radius += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    //WEAPON11
    public void Weapon11Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"))["damage"] * increase;
            weapondata.weapon11Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon11Throwspeed(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"))["msBetweenShots"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"))["msBetweenShots"] / (1 + increase));
            weapondata.weapon11Stats.msBetweenShots -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon11Duration(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"))["duration"] * increase;
            weapondata.weapon11Stats.duration += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon11Puddlesize(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"))["puddleRadius"] * increase;
            weapondata.weapon11Stats.puddleRadius += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon11Slowpecentage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon11StatsBase.json"))["slowPercentage"] * increase;
            weapondata.weapon11Stats.slowPercentage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    //WEAPON12
    public void Weapon12Damage(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"))["damage"] * increase;
            weapondata.weapon12Stats.damage += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon12ShootingSpeed(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"))["msBetweenShots"] - (JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"))["msBetweenShots"] / (1 +increase));
            weapondata.weapon12Stats.msBetweenShots -= valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon12Range(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"))["range"] * increase;
            weapondata.weapon12Stats.range += valueIncrease;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }

    public void Weapon12Projectiles(float increase)
    {
        if (upgraded == false && knowledge.knowledge > 0)
        {
            float valueIncrease = JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"))["sideProjectiles"] * increase;
            weapondata.weapon12Stats.sideProjectiles += valueIncrease;
            weapondata.weapon12Stats.damage -= JSON.Parse(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons/weapon12StatsBase.json"))["damage"] * 0.25f;
            knowledge.knowledge -= 1;
            upgraded = true;
        }
        else
        {
            print("Already upgraded");
        }
    }
}
