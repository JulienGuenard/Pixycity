using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region Variables
    private Vector2 autoSize;
    private TooltipTrigger triggerCurrent;

    [SerializeField] private FeedbackObject feedbackEventImage;
    [SerializeField] private FeedbackObject feedbackEventButton;

    [SerializeField] private RectTransform tooltipRect;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private Transform rootParent;

    #region Get / Set
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

    public void CursorEnter(TooltipTrigger trigger, FeedbackObject feedback, Vector2 offset, Vector2 size)
    {
        if (tooltipRect.gameObject.activeInHierarchy)   return;
        if (trigger == triggerCurrent)                  return;

        tooltipRect.gameObject.SetActive(true);
        triggerCurrent = trigger;

        Vector2 falseOffset = new Vector2(200, 200);
        autoSize = new Vector2(0, 0);

        tooltipRect.anchorMin = offset + falseOffset;
        tooltipRect.anchorMax = offset + falseOffset + size;

        if (feedback == feedbackEventImage)
        {
            tooltipText.text = trigger.GetComponentInParent<EventGMB>().EventObj.infos.tooltipIcon[0];
        }

        else if (feedback == feedbackEventButton)
        {
            tooltipText.text = trigger.GetComponentInParent<ChoiceGMB>().ChoiceObj.infos.tooltipIcon[0];
        }
        else
        {
            tooltipText.text = feedback.infos.tooltipIcon[0];
        }

        StartCoroutine(ActiveDelay(offset, size));
    }

    IEnumerator ActiveDelay(Vector2 offset, Vector2 size)
    {
        yield return new WaitForSeconds(0.01f);

        autoSize = new Vector2(0, 0.04f * tooltipText.textInfo.lineCount);

        if (offset.y > 0.5f)
        {
            tooltipRect.anchorMin = offset - autoSize - new Vector2(size.x, 0);
            tooltipRect.anchorMax = offset;
        }

        if (offset.y <= 0.5f)
        {
            tooltipRect.anchorMin = offset;
            tooltipRect.anchorMax = offset + autoSize + new Vector2(size.x, 0);
        }
    }

    public void CursorExit(TooltipTrigger trigger)
    {
        if (trigger != triggerCurrent) return;

        triggerCurrent = null;
        tooltipRect.gameObject.SetActive(false);
    }
}
