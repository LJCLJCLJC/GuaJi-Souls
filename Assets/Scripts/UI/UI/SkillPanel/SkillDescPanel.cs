using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescPanel : BasePanel
{

    public Image icon;
    public Text desc;
    public Text level;
    public Button btnLevelUp;
    private SkillVo skillVo;
    void Start()
    {
        btnLevelUp.onClick.AddListener(BtnClick);
    }
    public void Create(SkillVo skillVo)
    {
        this.skillVo = skillVo;
        StaticSkillVo staticSkillVo = StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(skillVo.id);
        icon.sprite = ResourceManager.Instance.GetSkillIcon(staticSkillVo.icon);
        desc.text = staticSkillVo.desc;
        level.text = "Lv." + skillVo.level;
    }
    private void BtnClick()
    {
        int result = skillVo.LevelUp();
        if (result == 0)
        {
            UIManager.Instance.CreateTipPanel("技能已满级");
        }
        else if (result == 1)
        {
            UIManager.Instance.CreateTipPanel("经验不足");
        }
        else if (result == 2)
        {
            UIManager.Instance.CreateTipPanel("升级成功");
            GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_SERVANT, null);
        }

    }
}
