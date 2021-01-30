using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Spawner spawner;

    public int score;
    public float secondsInDay;
    public Light light;
    public Transform lightStartLocation;
    public Transform lightEndLocation;
    public float colorLerpSpeed;
    public Color[] lightColorsOverDay;


    public ParticleSystem snowParticle;
    public ParticleSystem windParticle;

    private float timer;
    private int lightColorIndex;

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
            light.transform.Translate(((lightEndLocation.position - lightStartLocation.position) / secondsInDay) * Time.deltaTime);
            LerpLightColor();
            UIManager.instance.SetTimeOfDayProgress(timer, secondsInDay);
        }
    }

    public void StartDay ()
    {
        timer = secondsInDay;
        print("Start Day");
        spawner.OnStartDay();
        dayIsUnderway = true;
        lightColorIndex = 0;
        light.transform.position = lightStartLocation.position;
        score = 0;
        UIManager.instance.ChangeMenu(UIState.Game);
        UIManager.instance.SetScore(score);
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

    void LerpLightColor()
    {
        if (lightColorsOverDay == null)
        {
            return;
        }
        
        if (light.color == lightColorsOverDay[lightColorIndex])
        {
            if (lightColorIndex < lightColorsOverDay.Length - 1)
            {
                lightColorIndex++;
                
                
            }
        }
        else
        {
            light.color = Color.Lerp(light.color, lightColorsOverDay[lightColorIndex], (colorLerpSpeed * lightColorsOverDay.Length / secondsInDay) * Time.deltaTime);
        }
    }

    public void AddToScore (int scoreAdded)
    {
        score += scoreAdded;
        UIManager.instance.SetScore(score);
    }

    void IncreaseSnowAndWind ()
    {
        if (snowParticle != null)
            snowParticle.emissionRate += 20;

        if(windParticle != null)
            windParticle.emissionRate += 4;
    }


}
