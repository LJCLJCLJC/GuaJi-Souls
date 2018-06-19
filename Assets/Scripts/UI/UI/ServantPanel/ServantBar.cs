using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServantBar : MonoBehaviour {

    public Text textName;
    public Text desc;
    public Text level;
    public GameObject select;
    public Image icon;
    public Button btnSelect;
    public ServantVo servantVo;
    private CallBackFunctionWithObject callback;

    private void Start()
    {
        btnSelect.onClick.AddListener(BtnClick);
    }

    public void Create(ServantVo servantVo,CallBackFunctionWithObject callback)
    {
        this.servantVo = servantVo;
        this.callback = callback;
        StaticUnitLevelVo staticUnitLevelVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(servantVo.unitId, servantVo.level);
        StaticUnitVo staticUnitVo = StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(servantVo.unitId);
        textName.text = staticUnitVo.name;
        desc.text = staticUnitVo.desc;
        level.text = "Lv." + servantVo.level;
    }

    private void BtnClick()
    {
        callback(servantVo);
    }

    public void ChangeSelect(int index)
    {
        if (servantVo.id == index)
        {
            select.SetActive(true);
        }
        else
        {
            select.SetActive(false);
        }
    }
}
