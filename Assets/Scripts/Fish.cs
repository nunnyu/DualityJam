using System;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private Boolean isLight;
    [SerializeField] private float swimDistance = 1f;
    [SerializeField] private float swimSpeed = 1f;
    private float flipThreshold = 0.99f;
    [SerializeField] private bool verticalMovement = false; // toggle horizontal or vertical 
    [SerializeField] private Health lumine;
    [SerializeField] private Health umbra;

    private float timer = 0f;
    private Vector2 originalPosition;
    private bool flipped = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime * swimSpeed;
        float offset = Mathf.Cos(timer) * swimDistance;

        if (verticalMovement)
        {
            // Vertical swimming
            transform.position = new Vector2(originalPosition.x, originalPosition.y + offset);

            // Rotate sprite up/down
            float dir = Mathf.Sin(timer);
            float angle = dir > 0 ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // Horizontal swimming
            transform.position = new Vector2(originalPosition.x + offset, originalPosition.y);

            // Flip sprite left/right
            float direction = Mathf.Cos(timer);
            if (direction > flipThreshold && flipped)
            {
                Flip();
            }
            else if (direction < -flipThreshold && !flipped)
            {
                Flip();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (isLight && other.CompareTag("Attack"))
        {
            Destroy(gameObject);
            FindFirstObjectByType<ManageAudio>().Play("Fish");
            // Debug.Log("kill fish");
        }
        else if (!isLight && other.CompareTag("DarkAttack"))
        {
            Destroy(gameObject);
            FindFirstObjectByType<ManageAudio>().Play("Fish");
        }
        else if (other.CompareTag("Lumine"))
        {
            lumine.Die();
        }
        else if (other.CompareTag("Umbra"))
        {
            umbra.Die();
        }
    }

    void Flip()
    {
        flipped = !flipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}