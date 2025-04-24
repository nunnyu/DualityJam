using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    [HideInInspector] public static ManageGame Instance { get; private set; }
    // having static here makes it belong to the class, not the script

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
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
