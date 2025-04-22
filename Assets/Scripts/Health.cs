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

        // DEATH HANDLER
        if (CheckDead()) {
            Debug.Log("someone died!");
            // return to main menu or something 
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision) {
        if (isLumine) {
            if (collision.tag.Equals("DarkGround")) {
                inDanger = true;
            } else {
                inDanger = false;
            }
        } else {
            if (collision.tag.Equals("LightGround")) {
                inDanger = true;
            } else {
                inDanger = false;
            }
        }
    }
    
    Boolean CheckDead() {
        // if lumine is too dark, they die 
        // umbra does the opposite 
        if (isLumine) {
            if (currentHealth <= 0) {
                return true;
            }  else {
                return false;
            }
        } else { // if umbra 
            if (currentHealth >= maxBrightness) {
                return true;
            } else {
                return false;
            }
        } 
    }

    // heals or does damage based on territory 
    void updateHealth() {
        if (this.CheckDead()) {
            Destroy(gameObject);
        }

        // lumine goes into the darkness to get stronger, but loses brightness 
        // umbra does the opposite 
        if (inDanger){
            if (isLumine) {
                currentHealth -= damageTakenValue;
            } else {
                currentHealth += damageTakenValue;
            }
        } else {
            if (isLumine) { // if lumine
                if (currentHealth < startHealth) {
                    currentHealth += damageTakenValue / 2;
                }
            } else { // if umbra
                if (currentHealth > 0) {
                    currentHealth -= damageTakenValue / 2;
                }
            }
        }
    }
}
