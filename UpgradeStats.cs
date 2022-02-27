using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStats : MonoBehaviour
{
    public enum Weapon { Weapon5, Weapon6, Weapon8, Weapon9, Weapon10, Weapon11, Weapon12 }
    public Weapon weapon;

    WeaponData weaponData;
    TMP_Text TMPStats;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();
        TMPStats = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (weapon == Weapon.Weapon5)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon5Stats.damage.ToString("F1") + " damage per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Maximum temperature: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon5Stats.maxHeat.ToString("F1") + " °C </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Warm-up time: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon5Stats.warmedUpTime.ToString("F1") + " seconds </color></size>";
        }

        if (weapon == Weapon.Weapon6)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon6Stats.damage.ToString("F1") + " damage per shock </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Shock rate: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + (1000 / weaponData.weapon6Stats.msBetweenShots).ToString("F1") + " shocks per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Total chains: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon6Stats.chains.ToString("F1") + " chains per shock </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Shock range: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon6Stats.viewRadius + " meters </color></size>";
        }

        if (weapon == Weapon.Weapon8)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon8Stats.damage.ToString("F1") + " damage per balloon </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Throwing rate: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + (1000 / weaponData.weapon8Stats.msBetweenShots).ToString("F1") + " throws per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Explosion diameter: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon8Stats.radius + " meters </color></size>";
        }

        if (weapon == Weapon.Weapon9)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon9Stats.damage.ToString("F1") + " damage per balloon </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Dropping rate: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + (1000 / weaponData.weapon9Stats.msBetweenShots).ToString("F1") + " traps per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Trap duration: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon9Stats.lifetime.ToString("F1") + " seconds </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Arming time: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon9Stats.armingTime.ToString("F1") + " seconds </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Explosion diameter: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon9Stats.radius + " meters </color></size>";
        }

        if (weapon == Weapon.Weapon10)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon10Stats.damage.ToString("F1") + " damage per balloon </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Throw rate: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + (1000 / weaponData.weapon10Stats.msBetweenShots).ToString("F1") + " throws per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Explosion diameter: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon10Stats.radius + " meters </color></size>";
        }

        if (weapon == Weapon.Weapon11)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon11Stats.damage.ToString("F1") + " damage per balloon </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Throw rate: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + (1000 / weaponData.weapon11Stats.msBetweenShots).ToString("F1") + " throws per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Slowing puddle duration: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon11Stats.duration.ToString("F1") + " seconds </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Slowing puddle diameter: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon11Stats.puddleRadius.ToString("F1") + " meters </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Slow amount: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon11Stats.slowPercentage + "% </color></size>";
        }

        if (weapon == Weapon.Weapon12)
        {
            TMPStats.text =
                "<size=18><color=#8d9dec><b>Damage: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + weaponData.weapon12Stats.damage.ToString("F1") + " damage per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Shooting rate: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + (1000 / weaponData.weapon12Stats.msBetweenShots).ToString("F1") + " shots per second </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Projectile range: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" +  (10 * weaponData.weapon12Stats.range).ToString("F1") + " meters </color></size>" +
                "\n<size=6> </size>" +
                "\n<size=18><color=#8d9dec><b>Projectiles: </size></color></b>" +
                "\n<size=16><color=#FFFFFF>" + 2 * weaponData.weapon12Stats.sideProjectiles + " projectiles per shot </color></size>";
        }
    }
}
