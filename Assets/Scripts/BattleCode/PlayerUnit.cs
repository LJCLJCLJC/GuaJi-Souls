using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    private List<StaticSkillLevelVo> tempSkillList = new List<StaticSkillLevelVo>();
    public void Create()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_PLAYER_UNIT, UpdateProperty);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_SKILL, OnUpdateSkill);
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_PLAYER_UNIT, UpdateProperty);
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_SKILL, OnUpdateSkill);

        state = UnitState.Player;
        unitName = DataManager.Instance.roleVo.name;
        staticUnitVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(DataManager.Instance.roleVo.charactor, DataManager.Instance.roleVo.level);
        nameStr = DataManager.Instance.roleVo.name;
        nowHp = DataManager.Instance.roleVo.baseHp + staticUnitVo.hp;
        nowMp = DataManager.Instance.roleVo.baseMp + staticUnitVo.mp;
        level = DataManager.Instance.roleVo.level;
        OnUpdateSkill(null);
        UpdateVo(null);
        dead = false;
    }

    private void UpdateProperty(object obj)
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

        maxHp = DataManager.Instance.roleVo.baseHp + staticUnitVo.hp + equipHp;
        maxMp = DataManager.Instance.roleVo.baseMp + staticUnitVo.mp + equipMp;
        attackBig = DataManager.Instance.roleVo.baseAttackBig + staticUnitVo.attackBig + equipAttack;
        attackSmall = DataManager.Instance.roleVo.baseAttackSmall + staticUnitVo.attackSmall + equipAttack;
        magicAttack = DataManager.Instance.roleVo.baseMagicAttack + staticUnitVo.magicAttack + equipMagicAttack;
        defance = DataManager.Instance.roleVo.baseDeface + staticUnitVo.defence + equipDefance;
        magicDefance = DataManager.Instance.roleVo.baseMagicDefance + staticUnitVo.magicDefance + equipMagicDefance;
        critNum = DataManager.Instance.roleVo.baseCrit + staticUnitVo.critNum + equipCritNum;
        duckNum = DataManager.Instance.roleVo.baseDuck + staticUnitVo.duckNum + equipDuckNum;
        critDamage = DataManager.Instance.roleVo.baseCritDamage + staticUnitVo.critDamage + equipCritDamage;
        speed = DataManager.Instance.roleVo.baseSpeed + staticUnitVo.speed + equipSpeed;
        defanceRate = DataManager.Instance.roleVo.baseDefanceRate + staticUnitVo.defanceRate + equipDefanceRate;
        magicRate = DataManager.Instance.roleVo.baseMagicRate + staticUnitVo.magicRate;
    }

    private void UpdateVo(object obj)
    {
        UpdateProperty(null);
        _buffs.Clear();
        skillList.Clear();
        for (int i = 0; i < tempSkillList.Count; i++)
        {
            
            skillList.Add(tempSkillList[i]);
        }

    }
    private void OnUpdateSkill(object obj)
    {
        for (int i = 0; i < DataManager.Instance.skillModel.nowSkill.Count; i++)
        {
            SkillVo skillVo = DataManager.Instance.skillModel.nowSkill[i];
            tempSkillList.Add(StaticDataPool.Instance.staticSkillLevelPool.GetStaticDataVo(skillVo.id, skillVo.level));
        }
    }

    public override void Attack()
    {
        aim = BattleController.Instance.enemy;
        base.Attack();
    }
    protected override void Die()
    {
        base.Die();
        Debug.Log("角色死亡");
        if (BattleController.Instance.servant.dead)
        {
            if (BattleController.Instance.ifBoss)
            {
                UIManager.Instance.CreateTipPanel("你丢失了所有的灵魂");
                DataManager.Instance.roleVo.exp = 0;
            }
            GameRoot.Instance.evt.CallEvent(GameEventDefine.BATTLE_END, false);
            BattleController.Instance.pauseRound = 2;
        }
    }

    public void AddExp(int Exp)
    {
        DataManager.Instance.roleVo.exp += Exp;
        GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);
    }
}
