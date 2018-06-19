using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : BasePanel
{
    public SelectGroup selectGroup;
    public ScrollRect scrollRect;
    public Text num;
    private List<ItemBar> barList = new List<ItemBar>();
    private int nowItem;
    private int nowTab = 0;

    void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.UPDATE_ITEM, OnUpdate);
        selectGroup.callBack = ChangeTab;
        ChangeTab(nowTab);
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.UPDATE_ITEM, OnUpdate);
    }

    private void OnUpdate(object obj)
    {
        num.text = DataManager.Instance.itemModel._dataList.Count + "/" + DataManager.Instance.roleVo.maxItemNum;
        Tools.ClearChildFromParent(scrollRect.content);
        barList.Clear();
        int count = 0;
        for (int i = 0; i < DataManager.Instance.itemModel._dataList.Count; i++)
        {
            if (StaticDataPool.Instance.staticItemPool.GetStaticDataVo(DataManager.Instance.itemModel._dataList[i].id).type == nowTab)
            {
                GameObject Obj = Tools.CreateGameObject("UI/ItemPanel/ItemBar", scrollRect.content);
                ItemBar bar = Obj.GetComponent<ItemBar>();
                barList.Add(bar);
                bar.Create(DataManager.Instance.itemModel._dataList[i], SelectItem);
                count++;
            }
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, 150 * count);

    }
    private void ChangeTab(int index)
    {
        Tools.ClearChildFromParent(scrollRect.content);
        barList.Clear();
        nowTab = index;
        OnUpdate(null);
        SelectItem(-1);
        scrollRect.content.localPosition = Vector3.zero;
    }
    private void SelectItem(int itemId)
    {
        nowItem = itemId;
        for (int i = 0; i < barList.Count; i++)
        {
            barList[i].ChangeSelect(itemId);
        }
    }
}
