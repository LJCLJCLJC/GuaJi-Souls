using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBar : MonoBehaviour
{
    public Image icon;
    public Text textName;
    public Text textDesc;
    public Text textNum;
    public Button btnSell;
    public Button btnUse;
    public GameObject select;
    private int nowItem;
    private CallBackFunctionWithInt callback;
    private ItemVo itemVo;
    private StaticItemVo staticItemVo;

    void Start()
    {
        btnSell.onClick.AddListener(delegate () { BtnClick(btnSell); });
        btnUse.onClick.AddListener(delegate () { BtnClick(btnUse); });
    }

    public void Create(ItemVo itemVo,CallBackFunctionWithInt callback)
    {
        if (itemVo.num <= 0) Destroy(gameObject);
        this.callback = callback;
        this.itemVo = itemVo;
        staticItemVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(itemVo.id);
        textName.text = staticItemVo.name;
        textDesc.text = staticItemVo.desc;
        textNum.text = "X" + itemVo.num;
        icon.sprite = ResourceManager.Instance.GetItemIcon(staticItemVo.icon);

    }

    public void ChangeSelect(int now)
    {
        nowItem = now;
        if (nowItem == itemVo.id)
        {
            select.SetActive(true);
        }
        else
        {
            select.SetActive(false);
        }
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnSell":
                DataManager.Instance.itemModel.Sell(itemVo);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_ITEM, null);
                break;
            case "btnUse":
                if (nowItem == itemVo.id)
                {
                    DataManager.Instance.itemModel.Use(itemVo);
                    GameRoot.Instance.evt.CallEvent(GameEventDefine.UPDATE_ITEM, null);
                }
                callback(itemVo.id);
                break;
        }
    }
}
