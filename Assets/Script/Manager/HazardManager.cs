using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private List<StructBuildingMeteos> meteoPerBuildingList = new List<StructBuildingMeteos>();

    public List<StructBuildingMeteos> MeteoPerBuildingListGet()
    {
        return meteoPerBuildingList;
    }
    public void MeteoPerBuildingListAdd(StructBuildingMeteos item)
    {
        meteoPerBuildingList.Add(item);
    }
    public void MeteoPerBuildingListRemove(StructBuildingMeteos item)
    {
        meteoPerBuildingList.Remove(item);
    }

    public static HazardManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void CheckMeteo(MeteoObject meteoObj)
    {
        if (meteoPerBuildingList.Count == 0) return;

        foreach (StructBuildingMeteos obj in MeteoPerBuildingListGet())
        {
            obj.meteoEnumList.Add(meteoObj.meteo);
            bool meteoReset = true;

            foreach (HazardObject hazard in obj.building.hazards)
            {
                if (CheckMeteo_CompareHazard(obj, hazard)) continue;
                meteoReset = CheckMeteo_OneHazardStillPossible(obj, hazard, meteoReset);
            }

            if (meteoReset) obj.meteoEnumList.Clear();
        }
    }
    bool CheckMeteo_CompareHazard(StructBuildingMeteos obj, HazardObject hazard)
    {
        if (obj.meteoEnumList.Count != hazard.meteoList.Count)  return false;

        for (int i = 0; i < obj.meteoEnumList.Count; i++)
        {
            if (obj.meteoEnumList[i] != hazard.meteoList[i])    break;
            if (i < obj.meteoEnumList.Count - 1)                continue;

            EventManager.instance.EventListAdd(hazard.eventObj);
            return true;
        }
        return false;
    }
    bool CheckMeteo_OneHazardStillPossible(StructBuildingMeteos obj, HazardObject hazard, bool meteoReset)
    {
        bool meteoResetCheckHazard = true;

        for (int i = 0; i < obj.meteoEnumList.Count; i++)
        {
            if (i >= hazard.meteoList.Count)                    { meteoResetCheckHazard = true; continue; }
            if (obj.meteoEnumList[i] == hazard.meteoList[i])    { meteoResetCheckHazard = false; continue; }
            break;
        }

        if (meteoReset) return meteoResetCheckHazard;
        return meteoReset;
    }
}
