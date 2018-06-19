using System.Collections.Generic;

public class StaticUnitVo
{
    public int unitId;
    public string name;
    public string desc;
    public string icon;
    public string sprite;
    public int type;

    public StaticUnitVo(string[] arr)
    {
        unitId = int.Parse(arr[0]);
        name = arr[1];
        desc = arr[2];
        icon = arr[3];
        sprite = arr[4];
        type = int.Parse(arr[5]);
    }
}
public class StaticUnitLevelVo
{
    public int id;
    public int unitId;
    public string name;
    public int level;
    public float hp;
    public float attackSmall;
    public float attackBig;
    public float magicAttack;
    public float defence;
    public float critNum;
    public float duckNum;
    public float critDamage;
    public float mp;
    public float speed;
    public float magicDefance;
    public float defanceRate;
    public float magicRate;
    public List<int> skillList = new List<int>();
    public int needExp;
    public int dropExp;

    public StaticUnitLevelVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        unitId = int.Parse(arr[1]);
        name = arr[2];
        level = int.Parse(arr[3]);
        hp = float.Parse(arr[4]);
        attackSmall = float.Parse(arr[5]);
        attackBig = float.Parse(arr[6]);
        magicAttack = float.Parse(arr[7]);
        defence = float.Parse(arr[8]);
        critNum = float.Parse(arr[9]);
        duckNum = float.Parse(arr[10]);
        critDamage = float.Parse(arr[11]);
        mp = float.Parse(arr[12]);
        speed = float.Parse(arr[13]);
        magicDefance = float.Parse(arr[14]);
        string[] skillArr = arr[15].Split('|');
        for (int i = 0; i < skillArr.Length; i++)
        {
            if (skillArr[i] != "0")
            {
                skillList.Add(int.Parse(skillArr[i]));
            }
        }
        defanceRate = float.Parse(arr[16]);
        magicRate = float.Parse(arr[17]);
        needExp = int.Parse(arr[18]);
        dropExp = int.Parse(arr[19]);
    }
}
public class StaticMapVo
{
    public int id;
    public string name;
    public string desc;
    public List<int> enemyIdList = new List<int>();
    public List<List<int>> dropEquip = new List<List<int>>();
    public List<List<int>> dropItem = new List<List<int>>();
    public List<List<int>> openConditions = new List<List<int>>();
    public int progress;
    public string image;
    public int bossId;
    public int level;

    public StaticMapVo(string[] arr)
    {
        int i;
        id = int.Parse(arr[0]);
        name = arr[1];
        desc = arr[2];
        if (arr[3] != "")
        {
            string[] enemyStr = arr[3].Split('|');
            for (i = 0; i < enemyStr.Length; i++)
            {
                enemyIdList.Add(int.Parse(enemyStr[i]));
            }
        }
        string[] dropEquipStr = arr[4].Split('|');
        for(i = 0; i < dropEquipStr.Length; i++)
        {
            if (dropEquipStr[i] == "") continue;
            List<int> list = new List<int>();
            string[] str = dropEquipStr[i].Split('#');
            list.Add(int.Parse(str[0]));
            list.Add(int.Parse(str[1]));
            list.Add(int.Parse(str[2]));
            dropEquip.Add(list);
        }
        string[] dropItemStr = arr[5].Split('|');
        for (i = 0; i < dropItemStr.Length; i++)
        {
            if (dropItemStr[i] == "") continue;
            List<int> list = new List<int>();
            string[] str = dropItemStr[i].Split('#');
            list.Add(int.Parse(str[0]));
            list.Add(int.Parse(str[1]));
            list.Add(int.Parse(str[2]));
            dropItem.Add(list);
        }
        string[] openStr = arr[6].Split('|');
        for (i = 0; i < openStr.Length; i++)
        {
            if (openStr[i] == "") continue;
            List<int> list = new List<int>();
            string[] str = openStr[i].Split('#');
            for(int j = 0; j < str.Length; j++)
            {
                if (str[j] != "")
                {
                    list.Add(int.Parse(str[j]));
                }
            }
            openConditions.Add(list);
        }
        progress = int.Parse(arr[7]);
        image = arr[8];
        if(arr[9]!="") bossId = int.Parse(arr[9]);
        if (arr[10] != "")level = int.Parse(arr[10]);
    }
}
public class StaticSkillVo
{
    public int skillId;
    public string skillname;
    public string desc;
    public string icon;
    public int type;

