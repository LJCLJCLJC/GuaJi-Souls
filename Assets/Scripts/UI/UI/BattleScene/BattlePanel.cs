using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    public Button btnBattleStart;
    public Button btnMap;
    public Button btnSkill;
    public Button btnEquip;
    public Button btnItem;
    public Button btnChallenge;
    public Text nowMap;
    public Slider progress;
    public UnitCell playerCell;
    public UnitCell servantCell;
    public UnitCell enemyCell;

    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.BATTLE_START, OnUpdate);
        GameRoot.Instance.evt.AddListener(GameEventDefine.BATTLE_END, OnUpdateCell);
        btnBattleStart.onClick.AddListener(delegate () { BtnClick(btnBattleStart); });
        btnMap.onClick.AddListener(delegate () { BtnClick(btnMap); });
        btnSkill.onClick.AddListener(delegate () { BtnClick(btnSkill); });
        btnEquip.onClick.AddListener(delegate () { BtnClick(btnEquip); });
        btnItem.onClick.AddListener(delegate () { BtnClick(btnItem); });
        btnChallenge.onClick.AddListener(delegate () { BtnClick(btnChallenge); });
        OnUpdate(null);
    }

    private void OnUpdate(object obj)
    {
        OnUpdateCell(null);
        playerCell.Create(BattleController.Instance.player);
        servantCell.Create(BattleController.Instance.servant);
        enemyCell.Create(BattleController.Instance.enemy);

    }

    private void OnUpdateCell(object obj)
    {
        StaticMapVo staticMapVo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(DataManager.Instance.mapModel.nowMap);
        MapVo nowMapVo = DataManager.Instance.mapModel.GetMapVo(DataManager.Instance.mapModel.nowMap);
        nowMap.text = staticMapVo.name;
        float showProgress = nowMapVo.progress < staticMapVo.progress ? nowMapVo.progress : staticMapVo.progress;
        progress.value = (showProgress / staticMapVo.progress);
        if (nowMapVo.progress >= staticMapVo.progress)
        {
            btnChallenge.gameObject.SetActive(true);
        }
        else
        {
            btnChallenge.gameObject.SetActive(false);
        }
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnBattleStart":
                GameRoot.Instance.evt.CallEvent(GameEventDefine.BATTLE_START, false);
                break;
            case "btnMap":
                UIManager.Instance.Create(Panel_ID.MapPanel);
                UIManager.Instance.Destroy(Panel_ID.SkillPanel);
                UIManager.Instance.Destroy(Panel_ID.EquipPanel);
                UIManager.Instance.Destroy(Panel_ID.ItemPanel);
                break;
            case "btnSkill":
                UIManager.Instance.Create(Panel_ID.SkillPanel);
                UIManager.Instance.Destroy(Panel_ID.MapPanel);
                UIManager.Instance.Destroy(Panel_ID.EquipPanel);
                UIManager.Instance.Destroy(Panel_ID.ItemPanel);
                break;
            case "btnEquip":
                UIManager.Instance.Create(Panel_ID.EquipPanel);
                UIManager.Instance.Destroy(Panel_ID.MapPanel);
                UIManager.Instance.Destroy(Panel_ID.SkillPanel);
                UIManager.Instance.Destroy(Panel_ID.ItemPanel);
                break;
            case "btnItem":
                UIManager.Instance.Create(Panel_ID.ItemPanel);
                UIManager.Instance.Destroy(Panel_ID.MapPanel);
                UIManager.Instance.Destroy(Panel_ID.SkillPanel);
                UIManager.Instance.Destroy(Panel_ID.EquipPanel);
                break;
            case "btnChallenge":
                UIManager.Instance.CreateConfirmPanel("注意！！挑战BOSS如果战败会失去所有灵魂！！", delegate (object obj)
                {
                    GameRoot.Instance.evt.CallEvent(GameEventDefine.BATTLE_START, true);
                });
                break;
        }
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BATTLE_START, OnUpdate);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BATTLE_END, OnUpdateCell);
    }
}
