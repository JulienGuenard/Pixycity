using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI toolText;
    [SerializeField] private TextMeshProUGUI dreamText;

    [SerializeField] private Image meteoCurrent;

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private TextMeshProUGUI currentObjectifText;
    [SerializeField] private Image currentObjectifImage;
    [SerializeField] private TooltipTrigger currentObjectifTooltip;

    [SerializeField] private GameObject victoryScreen;
    private GameObject currentScreen;
    #region Get / Set
    public TextMeshProUGUI PopulationText
    {
        get { return populationText; }
        set { populationText = value; }
    }
    public TextMeshProUGUI MoneyText
    {
        get { return moneyText; }
        set { moneyText = value; }
    }
    public TextMeshProUGUI FoodText
    {
        get { return foodText; }
        set { foodText = value; }
    }
    public TextMeshProUGUI ToolText
    {
        get { return toolText; }
        set { toolText = value; }
    }
    public TextMeshProUGUI DreamText
    {
        get { return dreamText; }
        set { dreamText = value; }
    }

    public Image MeteoCurrent
    {
        get { return meteoCurrent; }
        set { meteoCurrent = value; }
    }

    public TextMeshProUGUI TimerText
    {
        get { return timerText; }
        set { timerText = value; }
    }

    public TextMeshProUGUI CurrentObjectifText
    {
        get { return currentObjectifText; }
        set { currentObjectifText = value; }
    }
    public Image CurrentObjectifImage
    {
        get { return currentObjectifImage; }
        set { currentObjectifImage = value; }
    }
    public TooltipTrigger CurrentObjectifTooltip
    {
        get { return currentObjectifTooltip; }
        set { currentObjectifTooltip = value; }
    }

    public GameObject VictoryScreen
    {
        get { return victoryScreen; }
        set { victoryScreen = value; }
    }
    public GameObject CurrentScreen
    {
        get { return currentScreen; }
        set { currentScreen = value; }
    }
    #endregion

    #endregion

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
