using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantUnit : BaseUnit
{
    private ServantVo servantVo;
    public void Create()
    {
        servantVo = DataManager.Instance.servantModel.nowServant;
        StaticUnitLevelVo staticUnitVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(servantVo.unitId, servantVo.level);
        Create(staticUnitVo);
        skillList.Clear();
        for (int i = 0; i < servantVo.skills.Count; i++)
        {
            skillList.Add(StaticDataPool.Instance.staticSkillLevelPool.GetStaticDataVo(servantVo.skills[i].id, servantVo.skills[i].level));
        }
        unitName = staticUnitVo.name;
    }
    public override void Attack()
    {
        aim = BattleController.Instance.enemy;
        base.Attack();
    }
    protected override void Die()
    {
        base.Die();
        Debug.Log("仆从死亡");
        if (BattleController.Instance.player.dead)
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
        if (servantVo.level == 150) return;
        servantVo.exp += Exp;
        
    }
    private void LevelUp()
    {
        if (servantVo.exp > staticUnitVo.needExp)
        {
            servantVo.level++;
            servantVo.exp = servantVo.exp - staticUnitVo.needExp;
        }
    }
}
