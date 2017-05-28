using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour 
{
	[SerializeField]
	GameObject m_PauseCanvas;

	[SerializeField]
	bool m_Active = false;

	void Start () 
	{
		m_PauseCanvas = GameObject.Find("PauseCanvas");
		m_PauseCanvas.SetActive(m_Active);
	}

	public void OnPause()
	{
		m_Active = !m_Active;
		//SIMPlIFY!
		if (m_Active)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
		m_PauseCanvas.SetActive(m_Active);
	}
}
