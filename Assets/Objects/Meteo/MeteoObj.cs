using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MeteoObject", order = 1)]
public class MeteoObject : ScriptableObject
{
    public FeedbackObject infos;
    public EnumMeteo meteo;
}