using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCell : MonoBehaviour
{
    public Text unitName;
    public Text textHealth;
    public Text textAttack;
    public Text textDefance;
    public Text textNowSpeed;
    public Slider sliderHp;
    public GameObject now;
    public Transform tsBuff;
    private BaseUnit unit;
    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_UNIT_CELL, OnUpdate);
    }
    public void Create(BaseUnit unit)
    {
        this.unit = unit;
        unitName.text = unit.unitName;
        textHealth.text = (int)unit.nowHp + "/" + (int)unit.maxHp;
        textAttack.text = ((int)unit.attackBig).ToString();
        textDefance.text = ((int)unit.defance).ToString();
        textNowSpeed.text = ((int)unit.speed).ToString();
    }
    
    private void OnUpdate(object obj)
    {
        textHealth.text = (int)unit.nowHp + "/" + (int)unit.maxHp;
        sliderHp.value = unit.nowHp / unit.maxHp;
        textAttack.text = ((int)unit.attackBig).ToString();
        textDefance.text = ((int)unit.defance).ToString();
        textNowSpeed.text = ((int)unit.speed).ToString();
        UnitState state = (UnitState)obj;
        if (state == unit.state && BattleController.Instance.pauseRound <= 0)
        {
            now.SetActive(true);
        }
        else if(state != unit.state || BattleController.Instance.pauseRound > 0)
        {
            now.SetActive(false);
        }
        Tools.ClearChildFromParent(tsBuff);
        for(int i = 0; i < unit._buffs.Count; i++)
        {
            GameObject buffObj = Tools.CreateGameObject("UI/BattleScene/BuffCell", tsBuff);
            buffObj.GetComponent<BuffCell>().Create(unit._buffs[i]);
        }
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_UNIT_CELL, OnUpdate);

    }
}
