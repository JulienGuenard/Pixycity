using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    float scrollHoverX = 0;

    public static InputManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        InputMenu();
        InputReset();
        InputAxis();
    }

    void InputMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TransitionManager.instance.FadeIn("MainMenu");
    }

    void InputReset()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void InputAxis()
    {
        float scrollX = Input.GetAxisRaw("Horizontal") + scrollHoverX;
        CameraManager.instance.Scroll(scrollX);
    }

    public void HoverInputAxis(int value)
    {
        scrollHoverX = value;
    }
}
