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

	float DELAY_BITE_DURATION = 2.5f;
	Vector2 RABBIT_START_POS = new Vector2 (2, 0);

	[Header ("Accessor")]
	[SerializeField]
	GameObject m_MusicManagerObject;
	[SerializeField]
	MusicManager m_MusicManagerScript;
	Animator m_Animator;
	[SerializeField]
	RabbitScript m_RabbitScript;
	[SerializeField]
	GameObject m_RabbitPrefab;
	GameObject m_CurrentRabbit;

	TouchPanel m_TouchPanel;

	[Header ("Stoat Move Times")]
	[SerializeField]
	float[] m_Level01MoveTimeArr = new float[]{32f, 68f};

	[SerializeField]
	int m_ArrayCounter = 0;

	void Start()
	{
		m_StartPos = transform.position;
		m_EndPos = transform.position;
		m_MusicManagerScript = m_MusicManagerObject.GetComponent<MusicManager>();
		m_Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
		FindRabbit();
		m_TouchPanel = GameObject.FindGameObjectWithTag("TouchPanel").GetComponent<TouchPanel>();
	}

	void Update()
	{
		//TODO - might need to reword this.  When rabbit is destroy, this will still check.
		//Should probably say, if rabbit is not destroyed && pos.x  >2.1f && is not gameover.
		if (m_CurrentRabbit != null && m_CurrentRabbit.transform.position.x > 2.1f && !m_TouchPanel.m_IsGameOver)
		{
			Vector2 pos = m_CurrentRabbit.transform.position;
			pos.x -= Time.deltaTime;
			m_CurrentRabbit.transform.position = pos;
			if (pos.x < 2.1f)
			{
				m_RabbitScript.IsEnteringScene(false);
			}
		}
		//Timer is ticking, move closer to rabbit as long as the current array counter is less than 2 and if music time is less than the value in the 
		//m_Level01TimerArray[ ] , then set the lerp position and increment m_ArrayCounter
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
		transform.position = Vector2.Lerp(m_StartPos, m_EndPos, (m_MoveDuration - m_Timer.GetTimer()) / m_MoveDuration);
	}

	public void SetLerpPositions()
	{
		m_StartPos = transform.position;
		m_EndPos = new Vector2(transform.position.x + m_MoveAmount, transform.position.y);
		m_Timer.SetTimer(m_MoveDuration);
	}

	IEnumerator Bite()
	{
		yield return new WaitForSeconds(DELAY_BITE_DURATION);
		m_Animator.SetTrigger("isBiting");
		yield return new WaitForSeconds(0.5f);
		m_RabbitScript.Bitten();
	}

	public void FindRabbit()
	{
		Vector2 startPos = new Vector2(4, 0);
		m_CurrentRabbit = GameManager.instance.InstantiateRabbit();
		m_CurrentRabbit.transform.localPosition = startPos;
		m_RabbitScript = m_CurrentRabbit.GetComponentInChildren<RabbitScript>();
	}
}