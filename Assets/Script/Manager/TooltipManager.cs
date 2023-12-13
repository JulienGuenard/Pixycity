using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region Variables
    [TextArea][SerializeField] private string textSeparator;

    [SerializeField] private FeedbackObject feedbackEventImage;
    [SerializeField] private FeedbackObject feedbackEventButton;

    [SerializeField] private RectTransform tooltipRect;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private Vector2 autoSize;
    private TooltipTrigger triggerCurrent;
    private Transform rootParent;
    #region Get / Set
    public FeedbackObject FeedbackEventImage
    {
        get { return feedbackEventImage; }
        set { feedbackEventImage = value; }
    }

    public FeedbackObject FeedbackEventButton
    {
        get { return feedbackEventButton; }
        set { feedbackEventButton = value; }
    }

    public RectTransform TooltipRect
        {
            get { return tooltipRect; }
            set { tooltipRect = value; }
        }

    public TextMeshProUGUI TooltipText
        {
            get { return tooltipText; }
            set { tooltipText = value; }
        }

    public Transform RootParent
    {
        get { return rootParent; }
        set { rootParent = value; }
    }
    #endregion

    #endregion

    public static TooltipManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        rootParent = tooltipRect.transform.parent;

}

    public void CursorEnter(TooltipTrigger trigger)
    {
        if (tooltipRect.gameObject.activeInHierarchy)   return;
        if (trigger == triggerCurrent)                  return;

        triggerCurrent = trigger;

        CursorEnter_ChangePosition();
        CursorEnter_ChangeValues();
        StartCoroutine(ActiveDelay());
    }

    private void CursorEnter_ChangePosition()
    {
        tooltipRect.gameObject.SetActive(true);

        Vector2 falseOffset = new Vector2(200, 200);
        autoSize = new Vector2(0, 0);

        tooltipRect.anchorMin = triggerCurrent.Offset + falseOffset;
        tooltipRect.anchorMax = triggerCurrent.Offset + falseOffset + triggerCurrent.Size;
    }
    private void CursorEnter_ChangeValues()
    {
        string stringNew = " ";

        stringNew = ChangeValue_Description();
        stringNew += ChangeValue_Resource();

        tooltipText.text = stringNew;
    }

    string ChangeValue_Description()
    {
        if (triggerCurrent.EventGMB != null)        return triggerCurrent.EventGMB.EventObj.infos.tooltipIcon[0];
        else if (triggerCurrent.ChoiceGMB != null)  return triggerCurrent.ChoiceGMB.ChoiceObj.infos.tooltipIcon[0];
        else                                        return triggerCurrent.feedback.infos.tooltipIcon[0];
    }
    string ChangeValue_Resource()
    {
        if (triggerCurrent.ChoiceGMB != null)           return CV_Resource_Choice();

        List<FeedbackObject> list = ResourceManager.instance.FeedbackListGet();

        if (list.Contains(triggerCurrent.feedback))     return CV_Resource_PlayerResources(list);
        else                                            return null;
    }

    string CV_Resource_Choice()
    {
        StructResource res = triggerCurrent.ChoiceGMB.ChoiceObj.resources;

        string txt = textSeparator + "Cost" + ": ";
        List<float> costList = new List<float> { res.dreamCost, res.moneyCost, res.populationCost, res.toolCost, res.foodCost };
        txt = CV_Resource_Choice_Loop(costList, txt, "");

        txt += "<br>" + "Income" + ": ";
        List<float> incomeList = new List<float> { res.dreamIncome, res.moneyIncome, res.populationIncome, res.toolIncome, res.foodIncome };
        txt = CV_Resource_Choice_Loop(incomeList, txt, "+");

        txt += "<br>" + "Max" + ": ";
        List<float> maxList = new List<float> { res.dreamMax, res.moneyMax, res.populationMax, res.toolMax, res.foodMax };
        txt = CV_Resource_Choice_Loop(maxList, txt, "+");

        txt += textSeparator + "Meteo Events" + "<br>" ;

        for(int i = 0; i < triggerCurrent.ChoiceGMB.ChoiceObj.hazards.Count; i++)
        {
            for (int y = 0; y < triggerCurrent.ChoiceGMB.ChoiceObj.hazards[i].meteoList.Count; y++)
            {
                EnumMeteo enumMeteo = triggerCurrent.ChoiceGMB.ChoiceObj.hazards[i].meteoList[y];

                switch (enumMeteo)
                {
                    case EnumMeteo.Sun: txt += " " + "<size='50'>" + "<voffset=10><sprite=" + 7 + "></voffset>"; break;
                    case EnumMeteo.Rain: txt += " " + "<size='50'>" + "<voffset=10><sprite=" + 6 + "></voffset>"; break;
                    case EnumMeteo.Cloud: txt += " " + "<size='50'>" + "<voffset=10><sprite=" + 5 + "></voffset>"; break;
                }
            }
            txt += "<br>";
        }

        return txt;
    }

    string CV_Resource_Choice_Loop(List<float> list, string txt, string txtOther)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != 0)
            {
                txt += txtOther;
                txt += list[i];
                txt += " " + "<voffset=10><sprite=" + i + "></voffset>";
                txt += " ";
            }
        }

        return txt;
    }

    string CV_Resource_PlayerResources(List<FeedbackObject> list)
    {
        StructSingleResource res = new StructSingleResource();
        int y = 0;

        for(int i = 0; i < list.Count; i++)
        {
            if (list[i] == triggerCurrent.feedback) 
            {
                res = ResourceManager.instance.ResourceListGet()[i].resources;
                y = i;
                break; 
            }
        }

        string txt = textSeparator + "Income: " + res.income;
        txt += "<br>" + "<size=10> </size>Max: " + res.max;
        if (y == 0) txt += "<br>" + "<size=10> </size>Used: " + res.popUsed;

        return txt;
    }

    IEnumerator ActiveDelay()
    {
        yield return new WaitForSeconds(0.01f);
        if (triggerCurrent == null) yield break;

        autoSize = new Vector2(0, 0.04f * tooltipText.textInfo.lineCount);

        if (triggerCurrent.Offset.y > 0.5f)
        {
            tooltipRect.anchorMin = triggerCurrent.Offset - autoSize - new Vector2(triggerCurrent.Size.x, 0);
            tooltipRect.anchorMax = triggerCurrent.Offset;
        }

        if (triggerCurrent.Offset.y <= 0.5f)
        {
            tooltipRect.anchorMin = triggerCurrent.Offset;
            tooltipRect.anchorMax = triggerCurrent.Offset + autoSize + new Vector2(triggerCurrent.Size.x, 0);
        }
    }

    public void CursorExit(TooltipTrigger trigger)
    {
        if (trigger != triggerCurrent) return;

        triggerCurrent = null;
        tooltipRect.gameObject.SetActive(false);
    }
}
