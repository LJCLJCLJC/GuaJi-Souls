using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public delegate void CallBackGameObject(GameObject obj,object param);
public class CallBackVo
{
	public Transform parent;
	public Vector3 pos;
	public Vector3 scale;
	public object param;
	public CallBackGameObject callback;
}
public class BasePool
{
	private string url;
	private List<GameObject> goList=new List<GameObject>();
	private List<CallBackVo> callList=new List<CallBackVo>();

	private UnityEngine.Object bundle=null;
	public BasePool(string resUrl)
	{
		url=resUrl;
	}

	public GameObject GetRes(Transform parent,Vector3 pos,Vector3 scale,object param,CallBackGameObject callback)
	{
		GameObject preObject = null;
		if(goList.Count>0)
		{
			preObject=goList[0];
			goList.Remove(preObject);
			preObject.SetActive(true);
			preObject.transform.parent = parent;
			preObject.transform.localPosition = pos;
			preObject.transform.localScale = scale;
			preObject.transform.localRotation=new Quaternion(0,0,0,0);
			if(callback!=null)
			{
				callback(preObject,param);
			}
		}
		else
		{
			if(bundle==null)
			{
                UnityEngine.Object initObj = Resources.Load(url);
                bundle = initObj;

            }
			if(bundle==null)
			{
				Debug.LogError(url+" no url res");
			}
			preObject=Tools.CreateGameObjectByObject(bundle,parent,pos,scale);
			if(callback!=null)
			{
				callback(preObject,param);
			}
		}
		return preObject;
	}

	public void PutRes(GameObject goObj)
	{
		goList.Add(goObj);
		goObj.SetActive(false);
	}

	public void Clear()
	{
		GameObject goObj;
		for(int i=goList.Count-1;i>=0;i--)
		{
			goObj=goList[i];
			GameObject.Destroy(goObj);
		}
		goList.Clear();
	}
}
