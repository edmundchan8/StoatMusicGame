using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField]
	Text m_Text;

	[SerializeField]
	float m_Timer;

	void Start () 
	{
		m_Timer = Time.timeSinceLevelLoad;
	}

	void Update () 
	{
		m_Timer += Time.deltaTime;
		m_Text.text = "Time: " + m_Timer.ToString("F2");
	}
}
