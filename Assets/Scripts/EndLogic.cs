using System;
using UnityEngine;

public class EndLogic : MonoBehaviour
{
    [SerializeField] private Boolean forLumine;
    private ManageGame manageGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manageGame = FindFirstObjectByType<ManageGame>(); // There should be only one anyways 
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // they must be at the right exit, and have a key
        if (forLumine && collision.tag.Equals("Lumine") && manageGame.lumineKey) 
        {
            manageGame.lumend = true;
            FindFirstObjectByType<ManageAudio>().Play("End");
            Destroy(gameObject);
        } 
        else if (!forLumine && collision.tag.Equals("Umbra") && manageGame.umbraKey) 
        {
            manageGame.umbrend = true;
            FindFirstObjectByType<ManageAudio>().Play("End");
            Destroy(gameObject);
        }
    }
}
