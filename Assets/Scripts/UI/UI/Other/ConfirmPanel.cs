using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public delegate void CallBackFunctionWithObject(object obj);
public class ConfirmPanel : BasePanel
{
    public Button btnOk;
    public Button btnCancel;
    public Text textTip;

    public CallBackFunctionWithObject callbackOk = null;
    public CallBackFunctionWithObject callbackCancel = null;
    public object okParam = null;
    public object cancelParam = null;
    public string tip;

    private void Start()
    {
        btnOk.onClick.AddListener(delegate () { BtnClick(btnOk); });
        btnCancel.onClick.AddListener(delegate () { BtnClick(btnCancel); });
        textTip.text = tip;
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnOk":
                if (callbackOk != null)
                {
                    callbackOk(okParam);
                }
                UIManager.Instance.Destroy(Panel_ID.ConfirmPanel);
                break;
            case "btnCancel":
                if (callbackCancel != null)
                {
                    callbackCancel(cancelParam);
                }
                UIManager.Instance.Destroy(Panel_ID.ConfirmPanel);
                break;
        }
    }
}
