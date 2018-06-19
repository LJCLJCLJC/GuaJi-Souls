using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCell : MonoBehaviour
{
    public Image icon;
    public Text textRound;
    public Button btnSelect;

    private BuffVo vo;

    private void Start()
    {
        btnSelect.onClick.AddListener(BtnClick);
    }
    public void Create(BuffVo vo)
    {
        this.vo = vo;
        StaticBuffVo staticBuffVo = StaticDataPool.Instance.staticBuffPool.GetStaticDataVo(vo.staticVo.buffId);
        icon.sprite = ResourceManager.Instance.GetSkillIcon(staticBuffVo.icon);
        textRound.text = vo.Round.ToString();
    }

    private void BtnClick()
    {

    }

}
