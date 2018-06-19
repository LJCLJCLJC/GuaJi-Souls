using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGroup : MonoBehaviour {

    public SelectBtn[] selectBtns;
    public int nowSelect = 0;
    public CallBackFunctionWithInt callBack;

    void Start ()
    {
		foreach(SelectBtn select in selectBtns)
        {
            select.GetComponent<Button>().onClick.AddListener(delegate () { BtnClick(select); });
        }
        ChangeSelect();


    }
	private void BtnClick(SelectBtn btn)
    {
        if (nowSelect != btn.index)
        {
            nowSelect = btn.index;
            ChangeSelect();
            if (callBack != null)
            {
                callBack(nowSelect);
            }
        }
    }
    private void ChangeSelect()
    {
        for(int i = 0; i < selectBtns.Length; i++)
        {
            if(nowSelect== selectBtns[i].index)
            {
                selectBtns[i].ChangeSelect(true);
            }
            else
            {
                selectBtns[i].ChangeSelect(false);
            }
        }
    }

}
