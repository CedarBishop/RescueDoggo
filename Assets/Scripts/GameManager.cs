using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int score;
    public float secondsInDay;

    private float timer;

    private bool dayIsUnderway;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartDay();
    }

    private void Update()
    {
        if (dayIsUnderway)
        {
            if (timer <= 0)
            {
                EndDay();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    public void StartDay ()
    {
        timer = secondsInDay;
        dayIsUnderway = true;
    }

    public void EndDay ()
    {
        dayIsUnderway = false;
        RestartLevel();
    }

    void RestartLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
