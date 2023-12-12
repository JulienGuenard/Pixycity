using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    #region Variables
    public FeedbackObject feedback;
    public Vector2 offset;
    public Vector2 size;

    private EventGMB eventGMB;
    private ChoiceGMB choiceGMB;
    #region Get / Set
    public FeedbackObject Feedback
    {
        get { return feedback; }
        set { feedback = value; }
    }

    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    public Vector2 Size
    {
        get { return size; }
        set { size = value; }
    }

    public EventGMB EventGMB
    {
        get { return eventGMB; }
        set { eventGMB = value; }
    }

    public ChoiceGMB ChoiceGMB
    {
        get { return choiceGMB; }
        set { choiceGMB = value; }
    }
    #endregion

    #endregion

    private void Start()
    {
        if (feedback == TooltipManager.instance.FeedbackEventImage)         eventGMB = GetComponentInParent<EventGMB>();
        else if (feedback == TooltipManager.instance.FeedbackEventButton)   choiceGMB = GetComponentInParent<ChoiceGMB>();
    }

    public void OnMouseEnter()
    {
        TooltipManager.instance.CursorEnter(this);
    }

    public void OnMouseExit()
    {
        TooltipManager.instance.CursorExit(this);
    }
}
