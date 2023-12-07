using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Resources
    [SerializeField] private TextMeshProUGUI populationText;
    public TextMeshProUGUI PopulationText
    {
        get { return populationText; }
        set { populationText = value; }
    }

    [SerializeField] private TextMeshProUGUI moneyText;
    public TextMeshProUGUI MoneyText
    {
        get { return moneyText; }
        set { moneyText = value; }
    }

    [SerializeField] private TextMeshProUGUI foodText;
    public TextMeshProUGUI FoodText
    {
        get { return foodText; }
        set { foodText = value; }
    }

    [SerializeField] private TextMeshProUGUI toolText;
    public TextMeshProUGUI ToolText
    {
        get { return toolText; }
        set { toolText = value; }
    }

    [SerializeField] private TextMeshProUGUI dreamText;
    public TextMeshProUGUI DreamText
    {
        get { return dreamText; }
        set { dreamText = value; }
    }
    #endregion

    #region Meteo
    [SerializeField] private Image meteoCurrent;
    public Image MeteoCurrent
    {
        get { return meteoCurrent; }
        set { meteoCurrent = value; }
    }
    #endregion

    #region Timer
    [SerializeField] TextMeshProUGUI timerText;
    public TextMeshProUGUI TimerText
    {
        get { return timerText; }
        set { timerText = value; }
    }
    #endregion

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
