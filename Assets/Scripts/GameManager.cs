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
    public Light mainLight;
    public Light rimLight;
    public Vector3 lightStartRotation;
    public Vector3 lightEndRotation;
    public float colorLerpSpeed;
    public Color[] lightColorsOverDay;
    public Gradient colorGradient;

    public ParticleSystem snowParticle;
    public ParticleSystem windParticle;

    public List<string> rescuedPersonNames;

    private float timer;
    private int lightColorIndex;

    private bool dayIsUnderway;

    private float mainLightXRotation;
    private float rimLightXRotation;

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
        MusicManager.instance.PlayChillGameMusic();
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

            float amount = (lightEndRotation.x - lightStartRotation.x) / secondsInDay * Time.deltaTime;
            mainLightXRotation += amount;
            mainLight.transform.rotation = Quaternion.Euler(new Vector3(mainLightXRotation, 0,0));
            rimLightXRotation -= amount;
            rimLight.transform.rotation = Quaternion.Euler(new Vector3(rimLightXRotation, 0, 0));
            LerpLightColor();
            UIManager.instance.SetTimeOfDayProgress(timer, secondsInDay);
        }
    }

    public void StartDay ()
    {
        timer = secondsInDay;
        rescuedPersonNames = new List<string>();
        print("Start Day");
        spawner.OnStartDay();
        dayIsUnderway = true;
        lightColorIndex = 0;
        mainLight.transform.rotation = Quaternion.Euler(lightStartRotation);
        mainLightXRotation = lightStartRotation.x;
        rimLight.transform.rotation = Quaternion.Euler(lightEndRotation);
        rimLightXRotation = lightEndRotation.x;
        score = 0;
        UIManager.instance.ChangeMenu(UIState.Game);
        UIManager.instance.SetScore(score);
    }

    public void EndDay ()
    {
        dayIsUnderway = false;
        UIManager.instance.ChangeMenu(UIState.EndGame);
        MusicManager.instance.PlayChillGameMusic();
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LerpLightColor()
    {
        if (lightColorsOverDay == null)
        {
            return;
        }
        
        if (mainLight.color == lightColorsOverDay[lightColorIndex])
        {
            if (lightColorIndex < lightColorsOverDay.Length - 1)
            {
                lightColorIndex++;
                if (lightColorIndex == lightColorsOverDay.Length / 2)
                {
                    MusicManager.instance.PlayDramaticGameMusic();
                }
                IncreaseSnowAndWind();
            }
        }
        else
        {
            mainLight.color = Color.Lerp(mainLight.color, lightColorsOverDay[lightColorIndex], (colorLerpSpeed * lightColorsOverDay.Length / secondsInDay) * Time.deltaTime);
        }
    }

    public void RescuedPerson (int scoreAdded, string rescueeName)
    {
        score += scoreAdded;
        rescuedPersonNames.Add(rescueeName);
        UIManager.instance.SetScore(score);
    }

    void IncreaseSnowAndWind ()
    {
        if (snowParticle != null)
            snowParticle.emissionRate += 10;

        if(windParticle != null)
            windParticle.emissionRate += 2;
    }


}
