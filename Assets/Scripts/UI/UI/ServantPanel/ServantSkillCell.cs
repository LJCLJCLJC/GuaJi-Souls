using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServantSkillCell : MonoBehaviour
{
    public Image icon;
    public Text level;
    public Button btnSelect;
    private SkillVo skillVo;
    private void Start()
    {
        if (btnSelect != null)
        {
            btnSelect.onClick.AddListener(BtnClick);
        }
    }
    public void Create(SkillVo skillVo)
    {
        this.skillVo = skillVo;
        StaticSkillVo staticSkillVo = StaticDataPool.Instance.staticSkillPool.GetStaticDataVo(skillVo.id);
        icon.sprite = ResourceManager.Instance.GetSkillIcon(staticSkillVo.icon);
        level.text = "Lv." + skillVo.level;
    }
    private void BtnClick()
    {
        GameObject panel = UIManager.Instance.Create(Panel_ID.SkillDescPanel);
        panel.transform.localPosition = transform.localPosition;
        panel.GetComponent<SkillDescPanel>().Create(skillVo);
    }
}
