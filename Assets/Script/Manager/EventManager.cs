using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private float eventCooldownInitial;
    [SerializeField] private float eventCooldown;
    private float eventCooldownActual;
    private int eventNextID;

    [SerializeField] private float scrollY;
    [SerializeField] private float scrollYSpeed;
    [SerializeField] private float scrollYSpeedCurb;
    [SerializeField] private float scrollYMinAutoMoveAbove;
    [SerializeField] private AnimationCurve scrollYCurb;
    private float scrollYSpeedActual = 1f;
    private float scrollYActual;
    #endregion

    #region Lists
    [SerializeField] private List<EventGMB> eventGMBList;
    public List<EventGMB> EventGMBListGet()
    {
        return eventGMBList;
    }
    public void EventGMBListAdd(EventGMB item)
    {
        eventGMBList.Add(item);
    }
    public void EventGMBListRemove(EventGMB item)
    {
        eventGMBList.Remove(item);
    }

    private List<EventObject> eventList = new List<EventObject>();
    public List<EventObject> EventListGet()
    {
        return eventList;
    }
    public void EventListAdd(EventObject item)
    {
        eventList.Add(item);
    }
    public void EventListRemove(EventObject item)
    {
        eventList.Remove(item);
    }
    #endregion

    public static EventManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        eventList.Clear();

        eventCooldownActual = eventCooldownInitial;
        eventNextID = 3;
    }

    private void Start()
    {
        eventList.Clear();
    }

    private void Update()
    {
        Update_NewEvent();
        Update_EventScroll();
    }
    private void Update_NewEvent()
    {
        if (!NewEvent_Cooldown())   return;
        if (!NewEvent_CheckAvailability()) return;

        NewEvent_UpdateInfos();
        NewEvent_ScrollDownOneTime();
    }
    private void Update_EventScroll()
    {
        if (scrollYActual <= 0) return;

        EventScroll_MoveInit();
        EventScroll_MoveThenGoAbove();
    }

    private void NewEvent_UpdateInfos()
    {
        EventGMB eventGMB = EventGMBListGet()[eventNextID];
        EventObject eventOBJ = EventListGet()[0];
        List<ChoiceGMB> list = eventGMB.ChoiceGMBListGet();

        eventGMB.Title.text = eventOBJ.name;
        eventGMB.EventObj = eventOBJ;
        eventGMB.EventImage.sprite = eventOBJ.infos.spriteIcon;
        for (int i = 0; i < 3; i++)
        {
            if (eventOBJ.choiceList.Count > i)
            {
                list[i].ChoiceObj = eventOBJ.choiceList[i];
                ChoiceManager.instance.ChoiceListAdd(list[i]);
            }
            else
            {
                list[i].ChoiceObj = null;
            }
        }

        eventNextID--;

        EventListRemove(eventOBJ);
    }
    private bool NewEvent_Cooldown()
    {
        if (TimeManager.instance.GTime < eventCooldownActual) return false;

        eventCooldownActual += eventCooldown - eventCooldownInitial;
        eventCooldownInitial = 0;

        return true;
    }
    private bool NewEvent_CheckAvailability()
    {
        if (eventList.Count == 0) return false;

        return true;
    }
    private void NewEvent_ScrollDownOneTime()
    {
        scrollYActual += scrollY;
    }

    private void EventScroll_MoveInit()
    {
        scrollYSpeedActual = scrollYSpeedCurb * scrollYCurb.Evaluate((scrollYActual * (2 / scrollY))) + scrollYSpeed;
        scrollYActual -= scrollYSpeedActual;

        if (scrollYActual <= 0) scrollYSpeedActual += scrollYActual;
    }
    private void EventScroll_MoveThenGoAbove()
    {
        EventGMB eventGMBBelow = null;

        foreach (EventGMB eventGMB in EventGMBListGet())
        {
            eventGMB.Rect.anchorMin -= new Vector2(0, scrollYSpeedActual);
            eventGMB.Rect.anchorMax -= new Vector2(0, scrollYSpeedActual);

            if (eventGMB.Rect.anchorMin.y <= scrollYMinAutoMoveAbove) eventGMBBelow = eventGMB;
        }

        if (eventGMBBelow == null) return;

        EventGoAbove(eventGMBBelow);
    }

    public void EventGoAbove(EventGMB eventGMB)
    {
        eventGMB.Rect.anchorMin = new Vector2(eventGMB.Rect.anchorMin.x, scrollY + EventGMBListGet()[0].Rect.anchorMin.y);
        eventGMB.Rect.anchorMax = new Vector2(eventGMB.Rect.anchorMax.x, scrollY + EventGMBListGet()[0].Rect.anchorMax.y);

        if (EventGMBListGet()[3] != eventGMB)
        {
            EventGMBListGet()[3].Rect.anchorMin = new Vector2(eventGMB.Rect.anchorMin.x, scrollY + EventGMBListGet()[3].Rect.anchorMin.y);
            EventGMBListGet()[3].Rect.anchorMax = new Vector2(eventGMB.Rect.anchorMax.x, scrollY + EventGMBListGet()[3].Rect.anchorMax.y);

            if (EventGMBListGet()[2] != eventGMB)
            {
                EventGMBListGet()[2].Rect.anchorMin = new Vector2(eventGMB.Rect.anchorMin.x, scrollY + EventGMBListGet()[2].Rect.anchorMin.y);
                EventGMBListGet()[2].Rect.anchorMax = new Vector2(eventGMB.Rect.anchorMax.x, scrollY + EventGMBListGet()[2].Rect.anchorMax.y);
            }
        }

        eventNextID++;
        EventGMBListRemove(eventGMB);
        EventGMBListGet().Insert(0, eventGMB);
    }
}
