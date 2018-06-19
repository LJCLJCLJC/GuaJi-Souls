using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button btnStart;
	void Start ()
    {
        btnStart.onClick.AddListener(delegate () { BtnClick(btnStart); });
	}
    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnStart":
                UIManager.Instance.Create(Panel_ID.ChoosePanel);
                break;
        }
    }

}
