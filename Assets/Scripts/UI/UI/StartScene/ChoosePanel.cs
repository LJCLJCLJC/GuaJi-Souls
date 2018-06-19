using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel : BasePanel {

    public PlayerCell[] playerCell;
	void Start () {
        List<RoleVo> roleList = DataManager.Instance.GetAllPlayer();
        for(int i = 0; i < playerCell.Length; i++)
        {
            for(int j = 0; j < roleList.Count; j++)
            {
                if (roleList[j].id == playerCell[i].id)
                {
                    playerCell[i].Create(roleList[j]);
                }
            }
        }
	}
	
}
