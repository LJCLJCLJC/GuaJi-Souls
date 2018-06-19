using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Button btnSelect;
    public Image icon;
    public Text textName;
    public Text textDesc;
    public Text textLevel;
    public GameObject select;
    private int nowId;
    private int nowPos;
    private SkillVo vo;
    private CallBackFunctionWithObject callback;

    private void Start()
    {
        btnSelect.onClick.AddListener(delegate() { BtnClick(btnSelect); });
    }
    public void Create(SkillVo skillVo,CallBackFunctionWithObject clickCallback)
    {
        StaticSkillVo staticSkillVo = StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(skillVo.id);
        vo = skillVo;
        icon.sprite = ResourceManager.Instance.GetSkillIcon(staticSkillVo.icon);
        textName.text = staticSkillVo.skillname;
        textDesc.text = staticSkillVo.desc;
        textLevel.text = skillVo.level.ToString();
        callback = clickCallback;
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnSelect":
                callback(vo);
                break;
        }
    }

    public void ChangeSelect(int index,int pos)
    {
        nowId = index;
        nowPos = pos;
        if (index == vo.id)
        {
            select.SetActive(true);
        }
        else
        {
            select.SetActive(false);
        }
    }
}
