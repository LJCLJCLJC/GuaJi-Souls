using UnityEngine;
using System.Collections;

public class ActivePool
{
	public static ActivePool instance;
	private Hashtable dicArray=new Hashtable();
	public ActivePool()
	{
		instance = this;
	}

	public GameObject GetRes(string url,Transform parent,Vector3 pos,Vector3 scale,object param,CallBackGameObject callback)
	{
		BasePool pool;
		if(dicArray.Contains(url))
		{
			pool=dicArray[url] as BasePool;
		}
		else
		{
			pool =new BasePool(url);
			dicArray.Add(url,pool);
		}
		GameObject preObject =pool.GetRes(parent,pos,scale,param,callback);
		return preObject;
	}

	public void PutRes(string url,GameObject goObj)
	{
		if(string.IsNullOrEmpty(url)==false)
		{
			BasePool pool;
			if(dicArray.Contains(url))
			{
				pool=dicArray[url] as BasePool;
			}
			else
			{
				pool =new BasePool(url);
				dicArray.Add(url,pool);
			}
			pool.PutRes(goObj);
		}
		else
		{
			GameObject.Destroy(goObj);
		}

	}

	public void ClearRes()
	{
		BasePool pool;
		foreach (DictionaryEntry de in dicArray)
		{
			pool=de.Value as BasePool;
			pool.Clear();
		}
		dicArray.Clear();
		dicArray=new Hashtable();
	}
}

//public class BulletCreateVo
//{
//	public string url;
//	public AttackDataVo attackDataVo;
//	public Vector3 startPos;
//	public BaseActive attack;
//	public BaseActive beAttack;
//	public int bombSpeed;
//	public Vector3 tv;
//	public int index;
//}

//public class ShipCreateVo
//{
//	public BattleRoleVo roleVo;
//	public string url;
//}
