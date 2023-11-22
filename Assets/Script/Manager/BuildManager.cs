using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Variables
    [SerializeField] float newBuildCooldownInitial;
    [SerializeField] float newBuildCooldown;
    private float newBuildCooldownActual;

    [SerializeField] private int buildEventLimit;
    private int buildEventLimitActual;
    
    [SerializeField] EventObject buildEvent;

    public static BuildManager instance;

    #region Get / Set
    public int BuildEventLimitActual
    {
        get { return buildEventLimitActual; }
        set { buildEventLimitActual = value; }
    }
    #endregion

    #endregion

    #region Lists
    [SerializeField] private List<GameObject> slotBuildEmptyList;
    private List<GameObject> slotBuildList = new List<GameObject>();

    [SerializeField] List<ChoiceObject> cycleBuilding;
    private List<ChoiceObject> cycleBuildingActual = new List<ChoiceObject>();

    #region Get / Set
    public List<GameObject> SlotBuildEmptyListGet()
    {
        return slotBuildEmptyList;
    }
    public void SlotBuildEmptyListAdd(GameObject item)
    {
        slotBuildEmptyList.Add(item);
    }
    public void SlotBuildEmptyListRemove(GameObject item)
    {
        slotBuildEmptyList.Remove(item);
    }

    public List<GameObject> SlotBuildListGet()
    {
        return slotBuildList;
    }
    public void SlotBuildListAdd(GameObject item)
    {
        slotBuildList.Add(item);
    }
    public void SlotBuildListRemove(GameObject item)
    {
        slotBuildList.Remove(item);
    }

    public List<ChoiceObject> CycleListGet()
    {
        return cycleBuilding;
    }
    public void CycleListAdd(ChoiceObject item)
    {
        cycleBuilding.Add(item);
    }
    public void CycleListRemove(ChoiceObject item)
    {
        cycleBuilding.Remove(item);
    }

    public List<ChoiceObject> CycleListActualGet()
    {
        return cycleBuildingActual;
    }
    public void CycleListActualAdd(ChoiceObject item)
    {
        cycleBuildingActual.Add(item);
    }
    public void CycleListActualRemove(ChoiceObject item)
    {
        cycleBuildingActual.Remove(item);
    }
    #endregion

    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;

        AwakeVariables();
    }

    private void AwakeVariables()
    {
        newBuildCooldownActual = newBuildCooldownInitial;

        buildEvent.choiceList.Clear();
        for (int i = 0; i < 3; i++) buildEvent.choiceList.Add(null);
    }

    private void Update()
    {
        Update_NewEvent();
    }
    private void Update_NewEvent()
    {
        if (!NewEvent_Cooldown()) return;

        BuildEventLimitActual++;
        NewEvent_CycleBuild();
        EventManager.instance.EventListAdd(buildEvent);
    }

    private bool NewEvent_Cooldown()
    {
        if (TimeManager.instance.GTime < newBuildCooldownActual)    return false;
        if (buildEventLimitActual >= buildEventLimit)               return false;

        newBuildCooldownActual += newBuildCooldown - newBuildCooldownInitial;
        newBuildCooldownInitial = 0;

        return true;
    }
    private void NewEvent_CycleBuild()
    {
        if (CycleListGet().Count == 0) return;

        List<ChoiceObject> list = new List<ChoiceObject>();

        CycleBuild_SetCycleMax(list);
        CycleBuild_SetCycleActual(list);
        CycleBuild_RemoveBuildBtn();
    }

    private void CycleBuild_SetCycleMax(List<ChoiceObject> list)
    {
        int buildMax = 3;
        if (CycleListGet().Count < buildMax) buildMax = CycleListGet().Count;

        list.AddRange(CycleListGet().GetRange(0, buildMax));
    }
    private void CycleBuild_SetCycleActual(List<ChoiceObject> list)
    {
        CycleListActualGet().Clear();

        foreach (ChoiceObject choiceObject in list)
        {
            CycleListActualAdd(choiceObject);
            CycleListRemove(choiceObject);
            CycleListAdd(choiceObject);
        }
    }
    private void CycleBuild_RemoveBuildBtn()
    {
        while (CycleListActualGet().Count != buildEvent.choiceList.Count) buildEvent.choiceList.RemoveAt(buildEvent.choiceList.Count - 1);

        for (int i = 0; i < CycleListActualGet().Count; i++)
        {
            buildEvent.choiceList[i] = CycleListActualGet()[i];
        }
    }

    public void ClickBtn(ChoiceGMB choiceGMB)
    {
        ClickBtn_BuildNew(choiceGMB);
    }
    private void ClickBtn_BuildNew(ChoiceGMB choiceGMB)
    {
        ChoiceObject choiceObj = choiceGMB.ChoiceObj;

        if (choiceObj == null) return;
        if (!ResourceManager.instance.BuildCheckResources(choiceObj)) return;

        BuildEventLimitActual--;

        ResourceManager.instance.ChangeResources(choiceObj);
        BuildNew_UseSlot(choiceObj);
        BuildNew_CycleNextTier(choiceGMB, choiceObj);

        EventManager.instance.EventGoAbove(choiceGMB.EventGMB);
    }

    private void BuildNew_UseSlot(ChoiceObject choiceObj)
    {
        GameObject slot = SlotBuildEmptyListGet()[Random.Range(0, SlotBuildEmptyListGet().Count)];

        SlotBuildEmptyListRemove(slot);
        SlotBuildListAdd(slot);

        slot.GetComponent<SpriteRenderer>().sprite = choiceObj.building.buildingSprite;

        if (choiceObj.slot.posOffset != Vector2.zero) slot.transform.position = choiceObj.slot.posOffset;
        if (choiceObj.slot.scale != 0) slot.transform.localScale = new Vector3(choiceObj.slot.scale, choiceObj.slot.scale, choiceObj.slot.scale);
        if (choiceObj.slot.layer != 0) slot.GetComponent<SpriteRenderer>().sortingOrder = choiceObj.slot.layer;
    }
    private void BuildNew_CycleNextTier(ChoiceGMB choiceGMB, ChoiceObject choiceObj)
    {
        int id = CycleListGet().IndexOf(choiceObj);
        CycleListRemove(choiceObj);
        if (choiceObj.building.buildingNextTier != null) CycleListGet().Insert(id, choiceObj.building.buildingNextTier);

        for (int i = 0; i < 3; i++)
        {
            ChoiceManager.instance.ChoiceListRemove(choiceGMB.EventGMB.ChoiceGMBListGet()[i]);
        }
    }
}
