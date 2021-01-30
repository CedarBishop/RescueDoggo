using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum UIState {Main, Game, Pause, Options}

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Text scoreText;
    public Image timeOfDayFillImage;

    public GameObject MenuMain;
    public GameObject MenuGame;
    public GameObject MenuOptions;
    public GameObject MenuPause;


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

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void TogglePause()
    {
        if (!MenuPause.activeSelf)
        {
            Debug.Log("Pausing");
            ChangeMenu(UIState.Pause);
            if (GetSceneName() != "Main")
            {
                Time.timeScale = 0;
            }
        }
        else if (GetSceneName() == "Main")
        {
            Debug.Log("Returning to Main Menu");
            ChangeMenu(UIState.Main);
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Returning to Game");
            ChangeMenu(UIState.Game);
            Time.timeScale = 1;
        }
    }

    public void ToggleOptions()
    {
        if (MenuOptions.activeSelf)
        {
            if (GetSceneName() != "Main")
            {
                ChangeMenu(UIState.Pause);
            }
            else
            {
                ChangeMenu(UIState.Main);
            }
        }
        else
        {
            ChangeMenu(UIState.Options);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