    public StaticSkillVo(string[] arr)
    {
        skillId = int.Parse(arr[0]);
        skillname = arr[1];
        desc = arr[2];
        icon = arr[3];
        type = int.Parse(arr[4]);
    }
}
public class StaticSkillLevelVo
{
    public int id;
    public string skillName;
    public int skillId;
    public int level;
    public int type;
    public int targetType;
    public float targetMax;
    public float targetMin;
    public int selfType;
    public int selfDepend;
    public float selfValue;
    public int cd;
    public float cost;
    public int buffTarget;
    public int buffId;
    public float buffRate;
    public int needExp;

    public StaticSkillLevelVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        skillName = arr[1];
        skillId = int.Parse(arr[2]);
        level = int.Parse(arr[3]);
        type = int.Parse(arr[4]);
        string[] targetStr = arr[5].Split('|');
        targetType = int.Parse(targetStr[0]);
        targetMax = float.Parse(targetStr[1]);
        targetMin = float.Parse(targetStr[2]);
        string[] selfStr = arr[6].Split('|');
        selfType = int.Parse(selfStr[0]);
        selfDepend = int.Parse(selfStr[1]);
        selfValue = float.Parse(selfStr[2]);
        cd = int.Parse(arr[7]);
        cost = float.Parse(arr[8]);
        string[] buffArr = arr[9].Split('|');
        buffTarget = int.Parse(buffArr[0]);
        buffId = int.Parse(buffArr[1]);
        buffRate = float.Parse(buffArr[2]);
        needExp = int.Parse(arr[10]);
    }
}
public class StaticBuffVo
{
    public int id;
    public string buffName;
    public string desc;
    public string icon;

    public StaticBuffVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        buffName = arr[1];
        desc = arr[2];
        icon = arr[3];
    }
}
public class StaticBuffLevelVo
{
    public int id;
    public string buffName;
    public int buffId;
    public int level;
    public int type;
    public int effectType;
    public float effectValue;
    public int cd;

    public StaticBuffLevelVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        buffName = arr[1];
        buffId = int.Parse(arr[2]);
        level = int.Parse(arr[3]);
        type = int.Parse(arr[4]);
        string[] effectStr = arr[5].Split('|');
        effectType = int.Parse(effectStr[0]);
        effectValue = float.Parse(effectStr[1]);
        cd = int.Parse(arr[6]);
    }
}
public class StaticEquipVo
{
    public int equipId;
    public string equipName;
    public string desc;
    public string icon;
    public int rare;
    public int part;
    public int role;

    public StaticEquipVo(string[] arr)
    {
        equipId = int.Parse(arr[0]);
        equipName = arr[1];
        desc = arr[2];
        icon = arr[3];
        rare = int.Parse(arr[4]);
        part = int.Parse(arr[5]);
        role = int.Parse(arr[6]);
    }
}
public class StaticEquipLevelVo
{
    public int id;
    public string equipName;
    public int equipId;
    public int level;
    public int part;
    public Dictionary<int, float> effect = new Dictionary<int, float>();
    public int upPrice;
    public float upRate;
    public int sellPrice;

    public StaticEquipLevelVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        equipName = arr[1];
        equipId = int.Parse(arr[2]);
        level = int.Parse(arr[3]);
        part = int.Parse(arr[4]);
        string[] effectArr = arr[5].Split('|');
        for(int i = 0; i < effectArr.Length; i++)
        {
            if (effectArr[i] != "")
            {
                string[] str = effectArr[i].Split('#');
                effect.Add(int.Parse(str[0]), float.Parse(str[1]));
            }
        }
        upPrice = int.Parse(arr[6]);
        upRate = float.Parse(arr[7]);
        sellPrice = int.Parse(arr[8]);
    }
}
public class StaticItemVo
{
    public int id;
    public string name;
    public int type;
    public string param1;
    public string param2;
    public int level;
    public int rare;
    public string desc;
    public string icon;
    public int price;
    public StaticItemVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        name = arr[1];
        type = int.Parse(arr[2]);
        param1 = arr[3];
        param2 = arr[4];
        level = int.Parse(arr[5]);
        rare = int.Parse(arr[6]);
        desc = arr[7];
        icon = arr[8];
        price = int.Parse(arr[9]);
    }
}
public class StaticShopVo
{
    public int id;
    public int type;
    public int itemId;
    public int weight;
    public int priceType;
    public int price;

    public StaticShopVo(string[] arr)
    {
        id = int.Parse(arr[0]);
        type = int.Parse(arr[1]);
        itemId = int.Parse(arr[2]);
        weight = int.Parse(arr[3]);
        priceType = int.Parse(arr[4]);
        price = int.Parse(arr[5]);
    }
}

