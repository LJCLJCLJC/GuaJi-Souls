using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }
    public RoleVo roleVo = new RoleVo();
    public ServantModel servantModel = new ServantModel();
    public EquipModel equipModel = new EquipModel();
    public SkillModel skillModel = new SkillModel();
    public ItemModel itemModel = new ItemModel();
    public MapModel mapModel = new MapModel();
    public int currentPlayer = 0;
    private PlayerData playerData = new PlayerData();
    private SettingData settingData = new SettingData();
    

    public void Save(int i)
    {
        playerData.id = i;
        playerData.roleVo = roleVo.Save();
        playerData.servantsModel = servantModel.Save();
        playerData.skillModel = skillModel.Save();
        playerData.equipModel = equipModel.Save();
        playerData.itemModel = itemModel.Save();
        playerData.mapModel = mapModel.Save();
        playerData.baseSkillModel = "";

        string jsonInfo = JsonUtility.ToJson(playerData);

        StreamWriter sw;
        FileInfo t = new FileInfo(Application.persistentDataPath + "//playerData_" + i + ".json");
        if (!t.Exists)
        {
            sw = t.CreateText();
        }
        else
        {
            sw = t.CreateText();

        }
        sw.Write(jsonInfo);
        sw.Close();
        sw.Dispose();
    }

    public void Delete(int i)
    {
        FileInfo t = new FileInfo(Application.persistentDataPath + "//playerData_" + i + ".json");
        if (t.Exists)
        {
            t.Delete();
        }
        else
        {
            return;
        }
    }

    public PlayerData Load(int i)
    {

        PlayerData pl = new PlayerData();
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(Application.persistentDataPath + "//playerData_" + i + ".json");
        }
        catch (Exception e)
        {
            return null;
        }

        string jsonStr = sr.ReadToEnd();
        PlayerData jsonInfo = JsonUtility.FromJson<PlayerData>(jsonStr);
        playerData = jsonInfo;
        currentPlayer = i;
        roleVo.Update(playerData.roleVo);
        servantModel.Load(playerData.servantsModel);
        equipModel.Load(playerData.equipModel);
        skillModel.Load(playerData.skillModel);
        itemModel.Load(playerData.itemModel);
        mapModel.Load(playerData.mapModel);
        sr.Close();
        sr.Dispose();
        return playerData;
    }

    public List<RoleVo> GetAllPlayer()
    {
        List<RoleVo> list = new List<RoleVo>();
        for (int i = 0; i < 3; i++)
        {
            RoleVo tempVo = new RoleVo();

            PlayerData pl = new PlayerData();
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(Application.persistentDataPath + "//playerData_" + i + ".json");
            }
            catch (Exception e)
            {
                continue;
            }

            string jsonStr = sr.ReadToEnd();
            PlayerData jsonInfo = JsonUtility.FromJson<PlayerData>(jsonStr);
            tempVo.Update(jsonInfo.roleVo);
            list.Add(tempVo);
            sr.Close();
            sr.Dispose();
        }
        return list;
    }

    public void SaveSetting(SettingData data)
    {
        string jsonInfo = JsonUtility.ToJson(data);

        StreamWriter sw;
        FileInfo t = new FileInfo(Application.persistentDataPath + "//settingData.json");
        if (!t.Exists)
        {
            sw = t.CreateText();
        }
        else
        {
            sw = t.CreateText();

        }
        sw.Write(jsonInfo);
        sw.Close();
        sw.Dispose();
    }

    public SettingData GetSetting()
    {
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(Application.persistentDataPath + "//settingData.json");
        }
        catch (Exception e)
        {
            return null;
        }


        string jsonStr = sr.ReadToEnd();
        SettingData jsonInfo = JsonUtility.FromJson<SettingData>(jsonStr);

        sr.Close();
        sr.Dispose();
        settingData = jsonInfo;
        return settingData;
    }

    public SettingData GetSettingData()
    {
        return settingData;
    }
}

[Serializable]
public class PlayerData
{
    public int id;
    public string roleVo;
    public string equipModel;
    public string skillModel;
    public string baseSkillModel;
    public string servantsModel;
    public string itemModel;
    public string mapModel;
}
[Serializable]
public class SettingData
{
    public float bgmVolume;
    public float effectVolume;
    public float viewSensitive;
}
