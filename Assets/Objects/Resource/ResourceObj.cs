using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ResourceObject", order = 1)]
public class ResourceObject : ScriptableObject
{
    public StructSingleResource resources;
}