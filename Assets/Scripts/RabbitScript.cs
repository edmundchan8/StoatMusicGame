using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour 
{
	[SerializeField]
	float SPEED = 2;
	/*[SerializeField]
	float DEATH_DURATION = 4f; */

	[Header("Accessor")]
	[SerializeField]
	GameObject m_GameOverTextObject;
	GameOverScript m_GameOverScript;

	bool m_BeginRun = false;

	void Start()
	{
		m_GameOverScript = m_GameOverTextObject.GetComponent<GameOverScript>();
	}

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
		//DestroyAfterTime();
		m_GameOverScript.SetLoseTextActive();
	}

	//Consider if we still want this as it causes some conflicts/null reference exceptions later on once destroyed
	/*public void DestroyAfterTime()
	{
		Destroy(gameObject, DEATH_DURATION);
	}
	*/
}
