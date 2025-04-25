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
    [HideInInspector] public int deadCount { get; set; }
    private Boolean inMenu = false;
    [HideInInspector] public int currentLevel = -1;

    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // remove duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ResetEnds();
    }

    void Start() 
    {
        FindFirstObjectByType<ManageAudio>().Play("Title");
        inMenu = true;
    }

    void Update() 
    {
        if (inMenu) 
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                FindFirstObjectByType<ManageAudio>().Stop("Title");
                inMenu = false;
                FindFirstObjectByType<ManageAudio>().PlayLoop("Theme");
                SceneManager.LoadScene("Level0");
            }
        }
        // reset level is space is pressed
        if (!inMenu && Input.GetKeyDown(KeyCode.Space) || deadCount == 2)
        {
            RestartLevel();
        }

        if (!inMenu && lumend && umbrend) 
        {
            ResetEnds();
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        // Debug.Log("loading next level");
        currentLevel++;
        SceneManager.LoadScene("Level" + currentLevel);
        ResetEnds();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
        FindFirstObjectByType<ManageAudio>().Play("Reset");
        ResetEnds();
    }

    private void ResetEnds() {
        lumend = false;
        umbrend = false;
        lumineKey = false;
        umbraKey = false;
        deadCount = 0;
    }
}
