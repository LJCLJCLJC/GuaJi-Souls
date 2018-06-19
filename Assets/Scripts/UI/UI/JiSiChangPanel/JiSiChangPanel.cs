using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JiSiChangPanel : BasePanel
{
    public Text charactor;
    public Text roleName;
    public Text[] data1;
    public Text[] data2;
    public Button btnLevelUp;
    public Button btnServant;

    void Start()
    {
        btnLevelUp.onClick.AddListener(delegate() { BtnClick(btnLevelUp); });
        btnServant.onClick.AddListener(delegate () { BtnClick(btnServant); } );
        OnUpdate(null);
    }

    private void OnUpdate(object obj)
    {
        float equipHp = 0, equipMp = 0, equipAttack = 0, equipMagicAttack = 0, equipDefance = 0, equipMagicDefance = 0, equipCritNum = 0, equipDuckNum = 0, equipCritDamage = 0, equipSpeed = 0, equipDefanceRate = 0;

        for (int i = 0; i < DataManager.Instance.equipModel.nowEquip.Count; i++)
        {
            StaticEquipLevelVo staticEquipLevelVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(DataManager.Instance.equipModel.nowEquip[i].equipId, DataManager.Instance.equipModel.nowEquip[i].level);
            foreach (var d in staticEquipLevelVo.effect)
            {
                switch (d.Key)
                {
                    case 1: equipHp += d.Value; break;
                    case 2: equipAttack += d.Value; break;
                    case 3: equipMagicAttack += d.Value; break;
                    case 4: equipDefance += d.Value; break;
                    case 5: equipCritNum += d.Value; break;
                    case 6: equipDuckNum += d.Value; break;
                    case 7: equipCritDamage += d.Value; break;
                    case 8: equipMp += d.Value; break;
                    case 9: equipSpeed += d.Value; break;
                    case 10: equipMagicDefance += d.Value; break;
                    case 11: equipDefanceRate += d.Value; break;

                }
            }
        }

        StaticUnitLevelVo nowVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(DataManager.Instance.roleVo.charactor, DataManager.Instance.roleVo.level);
        int nextLevel;
        if (nowVo.level == 150)
        {
            nextLevel = 150;
            btnLevelUp.gameObject.SetActive(false);
        }
        else
        {
            nextLevel = nowVo.level+1;
        }
        StaticUnitLevelVo nextVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(nowVo.unitId, nextLevel);
        charactor.text = nowVo.name;
        roleName.text = DataManager.Instance.roleVo.name;
        data1[0].text = nowVo.level.ToString();
        data1[1].text = nowVo.needExp.ToString();
        data1[2].text = ((int)(DataManager.Instance.roleVo.baseHp + nowVo.hp + equipHp)).ToString();
        data1[3].text = ((int)(DataManager.Instance.roleVo.baseMp + nowVo.mp + equipMp)).ToString();
        data1[4].text = ((int)(DataManager.Instance.roleVo.baseAttackSmall + nowVo.attackSmall + equipAttack)) + "~" + ((int)(DataManager.Instance.roleVo.baseAttackBig + nowVo.attackBig + equipAttack));
        data1[5].text = ((int)(DataManager.Instance.roleVo.baseMagicAttack + nowVo.magicAttack + equipMagicAttack)).ToString();
        data1[6].text = ((int)(DataManager.Instance.roleVo.baseDeface + nowVo.defence + equipDefance)).ToString();
        data1[12].text = ((int)(DataManager.Instance.roleVo.baseMagicDefance + nowVo.magicDefance + equipMagicDefance)).ToString();
        data1[7].text = ((int)(DataManager.Instance.roleVo.baseCrit + nowVo.critNum + equipCritNum)).ToString();
        data1[8].text = ((int)(DataManager.Instance.roleVo.baseDuck + nowVo.duckNum + equipDuckNum)).ToString();
        data1[9].text = (DataManager.Instance.roleVo.baseCritDamage + nowVo.critDamage + equipCritDamage) * 100 + "%";
        data1[10].text = ((int)(DataManager.Instance.roleVo.baseSpeed + nowVo.speed + equipSpeed)).ToString();
        data1[11].text = (DataManager.Instance.roleVo.baseMagicRate + nowVo.magicRate) * 100 + "%";
        data1[13].text = ((int)(DataManager.Instance.roleVo.baseDefanceRate + nowVo.defanceRate + equipDefanceRate)).ToString();

        data2[0].text = nextLevel.ToString();
        data2[1].text = "";
        data2[2].text = ((int)(DataManager.Instance.roleVo.baseHp + nextVo.hp + equipHp)).ToString();
        data2[3].text = ((int)(DataManager.Instance.roleVo.baseMp + nextVo.mp + equipMp)).ToString();
        data2[4].text = ((int)(DataManager.Instance.roleVo.baseAttackSmall + nextVo.attackSmall + equipAttack)) + "~" + ((int)(DataManager.Instance.roleVo.baseAttackBig + nowVo.attackBig + equipAttack));
        data2[5].text = ((int)(DataManager.Instance.roleVo.baseMagicAttack + nextVo.magicAttack + equipMagicAttack)).ToString();
        data2[6].text = ((int)(DataManager.Instance.roleVo.baseDeface + nextVo.defence + equipDefance)).ToString();
        data2[12].text = ((int)(DataManager.Instance.roleVo.baseMagicDefance + nextVo.magicDefance + equipMagicDefance)).ToString();
        data2[7].text = ((int)(DataManager.Instance.roleVo.baseCrit + nextVo.critNum + equipCritNum)).ToString();
        data2[8].text = ((int)(DataManager.Instance.roleVo.baseDuck + nextVo.duckNum + equipDuckNum)).ToString();
        data2[9].text = (DataManager.Instance.roleVo.baseCritDamage + nextVo.critDamage + equipCritDamage) * 100 + "%";
        data2[10].text = ((int)(DataManager.Instance.roleVo.baseSpeed + nextVo.speed + equipSpeed)).ToString();
        data2[11].text = (DataManager.Instance.roleVo.baseMagicRate + nextVo.magicRate) * 100 + "%";
        data2[13].text = ((int)(DataManager.Instance.roleVo.baseDefanceRate + nextVo.defanceRate + equipDefanceRate)).ToString();
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnLevelUp":
                bool result = DataManager.Instance.roleVo.LevelUp();
                if (result == true)
                {
                    OnUpdate(null);
                }
                else
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
                break;
            case "btnServant":
                UIManager.Instance.Create(Panel_ID.ServantPanel);
                break;
        }
    }
}
