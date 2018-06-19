using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : BasePanel
{
    private const int GONGFA = 0;
    private const int WUJI = 1;
    private const int FASHU = 2;
    private const int SHENGHUO = 3;
    public SelectGroup selectGroup;
    public ScrollRect scrollRect;
    public SkillCell[] skillCell;
    public SkillCell baseSkillCell;
    public GameObject tsNowSkill;
    public Image nowIcon;
    public Text nowName;
    public Text nowDesc;
    public Text nowLevel;
    public Text needExp;
    public Button btnLevelUp;
    public Button btnRemove;
    private SkillVo nowVo;
    private List<SkillBar> barList = new List<SkillBar>();
    private int nowPos;
    private int nowTab = 0;
    private int nowCellPos;
    void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_SKILL, OnUpdate);
        btnRemove.onClick.AddListener(delegate () { BtnClick(btnRemove); });
        btnLevelUp.onClick.AddListener(delegate () { BtnClick(btnLevelUp); });
        selectGroup.callBack = ChangeTab;
        ChangeTab(0);
        tsNowSkill.SetActive(false);
        OnUpdate(null);
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_SKILL, OnUpdate);
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnLevelUp":
                int result = nowVo.LevelUp();
                if (result == 0)
                {
                    UIManager.Instance.CreateTipPanel("技能已满级");
                }
                else if (result == 1)
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
                else if (result == 2)
                {
                    UIManager.Instance.CreateTipPanel("升级成功");
                    GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_SKILL, null);
                }
                break;
            case "btnRemove":
                DataManager.Instance.skillModel.RemoveNow(nowCellPos);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_SKILL, null);
                break;
        }
    }

    private void OnUpdate(object obj)
    {
        for (int i = 0; i < skillCell.Length; i++)
        {
            if (DataManager.Instance.skillModel.nowSkill.ContainsKey(i))
            {
                skillCell[i].Create(DataManager.Instance.skillModel.nowSkill[i], ChangeCellSelect);
            }
            else
            {
                skillCell[i].Create(null, ChangeCellSelect);
            }
        }
        ChangeTab(nowTab);
        UpdateInfo();
        UpdateInfo();
    }

    private void ChangeTab(int index)
    {
        Tools.ClearChildFromParent(scrollRect.content);
        List<SkillVo> skillList = new List<SkillVo>();
        barList.Clear();
        nowTab = index;
        int count = 0;
        if (index == 1)
        {
            for (int i = 0; i < DataManager.Instance.skillModel._dataList.Count; i++)
            {
                StaticSkillVo staticVo = StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(DataManager.Instance.skillModel._dataList[i].id);
                if (staticVo.type == 1)
                {
                    GameObject obj = Tools.CreateGameObject("UI/SkillPanel/SkillBar", scrollRect.content);
                    SkillBar bar = obj.GetComponent<SkillBar>();
                    barList.Add(bar);
                    bar.Create(DataManager.Instance.skillModel._dataList[i], ChangeSelect);
                    count++;
                }
            }
        }
        else if (index == 2)
        {
            for (int i = 0; i < DataManager.Instance.skillModel._dataList.Count; i++)
            {
                StaticSkillVo staticVo = StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(DataManager.Instance.skillModel._dataList[i].id);
                if (staticVo.type == 2)
                {
                    GameObject obj = Tools.CreateGameObject("UI/SkillPanel/SkillBar", scrollRect.content);
                    SkillBar bar = obj.GetComponent<SkillBar>();
                    barList.Add(bar);
                    bar.Create(DataManager.Instance.skillModel._dataList[i], ChangeSelect);
                    count++;
                }
            }
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, 150 * count);
        scrollRect.content.localPosition = Vector3.zero;
    }

    private void ChangeSelect(object obj)
    {
        tsNowSkill.SetActive(true);
        SkillVo vo = (SkillVo)obj;
        if (nowVo == vo)
        {
            DataManager.Instance.skillModel.SetNow(nowCellPos, vo);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_SKILL, null);
        }
        nowVo = vo;
        UpdateInfo();
        for (int i = 0; i < barList.Count; i++)
        {
            barList[i].ChangeSelect(vo.id, nowPos);
        }
        btnRemove.gameObject.SetActive(false);
    }

    private void ChangeCellSelect(object obj)
    {
        ArrayList list = (ArrayList)obj;
        SkillVo vo = (SkillVo)list[0];
        int nowCell = (int)list[1];

        nowCellPos = nowCell;
        for (int i = 0; i < skillCell.Length; i++)
        {
            skillCell[i].ChangeSelect(nowCell);
        }
        if (vo == null)
        {
            tsNowSkill.SetActive(false);
            return;
        }
        tsNowSkill.SetActive(true);
        nowVo = vo;
        UpdateInfo();
        btnRemove.gameObject.SetActive(true);
    }

    private void UpdateInfo()
    {
        if (nowVo == null) return;
        StaticSkillVo staticSkillVo = StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(nowVo.id);
        StaticSkillLevelVo staticSkillLevelVo = StaticDataPool.Instance.staticSkillLevelPool.GetStaticDataVo(nowVo.id, nowVo.level);
        nowIcon.sprite = ResourceManager.Instance.GetSkillIcon(staticSkillVo.icon);
        nowName.text = staticSkillVo.skillname;
        nowDesc.text = staticSkillVo.desc;
        nowLevel.text = "Lv." + nowVo.level;
        needExp.text = "需要灵魂：" + Tools.ChangeNum(staticSkillLevelVo.needExp);
    }

}
