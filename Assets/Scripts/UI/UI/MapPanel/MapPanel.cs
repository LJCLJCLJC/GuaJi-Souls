using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : BasePanel
{
    public GameObject tsMapInfo;
    public Image mapImage;
    public Text mapName;
    public Text desc;
    public Text enemyList;
    public Text equipList;
    public Text itemList;
    public Text openCondition;
    public Text level;
    public Slider progress;
    public GameObject clear;
    public Button btnStart;
    public Button btnShop;
    public MapCell[] mapCell;

    private MapVo nowVo;

    void Start()
    {
        tsMapInfo.SetActive(false);
        OnUpdate(null);
        btnStart.onClick.AddListener(delegate () { BtnClick(btnStart); });
        btnShop.onClick.AddListener(delegate () { BtnClick(btnShop); });
    }

    private void OnUpdate(object obj)
    {
        DataManager.Instance.mapModel.CheckOpen();
        for (int i = 0; i < mapCell.Length; i++)
        {
            MapVo mapVo = DataManager.Instance.mapModel.GetMapVo(mapCell[i].id);
            mapCell[i].Create(mapVo, MapSelect);
        }
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnStart":
                if (nowVo.id == 18)
                {
                    UIManager.Instance.Create(Panel_ID.JiSiChangPanel);
                }
                else
                {
                    UIManager.Instance.Create(Panel_ID.BattlePanel);
                    GameRoot.Instance.evt.CallEvent(GameEventDefine.LOAD_MAP, nowVo.id);
                }
                break;
            case "btnShop":
                UIManager.Instance.Create(Panel_ID.ShopPanel);
                break;
        }
    }
    private void MapSelect(object obj)
    {
        MapVo mapVo = (MapVo)obj;
        StaticMapVo staticMapVo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(mapVo.id);
        nowVo = mapVo;
        tsMapInfo.SetActive(true);
        mapImage.sprite = ResourceManager.Instance.GetMapImage(staticMapVo.image);
        mapName.text = staticMapVo.name;
        desc.text = staticMapVo.desc;
        int i;
        btnShop.gameObject.SetActive(false);
        if (nowVo.id == 18)
        {
            enemyList.gameObject.SetActive(false);
            equipList.gameObject.SetActive(false);
            itemList.gameObject.SetActive(false);
            level.gameObject.SetActive(false);
            openCondition.text = "开启条件:";
            for (i = 0; i < staticMapVo.openConditions.Count; i++)
            {
                for (int j = 1; j < staticMapVo.openConditions[i].Count; j++)
                {
                    switch (staticMapVo.openConditions[i][0])
                    {
                        case 0:
                            if (staticMapVo.openConditions[i][j] == 0)
                            {
                                openCondition.text += "无";
                                continue;
                            }
                            StaticMapVo vo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(staticMapVo.openConditions[i][j]);
                            openCondition.text += ("击败" + vo.name + "的BOSS ");
                            break;
                        case 1:
                            StaticItemVo vo1 = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(staticMapVo.openConditions[i][j]);
                            openCondition.text += ("获得道具" + vo1.name + " ");
                            break;
                    }
                }
            }
            if (DataManager.Instance.roleVo.ifShopOpen)
            {
                btnShop.gameObject.SetActive(true);
            }
        }
        else
        {
            enemyList.gameObject.SetActive(true);
            equipList.gameObject.SetActive(true);
            itemList.gameObject.SetActive(true);
            level.gameObject.SetActive(true);
            enemyList.text = "出现敌人:";
            List<int> enemys = new List<int>();
            for (i = 0; i < staticMapVo.enemyIdList.Count; i++)
            {
                if (!enemys.Contains(StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(staticMapVo.enemyIdList[i]).unitId))
                {
                    enemys.Add(StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(staticMapVo.enemyIdList[i]).unitId);
                    enemyList.text += (StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(staticMapVo.enemyIdList[i]).name + " ");
                }
            }
            itemList.text = "掉落道具:";
            for (i = 0; i < staticMapVo.dropItem.Count; i++)
            {
                itemList.text += (StaticDataPool.Instance.staticItemPool.GetStaticDataVo(staticMapVo.dropItem[i][0]).name + " ");
            }
            equipList.text = "掉落装备:";
            for (i = 0; i < staticMapVo.dropEquip.Count; i++)
            {
                equipList.text += (StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(staticMapVo.dropEquip[i][0]).equipName + " ");
            }
            openCondition.text = "开启条件:";
            for (i = 0; i < staticMapVo.openConditions.Count; i++)
            {
                for (int j = 1; j < staticMapVo.openConditions[i].Count; j++)
                {
                    switch (staticMapVo.openConditions[i][0])
                    {
                        case 0:
                            if (staticMapVo.openConditions[i][j] == 0)
                            {
                                openCondition.text += "无";
                                continue;
                            }
                            StaticMapVo vo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(staticMapVo.openConditions[i][j]);
                            openCondition.text += ("击败" + vo.name + "的BOSS ");
                            break;
                        case 1:
                            StaticItemVo vo1 = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(staticMapVo.openConditions[i][j]);
                            openCondition.text += ("获得道具" + vo1.name + " ");
                            break;
                    }
                }
            }
            level.text = "推荐等级:Lv." + staticMapVo.level;
            float showProgress = nowVo.progress < staticMapVo.progress ? nowVo.progress : staticMapVo.progress;
            progress.value = showProgress / staticMapVo.progress;
            if (nowVo.clear == true)
            {
                clear.SetActive(true);
            }
            else
            {
                clear.SetActive(false);
            }
        }
    }

}
