using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServantPanel : BasePanel
{
    public ScrollRect scrollRect;
    public Text roleName;
    public Text[] data1;
    public Text[] data2;
    public Image image;
    public Button btnLevelUp;
    public Button btnNowServant;
    public Button btnRemove;

    public Image nowServantIcon;
    public GameObject nowServantSelect;
    public Text nowServantName;
    public Text nowServantLevel;
    public Text num;
    public Transform tsSkill;

    private List<ServantBar> barList = new List<ServantBar>();
    private ServantVo nowVo;
    private int nowId;
    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_SERVANT, OnUpdate);
        btnLevelUp.onClick.AddListener(delegate () { BtnClick(btnLevelUp); });
        btnNowServant.onClick.AddListener(delegate () { BtnClick(btnNowServant); });
        btnRemove.onClick.AddListener(delegate () { BtnClick(btnRemove); });
        nowVo = DataManager.Instance.servantModel.nowServant;
        nowId = nowVo.id;
        OnUpdate(null);
    }

    private void OnUpdate(object obj)
    {
        num.text = DataManager.Instance.servantModel._dataList.Count + "/" + DataManager.Instance.roleVo.maxServantNum;
        Tools.ClearChildFromParent(scrollRect.content);
        barList.Clear();
        for (int i = 0; i < DataManager.Instance.servantModel._dataList.Count; i++)
        {
            GameObject barObj = Tools.CreateGameObject("UI/ServantPanel/ServantBar", scrollRect.content);
            ServantBar bar = barObj.GetComponent<ServantBar>();
            barList.Add(bar);
            bar.Create(DataManager.Instance.servantModel._dataList[i], ChangeSelect);
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, 150 * DataManager.Instance.servantModel._dataList.Count);
        UpdateInfo(nowVo);
        ChangeSelect(nowVo);
        UpdateNow();

    }

    private void UpdateNow()
    {
        StaticUnitVo nowStaticVo = StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(DataManager.Instance.servantModel.nowServant.unitId);
        nowServantIcon.sprite = ResourceManager.Instance.GetCharactorIcon(nowStaticVo.sprite);
        nowServantName.text = nowStaticVo.name;
        nowServantLevel.text = "Lv." + DataManager.Instance.servantModel.nowServant.level;

    }

    private void ChangeSelect(object obj)
    {
        ServantVo vo = (ServantVo)obj;
        if (nowId == vo.id)
        {
            DataManager.Instance.servantModel.SetNow(nowVo.id);
            UpdateNow();
        }
        nowId = vo.id;
        nowVo = vo;
        for (int i = 0; i < barList.Count; i++)
        {
            barList[i].ChangeSelect(nowId);
        }
        UpdateInfo(nowVo);
        if (DataManager.Instance.servantModel.nowServant != nowVo)
        {
            nowServantSelect.SetActive(false);
            btnRemove.gameObject.SetActive(true);
        }
        else
        {
            btnRemove.gameObject.SetActive(false);
            nowServantSelect.SetActive(true);
        }
    }

    private void UpdateInfo(ServantVo vo)
    {
        StaticUnitVo staticUnitVo = StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(nowVo.unitId);
        image.sprite = ResourceManager.Instance.GetCharactor(staticUnitVo.sprite);
        StaticUnitLevelVo nowLevelVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(nowVo.unitId, nowVo.level);
        int nextLevel;
        if (nowLevelVo.level == 150)
        {
            nextLevel = 150;
            btnLevelUp.gameObject.SetActive(false);
        }
        else
        {
            nextLevel = nowLevelVo.level + 1;
        }
        {
            StaticUnitLevelVo nextVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(nowLevelVo.unitId, nextLevel);
            roleName.text = nowLevelVo.name;
            data1[0].text = nowLevelVo.level.ToString();
            data1[1].text = nextVo.needExp.ToString();
            data1[2].text = ((int)(nowLevelVo.hp)).ToString();
            data1[3].text = ((int)(nowLevelVo.mp)).ToString();
            data1[4].text = ((int)(nowLevelVo.attackSmall)) + "~" + ((int)(nowLevelVo.attackBig));
            data1[5].text = ((int)(nowLevelVo.magicAttack)).ToString();
            data1[6].text = ((int)(nowLevelVo.defence)).ToString();
            data1[12].text = ((int)(nowLevelVo.magicDefance)).ToString();
            data1[7].text = ((int)(nowLevelVo.critNum)).ToString();
            data1[8].text = ((int)(nowLevelVo.duckNum)).ToString();
            data1[9].text = (nowLevelVo.critDamage) * 100 + "%";
            data1[10].text = ((int)(nowLevelVo.speed)).ToString();
            data1[11].text = (nowLevelVo.magicRate) * 100 + "%";
            data1[13].text = ((int)(nowLevelVo.defanceRate)).ToString();

            data2[0].text = nextLevel.ToString();
            data2[1].text = "";
            data2[2].text = ((int)(nextVo.hp)).ToString();
            data2[3].text = ((int)(nextVo.mp)).ToString();
            data2[4].text = ((int)(nextVo.attackSmall)) + "~" + ((int)(nowLevelVo.attackBig));
            data2[5].text = ((int)(nextVo.magicAttack)).ToString();
            data2[6].text = ((int)(nextVo.defence)).ToString();
            data2[12].text = ((int)(nextVo.magicDefance)).ToString();
            data2[7].text = ((int)(nextVo.critNum)).ToString();
            data2[8].text = ((int)(nextVo.duckNum)).ToString();
            data2[9].text = (nextVo.critDamage) * 100 + "%";
            data2[10].text = ((int)(nextVo.speed)).ToString();
            data2[11].text = (nextVo.magicRate) * 100 + "%";
            data2[13].text = ((int)(nextVo.defanceRate)).ToString();
        }
        Tools.ClearChildFromParent(tsSkill);
        for(int i = 0; i < vo.skills.Count; i++)
        {
            GameObject obj = Tools.CreateGameObject("UI/ServantPanel/ServantSkillCell", tsSkill, new Vector3(-840f + 130 * i, -490f, 0), Vector3.one);
            obj.GetComponent<ServantSkillCell>().Create(vo.skills[i]);
        }
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnLevelUp":
                bool result = DataManager.Instance.servantModel.LevelUp(nowVo);
                if (result == false)
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
                else
                {
                    OnUpdate(null);
                }
                break;
            case "btnNowServant":
                nowVo = DataManager.Instance.servantModel.nowServant;
                nowId = nowVo.unitId;
                nowServantSelect.SetActive(true);
                ChangeSelect(nowVo);
                break;
            case "btnRemove":
                UIManager.Instance.CreateConfirmPanel("真的要驱逐" + StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(nowVo.unitId).name + "吗？", OnRemoveConfirm);
                break;
        }
    }

    private void OnRemoveConfirm(object obj)
    {

        DataManager.Instance.servantModel.Remove(nowVo);
        StaticUnitVo staticUnitVo = StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(nowVo.unitId);
        UIManager.Instance.CreateTipPanel("你驱逐了" + staticUnitVo.name);
        nowVo = DataManager.Instance.servantModel.nowServant;
        nowId = nowVo.id;
        OnUpdate(null);
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_SERVANT, OnUpdate);
    }
}
