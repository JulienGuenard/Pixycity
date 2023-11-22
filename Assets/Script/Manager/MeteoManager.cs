using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoManager : MonoBehaviour
{
    public static MeteoManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
