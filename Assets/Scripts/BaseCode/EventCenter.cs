using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventDelegate(object obj);
public class EventCenter  {
    private Dictionary<int,EventDelegate> eventDic = new Dictionary<int, EventDelegate>();

    public void AddListener(int eventKey, EventDelegate func)
    {
        if (!eventDic.ContainsKey(eventKey))
        {
            EventDelegate del = null;
            eventDic[eventKey] = del;
        }
        eventDic[eventKey] += func;
    }

    public void RemoveListener(int eventKey,EventDelegate func)
    {
        if(!eventDic.ContainsKey(eventKey))
        {
            return;
        }
        eventDic[eventKey] -= func;
        if (eventDic[eventKey] == null)
        {
            eventDic.Remove(eventKey);
        }
    }

    public void CallEvent(int eventKey,object obj)
    {
        if (!eventDic.ContainsKey(eventKey))
        {
            return;
        }
        eventDic[eventKey](obj);
    }

    public void Clear()
    {
        eventDic.Clear();
    }
}
