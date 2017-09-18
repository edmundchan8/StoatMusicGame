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

	[Header ("Accessor")]
	[SerializeField]
	GameObject m_MusicManagerObject;
	[SerializeField]
	MusicManager m_MusicManagerScript;
	Animator m_Animator;
	[SerializeField]
	RabbitScript m_RabbitScript;

	[Header ("Stoat Move Times")]
	[SerializeField]
	float[] m_Level01MoveTimeArr = new float[]{32f, 68f};

	[SerializeField]
	int m_ArrayCounter=0;

	void Start()
	{
		m_StartPos = transform.position;
		m_EndPos = transform.position;
		m_MusicManagerScript = m_MusicManagerObject.GetComponent<MusicManager>();
		m_Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
		GameObject rabbit = GameObject.Find("Rabbit");
		m_RabbitScript = rabbit.GetComponentInChildren<RabbitScript>();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.B))
		{
			StartCoroutine("Bite");
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
}