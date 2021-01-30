using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState {Main, Game, Pause, Options}

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Text scoreText;
    public Image timeOfDayFillImage;

    [SerializeField] private GameObject MenuMain;
    [SerializeField] private GameObject MenuGame;
    [SerializeField] private GameObject MenuPause;
    [SerializeField] private GameObject MenuOptions;


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

    public void ChangeMenu(UIState newMenu)
    {
        switch (newMenu)
        {
            case UIState.Main:
                MenuMain.SetActive(true);
                MenuGame.SetActive(false);
                MenuPause.SetActive(false);
                MenuOptions.SetActive(false);
                break;
            case UIState.Game:
                MenuMain.SetActive(false);
                MenuGame.SetActive(true);
                MenuPause.SetActive(false);
                MenuOptions.SetActive(false);
                break;

            case UIState.Pause:
                MenuMain.SetActive(false);
                MenuGame.SetActive(false);
                MenuPause.SetActive(true);
                MenuOptions.SetActive(false);

                break;
            case UIState.Options:
                MenuMain.SetActive(false);
                MenuGame.SetActive(false);
                MenuPause.SetActive(false);
                MenuOptions.SetActive(true);
                break;
        }
    }


}
