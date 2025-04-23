using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Boolean isLumine;
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private Health healthScript;
    [SerializeField] private float damage;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    private Vector2 ogAttackScale; // original size of the attack
    private float ogDamage; // original damage number


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.ogAttackScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        this.ogDamage = this.damage;
    }

    // Turns health into a scale value for the weapon size & damage 
    public float GetValueFromRatio(float ratio)
    {
        // Ensure the ratio is clamped between 0 and 1
        ratio = Mathf.Clamp01(ratio);

        // Returns ratio from min to max
        return minSize + (maxSize - minSize) * ratio;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current damage: " + damage);

        // SIZE & DAMAGE HANDLER
        this.transform.localScale = ogAttackScale * this.GetValueFromRatio(healthScript.GetHealthRatio());
        this.damage = ogDamage * this.GetValueFromRatio(healthScript.GetHealthRatio());

        // DIRECTION & ANGLE HANDLER
        Vector2 moveDir = movementScript.getLastInput();

        if (moveDir != Vector2.zero)
        {
            GetComponent<SpriteRenderer>().enabled = true; // make the "attack" visible

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg; // finds the angle the player is moving
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false; // no attack happens
        }

        float distanceFromPlayer = 1f;
        Vector3 offset = (Vector3)moveDir.normalized * distanceFromPlayer;
        transform.position = movementScript.transform.position + offset;
    }
}
