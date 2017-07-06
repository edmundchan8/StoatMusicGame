using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour 
{
	[Header("CONST")]
	[SerializeField]
	float FADE_DURATION = 1f;

	[Header("Variables")]
	[SerializeField]
	Color m_PanelColor;

	[Header ("Accessor")]
	[SerializeField]
	Canvas m_FadeOutCanvas;
	[SerializeField]
	Image m_Panel;
	[SerializeField]
	GameObject m_MusicManager;
	[SerializeField]
	Timer m_FadeTimer;

	void Start () 
	{
		//Part of fade out code, game starts as white, when time up, reveals menu
		m_PanelColor = Color.white;
		m_Panel.color = m_PanelColor;
		m_FadeTimer.SetTimer(FADE_DURATION);
	}

	void Update()
	{
		m_FadeTimer.Update(Time.deltaTime);
		if (m_FadeTimer.GetTimer() > 0)
		{
			float alpha = (m_FadeTimer.GetTimer()/FADE_DURATION);
			m_PanelColor.a = alpha;
			m_Panel.color = m_PanelColor;
			if (alpha < 1)
			{
				//once we hit a certain point, I want code to access musicmanager gameobject here and start playing 
				//the music on the musicmanager
				m_MusicManager.SetActive(true);
			}
		}
		else
		{
			//disable fadeoutPanel
			gameObject.SetActive(false);
			//disable the fadeout canvas that the panel is on
			m_FadeOutCanvas.gameObject.SetActive(false);
		}
	}

}
