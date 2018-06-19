using UnityEngine;
using System.Collections;

public delegate void RecycleDegelate(GameObject obj);
public class PoolItem : MonoBehaviour
{
    public RecycleDegelate callback;
    public Vector3 originPosition;
    public Quaternion originRotation;
    public Vector3 originScale;
    protected void Awake()
    {
        callback = null;
        gameObject.SetActive(false);
    }

    public virtual void Init()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        originScale = transform.localScale;
    }
    public virtual void ResetItem()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
        transform.localScale = originScale;
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        if (callback != null)
        {
            callback(gameObject);
        }
    }

}
