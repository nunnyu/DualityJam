using System;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float swimDistance;
    [SerializeField] private float swimSpeed;
    private float timer;
    private Vector2 originalPosition;
    private Boolean flipped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT HANDLER 
        timer += Time.deltaTime * swimSpeed;
        float offset = Mathf.Cos(timer) * swimDistance;
        transform.position = new Vector2(originalPosition.x + offset, originalPosition.y);

        // FLIP
        float direction = Mathf.Cos(timer);
        if (direction > 0 && flipped)
        {
            Invoke("Flip", 0.02f);
        }
        else if (direction < 0 && !flipped)
        {
            Invoke("Flip", 0.02f);
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
