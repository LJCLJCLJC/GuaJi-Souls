using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
    int randomAim;
    bool ifBoss;
    public void Create(StaticUnitLevelVo staticUnitVo, bool ifBoss=false)
    {
        unitName = staticUnitVo.name;
        state = UnitState.Enemy;
        this.ifBoss = ifBoss;
        base.Create(staticUnitVo);

    }
    public override void Attack()
    {
        randomAim = Random.Range(0, 2);
        if (randomAim == 0)
        {
            if (BattleController.Instance.player.dead)
            {
                aim = BattleController.Instance.servant;
            }
            else
            {
                aim = BattleController.Instance.player;
            }
        }
        else
        {
            if (BattleController.Instance.servant.dead)
            {
                aim = BattleController.Instance.player;
            }
            else
            {
                aim = BattleController.Instance.servant;
            }
        }
        base.Attack();
    }
    protected override void Die()
    {
        BattleController.Instance.player.AddExp(staticUnitVo.dropExp);
        BattleController.Instance.servant.AddExp(staticUnitVo.dropExp);
        DataManager.Instance.mapModel.AddProgress();
        float ifDropServant = Random.Range(0f, 1f);
        if (ifDropServant < DataManager.Instance.roleVo.dropServantRate)
        {
            bool result = DataManager.Instance.servantModel.Add(staticUnitVo);
            if (result == true)
            {
                UIManager.Instance.CreateTipPanel(staticUnitVo.name + "觉得你很有前途，成为了你的随从");
            }
        }
        if (ifBoss)
        {
            BattleController.Instance.nowMapVo.clear = true;
            DataManager.Instance.mapModel.AddProgress(true);
        }
        base.Die();
        //Todo
        GameRoot.Instance.evt.CallEvent(GameEventDefine.BATTLE_END, false);
        BattleController.Instance.pauseRound = 1;
        StaticMapVo nowMapVo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(BattleController.Instance.nowMap);
        int randomEquip = Random.Range(0, 10000);
        for(int i = 0; i < nowMapVo.dropEquip.Count; i++)
        {
            if(randomEquip > nowMapVo.dropEquip[i][1]&& randomEquip < nowMapVo.dropEquip[i][2])
            {
                StaticEquipLevelVo newVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(nowMapVo.dropEquip[i][0]);
                DataManager.Instance.equipModel.Add(newVo);
                UIManager.Instance.CreateTipPanel("获得了"+newVo.equipName);
                break;
            }
        }
        int randomItem = Random.Range(0, 10000);
        for (int i = 0; i < nowMapVo.dropItem.Count; i++)
        {
            if (randomItem > nowMapVo.dropItem[i][1] && randomItem < nowMapVo.dropItem[i][2])
            {
                StaticItemVo newVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(nowMapVo.dropItem[i][0]);
                DataManager.Instance.itemModel.Add(newVo);
                UIManager.Instance.CreateTipPanel("获得了" + newVo.name);
                break;
            }
        }
        DataManager.Instance.Save(DataManager.Instance.currentPlayer);
    }
}
