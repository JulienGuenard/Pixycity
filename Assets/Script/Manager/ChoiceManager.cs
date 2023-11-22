using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private Sprite lockedIcon;

    private List<ChoiceGMB> choiceList = new List<ChoiceGMB>();
    public List<ChoiceGMB> ChoiceListGet()
    {
        return choiceList;
    }
    public void ChoiceListAdd(ChoiceGMB item)
    {
        choiceList.Add(item);
    }
    public void ChoiceListRemove(ChoiceGMB item)
    {
        choiceList.Remove(item);
    }

    public static ChoiceManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        Update_CheckCost();
    }

    private void Update_CheckCost()
    {
        foreach(ChoiceGMB choice in ChoiceListGet())
        {
            CheckCost_LockChoice(choice);

            if (ResourceManager.instance.PopulationFree < choice.ChoiceObj.resources.populationCost)  continue;
            if (ResourceManager.instance.FoodCurrent < choice.ChoiceObj.resources.foodCost)           continue;
            if (ResourceManager.instance.MoneyCurrent < choice.ChoiceObj.resources.moneyCost)         continue;
            if (ResourceManager.instance.ToolCurrent < choice.ChoiceObj.resources.toolCost)           continue;
            if (ResourceManager.instance.DreamCurrent < choice.ChoiceObj.resources.dreamCost)         continue;

            choice.BtnImage.sprite = choice.ChoiceObj.infos.spriteIcon;
        }
    }

    private void CheckCost_LockChoice(ChoiceGMB choice)
    {
        choice.BtnImage.sprite = lockedIcon;
    }
}
