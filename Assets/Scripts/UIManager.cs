using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum UIState {Main, Game, Pause, Options, PreGame, EndGame}

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Text scoreText;
    public Image timeOfDayFillImage;

    public GameObject MenuMain;
    public GameObject MenuGame;
    public GameObject MenuOptions;
    public GameObject MenuPause;
    public GameObject MenuPreGame;
    public GameObject MenuEndGame;

    public Animator preGameAnimator;

    public DialogueCanvas dialogueCanvasPrefab;

    private DialogueCanvas dialogueCanvas;

    public Slider musicSlider;
    public Slider sfxSlider;

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
        dialogueCanvas = Instantiate(dialogueCanvasPrefab, transform.position, Quaternion.identity);
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
        MenuMain.SetActive(false);
        MenuGame.SetActive(false);
        MenuPause.SetActive(false);
        MenuOptions.SetActive(false);
        MenuPreGame.SetActive(false);
        MenuEndGame.SetActive(false);

        switch (newMenu)
        {
            case UIState.Main:
                MenuMain.SetActive(true);
                Time.timeScale = 0;
                MusicManager.instance.PlayMenuMusic();
                break;

            case UIState.Game:
                MenuGame.SetActive(true);
                Time.timeScale = 1;
                break;

            case UIState.Pause:
                MenuPause.SetActive(true);
                Time.timeScale = 0;
                break;

            case UIState.Options:
                MenuOptions.SetActive(true);
                musicSlider.value = MusicManager.instance.GetMusicVolume();
                sfxSlider.value = 1;
                Time.timeScale = 0;
                break;

            case UIState.PreGame:
                MenuPreGame.SetActive(true);
                Time.timeScale = 1;
                break;

            case UIState.EndGame:
                MenuEndGame.SetActive(true);
                ReportCard reportCard = MenuEndGame.GetComponent<ReportCard>();
                reportCard.Initialise();
                Time.timeScale = 0;
                break;

            default:
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
            //if (GetSceneName() != "Main")
            //{
            //    Time.timeScale = 0;
            //}
        }
        else if (GetSceneName() == "Main")
        {
            Debug.Log("Returning to Main Menu");
            ChangeMenu(UIState.Main);
        }
        else
        {
            Debug.Log("Returning to Game");
            ChangeMenu(UIState.Game);
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
        if (sceneName == "Main")
        {
            ChangeMenu(UIState.Main);
        }
    }

    public void ToggleDialogueCanvas(Vector3 worldPosition, string textToDisplay)
    {
        dialogueCanvas.ToggleActivation(worldPosition, textToDisplay);
    }

    public void DeactivateDialogueCanvas ()
    {
        dialogueCanvas.Deactivate();
    }

    public void RestartButton ()
    {
        GameManager.instance.RestartLevel();
    }

    public void OnMusicSliderChanged ()
    {
        MusicManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void OnSFXSliderChanged ()
    {
        
    }

    public void ClosePreGame()
    {
        preGameAnimator.SetBool("ExitTransition", true);
    }
}

