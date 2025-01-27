using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructResource
{
    [Header("Cost")]
    public int populationCost;
    public int moneyCost, foodCost, toolCost, dreamCost;

    [Header("Income")]
    public float populationIncome;
    public float moneyIncome, foodIncome, toolIncome, dreamIncome;

    [Header("Max")]
    public float populationMax;
    public float moneyMax, foodMax, toolMax, dreamMax;

    [Header("Used")]
    [HideInInspector] public float populationUsed;
}

[System.Serializable]
public class StructBuildingMeteos
{
    public ChoiceObject building;
    public List<EnumMeteo> meteoEnumList = new List<EnumMeteo>();
}

[System.Serializable]
public struct StructSingleResource
{
    public EnumResource resourceType;
    public float current;
    public int cost;
    public float income;
    public float max;
    public float popUsed;
}

[System.Serializable]
public struct StructEventTooltip
{
    public string eventName;
    public Sprite spriteIcon;

    [TextArea] public List<string> tooltipIngame, tooltipIcon;
}

[System.Serializable]
public struct StructBuilding
{
    public Sprite buildingSprite;
    public ChoiceObject buildingNextTier;
}

[System.Serializable]
public struct StructSlotPosition
{
    public int specificSlotID;
    public Vector2 posOffset;
    public int layer;
    public float scale;
}