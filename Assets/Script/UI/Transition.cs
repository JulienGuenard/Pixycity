using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public void SceneTransition()
    {
        SceneManager.LoadScene(TransitionManager.instance.NextScene);
    }
}
