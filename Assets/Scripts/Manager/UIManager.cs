using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public virtual void StartUIManager()
    {
        Instance = this;
    }
    private void Awake()
    {
        StartUIManager();
        ParsePanelTypeJson();
    }
    public Transform tsCanvas;
    public CanvasScaler canvasScaler;
    protected Dictionary<Panel_ID, string> panelPathDic;
    protected Dictionary<Panel_ID, BasePanel> panelDic;
    protected List<BasePanel> panelList = new List<BasePanel>();
    protected Panel_ID nowId;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        }
    }
    public virtual GameObject Create(Panel_ID panel_ID)
    {
        if (GetPanelById(panel_ID) != null)
        {
            Destroy(panel_ID);
        }
        GameObject obj = Tools.CreateGameObject(panelPathDic[panel_ID], tsCanvas);
        obj.transform.localScale = Vector3.one;
        panelList.Add(obj.GetComponent<BasePanel>());
        return obj;
    }

    public void CreateConfirmPanel(string tip, CallBackFunctionWithObject callbackOk = null, object paramOk = null, CallBackFunctionWithObject callbackCancel = null, object paramCancel = null)
    {

        GameObject obj = Create(Panel_ID.ConfirmPanel);
        obj.GetComponent<ConfirmPanel>().tip = tip;
        obj.GetComponent<ConfirmPanel>().callbackOk = callbackOk;
        obj.GetComponent<ConfirmPanel>().okParam = paramOk;
        obj.GetComponent<ConfirmPanel>().callbackCancel = callbackCancel;
        obj.GetComponent<ConfirmPanel>().cancelParam = paramCancel;
    }

    public void CreateTipPanel(string tip)
    {
        GameObject obj = Tools.CreateGameObject("UI/Other/TipPanel", tsCanvas);
        obj.GetComponent<TipPanel>().Create(tip);
    }

    public virtual void Destroy(Panel_ID panel_ID)
    {
        BasePanel panel = GetPanelById(panel_ID);
        if (panel != null)
        {
            panelList.Remove(panel);
            Destroy(panel.gameObject);
        }
    }

    public virtual BasePanel GetPanelById(Panel_ID panelId)
    {
        BasePanel panel = null;
        for (int i = 0; i < panelList.Count; i++)
        {
            panel = panelList[i];
            if (panel != null)
            {
                if (panel.GetComponent<BasePanel>().panelID == panelId)
                {
                    return panel;
                }
            }
        }
        return null;
    }

    [System.Serializable]
    class PanelIDJson
    {
        public PanelInfo[] infoList = null;
    }

    private BasePanel GetPanel(Panel_ID id)
    {
        if (panelDic == null)
        {
            panelDic = new Dictionary<Panel_ID, BasePanel>();
        }

        BasePanel panel;
        panelDic.TryGetValue(id, out panel);
        if (panel == null)
        {
            string path;
            panelPathDic.TryGetValue(id, out path);
            GameObject newPanel = Tools.CreateGameObject(path, tsCanvas);
            panelDic.Add(id, newPanel.GetComponent<BasePanel>());
            return newPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    private void ParsePanelTypeJson()
    {
        panelPathDic = new Dictionary<Panel_ID, string>();
        TextAsset ta = Resources.Load<TextAsset>("UI/UIPanelType");
        PanelIDJson jsonObject = JsonUtility.FromJson<PanelIDJson>(ta.text);
        foreach (PanelInfo info in jsonObject.infoList)
        {
            panelPathDic.Add(info.panelID, info.path);
        }
    }
}
