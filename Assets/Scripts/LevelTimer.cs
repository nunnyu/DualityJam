using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Image timeBar;
    public float levelTime = 15f; 

    private float currentTime;

    void Start()
    {
        currentTime = levelTime;
        timeBar.fillAmount = 1f;
    }

    void Update()
    {
        if (currentTime <= 5f)
        {
            timeBar.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time * 4, 1));
        }
        else
        {
            timeBar.color = Color.white;
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeBar.fillAmount = currentTime / levelTime;
        }
        else
        {
            FindFirstObjectByType<ManageGame>().RestartLevel();
        }
    }
}