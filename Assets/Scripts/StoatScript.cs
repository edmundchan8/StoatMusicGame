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
	float m_CurrentTime;

	void Start()
	{
		m_StartPos = transform.position;
		m_EndPos = transform.position;
	}

	void Update()
	{
		m_CurrentTime += Time.deltaTime;
		MoveCloserRabbit();
	}

	public void MoveCloserRabbit() 
	{
		transform.position = Vector2.Lerp(m_StartPos, m_EndPos, m_CurrentTime / m_MoveDuration);
	}

	public void SetLerpPositions()
	{
		m_StartPos = transform.position;
		m_EndPos = new Vector2(transform.position.x + m_MoveAmount, transform.position.y);
		m_CurrentTime = 0;
	}
}