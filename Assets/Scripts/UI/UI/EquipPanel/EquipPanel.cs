using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : BasePanel
{
    public SelectGroup selectGroup;
    public ScrollRect scrollRect;
    public Transform tsAffect;
    public Text textRole;
    public Text textName;
    public Text num;
    public EquipCell[] equipCell;
    public Button btnRemove;
    private List<EquipBar> barList = new List<EquipBar>();
    private int nowTab = 0;
    private int nowCell = 0;
    private EquipVo nowCellVo;
    private EquipVo nowVo;
    void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_EQUIP, OnUpdate);
        btnRemove.onClick.AddListener(delegate () { BtnClick(btnRemove); });
        selectGroup.callBack = ChangeTab;
        ChangeTab(1);
        OnUpdate(null);
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_EQUIP, OnUpdate);
    }
    private void OnUpdate(object obj)
    {
        num.text = DataManager.Instance.equipModel._dataList.Count + "/" + DataManager.Instance.roleVo.maxEquipNum;
        for (int i = 0; i < equipCell.Length; i++)
        {
            equipCell[i].Create(null, ChangeSelectCell);
            for (int j = 0; j < DataManager.Instance.equipModel.nowEquip.Count; j++)
            {
                StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(DataManager.Instance.equipModel.nowEquip[j].equipId);
                if (staticEquipVo.part == equipCell[i].pos)
                {
                    equipCell[i].Create(DataManager.Instance.equipModel.nowEquip[j], ChangeSelectCell);
                }
            }
            equipCell[i].ChangeSelect(-1);
        }
        int count = 0;
        Tools.ClearChildFromParent(scrollRect.content);
        barList.Clear();
        for (int i = 0; i < DataManager.Instance.equipModel._dataList.Count; i++)
        {
            StaticEquipLevelVo staticEquipLevelVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(DataManager.Instance.equipModel._dataList[i].equipId, DataManager.Instance.equipModel._dataList[i].level);
            if (nowTab == staticEquipLevelVo.part)
            {
                GameObject barObj = Tools.CreateGameObject("UI/EquipPanel/EquipBar", scrollRect.content);
                EquipBar bar = barObj.GetComponent<EquipBar>();
                barList.Add(bar);
                bar.Create(DataManager.Instance.equipModel._dataList[i], ChangeSelect);
                count++;
            }
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, 150 * count);
        ShowTip(nowVo);
    }

    private void ChangeTab(int index)
    {
        nowTab = index;
        OnUpdate(null);
        scrollRect.content.localPosition = Vector3.zero;

    }
    private void ChangeSelect(object obj)
    {
        EquipVo equipVo = (EquipVo)obj;
        if (nowVo == equipVo)
        {
            bool result = DataManager.Instance.equipModel.SetNow(equipVo);
            if (result == true)
            {
                GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_PLAYER_UNIT, null);
                OnUpdate(null);
            }
            else
            {
                UIManager.Instance.CreateTipPanel("当前职业不能装备");
            }
        }
        nowVo = equipVo;
        ShowTip(nowVo);
        btnRemove.gameObject.SetActive(false);
        for (int i = 0; i < barList.Count; i++)
        {
            barList[i].ChangeSelect(nowVo.id);
        }
    }
    private void ChangeSelectCell(object obj)
    {
        ArrayList list = (ArrayList)obj;
        EquipVo equipVo = (EquipVo)list[0];
        int selectCell = (int)list[1];
        nowCell = selectCell;
        nowCellVo = equipVo;
        ShowTip(equipVo);
        if (equipVo != null)
        {
            btnRemove.gameObject.SetActive(true);
        }
        for (int i = 0; i < equipCell.Length; i++)
        {
            equipCell[i].ChangeSelect(selectCell);
        }
    }
    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnRemove":
                DataManager.Instance.equipModel.RemoveNow(nowCellVo);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_PLAYER_UNIT, null);
                ShowTip(null);
                OnUpdate(null);
                break;
        }
    }
    private void ShowTip(EquipVo vo)
    {
        Tools.ClearChildFromParent(tsAffect);
        if (vo == null) return;
        StaticEquipLevelVo staticEquipLevelVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(vo.equipId, vo.level);
        StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(vo.equipId);
        textName.text=staticEquipLevelVo.equipName+"+" + vo.level;
        switch (staticEquipVo.role)
        {
            case -1:textRole.text = "全职业通用";break;
            case 0: textRole.text = "猎人专用"; break;
            case 1: textRole.text = "战士专用"; break;
            case 2: textRole.text = "法师专用"; break;
            case 3: textRole.text = "菜鸟专用"; break;
        }
        int i = 0;
        foreach(var d in staticEquipLevelVo.effect)
        {
            string str = "";
            switch (d.Key)
            {
                case 1: str = "生命:+"; break;
                case 2: str = "攻击:+"; break;
                case 3: str = "法攻:+"; break;
                case 4: str = "护甲:+"; break;
                case 5: str = "暴击:+"; break;
                case 6: str = "闪避:+"; break;
                case 7: str = "暴伤:+"; break;
                case 8: str = "法力:+"; break;
                case 9: str = "速度:+"; break;
                case 10: str = "法抗:+"; break;
                case 11: str = "格挡率:+"; break;
            }
            str += (int)d.Value;
            GameObject obj = Tools.CreateGameObject("UI/EquipPanel/affectCell", tsAffect, new Vector3(0, -30 * i, 0), Vector3.one);
            obj.GetComponent<Text>().text = str;
            i++;
        }
    }

}
