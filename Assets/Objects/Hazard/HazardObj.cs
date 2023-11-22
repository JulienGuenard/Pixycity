using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HazardObject", order = 1)]
public class HazardObject : ScriptableObject
{
    public List<EnumMeteo> meteoList;
    public EventObject eventObj;
}