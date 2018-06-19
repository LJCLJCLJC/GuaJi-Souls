using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    public int pos;
    public Image icon;
    public Button btnSelect;
    public GameObject select;
    private SkillVo vo = new SkillVo();
    private CallBackFunctionWithObject callback;
    private int nowPos;
    private void Start()
    {
        btnSelect.onClick.AddListener(BtnClick);
    }
    public void Create(SkillVo vo, CallBackFunctionWithObject callback)
    {
        this.vo = vo;
        this.callback = callback;
        if (vo != null)
        {
            icon.sprite = ResourceManager.Instance.GetSkillIcon(StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(vo.id).icon);
        }
        else
        {
            icon.sprite = null;
        }
        
        ChangeSelect(nowPos);
    }
    private void BtnClick()
    {
        if (callback != null)
        {
            ArrayList list = new ArrayList();
            list.Add(vo);
            list.Add(pos);
            callback(list);
        }
    }

    public void ChangeSelect(int index)
    {
        nowPos = index;
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
