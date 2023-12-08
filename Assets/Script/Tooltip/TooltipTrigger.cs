using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    public FeedbackObject feedback;
    public Vector2 offset;
    public Vector2 size;

    public void OnMouseEnter()
    {
        TooltipManager.instance.CursorEnter(this, feedback, offset, size);
    }

    public void OnMouseExit()
    {
        TooltipManager.instance.CursorExit(this);
    }
}
