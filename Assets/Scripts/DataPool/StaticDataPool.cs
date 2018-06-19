using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataPool
{
    public StaticUnitPool staticUnitPool;
    public StaticUnitLevelPool staticUnitLevelPool;
    public StaticMapPool staticMapPool;
    public StaticSkillPool staticSkillPool;
    public StaticSkillLevelPool staticSkillLevelPool;
    public StaticBuffPool staticBuffPool;
    public StaticBuffLevelPool staticBuffLevelPool;
    public StaticEquipPool staticEquipPool;
    public StaticEquipLevelPool staticEquipLevelPool;
    public StaticItemPool staticItemPool;
    public StaticShopPool staticShopPool;

    private static StaticDataPool _instance;

    public static StaticDataPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StaticDataPool();
            }
            return _instance;
        }
    }

    public void CreateData()
    {
        staticUnitPool = new StaticUnitPool();
        staticUnitLevelPool = new StaticUnitLevelPool();
        staticMapPool = new StaticMapPool();
        staticSkillPool = new StaticSkillPool();
        staticSkillLevelPool = new StaticSkillLevelPool();
        staticBuffPool = new StaticBuffPool();
        staticBuffLevelPool = new StaticBuffLevelPool();
        staticEquipPool = new StaticEquipPool();
        staticEquipLevelPool = new StaticEquipLevelPool();
        staticItemPool = new StaticItemPool();
        staticShopPool = new StaticShopPool();
        ResourceManager.Instance.StartLoadResource("Data/Unit", LoadAsset, staticUnitPool);
        ResourceManager.Instance.StartLoadResource("Data/UnitLevel", LoadAsset, staticUnitLevelPool);
        ResourceManager.Instance.StartLoadResource("Data/Map", LoadAsset, staticMapPool);
        ResourceManager.Instance.StartLoadResource("Data/Skill", LoadAsset, staticSkillPool);
        ResourceManager.Instance.StartLoadResource("Data/SkillLevel", LoadAsset, staticSkillLevelPool);
        ResourceManager.Instance.StartLoadResource("Data/Buff", LoadAsset, staticBuffPool);
        ResourceManager.Instance.StartLoadResource("Data/BuffLevel", LoadAsset, staticBuffLevelPool);
        ResourceManager.Instance.StartLoadResource("Data/Equip", LoadAsset, staticEquipPool);
        ResourceManager.Instance.StartLoadResource("Data/EquipLevel", LoadAsset, staticEquipLevelPool);
        ResourceManager.Instance.StartLoadResource("Data/Item", LoadAsset, staticItemPool);
        ResourceManager.Instance.StartLoadResource("Data/Shop", LoadAsset, staticShopPool);
    }

    private void LoadAsset(TextAsset obj, object loadPool)
    {
        TextAsset binAsset = obj;
        if (loadPool == staticUnitLevelPool)
            staticUnitLevelPool.AddData(LoadData(binAsset));
        else if (loadPool == staticUnitPool)
            staticUnitPool.AddData(LoadData(binAsset));
        else if (loadPool == staticMapPool)
            staticMapPool.AddData(LoadData(binAsset));
        else if (loadPool == staticSkillPool)
            staticSkillPool.AddData(LoadData(binAsset));
        else if (loadPool == staticSkillLevelPool)
            staticSkillLevelPool.AddData(LoadData(binAsset));
        else if (loadPool == staticBuffPool)
            staticBuffPool.AddData(LoadData(binAsset));
        else if (loadPool == staticBuffLevelPool)
            staticBuffLevelPool.AddData(LoadData(binAsset));
        else if (loadPool == staticEquipPool)
            staticEquipPool.AddData(LoadData(binAsset));
        else if (loadPool == staticEquipLevelPool)
            staticEquipLevelPool.AddData(LoadData(binAsset));
        else if (loadPool == staticItemPool)
            staticItemPool.AddData(LoadData(binAsset));
        else if (loadPool == staticShopPool)
            staticShopPool.AddData(LoadData(binAsset));


    }

    private string[] LoadData(TextAsset binAsset)
    {

        string[] lineArray = binAsset.text.Split("\n"[0]);
        return lineArray;
    }
}
public class StaticUnitPool
{
    private List<StaticUnitVo> _datapool;
    public StaticUnitPool()
    {
        _datapool = new List<StaticUnitVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticUnitVo vo = new StaticUnitVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticUnitVo GetStaticDataVo(int unitId)
    {
        return _datapool.Find(p => p.unitId == unitId);
    }
}

public class StaticUnitLevelPool
{
    private List<StaticUnitLevelVo> _datapool;
    public StaticUnitLevelPool()
    {
        _datapool = new List<StaticUnitLevelVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticUnitLevelVo vo = new StaticUnitLevelVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticUnitLevelVo GetStaticDataVo(int unitId, int level)
    {
        return _datapool.Find(p => p.unitId == unitId && p.level == level);
    }
    public StaticUnitLevelVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}
public class StaticMapPool
{
    public List<StaticMapVo> _datapool;
    public StaticMapPool()
    {
        _datapool = new List<StaticMapVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticMapVo vo = new StaticMapVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticMapVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
    public int Count
    {
        get
        {
            return _datapool.Count;
        }
    }
}
public class StaticSkillPool
{
    private List<StaticSkillVo> _datapool;
    public StaticSkillPool()
    {
        _datapool = new List<StaticSkillVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticSkillVo vo = new StaticSkillVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticSkillVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.skillId == id);
    }


}

public class StaticSkillLevelPool
{
    private List<StaticSkillLevelVo> _datapool;
    public StaticSkillLevelPool()
    {
        _datapool = new List<StaticSkillLevelVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticSkillLevelVo vo = new StaticSkillLevelVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticSkillLevelVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
    public StaticSkillLevelVo GetStaticDataVo(int skillId, int level)
    {
        return _datapool.Find(p => p.skillId == skillId && p.level == level);
    }

}
public class StaticBuffPool
{
    private List<StaticBuffVo> _datapool;
    public StaticBuffPool()
    {
        _datapool = new List<StaticBuffVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticBuffVo vo = new StaticBuffVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticBuffVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}
public class StaticBuffLevelPool
{
    private List<StaticBuffLevelVo> _datapool;
    public StaticBuffLevelPool()
    {
        _datapool = new List<StaticBuffLevelVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticBuffLevelVo vo = new StaticBuffLevelVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticBuffLevelVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
    public StaticBuffLevelVo GetStaticDataVo(int buffId, int level)
    {
        return _datapool.Find(p => p.buffId == buffId && p.level == level);
    }

}
public class StaticEquipPool
{
    private List<StaticEquipVo> _datapool;
    public StaticEquipPool()
    {
        _datapool = new List<StaticEquipVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticEquipVo vo = new StaticEquipVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticEquipVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.equipId == id);
    }
}
public class StaticEquipLevelPool
{
    private List<StaticEquipLevelVo> _datapool;
    public StaticEquipLevelPool()
    {
        _datapool = new List<StaticEquipLevelVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticEquipLevelVo vo = new StaticEquipLevelVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticEquipLevelVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
    public StaticEquipLevelVo GetStaticDataVo(int equipId, int level)
    {
        return _datapool.Find(p => p.equipId == equipId && level == p.level);
    }
}
public class StaticItemPool
{
    private List<StaticItemVo> _datapool;
    public StaticItemPool()
    {
        _datapool = new List<StaticItemVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticItemVo vo = new StaticItemVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticItemVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}
public class StaticShopPool
{
    private List<StaticShopVo> _datapool;
    public StaticShopPool()
    {
        _datapool = new List<StaticShopVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticShopVo vo = new StaticShopVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticShopVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
    public List<StaticShopVo> GetShopList(int num)
    {
        if (num > _datapool.Count) num = _datapool.Count;
        List<StaticShopVo> list = new List<StaticShopVo>();
        int totalWeight = 0;
        Dictionary<StaticShopVo, int> dic = new Dictionary<StaticShopVo, int>();
        for (int i = 0; i < _datapool.Count; i++)
        {
            totalWeight += _datapool[i].weight;
            dic.Add(_datapool[i], totalWeight);
        }
        for(int i = 0; i < num; i++)
        {
            int random = Random.Range(0, totalWeight+1);
            foreach (var d in dic)
            {
                if (random < d.Value)
                {
                    list.Add(d.Key);
                    break;
                }
            }
        }
        return list;
    }
}