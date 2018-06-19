using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    None,
    Player,
    Servant,
    Enemy
}

public class BaseUnit
{
    public int id;
    public string unitName;
    public int pos;
    public StaticUnitLevelVo staticUnitVo;
    public string nameStr;
    public float nowHp;
    public float maxHp;
    public float nowMp;
    public float maxMp;
    public float level;
    public float attackBig;
    public float attackSmall;
    public float magicAttack;
    public float defance;
    public float magicDefance;
    public float critNum;
    public float duckNum;
    public float critDamage;
    public float speed;
    public float defanceRate;
    public List<StaticSkillLevelVo> skillList = new List<StaticSkillLevelVo>();
    public float critRate;
    public float duckRate;
    public float magicRate;
    public BaseUnit aim;
    public bool dead = false;
    public UnitState state;
    //public Dictionary<KeyValuePair<StaticBuffVo, BaseUnit>, ArrayList> buff = new Dictionary<KeyValuePair<StaticBuffVo, BaseUnit>, ArrayList>();//(<buff,来源>,剩余回合,是否生效,增加的数值)
    public List<BuffVo> _buffs = new List<BuffVo>();

    public virtual void Create(StaticUnitLevelVo staticUnitVo)
    {
        this.staticUnitVo = staticUnitVo;
        nameStr = staticUnitVo.name;
        nowHp = staticUnitVo.hp;
        maxHp = staticUnitVo.hp;
        nowMp = staticUnitVo.mp;
        maxMp = staticUnitVo.mp;
        level = staticUnitVo.level;
        attackBig = staticUnitVo.attackBig;
        attackSmall = staticUnitVo.attackSmall;
        magicAttack = staticUnitVo.magicAttack;
        defance = staticUnitVo.defence;
        magicDefance = staticUnitVo.magicDefance;
        critNum = staticUnitVo.critNum;
        duckNum = staticUnitVo.duckNum;
        critDamage = staticUnitVo.critDamage;
        speed = staticUnitVo.speed;
        defanceRate = staticUnitVo.defanceRate;
        skillList.Clear();
        _buffs.Clear();
        for (int i = 0; i < staticUnitVo.skillList.Count; i++)
        {
            skillList.Add(StaticDataPool.Instance.staticSkillLevelPool.GetStaticDataVo(staticUnitVo.skillList[i]));
        }
        magicRate = staticUnitVo.magicRate;
        dead = false;
    }
    public virtual void Hurt(float num)
    {
        nowHp -= num;
        if (nowHp <= 0)
        {
            nowHp = 0;
            Die();
        }
    }
    protected virtual void Effect(int type, float value)
    {
        switch (type)
        {
            case 1: nowHp += value; break;
            case 2: nowMp += value; break;
        }
    }
    public virtual void Attack()
    {
        if (Random.Range(0, 1f) <= magicRate && skillList.Count != 0)
        {
            SkillAttack(aim);
        }
        else
        {
            NormalAttack(aim);
        }
        GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_UNIT_CELL, state);
    }
    public virtual void Buff()
    {
        List<BuffVo> removeList = new List<BuffVo>();

        for(int i = 0; i < _buffs.Count; i++)
        {
            BaseUnit inCome = _buffs[i].inCome;
            StaticBuffLevelVo _buff = _buffs[i].staticVo;
            switch (_buff.type)//0：物理伤害1：魔法伤害2：回血3：回蓝4：攻击提升5：法伤提升6：护甲提升7：闪避提升8：暴击提升
            {
                case 0:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buff.effectType == 0)
                        {
                            nowHp -= Random.Range(inCome.attackSmall, inCome.attackBig) * _buff.effectValue * (1 - (defance / (defanceRate * level + defance)));

                        }
                        else if (_buff.effectType == 1)
                        {
                            nowHp -= (Random.Range(inCome.attackSmall, inCome.attackBig) + _buff.effectValue) * (1 - (defance / (defanceRate * level + defance)));
                        }
                        _buffs[i].Round = _buffs[i].Round - 1;

                    }
                    else
                    {
                        removeList.Add(_buffs[i]);
                    }
                    break;
                case 1:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buff.effectType == 0)
                        {
                            nowHp -= inCome.magicAttack * _buff.effectValue * (1 - (defance / (defanceRate * level + defance)));

                        }
                        else if (_buff.effectType == 1)
                        {
                            nowHp -= (inCome.magicAttack + _buff.effectValue) * (1 - (defance / (defanceRate * level + defance)));
                        }
                        _buffs[i].Round = _buffs[i].Round - 1;

                    }
                    else
                    {
                        removeList.Add(_buffs[i]);
                    }
                    break;
                case 2:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buff.effectType == 0)
                        {
                            float addHp = maxHp * _buff.effectValue;
                            if (nowHp + addHp > maxHp)
                            {
                                addHp = maxHp - nowHp;
                            }
                            nowHp += addHp;

                        }
                        else if (_buff.effectType == 1)
                        {
                            float addHp = _buff.effectValue;
                            if (nowHp + addHp > maxHp)
                            {
                                addHp = maxHp - nowHp;
                            }
                            nowHp += addHp;
                        }
                        _buffs[i].Round = _buffs[i].Round - 1;

                    }
                    else
                    {
                        removeList.Add(_buffs[i]);
                    }
                    break;
                case 3:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buff.effectType == 0)
                        {
                            float addMp = maxMp * _buff.effectValue;
                            if (nowHp + addMp > maxMp)
                            {
                                addMp = maxMp - addMp;
                            }
                            nowMp += addMp;

                        }
                        else if (_buff.effectType == 1)
                        {
                            float addMp = _buff.effectValue;
                            if (nowMp + addMp > maxMp)
                            {
                                addMp = maxMp - nowMp;
                            }
                            nowMp += addMp;
                        }
                        _buffs[i].Round = _buffs[i].Round - 1;

                    }
                    else
                    {
                        removeList.Add(_buffs[i]);
                    }
                    break;
                case 4:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buffs[i].ifEffect == false)
                        {
                            if (_buff.effectType == 0)
                            {
                                float addNum = attackSmall * _buff.effectValue;
                                _buffs[i].num=addNum;
                                attackBig += addNum;
                                attackSmall += addNum;
                            }
                            else if (_buff.effectType == 1)
                            {
                                float addNum = _buff.effectValue;
                                _buffs[i].num=addNum;
                                attackBig += addNum;
                                attackSmall += addNum;
                            }
                            _buffs[i].ifEffect = true;
                        }
                    }
                    else
                    {
                        if (_buffs[i].ifEffect == true)
                        {
                            attackBig -= _buffs[i].num;
                            attackSmall -= _buffs[i].num;
                            _buffs[i].ifEffect = false;
                            removeList.Add(_buffs[i]);
                        }
                    }
                    _buffs[i].Round = _buffs[i].Round - 1;
                    break;
                case 5:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buffs[i].ifEffect == false)
                        {
                            if (_buff.effectType == 0)
                            {
                                float addNum = magicAttack * _buff.effectValue;
                                _buffs[i].num=addNum;
                                magicAttack += addNum;
                            }
                            else if (_buff.effectType == 1)
                            {
                                float addNum = _buff.effectValue;
                                _buffs[i].num=addNum;
                                magicAttack += addNum;
                            }
                            _buffs[i].ifEffect = true;
                        }
                    }
                    else
                    {
                        if (_buffs[i].ifEffect == true)
                        {
                            _buffs[i].ifEffect = false;
                            magicAttack -= _buffs[i].num;
                            removeList.Add(_buffs[i]);
                        }
                    }
                    _buffs[i].Round = _buffs[i].Round - 1;
                    break;
                case 6:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buffs[i].ifEffect == false)
                        {
                            if (_buff.effectType == 0)
                            {
                                float addNum = defance * _buff.effectValue;
                                _buffs[i].num=addNum;
                                defance += addNum;
                            }
                            else if (_buff.effectType == 1)
                            {
                                float addNum = _buff.effectValue;
                                _buffs[i].num=addNum;
                                defance += addNum;
                            }
                            _buffs[i].ifEffect = true;
                        }
                    }
                    else
                    {
                        if (_buffs[i].ifEffect == true)
                        {
                            _buffs[i].ifEffect = false;
                            defance -= _buffs[i].num;
                            removeList.Add(_buffs[i]);
                        }
                    }
                    _buffs[i].Round = _buffs[i].Round - 1;
                    break;
                case 7:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buffs[i].ifEffect == false)
                        {
                            if (_buff.effectType == 0)
                            {
                                float addNum = duckNum * _buff.effectValue;
                                _buffs[i].num=addNum;
                                duckNum += addNum;
                            }
                            else if (_buff.effectType == 1)
                            {
                                float addNum = _buff.effectValue;
                                _buffs[i].num=addNum;
                                duckNum += addNum;
                            }
                            _buffs[i].ifEffect = true;
                        }
                    }
                    else
                    {
                        if (_buffs[i].ifEffect == true)
                        {
                            _buffs[i].ifEffect = false;
                            duckNum -= _buffs[i].num;
                            removeList.Add(_buffs[i]);

                        }
                    }
                    _buffs[i].Round = _buffs[i].Round - 1;
                    break;
                case 8:
                    if (_buffs[i].Round > 0)
                    {
                        if (_buffs[i].ifEffect == false)
                        {
                            if (_buff.effectType == 0)
                            {
                                float addNum = critNum * _buff.effectValue;
                                _buffs[i].num=addNum;
                                critNum += addNum;
                            }
                            else if (_buff.effectType == 1)
                            {
                                float addNum = _buff.effectValue;
                                _buffs[i].num=addNum;
                                critNum += addNum;
                            }
                            _buffs[i].ifEffect = true;
                        }
                    }
                    else
                    {
                        if (_buffs[i].ifEffect == true)
                        {
                            _buffs[i].ifEffect = false;
                            critNum -= _buffs[i].num;
                            removeList.Add(_buffs[i]);

                        }
                    }
                    _buffs[i].Round = _buffs[i].Round - 1;
                    break;
            }
        }
        for(int i = 0; i < removeList.Count; i++)
        {
            _buffs.Remove(removeList[i]);
        }

    }
    public virtual void AddBuff(int id, int level, BaseUnit income)
    {
        StaticBuffLevelVo buffVo = StaticDataPool.Instance.staticBuffLevelPool.GetStaticDataVo(id, level);
        KeyValuePair<StaticBuffLevelVo, BaseUnit> _buff = new KeyValuePair<StaticBuffLevelVo, BaseUnit>(buffVo, income);

        BuffVo tempBuff = _buffs.Find(p => (p.staticVo == buffVo && p.inCome == income));
        if (tempBuff == null)
        {
            BuffVo newBuff = new BuffVo();
            newBuff.inCome = income;
            newBuff.staticVo = buffVo;
            newBuff.ifEffect = false;
            newBuff.Round = buffVo.cd;
            _buffs.Add(newBuff);
        }
        else
        {
            tempBuff.Round = tempBuff.staticVo.cd;
        }

        //if (!buff.ContainsKey(_buff))
        //{
        //    ArrayList al = new ArrayList();
        //    al.Add(buffVo.cd);
        //    al.Add(false);
        //    buff.Add(_buff, al);
        //}
        //else
        //{
        //    ArrayList al = new ArrayList();
        //    al.Add(buffVo.cd);
        //    al.Add(buff[_buff][1]);
        //    buff[_buff] = al;
        //}
    }
    protected virtual void NormalAttack(BaseUnit baseUnit)
    {
        float attackNum;
        critRate = 1 / (1 + 10000 / critNum);
        duckRate = 1 / (1 + 10000 / duckNum);

        if (Random.Range(0f, 1f) < duckRate)
        {
            attackNum = 0;
        }
        else
        {
            bool ifCrit = Random.Range(0f, 1f) < critRate;
            attackNum = Random.Range(attackSmall, attackBig) * (ifCrit ? critDamage : 1.0f) * (1 - (baseUnit.defance / (baseUnit.defanceRate * baseUnit.level + baseUnit.defance)));
        }
        baseUnit.Hurt(attackNum);

    }
    protected virtual void SkillAttack(BaseUnit baseUnit)
    {
        StaticSkillLevelVo nowSkill = skillList[Random.Range(0, skillList.Count)];
        if (nowMp >= nowSkill.cost)
        {
            nowMp -= nowSkill.cost;
        }
        float attackNum = 0;
        float atk = 0;
        float def = 0;
        critRate = 1 / (1 + 10000 / critNum);
        duckRate = 1 / (1 + 10000 / duckNum);
        bool ifCrit = Random.Range(0f, 1f) < critRate;
        //对敌人的效果
        if (nowSkill.type == 1)//法术
        {
            atk = magicAttack;
            def = baseUnit.magicDefance;
        }
        else if (nowSkill.type == 2)//物理
        {
            atk = Random.Range(attackSmall, attackBig);
            def = baseUnit.magicDefance;

        }
        else
        {
            attackNum = 0;
        }
        if (Random.Range(0f, 1f) < duckRate)
        {
            attackNum = 0;
        }
        else
        {
            if (nowSkill.targetType == 1)//百分比伤害
            {
                attackNum = atk * Random.Range(nowSkill.targetMin, nowSkill.targetMax) * (ifCrit ? critDamage : 1.0f) * (1 - (def / (baseUnit.defanceRate * baseUnit.level + def)));
            }
            else if (nowSkill.targetType == 2)//数值伤害
            {
                attackNum = (atk + Random.Range(nowSkill.targetMin, nowSkill.targetMax)) * (ifCrit ? critDamage : 1.0f) * (1 - (def / (baseUnit.defanceRate * baseUnit.level + def)));
            }
        }
        baseUnit.Hurt(attackNum);
        //对自己的效果,依据属性（0无效果1伤害2血量3蓝量4攻击小5攻击大6护甲7法攻8法抗9固定数值10暴击11闪避12最大血量)
        switch (nowSkill.selfDepend)
        {
            case 1: Effect(nowSkill.selfType, attackNum * nowSkill.selfValue); break;
            case 2: Effect(nowSkill.selfType, nowHp * nowSkill.selfValue); break;
            case 3: Effect(nowSkill.selfType, nowMp * nowSkill.selfValue); break;
            case 4: Effect(nowSkill.selfType, attackSmall * nowSkill.selfValue); break;
            case 5: Effect(nowSkill.selfType, attackBig * nowSkill.selfValue); break;
            case 6: Effect(nowSkill.selfType, defance * nowSkill.selfValue); break;
            case 7: Effect(nowSkill.selfType, magicAttack * nowSkill.selfValue); break;
            case 8: Effect(nowSkill.selfType, magicDefance * nowSkill.selfValue); break;
            case 9: Effect(nowSkill.selfType, nowSkill.selfValue); break;
            case 10: Effect(nowSkill.selfType, critNum * nowSkill.selfValue); break;
            case 11: Effect(nowSkill.selfType, duckNum * nowSkill.selfValue); break;
            case 12: Effect(nowSkill.selfType, maxHp * nowSkill.selfValue); break;
            default: break;
        }
        if (nowSkill.buffId != 0)
        {
            if (Random.Range(0, 1) < nowSkill.buffRate)
            {
                if (nowSkill.buffTarget == 0)
                {
                    baseUnit.AddBuff(nowSkill.buffId, nowSkill.level, this);
                }
                else if (nowSkill.buffTarget == 1)
                {
                    AddBuff(nowSkill.buffId, nowSkill.level, this);
                }
            }
        }
    }
    protected virtual void Die()
    {
        dead = true;
        GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_UNIT_CELL, UnitState.None);
        Debug.Log("Die");
    }
}
