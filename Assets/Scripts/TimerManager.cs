using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimerManager : MonoBehaviour
{

    private static TimerManager _mactive;
    public static TimerManager active
    {
        get
        {
            if (_mactive == null){
                _mactive = new GameObject("TimerManager").AddComponent<TimerManager>();
                DontDestroyOnLoad(_mactive.gameObject);
            }
            return _mactive;
        }
    }

    private List<Timer> timers = new List<Timer>();

    public void AddTimer(float delay, System.Action action, bool looping = false)
    {
        timers.Add(new Timer(delay, action, looping));
    }

    private void Update()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i].executionTime < Time.time)
            {
                timers[i].action?.Invoke();
                if (timers[i].looping == false)
                {
                    timers.RemoveAt(i);
                }
            }
        }
    }

}

public class Timer
{
    public float executionTime;
    public System.Action action;
    public bool looping;
    private float mdelay;

    public Timer(float delay, System.Action action, bool looping = false)
    {
        this.executionTime = Time.time + delay;
        this.action = action;
        this.looping = looping;
        this.mdelay = delay;

        if (looping)
            this.action += () =>
            {
                executionTime = Time.time + mdelay;
            };
    }
}
