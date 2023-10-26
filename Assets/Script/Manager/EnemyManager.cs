using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>();

    public static EnemyManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public List<GameObject> EnemyList
    {
        get { return enemyList; }
    }

    public void EnemyListAdd(GameObject value)
    {
        EnemyList.Add(value);
    }

    public void EnemyListRemove(GameObject value)
    {
        EnemyList.Remove(value);
    }
}
