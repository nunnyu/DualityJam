using System;
using System.Threading;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    [SerializeField] private float bounceHeight = 0.15f;
    [SerializeField] private float bounceSpeed = 2f;
    private ManageGame manageGame;
    private float timer;
    private Vector2 startPos; // for bounce


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
        manageGame = FindFirstObjectByType<ManageGame>(); // There should be only one anyways
        startPos = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime * bounceSpeed;
        float yOffset = Mathf.Sin(timer) * bounceHeight;
        transform.position = new Vector2(startPos.x, startPos.y + yOffset);
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.tag.Equals("Lumine")) 
        {
            manageGame.lumineKey = true;
            FindFirstObjectByType<ManageAudio>().Play("Key");
            Destroy(gameObject);
        } 
        else if (collision.tag.Equals("Umbra")) 
        {
            manageGame.umbraKey = true;
            FindFirstObjectByType<ManageAudio>().Play("Key");
            Destroy(gameObject);
        }
    }
}
