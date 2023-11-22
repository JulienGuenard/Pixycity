using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChoiceObject", order = 1)]
public class ChoiceObject : ScriptableObject
{
    public StructEventTooltip   infos;
    public StructBuilding       building;
    public StructSlotPosition   slot;
    public StructResource       resources;
    public List<HazardObject>      hazards;
}