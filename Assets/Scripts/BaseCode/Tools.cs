using UnityEngine;
using System.Collections;

public class Tools : MonoBehaviour
{

    public static GameObject CreateGameObject(string url,Transform parent,Vector3 pos,Vector3 scale)
    {
        Object @object = Resources.Load(url);
        if (@object == null)
        {
            return null;
        }
        GameObject obj = Instantiate(@object as GameObject);
        obj.transform.parent = parent;
        obj.transform.localPosition = pos;
        obj.transform.localScale = scale;

        return obj;
    }
    public static GameObject CreateGameObject(string url, Transform parent, Vector3 pos)
    {
        Object @object = Resources.Load(url);
        if (@object == null)
        {
            return null;
        }
        GameObject obj = Instantiate(@object as GameObject);
        obj.transform.parent = parent;
        obj.transform.localPosition = pos;
        return obj;
    }
    public static GameObject CreateGameObject(string url, Transform parent)
    {
        GameObject obj = Instantiate(Resources.Load(url) as GameObject);
        obj.transform.SetParent(parent, false);

        return obj;
    }
    public static GameObject CreateGameObject(string url)
    {
        GameObject obj = Instantiate(Resources.Load(url) as GameObject);

        return obj;
    }
    public static GameObject CreateGameObjectByObj(GameObject obj,Transform parent)
    {
        GameObject goObj = Instantiate(obj);
        goObj.transform.parent = parent;

        return goObj;
    }
    public static GameObject CreateGameObjectByObj(GameObject obj, Transform parent, Vector3 pos, Vector3 scale)
    {
        GameObject goObj = Instantiate(obj);
        goObj.transform.parent = parent;
        goObj.transform.localPosition = pos;
        goObj.transform.localScale = scale;
        return goObj;
    }

    public static void ClearChildFromParent(Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    public static GameObject CreateGameObjectByObject(UnityEngine.Object obj, Transform parent, Vector3 pos, Vector3 scale)
    {
        if (obj == null)
        {
            return null;
        }
        GameObject preObject = Instantiate(obj) as GameObject;
        preObject.transform.parent = parent;
        preObject.transform.localPosition = pos;
        preObject.transform.localScale = scale;
        preObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        return preObject;
    }

    public static string ChangeNum(int num)
    {
        string str = num + "";
        if (num > 10000)
        {
            str = num / 10000 + "万";
        }
        else if (num > 100000000)
        {
            str = num / 100000000 + "亿";
        }
        return str;
    }
}

public class Bezier
{
    public Vector3 p0 = Vector3.zero;
    public Vector3 p1 = Vector3.zero;
    public Vector3 p2 = Vector3.zero;


    public Bezier(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        this.p0 = v0;
        this.p1 = v1;
        this.p2 = v2;
    }

    public void UpdateTargetPos(Vector3 v2)
    {
        p2 = v2;
    }

    public Vector3 GetPointAtTime(float t)
    {
        float x = (1 - t) * (1 - t) * p0.x + 2 * t * (1 - t) * p1.x + t * t * p2.x;
        float y = (1 - t) * (1 - t) * p0.y + 2 * t * (1 - t) * p1.y + t * t * p2.y;
        float z = (1 - t) * (1 - t) * p0.z + 2 * t * (1 - t) * p1.z + t * t * p2.z;
        return new Vector3(x, y, z);
    }
}
