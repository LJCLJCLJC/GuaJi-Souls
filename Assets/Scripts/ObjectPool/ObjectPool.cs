using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 对象池
/// </summary>
public class ObjectPool
{
    public static ObjectPool Instance;
    private Queue<PoolItem> objQueue;
    private GameObject goObj;
    private int Count;
    private Transform parent;

    public ObjectPool()
    {
        Instance = this;
    }
    /// <summary>
    /// 初始化对象池,池内物体必须要继承PoolItem
    /// </summary>
    /// <param name="obj">要存入对象池的Gameobject</param>
    /// <param name="parent">存放的父物体</param>
    /// <param name="count">初始化数量</param>
    public ObjectPool(GameObject obj,Transform parent, int count)
    {
        if (obj.GetComponent<PoolItem>() == null)
        {
            Debug.LogError("没有找到PoolItem"+ obj.name);
            return;
        }
        objQueue = new Queue<PoolItem>();
        goObj = obj;
        Count = count;
        for (int i = 0; i < Count; i++)
        {
            PoolItem item = Tools.CreateGameObjectByObj(goObj, parent).GetComponent<PoolItem>();
            objQueue.Enqueue(item);
            this.parent = parent;
            item.callback = Recycle;
            item.Init();
        }
    }
    /// <summary>
    /// 取出一个物体
    /// </summary>
    public GameObject New()
    {
        PoolItem item = null;
        if (objQueue.Count > 0)
        {
            item = objQueue.Dequeue();
        }
        else
        {
            Count++;
            item = Tools.CreateGameObjectByObj(goObj, parent).GetComponent<PoolItem>();
            item.callback=Recycle;
            item.Init();
        }
        item.ResetItem();
        return item.gameObject;
    }
    /// <summary>
    /// 清空对象池
    /// </summary>
    public void Clear()
    {
        objQueue = new Queue<PoolItem>();
        Tools.ClearChildFromParent(parent);
    }
    public int GetCount()
    {
        return Count;
    }
    private void Recycle(GameObject obj)
    {
        if (obj.GetComponent<PoolItem>() == null) return;

        PoolItem item = obj.GetComponent<PoolItem>();
        objQueue.Enqueue(item);
    }

}
