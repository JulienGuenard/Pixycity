using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private bool isFadingOutAtStart;

    private string nextScene;
    public string NextScene
    {
        get { return nextScene; }
        set { nextScene = value; }
    }

    public static TransitionManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }

        if (isFadingOutAtStart) FadeOut();
    }

    public void FadeIn(string scene)
    {
        NextScene = scene;
        transitionAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        transitionAnimator.SetTrigger("FadeOut");
    }
}