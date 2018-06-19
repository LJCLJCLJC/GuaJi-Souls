using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantModel
{
    public ServantVo nowServant = new ServantVo();
    public List<ServantVo> _dataList = new List<ServantVo>();

    public void Load(string str)
    {
        string[] arr = str.Split('|');
        string nowArr = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] != "")
            {
                ServantVo vo = new ServantVo();
                vo.Update(arr[i]);
                _dataList.Add(vo);
            }
        }
        for (int i = 0; i < nowArr.Length; i++)
        {
            nowServant = _dataList.Find(p => p.id == int.Parse(arr[0]));
        }

    }
    public string Save()
    {
        string modelStr = "";
        string servantStr = "";
        for (int i = 0; i < _dataList.Count; i++)
        {
            servantStr += (_dataList[i].Save() + "|");
        }
        modelStr += (nowServant.id + "|" + servantStr);
        return modelStr;
    }

    public bool Add(StaticUnitLevelVo vo)
    {
        if (_dataList.Count >= DataManager.Instance.roleVo.maxServantNum) return false;
        ServantVo servantVo = new ServantVo();
        servantVo.unitId = vo.unitId;
        servantVo.level = 1;
        if (_dataList.Count != 0)
        {
            servantVo.id = _dataList[_dataList.Count - 1].id + 1;
        }
        else
        {
            servantVo.id = 0;
        }
        servantVo.exp = 0;
        servantVo.maxSkillNum = Random.Range(vo.skillList.Count, 6);
        for (int i = 0; i < vo.skillList.Count; i++)
        {
            AddSkill(servantVo, StaticDataPool.Instance.staticSkillLevelPool.GetStaticDataVo(vo.skillList[i]));
        }
        _dataList.Add(servantVo);
        return true;
    }
    public void SetNow(int id)
    {
        ServantVo vo = _dataList.Find(p => p.id == id);
        if (vo != null)
        {
            nowServant = vo;
        }
    }
    public bool LevelUp(ServantVo vo)
    {
        StaticUnitLevelVo staticUnitVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(vo.unitId, vo.level + 1);
        if (DataManager.Instance.roleVo.exp > staticUnitVo.needExp)
        {
            DataManager.Instance.roleVo.exp = DataManager.Instance.roleVo.exp - staticUnitVo.needExp;
            vo.level++;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);

            //todo
            return true;
        }
        return false;
    }
    public void AddSkill(ServantVo vo,StaticSkillLevelVo skillVo)
    {
        if (vo.skills.Count < vo.maxSkillNum)
        {
            SkillVo newSkill = new SkillVo();
            newSkill.id = skillVo.skillId;
            newSkill.level = skillVo.level;
            vo.skills.Add(newSkill);
        }
    }
    public bool Remove(ServantVo vo)
    {
        if (nowServant != vo)
        {
            _dataList.Remove(vo);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class EquipModel
{
    public List<EquipVo> nowEquip = new List<EquipVo>();
    public List<EquipVo> _dataList = new List<EquipVo>();

    public void Load(string str)
    {
        string[] arr = str.Split('|');
        string[] nowArr = arr[0].Split('#');
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] != "")
            {
                EquipVo vo = new EquipVo();
                vo.Update(arr[i]);
                _dataList.Add(vo);
            }
        }
        for (int i = 0; i < nowArr.Length; i++)
        {
            if (nowArr[i] != "")
            {
                nowEquip.Add(_dataList.Find(p => p.id == int.Parse(nowArr[i])));
            }
        }
    }
    public string Save()
    {
        string modelStr = "";
        string nowStr = "";
        for (int i = 0; i < nowEquip.Count; i++)
        {
            nowStr += (nowEquip[i].id + "#");
        }
        string skillStr = "";
        for (int i = 0; i < _dataList.Count; i++)
        {
            skillStr += (_dataList[i].Save() + "|");
        }
        modelStr += (nowStr + "|" + skillStr);
        return modelStr;
    }

    public void Add(StaticEquipLevelVo vo)
    {
        if (_dataList.Count >= DataManager.Instance.roleVo.maxEquipNum) return;

        EquipVo equipVo = new EquipVo();
        equipVo.id = _dataList.Count;
        equipVo.equipId = vo.equipId;
        equipVo.level = vo.level;
        equipVo.locked = false;
        _dataList.Add(equipVo);
    }

    public bool SetNow(EquipVo vo)
    {
        StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(vo.equipId);
        if (staticEquipVo.role != -1)
        {
            if (DataManager.Instance.roleVo.charactor != staticEquipVo.role)
            {
                return false;
            }
        }
        int part = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(vo.equipId, vo.level).part;
        EquipVo nowVo = nowEquip.Find(p => part == StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(p.equipId, p.level).part);
        if (nowVo != null)
        {
            nowEquip.Remove(nowVo);
            _dataList.Add(nowVo);
        }
        nowEquip.Add(vo);
        _dataList.Remove(vo);
        return true;
    }

    public void RemoveNow(EquipVo vo)
    {
        nowEquip.Remove(vo);
        _dataList.Add(vo);
    }

    public void Sell(EquipVo vo)
    {
        StaticEquipLevelVo staticVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(vo.equipId, vo.level);
        _dataList.Remove(vo);
        
        DataManager.Instance.roleVo.exp += staticVo.sellPrice;
        GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);
    }

    public int LevelUp(EquipVo vo)
    {
        //-1 已经满级 0 灵魂不足 1 强化失败 2 强化成功
        StaticEquipLevelVo staticVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(vo.equipId, vo.level);
        if (staticVo.level == 10) return -1;
        if (DataManager.Instance.roleVo.exp < staticVo.upPrice)
        {
            return 0;
        }
        else
        {
            DataManager.Instance.roleVo.exp -= staticVo.upPrice;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);
            float random = Random.Range(0, 1);
            if (random <= staticVo.upRate)
            {
                vo.level += 1;
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }

}

