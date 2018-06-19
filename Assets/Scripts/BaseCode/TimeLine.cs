using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TimeLineCall(object obj);
public class TimeLineData
{
    public TimeLineCall cb;
    public object param;
    public float runTime;
    public GameObject callGameObject;
}

public class TimeLine
{
    private static TimeLine _instance;
    public TimeLine()
    {
        _instance = this;
    }

    private List<TimeLineData> list = new List<TimeLineData>();
    private List<TimeLineData> listClone = new List<TimeLineData>();

    public void Update()
    {

        TimeLineData td = null;
        for (int i = 0; i < list.Count; i++)
        {
            listClone.Add(list[i]);
        }
        for (int i = 0; i < listClone.Count; i++)
        {
            td = listClone[i];
            if (td.runTime <= Time.time)
            {
                if (td.cb != null && td.callGameObject != null)
                {
                    td.cb(td.param);
                }
                list.Remove(td);
            }
        }
        listClone.Clear();

    }

    public void AddTimeEvent(TimeLineCall call, float delayTime, object param, GameObject callGameObject)
    {
        TimeLineData tData = new TimeLineData();
        tData.cb = call;
        tData.param = param;
        tData.runTime = delayTime + Time.time;
        tData.callGameObject = callGameObject;
        list.Add(tData);
    }
    public void RemoveTimeEvent(TimeLineCall call)
    {
        for (int i = 0; i < list.Count; i++)
        {
            TimeLineData td = list[i];
            if (td.cb == call)
            {
                list.Remove(td);
            }
        }
    }
    public static TimeLine GetInstance()
    {
        return _instance;
    }
    public void Clear()
    {
        list.Clear();
    }
}
