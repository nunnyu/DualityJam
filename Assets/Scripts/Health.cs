using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float startHealth;
    [SerializeField] private Boolean isLumine;
    [SerializeField] private float damageTakenValue;
    private float currentHealth;
    private float maxBrightness = 56f;
    private Boolean inDanger; // if in the danger zone (light / dark), take damage during this boolean

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.inDanger = false;
        this.currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(inDanger);

        // VISUAL HANDLER
        float intensity = currentHealth;
        globalLight.intensity = intensity; // updates the lighting level based on health

        // DAMAGE HANDLER 
        updateHealth();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Trigger area gizmo
        Gizmos.DrawWireSphere(transform.position, .2f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isLumine)
        {
            if (other.CompareTag("DarkGround"))
            {
                inDanger = true;
            }
            else if (other.CompareTag("LightGround"))
            {
                inDanger = false;
            }
        }
        else
        {
            if (other.CompareTag("LightGround"))
            {
                inDanger = true;
            }
            else if (other.CompareTag("DarkGround"))
            {
                inDanger = false;
            }
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
            if (isLumine) audioManager.Play("Lumine Death");
            else audioManager.Play("Umbra Death");
            Destroy(gameObject);
        }

        // lumine goes into the darkness to get stronger, but loses brightness 
        // umbra does the opposite 
        if (inDanger)
        {
            if (isLumine)
            {
                currentHealth -= damageTakenValue;
            }
            else
            {
                currentHealth += damageTakenValue;
            }
        }
        else
        {
            if (isLumine)
            { // if lumine
                if (currentHealth < startHealth)
                {
                    currentHealth += damageTakenValue / 2;
                }
            }
            else
            { // if umbra
                if (currentHealth > 0)
                {
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