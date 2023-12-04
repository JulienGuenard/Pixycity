using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FeedbackObject", order = 1)]
public class FeedbackObject : ScriptableObject
{
    public StructEventTooltip infos;
}