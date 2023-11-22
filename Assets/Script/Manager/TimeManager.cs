using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    int sec1 = 0;
    int sec2 = 0;
    int min1 = 0;
    int min2 = 0;
    int hrs1 = 0;
    int hrs2 = 0;

    private TextMeshProUGUI timer;
    private float newTime;
    private float gTime;
    public float GTime
    {
        get { return gTime; }
        set { gTime = value; }
    }

    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        timer = UIManager.instance.TimerText;
        newTime = Time.time;
    }

    private void Update()
    {
        Timer();
    }

    void Timer()
    {
        GTime = Time.time - newTime;

        hrs2 = Mathf.FloorToInt(GTime / 36000);
        hrs1 = Mathf.FloorToInt(GTime / 3600) - (hrs2 * 10);

        min2 = Mathf.FloorToInt(GTime / 600) - (hrs1 * 6) - (hrs2 * 60);
        min1 = Mathf.FloorToInt(GTime / 60) - (min2 * 10) - (hrs1 * 60) - (hrs2 * 600);

        sec2 = Mathf.FloorToInt(GTime / 10) - (min1 * 6) - (min2 * 60) - (hrs1 * 360) - (hrs2 * 3600);
        sec1 = Mathf.FloorToInt(GTime) - (sec2 * 10) - (min1 * 60) - (min2 * 600) - (hrs1 * 3600) - (hrs2 * 36000);

        if (hrs2 > 0) timer.text = hrs2.ToString() + hrs1.ToString() + ":" + min2.ToString() + min1.ToString() + ":" + sec2.ToString() + sec1.ToString();
        else if (hrs1 > 0) timer.text = hrs1.ToString() + ":" + min2.ToString() + min1.ToString() + ":" + sec2.ToString() + sec1.ToString();
        else if (min2 > 0) timer.text = min2.ToString() + min1.ToString() + ":" + sec2.ToString() + sec1.ToString();
        else timer.text = min1.ToString() + ":" + sec2.ToString() + sec1.ToString();
    }
}
