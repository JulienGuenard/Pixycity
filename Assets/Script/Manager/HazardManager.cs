using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    public static HazardManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
