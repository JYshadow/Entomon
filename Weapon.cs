using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Public")]
    public float shotDelayInMs = 0;

    [Header("Properties")]
    public string weaponName;
    [TextArea(15, 20)] public string descriptionCraft;
    public string descriptionInventory;
    public Sprite sprite;
    public GameObject upgradePanel;
    public Requirements[] requirements;

    [Header("Hands Animation")]
    public string idleAnimation;
    public string equipAnimation;
    public string unequipAnimation;
    public string pressAnimation;
    public string releaseAnimation;

    [Header("Sounds")]
    public AudioSource pressSoundSustain;

    /*public Animator weaponAnimator;
    public string weaponPressAnimation;
    public string weaponReleaseAnimation;*/

    [System.Serializable]
    public class Requirements
    {
        public GameObject component;
        public int amount;
    }

    string currentAnim;

    /*public void Update()
    {
        if (weaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !weaponAnimator.IsInTransition(0))
        {
            PlayWeaponAnimation(weaponIdle, 0,)
        }
    }*/

    /*public void PlayWeaponAnimation(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim) return; //if current animation is the same, do nothing
        weaponAnimator.CrossFade(newAnim, fadeDuration);
        currentAnim = newAnim;  //new animation is now current animation
    }*/
}
