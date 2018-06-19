using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCell : MonoBehaviour {

    public int id;
    public Button btnSelect;
    public GameObject goTop;
    private MapVo mapVo;
    private CallBackFunctionWithObject callback;

    private void Start()
    {
        btnSelect.onClick.AddListener(BtnClick);
    }

    public void Create(MapVo mapVo,CallBackFunctionWithObject callback)
    {
        this.mapVo = mapVo;
        if (mapVo.opened)
        {
            goTop.SetActive(true);
        }
        else
        {
            goTop.SetActive(false);
        }
        if (mapVo.show)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        this.callback = callback;
    }

    private void BtnClick()
    {
        callback(mapVo);
    }
}
