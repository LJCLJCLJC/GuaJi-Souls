using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    public int nowMap = 0;
    public MapVo nowMapVo;
    public static BattleController Instance;
    public int totalRound = 0;
    public PlayerUnit player = new PlayerUnit();
    public ServantUnit servant = new ServantUnit();
    public EnemyUnit enemy = new EnemyUnit();
    public float roundTime;
    public int pauseRound = 0;
    public bool ifBoss;
    private bool inBattle = false;
    private float startTime;
    private int nowPos = 0;
    private List<BaseUnit> battleList = new List<BaseUnit>();
    private List<BaseUnit> nextList = new List<BaseUnit>();
    private List<StaticUnitLevelVo> enemyList = new List<StaticUnitLevelVo>();
    private bool ifEnd = false;
    private void Awake()
    {
        Instance = this;
        GameRoot.Instance.evt.AddListener(GameEventDefine.BATTLE_START, OnBattleStart);
        GameRoot.Instance.evt.AddListener(GameEventDefine.BATTLE_END, OnBattleEnd);
        GameRoot.Instance.evt.AddListener(GameEventDefine.LOAD_MAP, OnLoadMap);
    }
    void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - startTime > roundTime)
        {
            DoRound();
            startTime = Time.time;
        }
    }
    private void OnLoadMap(object obj)
    {
        int mapId = (int)obj;
        nowMap = mapId;
        DataManager.Instance.mapModel.nowMap = nowMap;
        nowMapVo = DataManager.Instance.mapModel.GetMapVo(nowMap);
        StaticMapVo mapVo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(mapId);
        enemyList.Clear();
        for (int i = 0; i < mapVo.enemyIdList.Count; i++)
        {
            enemyList.Add(StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(mapVo.enemyIdList[i]));
        }
        inBattle = false;
    }
    private void OnBattleStart(object obj)
    {
        bool ifBoss = (bool)obj;
        this.ifBoss = ifBoss;
        player.Create();
        servant.Create();
        if (!ifBoss)
        {
            int enemyRandom = Random.Range(0, enemyList.Count);
            enemy.Create(enemyList[enemyRandom]);
        }
        else
        {
            StaticMapVo mapVo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(nowMap);
            enemy.Create(StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(mapVo.bossId), true);
        }
        ifEnd = false;
        pauseRound = 1;
        GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_UNIT_CELL, UnitState.None);
        battleList.Clear();
        battleList.Add(player);
        battleList.Add(enemy);
        battleList.Add(servant);
        OnUpdateList(null);
        nowPos = 0;

    }
    private void OnBattleEnd(object obj)
    {
        inBattle = false;
        ifBoss = (bool)obj;
        ifEnd = true;
    }
    private void OnUpdateList(object obj)
    {
        nextList.Clear();
        nextList = new List<BaseUnit>(battleList.ToArray());
        nextList.Sort(SortBySpeed);
        battleList = new List<BaseUnit>(nextList.ToArray());


    }
    private void DoRound()
    {
        if (pauseRound > 0)
        {
            pauseRound--;
            nowPos = 0;
            if (ifEnd && pauseRound == 0)
            {
                OnBattleStart(ifBoss);
            }
            else if(!ifEnd && pauseRound == 0)
            {
                inBattle = true;
                return;
            }
            else
            {
                return;
            }
        }
        if (inBattle)
        {
            if (nowPos >= 3)
            {
                nowPos = 0;
                totalRound++;
                battleList[0].Buff();
                battleList[1].Buff();
                battleList[2].Buff();

            }
            if (!battleList[nowPos].dead)
            {
                battleList[nowPos].Attack();
                nowPos++;
            }
            else
            {
                if (nowPos + 1 >= 3)
                    battleList[0].Attack();
                else
                    battleList[nowPos + 1].Attack();
                nowPos += 2;
            }
        }

    }
    private int SortBySpeed(BaseUnit u1, BaseUnit u2)
    {
        if (u1.speed > u2.speed)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BATTLE_START, OnBattleStart);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BATTLE_END, OnBattleEnd);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.LOAD_MAP, OnLoadMap);

    }
}