public class SkillModel
{
    public Dictionary<int, SkillVo> nowSkill = new Dictionary<int, SkillVo>();
    public List<SkillVo> _dataList = new List<SkillVo>();

    public void Load(string str)
    {
        string[] arr = str.Split('|');
        string[] nowArr = arr[0].Split('#');
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] != "")
            {
                SkillVo vo = new SkillVo();
                vo.Update(arr[i]);
                _dataList.Add(vo);
            }
        }
        for (int i = 0; i < nowArr.Length; i++)
        {
            if (nowArr[i] != "")
            {
                string[] _nowArr = nowArr[i].Split('&');
                nowSkill.Add(int.Parse(_nowArr[0]), _dataList.Find(p => p.id == int.Parse(_nowArr[1])));
            }
        }
    }
    public string Save()
    {
        string modelStr = "";
        string nowSkillStr = "";
        if (nowSkill.Count != 0)
        {
            foreach (var i in nowSkill)
            {
                nowSkillStr += (i.Key + "&" + i.Value.id + "#");
            }
        }
        string skillStr = "";
        for (int i = 0; i < _dataList.Count; i++)
        {
            skillStr += (_dataList[i].Save() + "|");
        }
        modelStr += (nowSkillStr + "|" + skillStr);
        return modelStr;
    }

    public void Add(StaticSkillVo vo)
    {
        if (_dataList.Find(p => p.id == vo.skillId) == null)
        {
            SkillVo skillVo = new SkillVo();
            skillVo.id = vo.skillId;
            skillVo.level = 1;
            skillVo.exp = 0;
            _dataList.Add(skillVo);
        }
    }

    public void SetNow(int pos, SkillVo vo)
    {
        foreach (var d in nowSkill)
        {
            if (d.Value == vo)
            {
                return;
            }
        }

        if (nowSkill.ContainsKey(pos))
        {
            nowSkill[pos] = vo;
        }
        else
        {
            nowSkill.Add(pos, vo);
        }
    }
    public void RemoveNow(int pos)
    {
        nowSkill.Remove(pos);
    }

}

