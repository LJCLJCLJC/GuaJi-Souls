using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {
    public static GameRoot Instance;

    public EventCenter evt;
    public TimeLine timeLine;
    public ActivePool activePool;
    public string currentLoadScene;
    private PlayerData nowPlayer;
    public int nowPlayerId;

    private void Awake()
    {
        Instance = this;
        evt = new EventCenter();
        timeLine = new TimeLine();
        activePool = new ActivePool();
        DontDestroyOnLoad(gameObject);
        InitData(null);

    }
    void Start () {
       
    }
    private void FixedUpdate()
    {
        timeLine.Update();
    }

    private void InitData(object obj)
    {
        StaticDataPool.Instance.CreateData();
    }
 

}
