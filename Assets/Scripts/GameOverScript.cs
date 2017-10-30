using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour 
{
	[Header ("CONST")]
	[SerializeField]
	float FADE_IN_DURATION = 2f;
	[SerializeField]
	float FLOAT_DOWN_DURATION = 0.01f;

	[Header ("Accessor")]
	[SerializeField]
	Timer m_Timer;
	[SerializeField]
	MusicManager m_MusicManager;

	[SerializeField]
	GameObject m_RestartButton;
	[SerializeField]
	GameObject m_MenuButton;

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
		m_RestartButton.SetActive(false);
		m_MenuButton.SetActive(false);
		if (m_MusicManager == null)
		{
			m_MusicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
		}
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
			FadeInText();
			FloatDownText();
			FadeAudioSound();
			m_RestartButton.SetActive(true);
			m_MenuButton.SetActive(true);
		}
	}

	void FadeInText()
	{
		m_TextColor.a = ((FADE_IN_DURATION - m_Timer.GetTimer()) / FADE_IN_DURATION);
		m_LoseText.color = m_TextColor;
	}

	void FloatDownText()
	{
		Vector2 loseTextObj = transform.position;
		loseTextObj.y -= FLOAT_DOWN_DURATION;
		transform.position = loseTextObj;
	}

	void FadeAudioSound()
	{
		m_MusicManager.FadeOutMusic();
	}
}