public class ItemModel
{
    public List<ItemVo> _dataList = new List<ItemVo>();
    public void Load(string str)
    {
        string[] arr = str.Split('|');
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != "")
            {
                ItemVo itemVo = new ItemVo();
                itemVo.Update(arr[i]);
                _dataList.Add(itemVo);
            }
        }
    }
    public string Save()
    {
        string modelStr = "";

        for (int i = 0; i < _dataList.Count; i++)
        {
            modelStr += (_dataList[i].Save() + "|");
        }
        return modelStr;
    }
    public void Add(StaticItemVo vo)
    {
        if (_dataList.Count >= DataManager.Instance.roleVo.maxItemNum) return;
        ItemVo itemVo = _dataList.Find(p => p.id == vo.id);
        if (itemVo != null)
        {
            itemVo.num++;
        }
        else
        {
            ItemVo newItem = new ItemVo();
            newItem.id = vo.id;
            newItem.num = 1;
            _dataList.Add(newItem);
        }
    }

    public void Use(ItemVo itemVo)
    {
        StaticItemVo staticVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(itemVo.id);
        switch (staticVo.type)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                if (DataManager.Instance.skillModel._dataList.Find(p => p.id == itemVo.id) == null)
                {
                    DataManager.Instance.skillModel.Add(StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(int.Parse(staticVo.param1)));
                    Remove(itemVo);
                }
                break;
        }
    }

    public void Sell(ItemVo vo)
    {
        StaticItemVo staticVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(vo.id);
        Remove(vo);
        DataManager.Instance.roleVo.exp += staticVo.price;
        GameRoot.Instance.evt.CallEvent(GameEventDefine.ROLE_INFO, null);
    }

    public void Remove(ItemVo vo)
    {
        if (vo.num > 1)
        {
            vo.num--;
        }
        else
        {
            vo.num--;
            _dataList.Remove(vo);
        }
    }
    public ItemVo GetItemVo(int id)
    {
        return _dataList.Find(p => p.id == id);
    }
}

public class MapModel
{
    public int nowMap;
    private List<MapVo> _dataList = new List<MapVo>();

    public void Load(string str)
    {
        string[] arr = str.Split('|');
        nowMap = int.Parse(arr[0]);
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] != "")
            {
                MapVo mapVo = new MapVo();
                mapVo.Update(arr[i]);
                _dataList.Add(mapVo);
            }
        }
    }
    public string Save()
    {
        string modelStr = "";

        modelStr += nowMap + "|";
        for (int i = 0; i < _dataList.Count; i++)
        {
            modelStr += (_dataList[i].Save() + "|");
        }
        return modelStr;
    }
    public void Add(StaticMapVo vo)
    {
        if (_dataList.Find(p => p.id == vo.id) != null) return;
        MapVo mapVo = new MapVo();
        mapVo.id = vo.id;
        mapVo.opened = vo.id == 1 ? true : false;
        mapVo.clear = false;
        mapVo.show = false;
        mapVo.progress = 0;
        _dataList.Add(mapVo);
    }
    public void Create()
    {
        nowMap = 1;
        for (int i = 0; i < StaticDataPool.Instance.staticMapPool._datapool.Count; i++)
        {
            Add(StaticDataPool.Instance.staticMapPool._datapool[i]);
        }
    }
    public MapVo GetMapVo(int id)
    {
        return _dataList.Find(p => p.id == id);
    }
    public void AddProgress(bool reset=false)
    {
        if (!reset)
        {
            _dataList.Find(p => p.id == nowMap).progress++;
        }
        else
        {
            _dataList.Find(p => p.id == nowMap).progress = 0;
        }
    }
    public void CheckOpen()
    {
        for (int i = 0; i < _dataList.Count; i++)
        {
            StaticMapVo staticVo = StaticDataPool.Instance.staticMapPool.GetStaticDataVo(_dataList[i].id);
            if (staticVo.id == 10&_dataList[i].clear == true)
            {
                DataManager.Instance.roleVo.ifShopOpen = true;
            }
            for (int j = 0; j < staticVo.openConditions.Count; j++)
            {
                for (int k = 1; k < staticVo.openConditions[j].Count; k++)
                {
                    switch (staticVo.openConditions[j][0])
                    {
                        case 0:
                            if (staticVo.openConditions[j][k] == 0)
                            {
                                _dataList[i].opened = true;
                                _dataList[i].show = true;
                                continue;
                            }
                            if (GetMapVo(staticVo.openConditions[j][k]).clear == true)
                            {
                                _dataList[i].opened = true;
                                _dataList[i].show = true;
                            }
                            else
                            {
                                if (GetMapVo(staticVo.openConditions[j][k]).opened == true)
                                {
                                    _dataList[i].show = true;
                                }
                                else
                                {
                                    _dataList[i].show = false;
                                }
                                _dataList[i].opened = false;
                            }
                            break;
                        case 1:
                            if (DataManager.Instance.itemModel.GetItemVo(staticVo.openConditions[j][k]) != null)
                            {
                                _dataList[i].opened = true;
                                _dataList[i].show = true;
                            }
                            else
                            {
                                _dataList[i].opened = false;
                                if (GetMapVo(staticVo.openConditions[j][k]).opened == true)
                                {
                                    _dataList[i].show = true;
                                }
                                else
                                {
                                    _dataList[i].show = false;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}