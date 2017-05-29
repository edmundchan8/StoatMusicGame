using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour 
{
	[Header("CONST")]
	[SerializeField]
	float FADE_DURATION = 2f;

	[Header("Variables")]
	[SerializeField]
	float m_Timer = 0;
	[SerializeField]
	Color m_PanelColor;

	[Header ("Accessor")]
	[SerializeField]
	Image m_Panel;
	[SerializeField]
	GameObject m_MusicManager;

	void Start () 
	{
		//Part of fade out code, game starts as white, when time up, reveals menu
		m_PanelColor = Color.white;
		m_Panel.color = m_PanelColor;
	}

	void Update()
	{
		m_Timer += Time.deltaTime;
		print(m_Timer);

		if (m_Timer < FADE_DURATION)
		{
			float alpha = m_Timer / FADE_DURATION;
			m_PanelColor.a -= alpha;
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
			gameObject.SetActive(false);
		}
	}

}
