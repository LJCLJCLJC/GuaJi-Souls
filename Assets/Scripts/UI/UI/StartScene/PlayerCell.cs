using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCell : MonoBehaviour {

    public int id;
    public Button btnGo;
    public Button btnCreate;
    public Button btnDelete;
    public Text textName;
    public Text textLevel;
    public Text textCharactor;
    public Text textMoney;
    private void Awake()
    {
        btnGo.gameObject.SetActive(false);
        btnDelete.gameObject.SetActive(false);
        textName.gameObject.SetActive(false);
        textLevel.gameObject.SetActive(false);
        textCharactor.gameObject.SetActive(false);
        textMoney.gameObject.SetActive(false);
        btnGo.onClick.AddListener(delegate () { BtnClick(btnGo); });
        btnCreate.onClick.AddListener(delegate () { BtnClick(btnCreate); });
        btnDelete.onClick.AddListener(delegate () { BtnClick(btnDelete); });
    }
    private void Start()
    {

    }
    public void Create(RoleVo roleVo)
    {
        btnCreate.gameObject.SetActive(false);
        btnDelete.gameObject.SetActive(true);
        btnGo.gameObject.SetActive(true);
        textName.gameObject.SetActive(true);
        textLevel.gameObject.SetActive(true);
        textCharactor.gameObject.SetActive(true);
        textMoney.gameObject.SetActive(true);
        textName.text = roleVo.name;
        textLevel.text = "Lv." +  roleVo.level.ToString();
        textCharactor.text = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(roleVo.charactor, 1).name;
        textMoney.text = roleVo.exp.ToString();
    }
    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnGo":
                DataManager.Instance.Load(id);
                UIManager.Instance.Create(Panel_ID.BattlePanel);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.LOAD_MAP, DataManager.Instance.mapModel.nowMap);
                break;
            case "btnCreate":
                GameObject obj = UIManager.Instance.Create(Panel_ID.CreatePanel);
                obj.GetComponent<CreatePanel>().Create(id);
                break;
            case "btnDelete":
                UIManager.Instance.CreateConfirmPanel("是否删除这个角色", OnOkClick);
                break;
        }
    }
    private void OnOkClick(object obj)
    {
        DataManager.Instance.Delete(id);
        UIManager.Instance.Create(Panel_ID.ChoosePanel);
    }
}
