using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float startHealth;
    [SerializeField] private Boolean isLumine;
    [SerializeField] private float damageTakenValue;
    [SerializeField] private LayerMask groundLayer;
    private float currentHealth;
    private float maxBrightness = 56f;
    private Boolean inDanger; // if in the danger zone (light / dark), take damage during this boolean
    private ManageGame manageGame;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manageGame = FindFirstObjectByType<ManageGame>();
        this.inDanger = false;
        this.currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(inDanger);
        RaycastCheckZone();

        // VISUAL HANDLER
        float intensity = currentHealth;
        globalLight.intensity = intensity; // updates the lighting level based on health
    }

    void FixedUpdate()
    {
        // DAMAGE HANDLER 
        updateHealth();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Trigger area gizmo
        Gizmos.DrawWireSphere(transform.position, .2f);
    }

    void RaycastCheckZone()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayer);
        if (hit.collider != null)
        {
            if (isLumine)
            {
                inDanger = hit.collider.CompareTag("DarkGround");
            }
            else
            {
                inDanger = hit.collider.CompareTag("LightGround");
            }
        }
        else
        {
            inDanger = false; // not standing on anything detectable
        }
    }
    
    Boolean CheckDead()
    {
        // if lumine is too dark, they die 
        // umbra does the opposite 
        if (isLumine)
        {
            if (currentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        { // if umbra 
            if (currentHealth >= maxBrightness)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // heals or does damage based on territory 
    void updateHealth()
    {
        if (this.CheckDead())
        {
            ManageAudio audioManager = FindFirstObjectByType<ManageAudio>();
            if (isLumine)audioManager.Play("Lumine Death");
            else audioManager.Play("Umbra Death");
            manageGame.deadCount++;
            Destroy(gameObject);
        }

        // lumine goes into the darkness to get stronger, but loses brightness 
        // umbra does the opposite 
        if (inDanger)
        {
            if (isLumine)
            {
                Debug.Log("Lumine is taking damage.");
                currentHealth -= damageTakenValue;
            }
            else
            {
                // Debug.Log("Umbra is taking damage.");
                currentHealth += damageTakenValue;
            }
        }
        else
        {
            if (isLumine)
            { // if lumine
                if (currentHealth < startHealth)
                {
                    Debug.Log("Lumine is healing.");
                    currentHealth += damageTakenValue / 2;
                }
            }
            else
            { // if umbra
                if (currentHealth > 0)
                {
                    // Debug.Log("Umbra is healing.");
                    currentHealth -= damageTakenValue / 2;
                }
            }
        }
    }

    // Gets the current (inverse) ratio of health, with 1 being dead
    // We want death to be the larger number, because we want them to be more powerful
    // With low health.  
    public float GetHealthRatio()
    {
        float ratio;
        if (isLumine)
        {
            ratio = 1 - currentHealth / startHealth;
            // Debug.Log("Lumine: " + ratio); 
        }
        else
        {
            ratio = currentHealth / maxBrightness;
            // Debug.Log("Umbra: " + ratio);
        }

        return ratio;
    }

    public float getHealth()
    {
        return currentHealth;
    }
}