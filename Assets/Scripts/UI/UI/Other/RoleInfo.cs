using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfo : MonoBehaviour {

    public Text exp;

    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.ROLE_INFO, OnUpdate);
        OnUpdate(null);
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.ROLE_INFO, OnUpdate);
    }

    private void OnUpdate(object obj)
    {
        exp.text= DataManager.Instance.roleVo.exp.ToString();
    }
}
