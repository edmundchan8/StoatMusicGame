using UnityEngine;
using System.Collections;
//TODO Need to get this to work from Level Manager script instead
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
	[Header ("Accessor")]
	[SerializeField]
	GameObject m_PauseCanvas;
	[SerializeField]
	GameObject m_GameBackground;
	[SerializeField]
	GameObject m_Rabbit;
	[SerializeField]
	GameObject m_Stoat;
	public GameOverScript m_GameOverScript;
	bool m_IsGameOver = false;
	[SerializeField]
	GameObject m_NoteHolder;

	[Header("CONSTANTS")]
	float LERP_DURATION = 4f;

	[Header("GameManager Variables")]
	//Make an instance of Gameobject so we can call this from anywhere
	public static GameManager instance;
	int m_CurrentLevel;
	Vector2 m_StartLerpPos;
	Vector2 m_BackgroundPos;
	bool m_CanInstantiateRabbit = true;
	[SerializeField]
	bool m_Pause = false;

	[Header("Timer")]
	[SerializeField]
	Timer m_Timer = new Timer();

	void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else
		{
			Destroy(instance);
		}
	}

	void Start () 
	{
		m_CurrentLevel = LevelManager.instance.GetCurrentLevel();
		InstantiateRabbit();
		m_CanInstantiateRabbit = false;
		m_PauseCanvas = GameObject.Find("PauseCanvas");
		m_PauseCanvas.SetActive(m_Pause);
		m_BackgroundPos = m_GameBackground.transform.localPosition;
		m_StartLerpPos = m_BackgroundPos;
		m_BackgroundPos.x = GetGameBackgroundPos();
	}

	void Update()
	{
		m_Timer.Update(Time.deltaTime);
		if (!m_Timer.HasCompleted() && m_CanInstantiateRabbit)
		{
			m_GameBackground.transform.localPosition = Vector2.Lerp(m_StartLerpPos, m_BackgroundPos, (LERP_DURATION - m_Timer.GetTimer()) / LERP_DURATION);
		}

		if (m_Timer.HasCompleted())
		{
			if (m_CanInstantiateRabbit)
			{
				InstantiateRabbit();
				m_CanInstantiateRabbit = false;
			}
		}
	}

	public void OnPause()
	{
		m_Pause = !m_Pause;
		if (m_Pause)
		{
			PauseGameAndMusic();
		}
		else
		{
			PlayGameAndMusic();
		}
		m_PauseCanvas.SetActive(m_Pause);
	}

	public void PauseGameAndMusic()
	{
		Time.timeScale = 0f;
		MusicManager.instance.PauseMusic();
	}

	public void PlayGameAndMusic()
	{
		Time.timeScale = 1f;
		MusicManager.instance.PlayMusic();
	}

	//TODO Need to get this to work from Level Manager script instead
	public void ReturnToTitle()
	{
		PlayGameAndMusic();
		SceneManager.LoadScene("Menu");
	}

	public void SetBackgroundLerpPos()
	{
		m_CurrentLevel = LevelManager.instance.GetCurrentLevel();
		m_StartLerpPos = m_BackgroundPos;
		m_BackgroundPos.x = GetGameBackgroundPos();
		m_Stoat.GetComponent<StoatScript>().ReturnStartLerpPos();
		m_CanInstantiateRabbit = true;
	}

	public float GetGameBackgroundPos()
	{
		float pos;
		if (m_CurrentLevel == 1)
		{
			pos = 496f;
		}
		else if (m_CurrentLevel == 2)
		{
			pos = 140f;
		}
		else if (m_CurrentLevel == 3)
		{
			pos = -300;
		}
		else
		{
			Debug.Log("level count value not expected, this should not appear");
			pos = 0f;
		}
		m_Timer.SetTimer(LERP_DURATION);
		return pos;
	}
	public GameOverScript ReturnGameOverScript()
	{
		m_IsGameOver = true;
		return m_GameOverScript;
	}

	public GameObject InstantiateRabbit()
	{
		m_CanInstantiateRabbit = false;
		return Instantiate(m_Rabbit, transform.position, transform.rotation) as GameObject;
	}

	public void OnLoseLevel()
	{
		m_Stoat.GetComponent<StoatScript>().IsMoving();
	}

	public bool IsGameOver()
	{
		return m_IsGameOver;
	}

	public void OnRestart()
	{
		LevelManager.instance.RestartLevel(LevelManager.instance.GetCurrentLevel());
	}

	public GameObject GetNoteInstantiatePos()
	{
		return m_NoteHolder;
	}
}
