using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    List<GameObject> projectileList = new List<GameObject>();

    public static ProjectileManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public List<GameObject> ProjectileList
    {
        get { return projectileList; }
    }

    public void ProjectileListAdd(GameObject value)
    {
        ProjectileList.Add(value);
    }

    public void ProjectileListRemove(GameObject value)
    {
        ProjectileList.Remove(value);
    }

    public void ProjectileInstantiate(GameObject p, Vector3 pos, Quaternion rot)
    {
        GameObject o = Instantiate(p, pos, rot);

        ProjectileListAdd(o);
    }
}
