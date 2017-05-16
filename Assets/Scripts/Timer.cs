using UnityEngine;
using System.Collections;
[System.Serializable]

public class Timer 
{
	[SerializeField]
	float m_Timer = 0f;

	public void SetTimer(float time)
	{
		m_Timer = time;
	}

	public bool Update (float tick)
	{
		if (m_Timer > 0.0f)
		{
			m_Timer = Mathf.Max(m_Timer - tick, 0f);
			if (m_Timer > 0.0f)
			{
				return true;
			}
		}
		return false;
	}

	public float GetTimer()
	{
		return m_Timer;
	}

	public bool HasCompleted()
	{
		return m_Timer <= 0.0f;	
	}
}