using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour 
{
	[SerializeField]
	Timer m_Timer;

	[SerializeField]
	float FADE_IN_DURATION = 2f;

	[SerializeField]
	Text m_LoseText;
	[SerializeField]
	Color m_TextColor;
	[SerializeField]
	float m_Alpha = 0;

	float m_DebugTime;

	void Start () 
	{
		m_LoseText = GetComponent<Text>();
		m_TextColor = m_LoseText.color;
		m_TextColor.a = m_Alpha;
		m_LoseText.color = m_TextColor;
		gameObject.SetActive(false);
	}

	public void SetLoseTextActive()
	{
		m_Timer.Update(Time.deltaTime);
		if (!gameObject.activeInHierarchy)
		{
			m_Timer.SetTimer(FADE_IN_DURATION);
		}
		if (m_TextColor.a != 1f)
		{
			gameObject.SetActive(true);
			m_TextColor.a = ((FADE_IN_DURATION - m_Timer.GetTimer()) / FADE_IN_DURATION);
			m_LoseText.color = m_TextColor;
		}
	}
}
