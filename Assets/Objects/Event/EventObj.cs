using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EventObject", order = 1)]
public class EventObject : ScriptableObject
{
    public StructEventTooltip infos;
    public List<ChoiceObject> choiceList;
}