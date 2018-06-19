using UnityEngine;
using System.Collections;

public class DeleteEft : MonoBehaviour 
{
	public float deleteTime;
	public ParticleSystem[] particles;
    public AudioSource audio;
	void Start()
	{
		TimeLine.GetInstance().AddTimeEvent(DestroyThis,deleteTime,null,this.gameObject);
        if (audio != null)
        {
            audio.volume = DataManager.Instance.GetSettingData().effectVolume;
        }
    }

	public void ResetDeleteTime()
	{
		TimeLine.GetInstance().RemoveTimeEvent(DestroyThis);
		TimeLine.GetInstance().AddTimeEvent(DestroyThis,deleteTime,null,this.gameObject);
	}

	public void ChangeTime(Transform attack,Transform beAttack)
	{
		float dis=Vector3.Distance(attack.localPosition,beAttack.localPosition);
		deleteTime=dis/100f;
	}

	public void ChangeMoveTime()
	{
		for(int i=0;i<particles.Length;i++)
		{
			particles[i].startLifetime=particles[i].startLifetime/deleteTime;
		}
	}

	public void ChangeWaitTime(float delayTime)
	{
		for(int i=0;i<particles.Length;i++)
		{
			particles[i].startDelay=delayTime;
		}
	}

	public void ChangeDeleteTime(float deleteTime1)
	{
		deleteTime=deleteTime1;
		ResetDeleteTime ();
	}

	private void DestroyThis(object obj)
	{
		GameObject.Destroy(this.gameObject);
	}

	void OnDestroy()
	{
		TimeLine.GetInstance().RemoveTimeEvent(DestroyThis);
	}
}
