using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectifManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private ObjectifObject objectifObjCurrent;

    [SerializeField] private List<ObjectifObject> objectifObjList;

    private bool objectifIsAchieved = false;
    #region Get / Set
    public ObjectifObject ObjectifObjCurrent
    {
        get { return objectifObjCurrent; }
        set 
        { 
            objectifObjCurrent = value;
            UIManager.instance.CurrentObjectifText.text = objectifObjCurrent.infos.infos.tooltipIngame[0];
            UIManager.instance.CurrentObjectifTooltip.feedback = objectifObjCurrent.infos;
        }
    }
    #endregion

    #endregion

    public static ObjectifManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        ObjectifObjCurrent = objectifObjCurrent;
    }

    private void Update()
    {
        CheckVictory();
    }

    public void CheckVictory()
    {
        if (objectifIsAchieved) return;

        StartCoroutine(CheckVictory_Countdown());
    }

    IEnumerator CheckVictory_Countdown()
    {
        yield return new WaitForSeconds(0.01f);

        if (objectifIsAchieved) yield break;

        foreach(ResourceObject res in ResourceManager.instance.ResourceListGet())
        {
            if (res.resources.resourceType != objectifObjCurrent.resourceToGatherList[0].resourceType)  continue;
            if (res.resources.current < objectifObjCurrent.resourceToGatherList[0].current)             continue;

            objectifIsAchieved = true;
            
            GameManager.instance.Victory();
        }
    }
}
