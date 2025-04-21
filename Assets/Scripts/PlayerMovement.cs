using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    [SerializeField]
    private float slideFactor;
    private Rigidbody2D rb;
    private Vector2 targetVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.rb = FindFirstObjectByType<Rigidbody2D>();
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
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

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