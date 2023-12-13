using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.FlowStateWidget;

public class MeteoManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int meteoTimelineLimit;
    [SerializeField] private MeteoObject meteoCurrent;
    private MeteoObject meteoPrevious;
    private bool meteoHasChanged = false;

    [SerializeField] private float scrollX;
    [SerializeField] private float scrollXSpeed;
    [SerializeField] private float scrollXMinAutoMoveAtLast;
    [SerializeField] private float scrollXMinCurrent;
    private float scrollXSpeedActual = 1f;
    private float scrollXActual;
    #endregion

    #region Lists
    private List<MeteoObject> meteoTimelineList = new List<MeteoObject>();
    public List<MeteoObject> MeteoTimelineListGet()
    {
        return meteoTimelineList;
    }
    public void MeteoTimelineListAdd(MeteoObject item)
    {
        meteoTimelineList.Add(item);
    }
    public void MeteoTimelineListRemove(MeteoObject item)
    {
        meteoTimelineList.Remove(item);
    }

    [SerializeField] private List<GameObject> meteoFXList;
    public List<GameObject> MeteoFXListGet()
    {
        return meteoFXList;
    }
    public void MeteoFXListAdd(GameObject item)
    {
        meteoFXList.Add(item);
    }
    public void MeteoFXListRemove(GameObject item)
    {
        meteoFXList.Remove(item);
    }

    [SerializeField] private List<MeteoObject> meteoObjList;
    public List<MeteoObject> MeteoObjListGet()
    {
        return meteoObjList;
    }
    public void MeteoObjListAdd(MeteoObject item)
    {
        meteoObjList.Add(item);
    }
    public void MeteoObjListRemove(MeteoObject item)
    {
        meteoObjList.Remove(item);
    }

    [SerializeField] private List<MeteoGMB> meteoGMBList;
    public List<MeteoGMB> MeteoGMBListGet()
    {
        return meteoGMBList;
    }
    public void MeteoGMBListAdd(MeteoGMB item)
    {
        meteoGMBList.Add(item);
    }
    public void MeteoGMBListRemove(MeteoGMB item)
    {
        meteoGMBList.Remove(item);
    }

    private List<MeteoObject> meteoList = new List<MeteoObject>();
    public List<MeteoObject> EventListGet()
    {
        return meteoList;
    }
    public void EventListAdd(MeteoObject item)
    {
        meteoList.Add(item);
    }
    public void EventListRemove(MeteoObject item)
    {
        meteoList.Remove(item);
    }
    #endregion

    public static MeteoManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        MeteoReset();
    }

    private void Update()
    {
        MeteoScroll_MoveThenGoAbove();
    }

    private void MeteoScroll_MoveThenGoAbove()
    {
        MeteoGMB meteoGMBBelow = null;
        scrollXSpeedActual = scrollXSpeed;

        foreach (MeteoGMB meteoGMB in MeteoGMBListGet())
        {
            meteoGMB.Rect.anchorMin -= new Vector2(scrollXSpeedActual, 0);
            meteoGMB.Rect.anchorMax -= new Vector2(scrollXSpeedActual, 0);

            if (meteoGMB.Rect.anchorMin.x <= scrollXMinAutoMoveAtLast) meteoGMBBelow = meteoGMB;
            if (meteoGMB.Rect.anchorMin.x <= scrollXMinCurrent) MeteoNew_ChangeCurrent(meteoGMB);
        }

        if (meteoGMBBelow == null) return;

        MeteoGoAbove(meteoGMBBelow);
        MeteoNew_Next(meteoGMBBelow);
    }

    public void MeteoGoAbove(MeteoGMB meteoGMB)
    {
        meteoGMB.Rect.anchorMin = new Vector2(scrollX + MeteoGMBListGet()[0].Rect.anchorMin.x, meteoGMB.Rect.anchorMin.y);
        meteoGMB.Rect.anchorMax = new Vector2(scrollX + MeteoGMBListGet()[0].Rect.anchorMax.x, meteoGMB.Rect.anchorMax.y);

        MeteoGMBListRemove(meteoGMB);
        MeteoGMBListGet().Insert(0, meteoGMB);
    }

    private void MeteoNew_Next(MeteoGMB meteoGMB)
    {
        meteoHasChanged = false;
        meteoGMB.MeteoObject = meteoObjList[Random.Range(0, meteoObjList.Count)];
    }
    private void MeteoNew_ChangeCurrent(MeteoGMB meteoGMB)
    {
        if (meteoHasChanged) return;
        meteoHasChanged = true;

        ChangeCurrent_ChangeValues(meteoGMB);
        ChangeCurrent_ChangeUI();
    }

    private void ChangeCurrent_ChangeValues(MeteoGMB meteoGMB)
    {
        meteoPrevious = meteoCurrent;
        meteoCurrent = meteoGMB.MeteoObject;
        meteoTimelineList.Add(meteoCurrent);

        UIManager.instance.MeteoCurrent.sprite = meteoCurrent.infos.infos.spriteIcon;
        HazardManager.instance.CheckMeteo(meteoCurrent);
    }
    private void ChangeCurrent_ChangeUI()
    {
        if (meteoTimelineList.Count > meteoTimelineLimit) MeteoTimelineListRemove(meteoTimelineList[0]);
        if (meteoPrevious.meteo == meteoCurrent.meteo) return;

        foreach (GameObject obj in MeteoFXListGet()) { obj.SetActive(false); }

        switch (meteoCurrent.meteo)
        {
            case EnumMeteo.Sun: MeteoFXListGet()[0].SetActive(true); break;
            case EnumMeteo.Cloud: MeteoFXListGet()[1].SetActive(true); break;
            case EnumMeteo.Rain: MeteoFXListGet()[2].SetActive(true); break;
        }
    }

    void MeteoReset()
    {
        foreach(MeteoGMB gmb in MeteoGMBListGet())
        {
            MeteoNew_Next(gmb);
        }
    }
}
