using UnityEngine;
using System.Collections;

public class StoatScript : MonoBehaviour 
{
	[SerializeField]
	float m_MoveAmount = 1f;
	[SerializeField]
	float m_MoveDuration = 4f;
	[SerializeField]
	Vector2 m_StartPos;
	[SerializeField]
	Vector2 m_EndPos;
	[SerializeField]
	Timer m_Timer;

	[Header ("Accessor")]
	[SerializeField]
	GameObject m_MusicManagerObject;
	[SerializeField]
	MusicManager m_MusicManagerScript;

	[Header ("Stoat Move Times")]
	[SerializeField]
	float[] m_Level01MoveTimeArr = new float[]{32f, 64f};

	[SerializeField]
	int m_ArrayCounter=0;

	void Start()
	{
		m_StartPos = transform.position;
		m_EndPos = transform.position;
		m_MusicManagerScript = m_MusicManagerObject.GetComponent<MusicManager>();
	}

	void Update()
	{
		m_Timer.Update(Time.deltaTime);
		MoveCloserRabbit();
		if (m_ArrayCounter < 2)
		{
			if (m_MusicManagerScript.GetCurrentMusicTime() > m_Level01MoveTimeArr[m_ArrayCounter])
			{
				SetLerpPositions();
				m_ArrayCounter++;
			}
		}
	}

	public void MoveCloserRabbit() 
	{
		print((m_MoveDuration - m_Timer.GetTimer()) / m_MoveDuration);
		transform.position = Vector2.Lerp(m_StartPos, m_EndPos, (m_MoveDuration - m_Timer.GetTimer()) / m_MoveDuration);
	}

	public void SetLerpPositions()
	{
		m_StartPos = transform.position;
		m_EndPos = new Vector2(transform.position.x + m_MoveAmount, transform.position.y);
		m_Timer.SetTimer(m_MoveDuration);
	}
}