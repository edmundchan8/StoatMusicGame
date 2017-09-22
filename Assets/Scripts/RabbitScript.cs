using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour 
{
	[SerializeField]
	float SPEED = 2f;
	[SerializeField]
	float DEATH_DURATION = 4f;

	[Header("Accessor")]
	GameObject m_GameManager;
	GameManager m_GameManagerScript;
	GameOverScript m_GameOverScript;
	Animator m_Animator;

	bool m_BeginRun = false;

	void Start()
	{
		m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
		m_GameManagerScript = m_GameManager.GetComponent<GameManager>();
		m_Animator = transform.GetChild(0).GetComponent<Animator>();
		m_GameOverScript = m_GameManagerScript.ReturnGameOverScript();
		IsEnteringScene(true);
	}

	void Update()
	{
		if (m_BeginRun)
		{	
			print("run");
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
		Destroy(gameObject, DEATH_DURATION);
	}

	public void IsEnteringScene(bool choice)
	{
		m_Animator.SetBool("isEntering", choice);
	}
}
