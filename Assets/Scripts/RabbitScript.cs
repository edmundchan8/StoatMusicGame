using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour 
{
	[SerializeField]
	float SPEED = 2f;
	[SerializeField]
	float DEATH_DURATION = 4f;

	[Header("Accessor")]
	GameOverScript m_GameOverScript;
	Animator m_Animator;

	bool m_BeginRun = false;

	void Start()
	{
		m_Animator = transform.GetChild(0).GetComponent<Animator>();
		IsEnteringScene(true);
		m_GameOverScript = GameManager.instance.ReturnGameOverScript();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Q))
		{
			DestroyAfterTime();
		}
		if (m_BeginRun)
		{
			transform.Translate(Vector2.right * Time.deltaTime * SPEED);
		}
	}

	public void RunAway()
	{
		m_Animator.SetTrigger("isRunning");
		m_BeginRun = !m_BeginRun;
		DestroyAfterTime();
		m_GameOverScript.SetLoseTextActive();
	}

	public void Bitten()
	{
		FlashCode flash = gameObject.GetComponentInChildren<FlashCode>();
		flash.SetFlashTimer();
	}

	//Consider if we still want this as it causes some conflicts/null reference exceptions later on once destroyed
	public void DestroyAfterTime()
	{
		Invoke("IncreaseLevelCount",4.0f);
		//fade out music

		Destroy(gameObject, DEATH_DURATION);
	}

	void IncreaseLevelCount()
	{
		GameManager.instance.IncrementCurrentLevel();
	}

	public void IsEnteringScene(bool choice)
	{
		m_Animator.SetBool("isEntering", choice);
	}
}
