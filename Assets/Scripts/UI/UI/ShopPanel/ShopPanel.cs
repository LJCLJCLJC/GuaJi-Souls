using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    public ShopItemCell[] shopItemCell;
    public Button btnReflesh;
    void Start()
    {
        btnReflesh.onClick.AddListener(delegate () { BtnClick(btnReflesh); });
        OnUpdate(null);
    }

    private void OnUpdate(object obj)
    {
        List<StaticShopVo> list = StaticDataPool.Instance.staticShopPool.GetShopList(shopItemCell.Length);
        for (int i = 0; i < list.Count; i++)
        {
            shopItemCell[i].Create(list[i]);
        }
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnReflesh":
                if (DataManager.Instance.roleVo.exp >= 10)
                {
                    DataManager.Instance.roleVo.exp -= 10;
                    OnUpdate(null);
                }
                else
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
                break;
        }
    }

}
