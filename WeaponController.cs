using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Public")]
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    [HideInInspector] public GameObject currentWeapon;
    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public bool playingWeaponSwitchAnimation;
    [HideInInspector] public string weaponSelection;
    [HideInInspector] public bool storedWeapon;

    [Header("Audio")]
    public AudioSource weaponSwitchSound;

    HandAnimation handAnimation;
    PlayerController playerController;
    Help help;
    Transform weaponPosition;
    float animationTime = 0.25f;
    bool firtWeapon = true;

    private void Start()
    {
        handAnimation = FindObjectOfType<HandAnimation>();
        playerController = FindObjectOfType<PlayerController>();
        help = FindObjectOfType<Help>();
        weaponPosition = GameObject.FindGameObjectWithTag("WeaponPosition").transform;
    }

    private void Update()
    {
        if (canInteract == true)
        {
            //Primary weapons
            if (Input.GetKeyDown(KeyCode.Alpha1) && primaryWeapon != null)
            {
                StartCoroutine(Primary());

                if (firtWeapon == true)
                {
                    help.DisplayHelp("Hold left mousebutton to fire the weapon. A weapon's effictiveness depends on the type of enemy you're firing at.", 10);
                    firtWeapon = false;
                }
            }

            //Secondary weapons
            if (Input.GetKeyDown(KeyCode.Alpha2) && secondaryWeapon != null)
            {
                StartCoroutine(Secondary());

                if (firtWeapon == true)
                {
                    help.DisplayHelp("Hold left mousebutton to fire the weapon. A weapon's effictiveness depends on the type of enemy you're firing at.", 10);
                    firtWeapon = false;
                }
            }
        }
    }

    private IEnumerator Primary()
    {
        if (currentWeapon == null)  //if current weapon is empty
        {
            StartCoroutine(EquipWeapon(primaryWeapon, "primary"));
        }
        else //if current weapon is not empty
        {
            if (weaponSelection != "primary")   //only un-equip if it is not primary selection
            {
                StartCoroutine(UnequipWeapon(false));
                yield return new WaitForSeconds(animationTime);
                StartCoroutine(EquipWeapon(primaryWeapon, "primary"));
            }
        }
        yield return null;
    }

    private IEnumerator Secondary()
    {
        if (currentWeapon == null)  //if current weapon is empty
        {
            StartCoroutine(EquipWeapon(secondaryWeapon, "secondary"));
        }
        else //if current weapon is not empty
        {
            if (weaponSelection != "secondary")   //only un-equip if it is not secondary selection
            {
                StartCoroutine(UnequipWeapon(false));
                yield return new WaitForSeconds(animationTime);
                StartCoroutine(EquipWeapon(secondaryWeapon, "secondary"));
            }
        }
        yield return null;
    }

    public IEnumerator UnequipWeapon(bool storeWeapon)
    {
        if (currentWeapon != null)
        {
            storedWeapon = storeWeapon;
            playerController.StopParticle();
            playingWeaponSwitchAnimation = true;
            handAnimation.PlayHandAnimation(currentWeapon.GetComponent<Weapon>().unequipAnimation, 0.0f);
            weaponSwitchSound.Play();
            yield return new WaitForSeconds(animationTime);

            Destroy(currentWeapon);
            playingWeaponSwitchAnimation = false;
        }
    }

    public IEnumerator EquipWeapon(GameObject weaponType, string weaponTypeString)
    {
        weaponSelection = weaponTypeString;

        playingWeaponSwitchAnimation = true;
        //PlayWeaponPositionAnimation("WeaponPosition_equip", 0.0f);  //defines initial position
        GameObject newCurrentWeapon = Instantiate(weaponType, weaponPosition.position, weaponPosition.rotation, weaponPosition);
        newCurrentWeapon.name = newCurrentWeapon.name.Replace("(Clone)", "");
        currentWeapon = newCurrentWeapon;
        handAnimation.handAnimator.Rebind();
        handAnimation.PlayHandAnimation(weaponType.GetComponent<Weapon>().equipAnimation, 0.0f);
        weaponSwitchSound.Play();
        yield return new WaitForSeconds(animationTime);

        playingWeaponSwitchAnimation = false;
    }

    public void RestoreWeapon()
    {
        if (storedWeapon == true) //Re-equip weapon if there is any stored
        {
            if (primaryWeapon != null && weaponSelection == "primary")
            {
                StartCoroutine(Primary());
                storedWeapon = false;
            }
            else if (secondaryWeapon != null && weaponSelection == "secondary")
            {
                StartCoroutine(Secondary());
                storedWeapon = false;
            }
        }
    }
}