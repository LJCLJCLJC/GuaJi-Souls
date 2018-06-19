using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleVo
{
    //base系列属性是加成属性，后期通过道具永久加成。主属性根据角色的职业等级读表。
    public int id;
    public string name;
    public int level;
    public int charactor;
    public int exp;
    public int money;
    public float baseHp;
    public float baseMp;
    public float baseAttackSmall;
    public float baseAttackBig;
    public float baseMagicAttack;
    public float baseDeface;
    public float baseMagicDefance;
    public float baseSpeed;
    public float baseCrit;
    public float baseDuck;
    public float baseCritDamage;
    public float baseDefanceRate;
    public float baseMagicRate;
    public float dropServantRate;
    public int maxEquipNum;
    public int maxItemNum;
    public int maxServantNum;
    public bool ifShopOpen;

    public void Update(string str)
    {
        string[] arr = str.Split('|');
        id = int.Parse(arr[0]);
        charactor = int.Parse(arr[1]);
        level = int.Parse(arr[2]);
        exp = int.Parse(arr[3]);
        money = int.Parse(arr[4]);
        baseHp = float.Parse(arr[5]);
        baseMp = float.Parse(arr[6]);
        baseAttackSmall = float.Parse(arr[7]);
        baseAttackBig = float.Parse(arr[8]);
        baseMagicAttack = float.Parse(arr[9]);
        baseDeface = float.Parse(arr[10]);
        baseMagicDefance = float.Parse(arr[11]);
        baseSpeed = float.Parse(arr[12]);
        baseCrit = float.Parse(arr[13]);
        baseDuck = float.Parse(arr[14]);
        baseCritDamage = float.Parse(arr[15]);
        baseDefanceRate = float.Parse(arr[16]);
        baseMagicRate = float.Parse(arr[17]);
        name = arr[18];
        dropServantRate = float.Parse(arr[19]);
        maxItemNum = int.Parse(arr[20]);
        maxEquipNum = int.Parse(arr[21]);
        maxServantNum = int.Parse(arr[22]);
        ifShopOpen = bool.Parse(arr[23]);

    }
    public string Save()
    {
        string str = id + "|" + charactor + "|" + level + "|" + exp + "|" + money + "|" + baseHp + "|" + baseMp + "|" + baseAttackSmall + "|" +
            baseAttackBig + "|" + baseMagicAttack + "|" + baseDeface + "|" + baseMagicDefance + "|" + baseSpeed + "|" + baseCrit + "|" + baseDuck
            + "|" + baseCritDamage + "|" + baseDefanceRate + "|" + baseMagicRate + "|" + name + "|" + dropServantRate + "|" + maxItemNum + "|" 
            + maxEquipNum + "|" + maxServantNum + "|" + ifShopOpen;
        return str;
    }

    public bool LevelUp()
    {
        StaticUnitLevelVo staticUnitVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(charactor, level + 1);
        if (exp > staticUnitVo.needExp)
        {
            exp = exp - staticUnitVo.needExp;
            level++;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);

            //todo
            return true;
        }
        return false;
    }

}

public class ServantVo
{
    public int id;
    public int unitId;
    public int level;
    public int exp;
    public List<SkillVo> skills = new List<SkillVo>();
    public bool locked;
    public int maxSkillNum;

    public void Update(string str)
    {
        string[] arr = str.Split('#');
        id = int.Parse(arr[0]);
        unitId = int.Parse(arr[1]);
        level = int.Parse(arr[2]);
        string[] skillArr = arr[3].Split('&');
        for (int i = 0; i < skillArr.Length; i++)
        {
            if (skillArr[i] != "")
            {
                string[] skill = skillArr[i].Split('*');
                SkillVo newSkill = new SkillVo();
                newSkill.id = int.Parse(skill[0]);
                newSkill.level = int.Parse(skill[1]);
                skills.Add(newSkill);
            }
        }
        locked = int.Parse(arr[4]) == 1 ? true : false;
        exp = int.Parse(arr[5]);
        maxSkillNum = int.Parse(arr[6]);
    }
    public string Save()
    {
        string skillStr = "";
        for (int i = 0; i < skills.Count; i++)
        {
            skillStr += (skills[i].id + "*" + skills[i].level + "&");
        }
        string lockedStr = (locked == true ? 1 : 0).ToString();
        string str = id + "#" + unitId + "#" + level + "#" + skillStr + "#" + lockedStr + "#" + exp + "#" + maxSkillNum;
        return str;
    }
}

public class EquipVo
{
    public int id;
    public int equipId;
    public int level;
    public bool locked;

    public void Update(string str)
    {
        string[] arr = str.Split('#');
        id = int.Parse(arr[0]);
        equipId = int.Parse(arr[1]);
        level = int.Parse(arr[2]);
        locked = int.Parse(arr[3]) == 1 ? true : false;
    }
    public string Save()
    {
        int lockedStr = (locked == true) ? 1 : 0;
        string str = id + "#" + equipId + "#" + level + "#" + lockedStr;
        return str;
    }

}

public class SkillVo
{
    public int id;
    public int level;
    public float exp;

    public void Update(string str)
    {
        string[] arr = str.Split('#');
        id = int.Parse(arr[0]);
        level = int.Parse(arr[1]);
        exp = float.Parse(arr[2]);
    }

    public string Save()
    {
        string str = id + "#" + level + "#" + exp;
        return str;
    }
    public int LevelUp()
    {
        //0 满级 1 经验不足 2 成功
        if (level == 10) return 0;
        else
        {
            StaticSkillLevelVo staticVo = StaticDataPool.Instance.staticSkillLevelPool.GetStaticDataVo(id, level);
            if (DataManager.Instance.roleVo.exp >= staticVo.needExp)
            {
                DataManager.Instance.roleVo.exp -= staticVo.needExp;
                GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);
                level += 1;
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }

}

public class ItemVo
{
    public int id;
    public int num;

    public void Update(string str)
    {
        string[] arr = str.Split('#');
        id = int.Parse(arr[0]);
        num = int.Parse(arr[1]);
    }
    public string Save()
    {
        string str = id + "#" + num;
        return str;
    }
}

public class MapVo
{
    public int id;
    public bool opened;
    public bool show;
    public bool clear;
    public int progress;

    public void Update(string str)
    {
        string[] arr = str.Split('#');
        id = int.Parse(arr[0]);
        opened = bool.Parse(arr[1]);
        clear = bool.Parse(arr[2]);
        progress = int.Parse(arr[3]);
        show = bool.Parse(arr[4]);
    }
    public string Save()
    {
        string str = id + "#" + opened + "#" + clear + "#" + progress + "#" + show;
        return str;
    }

}

public class BuffVo
{
    public StaticBuffLevelVo staticVo;
    public BaseUnit inCome;
    public int Round;
    public bool ifEffect;
    public float num;
}
