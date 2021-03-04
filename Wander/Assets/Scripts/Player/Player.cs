using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class Player : NetworkBehaviour
{
    private Animator animator;

    private int health;
    public int attack, defense;
    private int score = 0;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    // Variables for HP & Stamina bars
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public float maxStamina = 100;
    public float currentStamina;
    public StaminaBar staminaBar;
    public bool dying = false;

    public PlayerController player;
    public LevelChanger levelChanger;

    void Awake()
    {
        animator = GetComponent<Animator>();
        health = 100;
        attack = 25;
        defense = 10;
        player = GetComponent<PlayerController>();
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        // cManager = gameObject.GetComponent<ConnectionManager>();
        // msgQueue = gameObject.GetComponent<MessageQueue>();
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
            staminaBar = GameObject.Find("Stamina Bar").GetComponent<StaminaBar>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.setTired(false);
                playerRun(0.2f);

                if (regen != null) { StopCoroutine(regen); }
                regen = StartCoroutine(RegenStamina());

                if (currentStamina <= 0)
                {   
                    currentStamina = 0;
                    player.setTired(true);
                    Debug.Log("Out of stamina");
                }
            }
        }
    }

    public void playerHit(int damage)
    {
        health -= damage - defense;
        Debug.Log("Remaining HP : " + health);

        currentHealth -= damage - defense;
        healthBar.SetHealth(currentHealth);

        if (health <= 0)
        {
            dying = true;
            animator.SetTrigger("death");
            levelChanger.FadeToLevel(0);
        }
    }

    public void playerRun(float stamina)
    {
        currentStamina -= stamina;
        staminaBar.SetStamina(currentStamina);
    }

    public IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while (currentStamina < maxStamina)
        {
            currentStamina += 1.8f;
            staminaBar.SetStamina(currentStamina);
            yield return regenTick;
        }
        regen = null;
    }

    public override void OnStartLocalPlayer(){
        transform.Find("MusicTag").tag = "LocalPlayer";
    }
}
