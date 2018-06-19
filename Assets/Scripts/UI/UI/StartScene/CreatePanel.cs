using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : BasePanel
{

    public const int TAB1 = 0;
    public const int TAB2 = 1;
    public const int TAB3 = 2;
    public const int TAB4 = 3;
    public const int TAB5 = 4;
    public const int TAB6 = 4;

    public Button btnGo;
    public SelectGroup selectGroup;
    public InputField inputName;
    public Text textCharactorName;
    public Text textHp;
    public Text textMp;
    public Text textSpeed;
    public Text textAttack;
    public Text textMagicAttack;
    public Text textDefance;
    public Text textMagicDefance;
    public Text textMagicRate;
    public Text textCrit;
    public Text textDuck;
    public Text textCritNum;
    private int charactorId = 0;
    private int id;
    private StaticUnitLevelVo unitVo;
    void Start()
    {
        btnGo.onClick.AddListener(delegate () { BtnClick(btnGo); });
        selectGroup.callBack = ChangeSelect;
        ChangeSelect(TAB1);
    }
    public void Create(int id)
    {
        this.id = id;
        
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnGo":
                UIManager.Instance.CreateConfirmPanel("是否用这个角色开始游戏", OnOkClick);
                break;
        }
    }

    private void OnOkClick(object obj)
    {
        RoleVo roleVo = DataManager.Instance.roleVo;
        roleVo.name = inputName.text;
        roleVo.id = id;
        roleVo.level = 1;
        roleVo.money = 0;
        roleVo.exp = 0;
        roleVo.charactor = charactorId;
        roleVo.baseAttackBig = 0;
        roleVo.baseAttackSmall = 0;
        roleVo.baseCrit = 0;
        roleVo.baseCritDamage = 0;
        roleVo.baseDeface = 0;
        roleVo.baseDefanceRate = 0;
        roleVo.baseDuck = 0;
        roleVo.baseHp = 0;
        roleVo.baseMagicAttack = 0;
        roleVo.baseMagicDefance = 0;
        roleVo.baseMp = 0;
        roleVo.baseSpeed = 0;
        roleVo.dropServantRate = 0.05f;
        roleVo.maxEquipNum = 50;
        roleVo.maxItemNum = 50;
        roleVo.maxServantNum = 20;
        roleVo.ifShopOpen = false;
        DataManager.Instance.servantModel.Add(StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(1, 1));
        DataManager.Instance.servantModel.SetNow(0);
        DataManager.Instance.mapModel.Create();
        DataManager.Instance.Save(id);
        UIManager.Instance.Create(Panel_ID.BattlePanel);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.LOAD_MAP, 1);
        DataManager.Instance.currentPlayer = id;
    }
    private void ChangeSelect(int select)
    {
        switch (select)
        {
            case TAB1:
                charactorId = 3;
                break;
            case TAB2:
                charactorId = 2;
                break;
            case TAB3:
                charactorId = 6;
                break;
            case TAB4:
                charactorId = 4;
                break;
            case TAB5:
                charactorId = 5;
                break;
        }
        unitVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(charactorId, 1);
        textCharactorName.text=unitVo.name;
        textHp.text=unitVo.hp.ToString();
        textMp.text = unitVo.mp.ToString();
        textSpeed.text = unitVo.speed.ToString();
        textAttack.text = unitVo.attackSmall.ToString()+"~"+unitVo.attackBig.ToString();
        textMagicAttack.text=unitVo.magicAttack.ToString();
        textDefance.text = unitVo.defence.ToString();
        textMagicDefance.text = unitVo.magicDefance.ToString();
        textMagicRate.text = unitVo.magicRate.ToString();
        textCrit.text = unitVo.critNum.ToString();
        textDuck.text = unitVo.duckNum.ToString();
        textCritNum.text = unitVo.critDamage * 100 + "%";
    }
}
