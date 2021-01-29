using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState {Game, Pause}

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Text scoreText;
    public Image timeOfDayFillImage;

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

    public void SetScore (int value)
    {
        scoreText.text = "Rescue points: " + value.ToString();
    }

    public void SetTimeOfDayProgress (float timeRemaining, float totalTime)
    {
        float progress = (totalTime - timeRemaining) / totalTime;
        timeOfDayFillImage.fillAmount = progress;
    }


}
