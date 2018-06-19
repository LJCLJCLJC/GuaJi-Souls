using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PanelInfo : ISerializationCallbackReceiver
{
    
    public string panelIDString;
    public string path;
    [NonSerialized]
    public Panel_ID panelID;


    public void OnAfterDeserialize()
    {
        panelID = (Panel_ID)Enum.Parse(typeof(Panel_ID), panelIDString);
    }

    public void OnBeforeSerialize()
    {

    }

}
