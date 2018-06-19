using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResourceCallback(TextAsset obj, object param);
public class ResourceManager
{
    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceManager();
            }
            return _instance;
        }
    }

    public object StartLoadResource(string path, ResourceCallback callback = null,object param = null)
    {

        TextAsset prefab = Resources.Load<TextAsset>(path);
        if(callback!=null)
        {
            callback(prefab, param);
        }
        return prefab;
    }

    public Sprite GetSkillIcon(string name)
    {
        Sprite sp = Resources.Load<Sprite>("PicRes/Icon/Skill/"+name);
        return sp;
    }
    public Sprite GetItemIcon(string name)
    {
        Sprite sp = Resources.Load<Sprite>("PicRes/Icon/Item/" + name);
        return sp;
    }
    public Sprite GetEquipIcon(string name)
    {
        Sprite sp = Resources.Load<Sprite>("PicRes/Icon/Equip/" + name);
        return sp;
    }
    public Sprite GetMapImage(string name)
    {
        Sprite sp = Resources.Load<Sprite>("PicRes/Map/" + name);
        return sp;
    }

    public Sprite GetCharactor(string name)
    {
        Sprite sp= Resources.Load<Sprite>("PicRes/Charactor/" + name);
        return sp;
    }
    public Sprite GetCharactorIcon(string name)
    {
        Sprite sp = Resources.Load<Sprite>("PicRes/Icon/Charactor" + name);
        return sp;
    }
}
