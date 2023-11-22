using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject windowAccueil;
    [SerializeField] private GameObject windowPlay;
    [SerializeField] private GameObject windowBuilds;
    [SerializeField] private GameObject windowAchievements;
    [SerializeField] private GameObject windowOptions;

    private GameObject windowActive;

    public static MainMenuManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        windowActive = windowAccueil;
    }

    public void WindowWorld()
    {
        TransitionManager.instance.FadeIn("World 1");
    }

    public void WindowAccueil()
    {
        windowActive.SetActive(false);
        windowAccueil.SetActive(true);
        windowActive = windowAccueil;
    }

    public void WindowPlay()
    {
        windowActive.SetActive(false);
        windowPlay.SetActive(true);
        windowActive = windowPlay;
    }

    public void WindowBuilds()
    {
        windowActive.SetActive(false);
        windowBuilds.SetActive(true);
        windowActive = windowBuilds;
    }

    public void WindowAchievements()
    {
        windowActive.SetActive(false);
        windowAchievements.SetActive(true);
        windowActive = windowAchievements;
    }

    public void WindowOptions()
    {
        windowActive.SetActive(false);
        windowOptions.SetActive(true);
        windowActive = windowOptions;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
