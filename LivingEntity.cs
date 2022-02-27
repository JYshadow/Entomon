using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour   //Cannot have void Update(), because it conflicts with other one's void Update()
{
    [Header("Public")]
    public float startingHealth;
    public float currentHealth;
    public float moveSpeed;
    public float slainExperience;
    public event System.Action OnDeath;
    [Space(5)]
    public float acidMulti;
    public float fireMulti;
    public float shockMulti;
    public float coldMulti;
    [Space(5)]
    public GameObject wetMaterial;
    public GameObject burningMaterial;

    [Header("Audio")]
    public AudioSource shockedsound;
    public AudioSource deathSound;

    [HideInInspector] public int instanceID;
    [HideInInspector] public bool slowed = false;
    [HideInInspector] public PlayerData playerData;

    DropsManager dropsManager;
    InputManager inputManager;
    FirstPersonCamera firstPersonCamera;
    WeaponController weaponController;
    Player player;
    GameObject itemToDrop;
    ParticleSystem[] shockEffects;
    ParticleSystem[] wetParticles;
    ParticleSystem[] burningParticles;
    GameObject respawnScreen;
    GameObject mainCamera;
    Color outlineColor;
    float distanceToGround;
    float dropNumber;
    bool droppable = false;
    float fadeElapsed = 0;
    float fadeDuration = 2f;
    float saturation = 0.75f;
    float brightness = 0.5f;
    float waterMulti = 1;
    float waterElapsed;
    float waterDuration = 10f;
    bool watered;
    float burningElapsed;
    float burningDuration = 2f;
    bool burning;
    float baseMoveSpeed;
    protected float baseMoveSpeedPlayer;
    bool fadeDeathScreen;
    bool dead = false;
    float currentFadeTime = 0;
    float fadeRespawnDuration = 3f;

    private void Awake()
    {
        if (gameObject.tag != ("Player"))
        {
            instanceID = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>().GetInstanceID();
        }
        playerData = FindObjectOfType<PlayerData>();
        dropsManager = FindObjectOfType<DropsManager>();
        inputManager = FindObjectOfType<InputManager>();
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
        player = FindObjectOfType<Player>();
        shockEffects = GameObject.FindGameObjectWithTag("ShockEffect").GetComponentsInChildren<ParticleSystem>();

        if (gameObject.tag == "Player")
        {
            respawnScreen = GameObject.FindGameObjectWithTag("RespawnScreen");
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            weaponController = FindObjectOfType<WeaponController>();
            respawnScreen.SetActive(false);
            mainCamera.GetComponent<AudioLowPassFilter>().enabled = false;
        }
    }

    protected virtual void Start()  //Overridden by enemy scripts
    {
        dropNumber = Random.Range(1, 100);
        baseMoveSpeed = moveSpeed;

        if (dropNumber <= 20)
        {
            droppable = true;
        }

        if (GetComponent<Outline>() != null)
        {
            GetComponent<Outline>().enabled = true;
        }

        //WATERED
        if (wetMaterial != null)
        {
            wetParticles = wetMaterial.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem wetParticle in wetParticles)
            {
                wetParticle.Stop();
            }
        }

        //BURNING
        if (burningMaterial != null)
        {
            burningParticles = burningMaterial.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem burningParticle in burningParticles)
            {
                burningParticle.Stop();
            }
        }
    }

    protected virtual void Update()
    {
        //Debug.DrawRay(transform.position + Vector3.up * 0.1f, transform.TransformDirection(Vector3.forward) * 20f, Color.red);

        if (gameObject.tag == "Enemy")
        {
            if (GetComponent<Outline>() != null)
            {
                GetComponent<Outline>().OutlineColor = outlineColor;
            }

            if (fadeElapsed < fadeDuration)
            {
                fadeElapsed += Time.deltaTime;

                if (gameObject.name == "Enemy1")
                {
                    outlineColor.a = Mathf.Lerp(0.05f, 0, fadeElapsed / fadeDuration);
                }
                else
                {
                    outlineColor.a = Mathf.Lerp(0.75f, 0, fadeElapsed / fadeDuration);
                }
            }

            //WATERED
            if (wetMaterial != null && watered == true)
            {
                if (waterElapsed < waterDuration)
                {
                    waterElapsed += Time.deltaTime;
                    waterMulti = 1.5f;

                    foreach (ParticleSystem wetParticle in wetParticles)
                    {
                        if (wetParticle.isStopped)
                        {
                            wetParticle.Play();
                        }
                    }
                }

                if (waterElapsed >= waterDuration)
                {
                    waterMulti = 1f;
                    watered = false;

                    foreach (ParticleSystem wetParticle in wetParticles)
                    {
                        if (wetParticle.isPlaying)
                        {
                            wetParticle.Stop();
                        }
                    }
                }
            }

            //BURNING
            if (burningMaterial != null && burning == true)
            {
                if (burningElapsed < burningDuration)
                {
                    burningElapsed += Time.deltaTime;

                    foreach (ParticleSystem burningParticle in burningParticles)
                    {
                        if (burningParticle.isStopped)
                        {
                            burningParticle.Play();
                        }
                    }
                }

                if (burningElapsed >= burningDuration)
                {
                    burning = false;

                    foreach (ParticleSystem burningParticle in burningParticles)
                    {
                        if (burningParticle.isPlaying)
                        {
                            burningParticle.Stop();
                        }
                    }
                }
            }
        }
        else
        {
            //Respawn
            if (fadeDeathScreen == true)
            {
                currentFadeTime += Time.deltaTime;

                if (currentFadeTime > fadeRespawnDuration)
                {
                    currentFadeTime = fadeRespawnDuration;
                }

                respawnScreen.GetComponent<Image>().color = Color.Lerp(new Color32(255, 0, 0, 100), new Color32(0, 0, 0, 255), currentFadeTime / fadeDuration);
            }
        }
    }

    public void TakeDamage(float damage, string element)
    {
        if (dead == false)
        {
            if (damage < currentHealth)
            {
                print(damage);

                if (element == "Acid")
                {
                    currentHealth = currentHealth - damage * acidMulti;
                    DisplayOutline(Color.HSVToRGB((1f / 3f) * (acidMulti - 0.5f), saturation, brightness));    //output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)
                }
                else if (element == "Fire")
                {
                    currentHealth = currentHealth - damage * fireMulti;
                    DisplayOutline(Color.HSVToRGB((1f / 3f) * (fireMulti - 0.5f), saturation, brightness));    //output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)

                    burning = true; //Start burning effect
                    burningElapsed = 0;
                }
                else if (element == "Shock")
                {
                    currentHealth = currentHealth - damage * shockMulti * waterMulti;
                    DisplayOutline(Color.HSVToRGB((1f / 3f) * (shockMulti - 0.5f), saturation, brightness));    //output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)

                    if (gameObject.tag == "Player")
                    {
                        foreach (ParticleSystem shockEffect in shockEffects)
                        {
                            shockEffect.Play();
                            shockedsound.Play();
                        }
                    }
                }
                else if (element == "Cold")
                {
                    currentHealth = currentHealth - damage * coldMulti;
                    DisplayOutline(Color.HSVToRGB((1f / 3f) * (coldMulti - 0.5f), saturation, brightness));    //output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)
                }
                else if (element == "Water")
                {
                    currentHealth = currentHealth - damage;
                    DisplayOutline(Color.HSVToRGB((1f / 3f) * (1 - 0.5f), saturation, brightness));    //output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)

                    watered = true; //More damage taken from shock
                    waterElapsed = 0;
                }
                else if (element == "Normal")
                {
                    currentHealth = currentHealth - damage;
                    DisplayOutline(Color.HSVToRGB((1f / 3f) * (1 - 0.5f), saturation, brightness));    //output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)
                }
            }
            else
            {
                if (gameObject.tag == "Player")
                {
                    currentFadeTime = 0;
                    StartCoroutine(Respawn());
                }
                else
                {
                    GiveExp();
                    DropItem();
                    OnDeath?.Invoke();  //Notify scripts that have subscribed to OnDeath (such as spawner)
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void DisplayOutline(Color takenColor)
    {
        fadeElapsed = 0;
        outlineColor = takenColor;
    }

    private void OnDisable()
    {
        Destroy(gameObject, 10f);    //Store data for X seconds before getting destroyed
    }

    public void GiveExp()
    {
        playerData.playerStats.experience += slainExperience;
    }

    public void Slow(float percentage)
    {
        if (slowed == false)
        {
            slowed = true;
            if (gameObject.layer == 12) //12 = Player
            {
                //print("slowed");
                playerData.playerStats.moveSpeed *= (1 - (percentage / 100));
            }
            else
            {
                moveSpeed *= (1 - (percentage / 100));
            }
        }
    }

    public void ResetMovespeed(float percentage)
    {
        if (slowed == true)
        {
            slowed = false;
            if (gameObject.layer == 12) //12 = Player
            {
                playerData.playerStats.moveSpeed = baseMoveSpeedPlayer;
            }
            else
            {
                moveSpeed = baseMoveSpeed;
            }
        }
    }

    public void DropItem()
    {
        if (droppable == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
            {
                distanceToGround = hit.distance;
            }

            itemToDrop = dropsManager.droppool[Random.Range(0, dropsManager.droppool.Length)];
            GameObject droppedItem = Instantiate(itemToDrop, transform.position + Vector3.down * distanceToGround * 0.99f + Vector3.up * itemToDrop.GetComponent<Item>().offset, transform.rotation);
            droppedItem.name = droppedItem.name.Replace("(Clone)", "");
        }
    }

    public IEnumerator Respawn()
    {
        dead = true;
        inputManager.respawning = true;
        inputManager.CloseAll();
        inputManager.pauseMenu.SetActive(false);

        deathSound.Play();
        respawnScreen.SetActive(true);
        fadeDeathScreen = true;
        mainCamera.GetComponent<AudioLowPassFilter>().enabled = true;
        yield return new WaitForSeconds(5);

        transform.position = new Vector3(9f, transform.position.y, 22f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        firstPersonCamera.cameraHolder.transform.localRotation = new Quaternion(0, 0, 0, 0);
        weaponController.UnequipWeapon(false);
        player.currentHealth = 200f;

        yield return new WaitForSeconds(1);
        mainCamera.GetComponent<AudioLowPassFilter>().enabled = false;
        respawnScreen.SetActive(false);
        inputManager.pauseMenu.SetActive(true);
        inputManager.respawning = false;
        dead = false;
    }
}
//LivingEntity applies to all living/damageable objects, so those can use this script as parent.