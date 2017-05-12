using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField]
	float m_Timer;
	[SerializeField]
	bool m_CanTick = false;

	public void StartTimer()
	{
		m_CanTick = true;
	}

	void Update () 
	{
		if (m_CanTick)
		{
			m_Timer += Time.deltaTime;
		}
	}
}
