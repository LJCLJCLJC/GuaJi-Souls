using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCell : MonoBehaviour {

    public Image icon;
    public Text itemName;
    public Text desc;
    public Text price;
    public Button btnBuy;
    private StaticShopVo staticShopVo;

    private void Start()
    {
        btnBuy.onClick.AddListener(BtnClick);
    }
    public void Create( StaticShopVo staticShopVo)
    {
        this.staticShopVo = staticShopVo;
        switch (staticShopVo.type)
        {
            case 1:
                StaticItemVo staticItemVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(staticShopVo.itemId);
                icon.sprite = ResourceManager.Instance.GetItemIcon(staticItemVo.icon);
                itemName.text = staticItemVo.name;
                desc.text = staticItemVo.desc;
                break;
            case 2:
                StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(staticShopVo.itemId);
                icon.sprite = ResourceManager.Instance.GetEquipIcon(staticEquipVo.icon);
                itemName.text = staticEquipVo.equipName;
                desc.text = staticEquipVo.desc;
                break;
            case 3:
                StaticUnitVo staticUnitVo = StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(staticShopVo.itemId);
                icon.sprite = ResourceManager.Instance.GetCharactorIcon(staticUnitVo.icon);
                itemName.text = staticUnitVo.name;
                desc.text = staticUnitVo.desc;
                break;
        }
        if (staticShopVo.priceType==1)
        {
            price.text = "灵魂:" + staticShopVo.price;
        }
        else if(staticShopVo.priceType == 2)
        {
            price.text = "金币:" + staticShopVo.price;
        }
    }

    private void BtnClick()
    {
        UIManager.Instance.CreateConfirmPanel("确定购买这个商品？", delegate (object obj)
        {
            bool enough = false;
            if (staticShopVo.priceType == 1)
            {
                enough = DataManager.Instance.roleVo.exp >= staticShopVo.price;
            }
            else if (staticShopVo.priceType == 2)
            {
                enough = DataManager.Instance.roleVo.exp >= staticShopVo.price;
            }
            if (enough)
            {
                switch (staticShopVo.type)
                {
                    case 1:
                        StaticItemVo staticItemVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(staticShopVo.itemId);
                        DataManager.Instance.itemModel.Add(staticItemVo);
                        UIManager.Instance.CreateTipPanel("购买了"+ staticItemVo.name);
                        break;
                    case 2:
                        StaticEquipVo staticEquipVo = StaticDataPool.Instance.staticEquipPool.GetStaticDataVo(staticShopVo.itemId);
                        StaticEquipLevelVo staticEquipLevelVo = StaticDataPool.Instance.staticEquipLevelPool.GetStaticDataVo(staticEquipVo.equipId, 1);
                        DataManager.Instance.equipModel.Add(staticEquipLevelVo);
                        UIManager.Instance.CreateTipPanel("购买了" + staticEquipVo.equipName);
                        break;
                    case 3:
                        StaticUnitVo staticUnitVo = StaticDataPool.Instance.staticUnitPool.GetStaticDataVo(staticShopVo.itemId);
                        StaticUnitLevelVo staticUnitLevelVo = StaticDataPool.Instance.staticUnitLevelPool.GetStaticDataVo(staticUnitVo.unitId, 1);
                        DataManager.Instance.servantModel.Add(staticUnitLevelVo);
                        UIManager.Instance.CreateTipPanel("购买了" + staticUnitVo.name);
                        break;
                }
            }
            else
            {
                if (staticShopVo.priceType == 1)
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
                else if (staticShopVo.priceType == 2)
                {
                    UIManager.Instance.CreateTipPanel("灵魂不足");
                }
            }
        });
    }
}
