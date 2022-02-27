using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;
using System;

public class ChargesManager : MonoBehaviour
{
    [HideInInspector] public int weapon4Charges;

    WeaponData weaponData;

    private void Start()
    {
        weaponData = FindObjectOfType<WeaponData>();

        weapon4Charges = weaponData.weapon4Stats.maxCharges;
    }
}
