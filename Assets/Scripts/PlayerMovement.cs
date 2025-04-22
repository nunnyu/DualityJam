using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Boolean playerOne;
    [SerializeField] 
    private float speed;
    [SerializeField]
    private float slideFactor;

    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 targetVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.freezeRotation = true;
    }

    private T FindObjectsByType<T>()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update() {
        ProcessInputs();
    }

    // For Physics! 
    void FixedUpdate() {
        this.rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, slideFactor);
    }

    void ProcessInputs() 
    { 
        float inputX = 0f;
        float inputY = 0f;

        if (playerOne) {
            if (Input.GetKey(KeyCode.W)) inputY = 1f;
            if (Input.GetKey(KeyCode.S)) inputY = -1f;
            if (Input.GetKey(KeyCode.A)) inputX = -1f;
            if (Input.GetKey(KeyCode.D)) inputX = 1f;
        } else { // 2nd control scheme is OKL;
            if (Input.GetKey(KeyCode.O)) inputY = 1f;
            if (Input.GetKey(KeyCode.L)) inputY = -1f;
            if (Input.GetKey(KeyCode.K)) inputX = -1f;
            if (Input.GetKey(KeyCode.Semicolon)) inputX = 1f;
        }

        // handles player flipping when facing left or right 
        if (inputX > 0) {
            Flip(true);
        } else if (inputX < 0) {
            Flip(false);
        } // we don't want to do else false because then the player always faces left
 
        Vector2 targetVector = new Vector2(inputX, inputY).normalized;
        targetVelocity = speed * targetVector;

        this.rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, slideFactor);
    }

    void Flip(Boolean facingRight) {
        Vector3 scale = transform.localScale;
        if (facingRight) {
            scale.x = 1;
        } else {
            scale.x = -1;
        }

        transform.localScale = scale;
    }
}