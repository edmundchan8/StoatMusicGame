using UnityEngine;
using System.Collections;

public class RabbitScript : MonoBehaviour 
{
	[Header("Rabbit Attributes")]
	[SerializeField]
	float SPEED = 2f;
	[SerializeField]
	float DEATH_DURATION = 4f;
	bool m_BeginRun = false;

	[Header("Accessor")]
	GameManager m_GameManager;
	GameOverScript m_GameOverScript;
	Animator m_Animator;

	void Start()
	{
		Vector2 startPos = transform.localPosition;
		startPos = new Vector2(4, 0);
		transform.localPosition = startPos;
		m_Animator = transform.GetChild(0).GetComponent<Animator>();
		IsEnteringScene(true);
		if (m_GameOverScript == null)
		{
			GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
			m_GameManager = gameManager.GetComponent<GameManager>();
			m_GameOverScript = m_GameManager.ReturnGameOverScript();
		}
	}

	void Update()
	{
		if (transform.localPosition.x > 2.1f && !LevelManager.instance.IsGameOver())
		{
			Vector2 pos = transform.position;
			pos.x -= Time.deltaTime;
			print(pos);
			transform.position = pos;
			if (pos.x < 2.1f)
			{
				IsEnteringScene(false);
			}
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
		m_GameManager.OnLoseLevel();
	}

	public void Bitten()
	{
		FlashCode flash = gameObject.GetComponentInChildren<FlashCode>();
		flash.SetFlashTimer();
		DestroyAfterTime();
	}

	//Consider if we still want this as it causes some conflicts/null reference exceptions later on once destroyed
	public void DestroyAfterTime()
	{
		Invoke("IncreaseLevelCount",4.0f);
		MusicManager.instance.FadeOutMusic();
		Destroy(gameObject, DEATH_DURATION);
	}

	void IncreaseLevelCount()
	{
		if (!LevelManager.instance.IsGameOver())
		{
			LevelManager.instance.IncrementLevel();
			m_GameManager.SetBackgroundLerpPos();
		}
	}

	public void IsEnteringScene(bool choice)
	{
		m_Animator.SetBool("isEntering", choice);
	}
}
