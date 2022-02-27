using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bars : MonoBehaviour
{
    public enum BarType { Health, EXP, Level }
    [Header("Public")]
    public BarType barType;

    [Header("Static")]
    public Animator animatorLevelbar;

    PlayerData playerData;
    Player player;
    Knowledge knowledge;
    float barStart;
    float experienceInLevel;
    string currentAnim;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        player = FindObjectOfType<Player>();
        knowledge = FindObjectOfType<Knowledge>();

        barStart = GetComponent<RectTransform>().anchoredPosition.y;
    }

    private void Update()
    {
        if (barType == BarType.Health)
        {
            float healthPercentage = player.currentHealth / playerData.playerStats.startingHealth;
            GetComponent<RectTransform>().anchoredPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, barStart * healthPercentage, GetComponent<RectTransform>().localPosition.z);
        }
        if (barType == BarType.EXP)
        {
            experienceInLevel = playerData.playerStats.experience - 100f * knowledge.playerLevel;
            float knowledgePercentage = experienceInLevel / 100f;
            GetComponent<RectTransform>().anchoredPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, barStart * knowledgePercentage, GetComponent<RectTransform>().localPosition.z);
        }
        if (barType == BarType.Level)
        {
            transform.Find("Icon").GetChild(0).GetComponent<TMP_Text>().text = "<size=75>Level</size>" + "\n<size=120><color=#ffc000><b>" + (knowledge.playerLevel + 1).ToString() + "</size></color></b>";
        }
    }

    private void PlayAnimation(string newAnim, float fadeDuration)  //Play animation with a certain name with a certain fade duration
    {
        if (currentAnim == newAnim) return; //If current animation is the same, do nothing
        animatorLevelbar.CrossFade(newAnim, fadeDuration);
        currentAnim = newAnim;  //New animation is now current animation
    }

    public IEnumerator DisplayUpgradesAvailable()
    {
        if (animatorLevelbar != null)
        {
            PlayAnimation("Levelbar_display", 0.0f);
            yield return new WaitForSeconds(10f);
            PlayAnimation("Levelbar_idle", 0.0f);
        }
    }
}
