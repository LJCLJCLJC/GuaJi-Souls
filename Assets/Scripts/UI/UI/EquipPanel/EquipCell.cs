using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipCell : MonoBehaviour
{
    public int pos;
    public Image icon;
    public GameObject select;
    public Button btnSelect;
    private CallBackFunctionWithObject callback;
    private EquipVo equipVo;

    private void Start()
    {
        btnSelect.onClick.AddListener(BtnClick);
    }

    public void Create(EquipVo equipVo, CallBackFunctionWithObject callback)
    {
        this.equipVo = equipVo;
        if (equipVo != null)
        {
            StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(equipVo.equipId);
            StaticEquipLevelVo staticEquipLevelVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(equipVo.equipId, equipVo.level);
            icon.sprite = ResourceManager.Instance.GetEquipIcon(staticEquipVo.icon);
        }
        else
        {
            icon.sprite = null;
        }
        this.callback = callback;
    }

    private void BtnClick()
    {
        ArrayList list = new ArrayList();
        list.Add(equipVo);
        list.Add(pos);
        callback(list);
    }

    public void ChangeSelect(int index)
    {
        if (index == pos)
        {
            select.SetActive(true);
        }
        else
        {
            select.SetActive(false);
        }
    }
}
