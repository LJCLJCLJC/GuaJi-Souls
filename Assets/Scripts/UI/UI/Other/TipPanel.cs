using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : MonoBehaviour
{
    public Text tips;
    public RectTransform bg;
    public float destroyTime = 1.5f;

    public void Create(string str)
    {
        tips.text = str;
        bg.sizeDelta = new Vector2(tips.fontSize * str.Length + 20, bg.sizeDelta.y);
        TimeLine.GetInstance().AddTimeEvent(DestroyObj, destroyTime, null, gameObject);
    }

    private void DestroyObj(object obj)
    {
        Destroy(gameObject);
    }
}
