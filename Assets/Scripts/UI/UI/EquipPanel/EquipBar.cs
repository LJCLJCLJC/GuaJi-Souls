using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipBar : MonoBehaviour
{
    public Button btnSelect;
    public Button btnSell;
    public Button btnLevelUp;
    public Image icon;
    public Text textName;
    public Text textDesc;
    public Text textLevel;
    public Text textRole;
    public GameObject select;
    private int equipId;
    private EquipVo equipVo;
    private int id;
    private CallBackFunctionWithObject callback;

    private void Start()
    {
        btnSelect.onClick.AddListener(delegate() { BtnClick(btnSelect); });
        btnSell.onClick.AddListener(delegate () { BtnClick(btnSell); });
        btnLevelUp.onClick.AddListener(delegate () { BtnClick(btnLevelUp); });
        select.SetActive(false);
    }
    public void Create(EquipVo equipVo,CallBackFunctionWithObject clickCallback)
    {
        StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(equipVo.equipId);
        icon.sprite = ResourceManager.Instance.GetEquipIcon(staticEquipVo.icon);
        textName.text = staticEquipVo.equipName;
        textDesc.text = staticEquipVo.desc;
        textLevel.text = equipVo.level.ToString();
        switch (staticEquipVo.role)
        {
            case -1: textRole.text = "全职业"; break;
            case 0: textRole.text = "猎人"; break;
            case 1: textRole.text = "战士"; break;
            case 2: textRole.text = "法师"; break;
            case 3: textRole.text = "菜鸟"; break;
        }
        this.equipVo = equipVo;
        id = equipVo.id;
        callback = clickCallback;
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnSelect":
                callback(equipVo);
                break;
            case "btnSell":
                DataManager.Instance.equipModel.Sell(equipVo);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_EQUIP, null);
                break;
            case "btnLevelUp":
                int resultLevelUp = DataManager.Instance.equipModel.LevelUp(equipVo);
                if (resultLevelUp == -1)
                {
                    UIManager.Instance.CreateTipPanel("装备已经满级");
                }
                else if (resultLevelUp == 0)
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
                else if (resultLevelUp == 1)
                {
                    UIManager.Instance.CreateTipPanel("强化失败");
                }
                else if (resultLevelUp == 2)
                {
                    UIManager.Instance.CreateTipPanel("强化成功");
                    GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_EQUIP, null);
                }
                break;
        }
    }

    public void ChangeSelect(int index)
    {
        if (index == id)
        {
            select.SetActive(true);
        }
        else
        {
            select.SetActive(false);
        }
    }
}
