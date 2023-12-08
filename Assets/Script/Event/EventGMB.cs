using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventGMB : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image eventImage;
    private RectTransform rect;
    private EventObject eventObj;

    [SerializeField] private List<ChoiceGMB> choiceGMBList;
    #endregion

    #region Get / Set
    public TextMeshProUGUI Title
    {
        get { return title; }
        set { title = value; }
    }
    public Image EventImage
    {
        get { return eventImage; }
        set { eventImage = value; }
    }
    public RectTransform Rect
    {
        get { return rect; }
        set { rect = value; }
    }
    public EventObject EventObj
    {
        get { return eventObj; }
        set { eventObj = value; }
    }

    public List<ChoiceGMB> ChoiceGMBListGet()
    {
        return choiceGMBList;
    }
    public void ChoiceGMBListAdd(ChoiceGMB item)
    {
        choiceGMBList.Add(item);
    }
    public void ChoiceGMBListRemove(ChoiceGMB item)
    {
        choiceGMBList.Remove(item);
    }
    #endregion

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
