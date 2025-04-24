using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    [HideInInspector] public static ManageGame Instance { get; private set; }
    // having static here makes it belong to the class, not the script
    [HideInInspector] public Boolean lumend { get; set; }
    [HideInInspector] public Boolean umbrend { get; set; }
    [HideInInspector] public Boolean lumineKey { get; set; }
    [HideInInspector] public Boolean umbraKey { get; set; }

    public int currentLevel = 0;

    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // remove duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        resetEnds();


        // TODO: REMOVE THIS IS TEMPORARY 
        FindFirstObjectByType<ManageAudio>().PlayLoop("Theme");
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("Level" + currentLevel);
        resetEnds();

        if (currentLevel == 0) 
        {
            FindFirstObjectByType<ManageAudio>().PlayLoop("Theme");
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
        resetEnds();
    }

    private void resetEnds() {
        lumend = false;
        umbrend = false;
        lumineKey = false;
        umbraKey = false;
    }
}
