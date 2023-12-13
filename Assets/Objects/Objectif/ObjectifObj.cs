using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectifObject", order = 1)]
public class ObjectifObject : ScriptableObject
{
    public FeedbackObject infos;
    public List<StructSingleResource> resourceToGatherList;
    public List<ChoiceObject> choiceToChooseList;
}