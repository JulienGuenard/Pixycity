using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string sceneMenu;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        Application.targetFrameRate = 60;
    }

    public void Continue()
    {
        UIManager.instance.CurrentScreen.SetActive(false);
        UIManager.instance.CurrentScreen = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        TransitionManager.instance.FadeIn(sceneMenu);
    }

    public void Victory()
    {
        UIManager.instance.VictoryScreen.SetActive(true);
        UIManager.instance.CurrentScreen = UIManager.instance.VictoryScreen;
    }
}
