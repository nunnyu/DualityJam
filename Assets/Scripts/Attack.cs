using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Boolean isLumine;
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private Health healthScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = movementScript.getLastInput();

        if (moveDir != Vector2.zero) {
            GetComponent<SpriteRenderer>().enabled = true; // make the "attack" visible

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg; // finds the angle the player is moving
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        } else {
            GetComponent<SpriteRenderer>().enabled = false; // no attack happens
        }

        float distanceFromPlayer = 1f;
        Vector3 offset = (Vector3) moveDir.normalized * distanceFromPlayer;
        transform.position = movementScript.transform.position + offset;

    }
}
