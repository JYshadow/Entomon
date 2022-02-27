using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class Player : LivingEntity
{
    [Header("Public")]
    public float regenPerSecond;

    [HideInInspector] public bool canInteract;

    Rigidbody rigidBody;
    PlayerController playerController;
    SpriteRenderer webEffect;
    GameObject playerHitBox;
    Vector3 currentPos;
    Vector3 endPos;
    Quaternion currentRot;
    Quaternion endRot;
    float duration;
    float nextRegenTime;
    float currentTime;
    bool forceMove;

    protected override void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        webEffect = GameObject.FindGameObjectWithTag("WebEffect").GetComponent<SpriteRenderer>();

        currentHealth = playerData.playerStats.startingHealth;
        webEffect.enabled = false;
        baseMoveSpeedPlayer = playerData.playerStats.moveSpeed;

        playerHitBox = GameObject.FindGameObjectWithTag("PlayerHitbox");
        playerHitBox.GetComponent<CapsuleCollider>().material.frictionCombine = PhysicMaterialCombine.Minimum;
        playerHitBox.GetComponent<CapsuleCollider>().material.bounceCombine = PhysicMaterialCombine.Minimum;
        playerHitBox.GetComponent<CapsuleCollider>().material.dynamicFriction = 0.0f;
        playerHitBox.GetComponent<CapsuleCollider>().material.staticFriction = 0.0f;
    }

    protected override void Update()
    {
        base.Update();

        moveSpeed = playerData.playerStats.moveSpeed;
        startingHealth = playerData.playerStats.startingHealth;

        if (Time.time > nextRegenTime && currentHealth < startingHealth)
        {
            nextRegenTime = Time.time + 1;

            currentHealth += regenPerSecond;
        }

        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }

        if (forceMove == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > duration)
            {
                currentTime = duration;
            }

            float t = currentTime / duration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            transform.position = Vector3.Lerp(currentPos, new Vector3(endPos.x, transform.position.y, endPos.z), t);
            transform.rotation = Quaternion.Lerp(currentRot, endRot, t);

            if (transform.position == new Vector3(endPos.x, transform.position.y, endPos.z))
            {
                forceMove = false;
            }
        }
    }

    private void FixedUpdate()   //Fixedupdate for moving and physics
    {
        if (currentHealth > 0 && canInteract == true)
        {
            rigidBody.AddForce(playerController.moveDirection.normalized * moveSpeed * 10, ForceMode.Acceleration);
        }
    }

    public void ForceMovePlayer(Vector3 takenEndPos, Quaternion takenEndRot, float takenDuration)
    {
        currentTime = 0;
        currentPos = transform.position;
        currentRot = transform.rotation;
        endPos = takenEndPos;
        endRot = takenEndRot;
        duration = takenDuration;
        forceMove = true;
    }
}