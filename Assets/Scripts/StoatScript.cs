using UnityEngine;
using System.Collections;

public class StoatScript : MonoBehaviour 
{
	[Header ("Stoat Attributes")]
	[SerializeField]
	float m_MoveAmount = 1f;
	[SerializeField]
	float m_MoveDuration = 4f;
	float DELAY_BITE_DURATION = 2.5f;
	Vector2 m_StartPos;
	Vector2 m_CurrentPos;
	Vector2 m_EndPos;

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

	[Header ("Stoat Move Attributes")]
	[SerializeField]
	float[] m_Level01MoveTimeArr = new float[]{32f, 68f};
	Timer m_StoatTimer = new Timer();
	int m_TotalStepTowardsRabbit = 0;

	void Start()
	{
		m_CurrentPos = transform.position;
		m_StartPos = m_CurrentPos;
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
		m_StoatTimer.Update(Time.deltaTime);
		MoveStoat();
		if (m_TotalStepTowardsRabbit < 2)
		{
			if (m_MusicManagerScript.GetCurrentMusicTime() > m_Level01MoveTimeArr[m_TotalStepTowardsRabbit])
			{
				SetLerpPositions();
				m_TotalStepTowardsRabbit++;
			}
		}

		if (!m_CurrentRabbit)
		{
			FindRabbit();
		}
	}

	public void MoveStoat() 
	{
		transform.position = Vector2.Lerp(m_CurrentPos, m_EndPos, (m_MoveDuration - m_StoatTimer.GetTimer()) / m_MoveDuration);
	}

	public void SetLerpPositions()
	{
		m_CurrentPos = transform.position;
		m_EndPos = new Vector2(transform.position.x + m_MoveAmount, transform.position.y);
		m_StoatTimer.SetTimer(m_MoveDuration);
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
		m_CurrentRabbit = GameObject.FindGameObjectWithTag("Rabbit");
		m_CurrentRabbit.transform.localPosition = startPos;
		m_RabbitScript = m_CurrentRabbit.GetComponentInChildren<RabbitScript>();
	}

	public void EndOfLevel()
	{
		SetLerpPositions();
		StartCoroutine("Bite");
	}

	public void ReturnStartLerpPos()
	{
		m_CurrentPos = transform.position;
		m_EndPos = new Vector2(m_StartPos.x, m_StartPos.y);
		m_StoatTimer.SetTimer(m_MoveDuration);
	}
}