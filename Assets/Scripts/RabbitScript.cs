using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour 
{
	[SerializeField]
	float SPEED = 2;
	[SerializeField]
	float DEATH_DURATION = 4f;

	bool m_BeginRun = false;

	void Update()
	{
		if (m_BeginRun)
		{	
			transform.Translate(Vector2.right * Time.deltaTime * SPEED);
		}
	}

	public void RunAway()
	{
		m_BeginRun = !m_BeginRun;
		DestroyAfterTime();
	}

	public void DestroyAfterTime()
	{
		Destroy(gameObject, DEATH_DURATION);
	}
}
