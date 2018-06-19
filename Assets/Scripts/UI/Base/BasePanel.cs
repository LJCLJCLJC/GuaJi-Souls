using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasePanel : MonoBehaviour
{
    public Panel_ID panelID;
    public CanvasGroup canvasGroup;
    public Button btnClose;

    private void Awake()
    {
        if (btnClose != null)
        {
            btnClose.onClick.AddListener(CloseClick);
        }
        canvasGroup.blocksRaycasts = true;
        transform.SetAsLastSibling();
    }

    private void CloseClick()
    {
        UIManager.Instance.Destroy(panelID);
    }
}

