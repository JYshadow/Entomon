using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class PlayerController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource footstepSound; 

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float delayedShotTime;
    [HideInInspector] public bool canShoot = true;

    WeaponController weaponController;
    ChargesManager chargesManager;
    HandAnimation handAnimation;
    PlayerRotate playerRotate;
    PlayerRotate playerRotateSmooth;
    PlayerRotate currentPlayerRotate;

    private void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();
        chargesManager = FindObjectOfType<ChargesManager>();
        handAnimation = FindObjectOfType<HandAnimation>();

        playerRotate = GetComponents<PlayerRotate>()[0];
        playerRotateSmooth = GetComponents<PlayerRotate>()[1];

        currentPlayerRotate = playerRotate;
    }

    private void Update()
    {
        if (canInteract == true)
        {
            //Movement input
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
            moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

            if (horizontalMovement != 0 || verticalMovement != 0)
            {
                if (footstepSound.isPlaying == false)
                {
                    footstepSound.Play();
                }
            }
            else
            {
                footstepSound.Stop();
            }


            //Shooting input   
            if (weaponController.currentWeapon != null)
            {
                if (weaponController.playingWeaponSwitchAnimation == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (weaponController.currentWeapon.GetComponent<Weapon>().shotDelayInMs > 0)
                        {
                            StartCoroutine(DelayShot(weaponController.currentWeapon.GetComponent<Weapon>().shotDelayInMs));
                        }
                    }

                    if (Input.GetMouseButton(0))
                    {
                        if (weaponController.currentWeapon.GetComponent<Weapon>().pressAnimation != "")
                        {
                            handAnimation.PlayHandAnimation(weaponController.currentWeapon.GetComponent<Weapon>().pressAnimation, 0.1f);
                        }

                        if (canShoot == true)
                        {
                            weaponController.currentWeapon.GetComponent<Trigger>().Shoot();  //Call Shoot() function in every script with Trigger
                            PlayParticle();

                            if (weaponController.currentWeapon.GetComponent<Weapon>().pressSoundSustain != null && weaponController.currentWeapon.GetComponent<Weapon>().pressSoundSustain.isPlaying == false)
                            {
                                weaponController.currentWeapon.GetComponent<Weapon>().pressSoundSustain.Play();
                            }
                        }
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        canShoot = true;
                        StopParticle();

                        if (weaponController.currentWeapon.GetComponent<Weapon>().releaseAnimation != "")
                        {
                            handAnimation.PlayHandAnimation(weaponController.currentWeapon.GetComponent<Weapon>().releaseAnimation, 0.1f);
                        }

                        if (weaponController.currentWeapon.GetComponent<Weapon>().pressSoundSustain != null)
                        {
                            weaponController.currentWeapon.GetComponent<Weapon>().pressSoundSustain.Stop();
                        }
                    }
                }
                else
                {
                    if (weaponController.currentWeapon.GetComponent<WeaponWithParticle>() != null)
                    {
                        canShoot = true;
                        StopParticle();
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (canInteract == true)
        {
            //Looking input
            currentPlayerRotate.Rotate();
        }
    }

    public void PlayParticle()
    {
        if (weaponController.currentWeapon != null && weaponController.currentWeapon.GetComponent<WeaponWithParticle>() != null)
        {
            weaponController.currentWeapon.GetComponent<WeaponWithParticle>().PlayParticle();
        }
    }

    public void StopParticle()
    {
        if (weaponController.currentWeapon != null && weaponController.currentWeapon.GetComponent<WeaponWithParticle>() != null)
        {
            weaponController.currentWeapon.GetComponent<WeaponWithParticle>().StopParticle();
        }
    }

    public void ResetAnimationToIdle()
    {
        handAnimation.PlayHandAnimation(weaponController.currentWeapon.GetComponent<Weapon>().idleAnimation, 0.1f);
    }

    private IEnumerator DelayShot(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay / 1000);
        canShoot = true;
    }
}
