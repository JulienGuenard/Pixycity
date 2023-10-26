using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Formules : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public float time;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = a.GetComponent<Rigidbody2D>();
    }
    #region Get & Set
    float abcd;

    public float Abcd
        {
            get { return abcd; }
            set { abcd = value; }
        }


    List<float> abcdList;

    public List<float> AbcdListGet()
    {
        return abcdList;
    }

    public void AbcdListAdd(float item)
    {
        abcdList.Add(item);
    }

    public void AbcdListRemove(float item)
    {
        abcdList.Remove(item);
    }

    #endregion

    #region Update
    void Update()
    {
        //   ObjectMoveTowards();
        ObjectMoveInFront();
        ObjectRotateTowards();
    }
    #endregion

    #region Move to & Look to (2D) 
    void ObjectMoveTowards()
    {
        a.transform.position = Vector3.MoveTowards(a.transform.position, b.transform.position, time);
    }
   
    void ObjectMoveInFront()
    {
        rb.velocity = a.transform.right * time;
    }

    void ObjectRotateTowards()
    {
        Vector3 relativePos = b.transform.position - a.transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        a.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    #endregion
}
