using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Variables
    [Header("Population")]
    [SerializeField] private float populationIncome;
    [SerializeField] private float populationMax;
    [SerializeField] private float populationCurrent;
    [SerializeField] private float populationFree;
    public float PopulationIncome
    {
        get { return populationIncome; }
        set { populationIncome = value; }
    }
    public float PopulationMax
    {
        get { return populationMax; }
        set { populationMax = value; }
    }
    public float PopulationCurrent
    {
        get { return populationCurrent; }
        set
        {
            if (value > populationMax && populationCurrent >= populationMax) populationCurrent = populationMax;
            else if (value < 0 && populationCurrent <= 0f) populationCurrent = 0f;
            else populationCurrent = value;

            PopulationFree += value;
        }
    }
    public float PopulationFree
    {
        get { return populationFree; }
        set
        {
            if (value > populationCurrent && populationFree >= populationCurrent) populationFree = populationCurrent;
            else if (value < 0 && populationFree <= 0f) populationFree = 0f;
            else populationFree = value;

            UIManager.instance.PopulationText.text = Mathf.FloorToInt(populationCurrent).ToString() + "(" + Mathf.FloorToInt(populationFree).ToString() + ")";
        }
    }

    [Header("Money")]
    [SerializeField] private float moneyIncome;
    [SerializeField] private float moneyMax;
    [SerializeField] private float moneyCurrent;
    public float MoneyIncome
    {
        get { return moneyIncome; }
        set { moneyIncome = value; }
    }
    public float MoneyMax
    {
        get { return moneyMax; }
        set { moneyMax = value; }
    }
    public float MoneyCurrent
    {
        get { return moneyCurrent; }
        set
        {
            if      (value > moneyMax && moneyCurrent >= moneyMax)  moneyCurrent = moneyMax; 
            else if (value < 0 && moneyCurrent <= 0f)               moneyCurrent = 0f;
            else                                                    moneyCurrent = value;

            UIManager.instance.MoneyText.text = Mathf.FloorToInt(moneyCurrent).ToString();
        }
    }

    [Header("Food")]
    [SerializeField] private float foodIncome;
    [SerializeField] private float foodMax;
    [SerializeField] private float foodCurrent;
    public float FoodIncome
    {
        get { return foodIncome; }
        set { foodIncome = value; }
    }
    public float FoodMax
    {
        get { return foodMax; }
        set { foodMax = value; }
    }
    public float FoodCurrent
    {
        get { return foodCurrent; }
        set
        {
            if (value > foodMax && foodCurrent >= foodMax) foodCurrent = foodMax;
            else if (value < 0 && foodCurrent <= 0f) foodCurrent = 0f;
            else foodCurrent = value;

            UIManager.instance.FoodText.text = Mathf.FloorToInt(foodCurrent).ToString();
        }
    }

    [Header("Tool")]
    [SerializeField] private float toolIncome;
    [SerializeField] private float toolMax;
    [SerializeField] private float toolCurrent;
    public float ToolIncome
    {
        get { return toolIncome; }
        set { toolIncome = value; }
    }
    public float ToolMax
    {
        get { return toolMax; }
        set { toolMax = value; }
    }
    public float ToolCurrent
    {
        get { return toolCurrent; }
        set
        {
            if (value > toolMax && toolCurrent >= toolMax) toolCurrent = toolMax;
            else if (value < 0 && toolCurrent <= 0f) toolCurrent = 0f;
            else toolCurrent = value;

            UIManager.instance.ToolText.text = Mathf.FloorToInt(toolCurrent).ToString();
        }
    }

    [Header("Dream")]
    [SerializeField] private float dreamIncome;
    [SerializeField] private float dreamMax;
    [SerializeField] private float dreamCurrent;
    public float DreamIncome
    {
        get { return dreamIncome; }
        set { dreamIncome = value; }
    }
    public float DreamMax
    {
        get { return dreamMax; }
        set { dreamMax = value; }
    }
    public float DreamCurrent
    {
        get { return dreamCurrent; }
        set
        {
            if (value > dreamMax && dreamCurrent >= dreamMax) dreamCurrent = dreamMax;
            else if (value < 0 && dreamCurrent <= 0f) dreamCurrent = 0f;
            else dreamCurrent = value;

            UIManager.instance.DreamText.text = Mathf.FloorToInt(dreamCurrent).ToString();
        }
    }
    #endregion

    #region Instance
    public static ResourceManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    private void Start()
    {
        PopulationCurrent = populationCurrent;
        MoneyCurrent = moneyCurrent;
        FoodCurrent = foodCurrent;
        ToolCurrent = toolCurrent;
        DreamCurrent = dreamCurrent;
    }

    private void Update()
    {
        PopulationIncomeUpdate();
        FoodIncomeUpdate();
        MoneyIncomeUpdate();
        ToolIncomeUpdate();
        DreamIncomeUpdate();
    }

    private void PopulationIncomeUpdate()
    {
        PopulationCurrent += PopulationIncome;
    }

    private void MoneyIncomeUpdate()
    {
        MoneyCurrent += MoneyIncome;
    }

    private void FoodIncomeUpdate()
    {
        FoodCurrent += FoodIncome;
    }

    private void ToolIncomeUpdate()
    {
        toolCurrent += toolIncome;
    }

    private void DreamIncomeUpdate()
    {
        DreamCurrent += DreamIncome;
    }

    public bool BuildCheckResources(ChoiceObject choiceObj)
    {
        if (choiceObj.resources.populationCost > populationCurrent) return false;
        else PopulationCurrent -= choiceObj.resources.populationCost;

        if (choiceObj.resources.moneyCost > MoneyCurrent) return false;
        else MoneyCurrent -= choiceObj.resources.moneyCost;

        if (choiceObj.resources.foodCost > FoodCurrent) return false;
        else FoodCurrent -= choiceObj.resources.foodCost;

        if (choiceObj.resources.toolCost > ToolCurrent) return false;
        else ToolCurrent -= choiceObj.resources.toolCost;

        if (choiceObj.resources.dreamCost > DreamCurrent) return false;
        else DreamCurrent -= choiceObj.resources.dreamCost;

        return true;
    }

    public void ChangeResources(ChoiceObject choiceObj)
    {
        PopulationIncome    += choiceObj.resources.populationIncome;
        MoneyIncome         += choiceObj.resources.moneyIncome;
        FoodIncome          += choiceObj.resources.foodIncome;
        ToolIncome          += choiceObj.resources.toolIncome;
        DreamIncome         += choiceObj.resources.dreamIncome;

        PopulationMax       += choiceObj.resources.populationMax;
        MoneyMax            += choiceObj.resources.moneyMax;
        FoodMax             += choiceObj.resources.foodMax;
        ToolMax             += choiceObj.resources.toolMax;
        DreamMax            += choiceObj.resources.dreamMax;
    }
}