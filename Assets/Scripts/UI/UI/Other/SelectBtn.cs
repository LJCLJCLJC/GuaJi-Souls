using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBtn : MonoBehaviour {
    public int index = 0;
    public GameObject selected;
    public GameObject unselected;
	public void ChangeSelect(bool ifselect)
    {
        if (ifselect)
        {
            selected.SetActive(true);
            unselected.SetActive(false);
        }
        else
        {
            selected.SetActive(false);
            unselected.SetActive(true);
        }
    }
}
